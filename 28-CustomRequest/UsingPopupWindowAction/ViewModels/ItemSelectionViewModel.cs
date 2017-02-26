using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using UsingPopupWindowAction.Notifications;

namespace UsingPopupWindowAction.ViewModels
{
    public class ItemSelectionViewModel : BindableBase, IInteractionRequestAware
    {
        public string SelectedItem { get; set; }

        public DelegateCommand SelectItemCommand { get; private set; }

        public DelegateCommand CancelCommand { get; private set; }

        public ItemSelectionViewModel()
        {
            SelectItemCommand = new DelegateCommand(AcceptSelectedItem);
            CancelCommand = new DelegateCommand(CancelInteraction);
        }

        private void CancelInteraction()
        {
            _notification.SelectedItem = null;
            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }

        private void AcceptSelectedItem()
        {
            _notification.SelectedItem = SelectedItem;
            _notification.Confirmed = true;
            FinishInteraction?.Invoke();
        }

        public Action FinishInteraction { get; set; }

        private ICustomNotification _notification;

        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (ICustomNotification)value); }
        }

    }
}
