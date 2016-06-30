using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using ViewSwitchingNavigation.Email.Model;
using ViewSwitchingNavigation.Email.Properties;

namespace ViewSwitchingNavigation.Email.ViewModels
{
    // todo: 08 - View lifetime
    // The view or view model in a region can indicate that when it
    // becomes inactive (usually not the visible one), that it be
    // removed from the region.  
    // This option can be specified either using the RegionMemberLifetime
    // attribute, as shown here, or by implementing the 
    // IRegionMemberLifetime interface.
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RegionMemberLifetime(KeepAlive = false)]
    public class ComposeEmailViewModel : BindableBase, IConfirmNavigationRequest
    {
        private const string NormalStateKey = "Normal";
        private const string SendingStateKey = "Sending";
        private const string SentStateKey = "Sent";

        private const string ReplyToParameterKey = "ReplyTo";
        private const string ToParameterKey = "To";

        private readonly IEmailService emailService;
        private readonly DelegateCommand sendEmailCommand;
        private readonly DelegateCommand cancelEmailCommand;
        private readonly InteractionRequest<Confirmation> confirmExitInteractionRequest;
        private EmailDocument emailDocument;
        private string sendState;
        private IRegionNavigationJournal navigationJournal;

        [ImportingConstructor]
        public ComposeEmailViewModel(IEmailService emailService)
        {
            this.sendEmailCommand = new DelegateCommand(this.SendEmail);
            this.cancelEmailCommand = new DelegateCommand(this.CancelEmail);
            this.confirmExitInteractionRequest = new InteractionRequest<Confirmation>();
            this.sendState = NormalStateKey;

            this.emailService = emailService;
        }

        public ICommand SendEmailCommand
        {
            get { return this.sendEmailCommand; }
        }

        public ICommand CancelEmailCommand
        {
            get { return this.cancelEmailCommand; }
        }

        public IInteractionRequest ConfirmExitInteractionRequest
        {
            get { return this.confirmExitInteractionRequest; }
        }

        public EmailDocument EmailDocument
        {
            get
            {
                return this.emailDocument;
            }

            set
            {
                this.SetProperty(ref this.emailDocument, value);
            }
        }

        public string SendState
        {
            get
            {
                return this.sendState;
            }

            private set
            {
                this.SetProperty(ref this.sendState, value);
            }
        }

        private async void SendEmail()
        {
            this.SendState = SendingStateKey;
            await this.emailService.SendEmailDocumentAsync(this.emailDocument);
            this.SendState = SentStateKey;

            // todo: 05 - Send Email: Navigating back
            // After the email has been 'sent' (we're using a mock mail service 
            // in this application), the view model uses the navigation journal 
            // it captured when it was navigated to (see the OnNavigatedTo in 
            // this class) to navigate the region to the prior view.  
            if (this.navigationJournal != null)
            {
                this.navigationJournal.GoBack();
            }
        }

        private void CancelEmail()
        {
            // todo: 06 - Cancel Email : Navigating backwards
            // When the user elects to cancel the email, we navigate the region backwards.
            //
            // Because the view model implements the IConfirmNavigationRequest
            // it has the option to interrupt the navigation for the region if there
            // changes to the email.  See the ConfirmNavigationRequest implementation below
            // for more details on this.
            // 
            if (this.navigationJournal != null)
            {
                this.navigationJournal.GoBack();
            }
        }

        void IConfirmNavigationRequest.ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            // todo: 07 - Confirming Navigation Requests
            // There are times when a view (or view model) wish to be able to cancel a navigation request.
            // For this email, the user may have started but not sent an email.  We want to confirm with
            // the user that they want to discard their changes before completing the navigation.
            //
            // The view model uses the InteractionRequest to prompt the user (this is explained in more
            // detail in Prism's MVVM guidance) and, if the user confirms they want to navigate away,
            // then continues the navigation by using the continuationCallback passed in as a parameter.
            //
            // NOTE: You MUST invoke the continuationCallback action or you will halt this current
            // navigation request and no further processing of this request willl take place.  
            if (this.sendState == NormalStateKey)
            {
                this.confirmExitInteractionRequest.Raise(
                    new Confirmation { Content = Resources.ConfirmNavigateAwayFromEmailMessage, Title = Resources.ConfirmNavigateAwayFromEmailTitle },
                    c => { continuationCallback(c.Confirmed); });
            }
            else
            {
                continuationCallback(true);
            }
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            // We always want a new instance of a composed email, so we should return false to indicate
            // this doesn't handle the navigation request.
            return false;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            // Intentionally not implemented
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            // todo: 09 - Email OnNavigatedTo : Accessing navigation context.
            // When this view model is navigated to it gains access to the
            // NavigationContext to determine if we are composing a new email
            // or replying to an existing one.
            //
            // The navigation context offers the context information through
            // the Parameters property that is a string/value dictionary
            // built using the NavigationParameters class.
            //
            // In this example, we look for the 'ReplyTo' value to 
            // determine if we are replying to an email and, if so, 
            // retrieving it's relevant information from the email service
            // to pre-populate response values.
            //
            var emailDocument = new EmailDocument();

            var parameters = navigationContext.Parameters;

            var replyTo = parameters[ReplyToParameterKey];
            Guid replyToId;
            if (replyTo != null)
            {
                if (replyTo is Guid)
                {
                    replyToId = (Guid)replyTo;
                }
                else
                {
                    replyToId = Guid.Parse(replyTo.ToString());
                }

                var replyToEmail = this.emailService.GetEmailDocument(replyToId);
                if (replyToEmail != null)
                {
                    emailDocument.To = replyToEmail.From;
                    emailDocument.Subject = Resources.ResponseMessagePrefix + replyToEmail.Subject;

                    emailDocument.Text =
                        Environment.NewLine +
                        replyToEmail.Text
                            .Split(Environment.NewLine.ToCharArray())
                            .Select(l => l.Length > 0 ? Resources.ResponseLinePrefix + l : l)
                            .Aggregate((l1, l2) => l1 + Environment.NewLine + l2);
                }
            }
            else
            {
                var to = parameters[ToParameterKey];
                if (to != null)
                {
                    emailDocument.To = to.ToString();
                }
            }

            this.EmailDocument = emailDocument;

            // todo: 10 - Email OnNaviatedTo : Capture the navigation service journal
            // You can capture the navigation service or navigation service journal
            // to navigate the region you're placed in without having to expressly 
            // know which region to navigate.
            //
            // This is useful if you need to navigate 'back' at some point after this
            // view model closes.
            this.navigationJournal = navigationContext.NavigationService.Journal;
        }
    }
}
