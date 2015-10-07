using InteractivityQuickstart.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace InteractivityQuickstart.ViewModels
{
    public class ItemSelectionViewModel : BindableBase, IInteractionRequestAware
    {
        private ItemSelectionNotification notification;

        public ItemSelectionViewModel()
        {
            this.SelectItemCommand = new DelegateCommand(this.AcceptSelectedItem);
            this.CancelCommand = new DelegateCommand(this.CancelInteraction);
        }

        // Both the FinishInteraction and Notification properties will be set by the PopupWindowAction
        // when the popup is shown.
        public Action FinishInteraction { get; set; }

        public INotification Notification 
        {
            get
            {
                return this.notification;
            }
            set
            {
                if (value is ItemSelectionNotification)
                {
                    // To keep the code simple, this is the only property where we are raising the PropertyChanged event,
                    // as it's required to update the bindings when this property is populated.
                    // Usually you would want to raise this event for other properties too.
                    this.notification = value as ItemSelectionNotification;
                    this.OnPropertyChanged(() => this.Notification);
                }
            }
        }

        public string SelectedItem { get; set; }

        public ICommand SelectItemCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public void AcceptSelectedItem()
        {
            if (this.notification != null)
            {
                this.notification.SelectedItem = this.SelectedItem;
                this.notification.Confirmed = true;
            }

            this.FinishInteraction();
        }

        public void CancelInteraction()
        {
            if (this.notification != null)
            {
                this.notification.SelectedItem = null;
                this.notification.Confirmed = false;
            }

            this.FinishInteraction();
        }
    }
}
