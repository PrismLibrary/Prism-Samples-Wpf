using System;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using UsingPopupWindowAction.Notifications;

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

        public InteractionRequest<INotification> CustomPopupRequest { get; set; }
        public DelegateCommand CustomPopupCommand { get; set; }

        public InteractionRequest<ICustomNotification> CustomNotificationRequest { get; set; }
        public DelegateCommand CustomNotificationCommand { get; set; }

        public MainWindowViewModel()
        {
            NotificationRequest = new InteractionRequest<INotification>();
            NotificationCommand = new DelegateCommand(RaiseNotification);

            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            ConfirmationCommand = new DelegateCommand(RaiseConfirmation);

            CustomPopupRequest = new InteractionRequest<INotification>();
            CustomPopupCommand = new DelegateCommand(RaiseCustomPopup);

            CustomNotificationRequest = new InteractionRequest<ICustomNotification>();
            CustomNotificationCommand = new DelegateCommand(RaiseCustomInteraction);
        }

        void RaiseNotification()
        {
            NotificationRequest.Raise(new Notification { Content = "Notification Message", Title = "Notification" }, r => Title = "Notified");
        }

        void RaiseConfirmation()
        {
            ConfirmationRequest.Raise(new Confirmation { Title = "Confirmation", Content = "Confirmation Message" }, r => Title = r.Confirmed ? "Confirmed" : "Not Confirmed");
        }

        void RaiseCustomPopup()
        {
            CustomPopupRequest.Raise(new Notification { Title = "Custom Popup", Content = "Custom Popup Message " }, r => Title = "Good to go");
        }

        private void RaiseCustomInteraction()
        {
            CustomNotificationRequest.Raise(new CustomNotification { Title = "Custom Notification" }, r =>
                {
                    if (r.Confirmed && r.SelectedItem != null)
                        Title = $"User selected: { r.SelectedItem}";
                    else
                        Title = "User cancelled or didn't select an item";
                });
        }
    }
}
