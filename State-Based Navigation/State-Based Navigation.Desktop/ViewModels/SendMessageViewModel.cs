// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace StateBasedNavigation.Desktop.ViewModels
{
    public class SendMessageViewModel : BindableBase, IConfirmation, IInteractionRequestAware
    {
        private string message;

        public SendMessageViewModel()
        {
            this.OKCommand = new DelegateCommand(this.SendMessage);
            this.CancelCommand = new DelegateCommand(this.Cancel);
        }

        public bool Confirmed { get; set; }

        public string Title { get; set; }

        public object Content { get; set; }

        public INotification Notification { get; set; }

        public Action FinishInteraction { get; set; }

        public ICommand OKCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.SetProperty(ref this.message, value);
            }
        }

        private void SendMessage()
        {
            this.Confirmed = true;
            this.FinishInteraction();
        }

        private void Cancel()
        {
            this.Confirmed = false;
            this.FinishInteraction();
        }
    }
}
