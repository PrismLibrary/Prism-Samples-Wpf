// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


using StateBasedNavigation.Desktop.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace StateBasedNavigation.Desktop.ViewModels
{
    public class ChatViewModel : BindableBase
    {
        private readonly IChatService chatService;
        private readonly InteractionRequest<SendMessageViewModel> sendMessageRequest;
        private readonly InteractionRequest<ReceivedMessage> showReceivedMessageRequest;
        private readonly ObservableCollection<Contact> contacts;
        private readonly CollectionView contactsView;
        private readonly DelegateCommand<bool?> showDetailsCommand;
        private bool showDetails;
        private bool sendingMessage;

        public ChatViewModel(IChatService chatService)
        {
            this.contacts = new ObservableCollection<Contact>();
            this.contactsView = new CollectionView(this.contacts);
            this.sendMessageRequest = new InteractionRequest<SendMessageViewModel>();
            this.showReceivedMessageRequest = new InteractionRequest<ReceivedMessage>();
            this.showDetailsCommand = new DelegateCommand<bool?>(this.ExecuteShowDetails, this.CanExecuteShowDetails);

            this.contactsView.CurrentChanged += this.OnCurrentContactChanged;

            this.chatService = chatService;
            this.chatService.Connected = true;
            this.chatService.ConnectionStatusChanged += (s, e) => this.OnPropertyChanged(() => this.ConnectionStatus);
            this.chatService.MessageReceived += this.OnMessageReceived;

            this.chatService.GetContacts(
                result =>
                {
                    if (result.Error == null)
                    {
                        foreach (var item in result.Result)
                        {
                            this.contacts.Add(item);
                        }
                    }
                });
        }

        public ObservableCollection<Contact> Contacts
        {
            get { return this.contacts; }
        }

        public ICollectionView ContactsView
        {
            get { return this.contactsView; }
        }

        public IInteractionRequest SendMessageRequest
        {
            get { return this.sendMessageRequest; }
        }

        public IInteractionRequest ShowReceivedMessageRequest
        {
            get { return this.showReceivedMessageRequest; }
        }

        public string ConnectionStatus
        {
            get
            {
                return this.chatService.Connected ? "Available" : "Unavailable";
            }

            set
            {
                this.chatService.Connected = value == "Available";
            }
        }

        public Contact CurrentContact
        {
            get
            {
                return this.contactsView.CurrentItem as Contact;
            }
        }

        public bool ShowDetails
        {
            get
            {
                return this.showDetails;
            }

            set
            {
                this.SetProperty(ref this.showDetails, value);
            }
        }

        public bool SendingMessage
        {
            get
            {
                return this.sendingMessage;
            }

            private set
            {
                this.SetProperty(ref this.sendingMessage, value);
            }
        }

        public ICommand ShowDetailsCommand
        {
            get { return this.showDetailsCommand; }
        }

        private void ExecuteShowDetails(bool? show)
        {
            if (show != null) this.ShowDetails = show.Value;
        }

        private bool CanExecuteShowDetails(bool? show)
        {
            return this.ContactsView.CurrentItem != null;
        }

        public void SendMessage()
        {
            var contact = this.CurrentContact;

            SendMessageViewModel viewModel = new SendMessageViewModel();
            viewModel.Title = "Send message to " + contact.Name;

            this.sendMessageRequest.Raise(
                viewModel,
                sendMessage =>
                {
                    if (sendMessage.Confirmed)
                    {
                        this.SendingMessage = true;

                        this.chatService.SendMessage(
                            contact,
                            sendMessage.Message,
                            result =>
                            {
                                this.SendingMessage = false;
                            });
                    }
                });
        }

        private void OnCurrentContactChanged(object sender, EventArgs a)
        {
            this.OnPropertyChanged(() => this.CurrentContact);
            this.showDetailsCommand.RaiseCanExecuteChanged();
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs a)
        {
            this.showReceivedMessageRequest.Raise(a.Message);
        }
    }
}
