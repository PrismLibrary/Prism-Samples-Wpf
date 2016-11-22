using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using System.Collections.Generic;

namespace InteractivityQuickstart.ViewModels
{
    public class ItemSelectionViewModel : BindableBase, IInteractionRequestAware, IConfirmation
    {
        public ItemSelectionViewModel()
        {
            this.Items = new List<string>();
            this.SelectedItem = null;

            this.SelectItemCommand = new DelegateCommand(this.AcceptSelectedItem);
            this.CancelCommand = new DelegateCommand(this.CancelInteraction);
        }

        public IList<string> Items { get; private set; }

        // Both the FinishInteraction and Notification properties will be set by the PopupWindowAction
        // when the popup is shown.
        public Action FinishInteraction { get; set; }

        public INotification Notification { get; set; } // not needed but required by interface

        public string SelectedItem { get; set; }

        public ICommand SelectItemCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public bool Confirmed { get; set; }

        public string Title { get; set; }

        public object Content { get; set; } // not needed but required by interface

        public void AcceptSelectedItem()
        {
            this.SelectedItem = this.SelectedItem;
            this.Confirmed = true;

            this.FinishInteraction();
        }

        public void CancelInteraction()
        {
            this.SelectedItem = null;
            this.Confirmed = false;

            this.FinishInteraction();
        }
    }
}
