using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace UsingPopupWindowAction.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public DelegateCommand NotificationCommand { get; set; }

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; set; }
        public DelegateCommand ConfirmationCommand { get; set; }

        public MainWindowViewModel()
        {
            NotificationRequest = new InteractionRequest<INotification>();
            NotificationCommand = new DelegateCommand(RaiseNotification);

            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            ConfirmationCommand = new DelegateCommand(RaiseConfirmation);
        }

        void RaiseNotification()
        {
            NotificationRequest.Raise(new Notification { Content = "Notification Message", Title = "Notification" }, r => Title = "Notified");
        }

        void RaiseConfirmation()
        {
            ConfirmationRequest.Raise(new Confirmation {
                Title = "Confirmation",
                Content = "Confirmation Message" }, 
                r => Title = r.Confirmed ? "Confirmed" : "Not Confirmed");
        }
    }
}
