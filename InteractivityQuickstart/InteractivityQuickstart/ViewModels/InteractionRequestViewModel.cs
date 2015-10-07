using InteractivityQuickstart.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Windows.Input;

namespace InteractivityQuickstart.ViewModels
{
    public class InteractionRequestViewModel : BindableBase
    {
        private string resultMessage;

        public InteractionRequestViewModel()
        {
            // To setup the interaction request we only need to create instances of the InteractionRequest class
            // and expose them through properties. The InteractionRequestTriggers will bind to them and subscribe
            // to the corresponding events automatically.
            // The InteractionRequest class requires a parameter that has to inherit from the INotification class.
            this.ConfirmationRequest = new InteractionRequest<IConfirmation>();
            this.NotificationRequest = new InteractionRequest<INotification>();
            this.CustomPopupViewRequest = new InteractionRequest<INotification>();
            this.ItemSelectionRequest = new InteractionRequest<ItemSelectionNotification>();

            // Commands for each of the buttons. Each of these raise a different interaction request.
            this.RaiseConfirmationCommand = new DelegateCommand(this.RaiseConfirmation);
            this.RaiseNotificationCommand = new DelegateCommand(this.RaiseNotification);
            this.RaiseCustomPopupViewCommand = new DelegateCommand(this.RaiseCustomPopupView);
            this.RaiseItemSelectionCommand = new DelegateCommand(this.RaiseItemSelection);
        }

        public string InteractionResultMessage
        {
            get
            {
                return this.resultMessage;
            }
            set
            {
                this.resultMessage = value;
                this.OnPropertyChanged("InteractionResultMessage");
            }
        }

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; private set; }

        public InteractionRequest<INotification> NotificationRequest { get; private set; }

        public InteractionRequest<INotification> CustomPopupViewRequest { get; private set; }

        public InteractionRequest<ItemSelectionNotification> ItemSelectionRequest { get; private set; }

        public ICommand RaiseConfirmationCommand { get; private set; }

        public ICommand RaiseNotificationCommand { get; private set; }

        public ICommand RaiseCustomPopupViewCommand { get; private set; }

        public ICommand RaiseItemSelectionCommand { get; private set; }

        private void RaiseNotification()
        {
            // By invoking the Raise method we are raising the Raised event and triggering any InteractionRequestTrigger that
            // is subscribed to it.
            // As parameters we are passing a Notification, which is a default implementation of INotification provided by Prism
            // and a callback that is executed when the interaction finishes.
            this.NotificationRequest.Raise(
               new Notification { Content = "Notification Message", Title = "Notification" },
               n => { InteractionResultMessage = "The user was notified."; });
        }

        private void RaiseConfirmation()
        {
            // By invoking the Raise method we are raising the Raised event and triggering any InteractionRequestTrigger that
            // is subscribed to it.
            // As parameters we are passing a Confirmation, which is a default implementation of IConfirmation (which inherits
            // from INotification) provided by Prism and a callback that is executed when the interaction finishes.
            this.ConfirmationRequest.Raise(
                new Confirmation { Content = "Confirmation Message", Title = "Confirmation" },
                c => { InteractionResultMessage = c.Confirmed ? "The user accepted." : "The user cancelled."; });
        }

        private void RaiseCustomPopupView()
        {
            // In this case we are passing a simple notification as a parameter.
            // The custom popup view we are using for this interaction request does not have a DataContext of its own
            // so it will inherit the DataContext of the window, which will be this same notification.
            this.InteractionResultMessage = "";
            this.CustomPopupViewRequest.Raise(
                new Notification { Content = "Message for the CustomPopupView", Title = "Custom Popup" });
        }

        private void RaiseItemSelection()
        {
            // Here we have a custom implementation of INotification which allows us to pass custom data in the 
            // parameter of the interaction request. In this case, we are passing a list of items.
            ItemSelectionNotification notification = new ItemSelectionNotification();
            notification.Items.Add("Item1");
            notification.Items.Add("Item2");
            notification.Items.Add("Item3");
            notification.Items.Add("Item4");
            notification.Items.Add("Item5");
            notification.Items.Add("Item6");

            notification.Title = "Items";

            // The custom popup view in this case has its own view model which implements the IInteractionRequestAware interface
            // therefore, its Notification property will be automatically populated with this notification by the PopupWindowAction.
            // Like this that view model is able to recieve data from this one without knowing each other.
            this.InteractionResultMessage = "";
            this.ItemSelectionRequest.Raise(notification,
                returned =>
                {
                    if (returned != null && returned.Confirmed && returned.SelectedItem != null)
                    {
                        this.InteractionResultMessage = "The user selected: " + returned.SelectedItem;
                    }
                    else
                    {
                        this.InteractionResultMessage = "The user cancelled the operation or didn't select an item.";
                    }
                });
        }
    }
}
