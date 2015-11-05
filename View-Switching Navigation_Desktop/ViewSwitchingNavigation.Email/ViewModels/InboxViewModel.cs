using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using ViewSwitchingNavigation.Email.Model;
using ViewSwitchingNavigation.Infrastructure;

namespace ViewSwitchingNavigation.Email.ViewModels
{
    [Export]
    public class InboxViewModel : BindableBase
    {
        private const string ComposeEmailViewKey = "ComposeEmailView";
        private const string ReplyToKey = "ReplyTo";
        private const string EmailViewKey = "EmailView";
        private const string EmailIdKey = "EmailId";

        private readonly IEmailService emailService;
        private readonly IRegionManager regionManager;
        private readonly DelegateCommand<object> composeMessageCommand;
        private readonly DelegateCommand<object> replyMessageCommand;
        private readonly DelegateCommand<EmailDocument> openMessageCommand;
        private readonly ObservableCollection<EmailDocument> messagesCollection;

        private static Uri ComposeEmailViewUri = new Uri(ComposeEmailViewKey, UriKind.Relative);

        [ImportingConstructor]
        public InboxViewModel(IEmailService emailService, IRegionManager regionManager)
        {
            this.composeMessageCommand = new DelegateCommand<object>(this.ComposeMessage);
            this.replyMessageCommand = new DelegateCommand<object>(this.ReplyMessage, this.CanReplyMessage);
            this.openMessageCommand = new DelegateCommand<EmailDocument>(this.OpenMessage);

            this.messagesCollection = new ObservableCollection<EmailDocument>();
            this.Messages = new ListCollectionView(this.messagesCollection);
            this.Messages.CurrentChanged += (s, e) =>
                this.replyMessageCommand.RaiseCanExecuteChanged();

            this.emailService = emailService;
            this.regionManager = regionManager;

            var task = this.Initialize();
        }

        private async Task Initialize()
        {
            var messages = await this.emailService.GetEmailDocumentsAsync();
            messages.ToList().ForEach(m => this.messagesCollection.Add(m));
        }

        public ICollectionView Messages { get; private set; }

        public ICommand ComposeMessageCommand
        {
            get { return this.composeMessageCommand; }
        }

        public ICommand ReplyMessageCommand
        {
            get { return this.replyMessageCommand; }
        }

        public ICommand OpenMessageCommand
        {
            get { return this.openMessageCommand; }
        }

        private void ComposeMessage(object ignored)
        {
            // todo: 02 - New Email: Navigating to a view in a region
            // Any region can be navigated by passing in a relative Uri for the email view name.
            // By the default, the navigation service will look for an item already in the region
            // with a type name that matches the Uri.
            //
            // If an item is not found in the region, the navigation services uses the 
            // ServiceLocator to find an item that matches the Uri, in the example below it would
            // be ComposeEmailView.
            this.regionManager.RequestNavigate(RegionNames.MainContentRegion, ComposeEmailViewUri);
        }

        private void ReplyMessage(object ignored)
        {
            // todo: 03 - Reply Email: Navigating to a view in a region with context
            // When navigating, you can also supply context so the target view or
            // viewmodel can orient their data to something appropriate.  In this case,
            // we've chosen to pass the email id in a name/value pairs.
            //
            // The recipient of the context can get access to this information by
            // implementing the INavigationAware or IConfirmNavigationRequest interface, as shown by the 
            // the ComposeEmailViewModel.
            //
            var currentEmail = this.Messages.CurrentItem as EmailDocument;

            if (currentEmail != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add(ReplyToKey, currentEmail.Id.ToString("N"));
                this.regionManager.RequestNavigate(RegionNames.MainContentRegion, ComposeEmailViewKey + parameters);
            }

            
        }

        private bool CanReplyMessage(object ignored)
        {
            return this.Messages.CurrentItem != null;
        }

        private void OpenMessage(EmailDocument document)
        {
            // todo: 04 - Open Email: Navigating to a view in a region with context
            // When navigating, you can also supply context so the target view or
            // viewmodel can orient their data to something appropriate.  In this case,
            // we've chosen to pass the email id in a name/value pair using and handmade Uri.
            //
            // The EmailViewModel retrieves this context by implementing the INavigationAware
            // interface.
            //
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(EmailIdKey, document.Id.ToString("N"));

            this.regionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(EmailViewKey + parameters, UriKind.Relative));
        }
    }
}
