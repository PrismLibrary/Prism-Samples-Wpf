// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using StateBasedNavigation.Desktop.Infrastructure;

namespace StateBasedNavigation.Desktop.Model
{
    public interface IChatService
    {
        event EventHandler ConnectionStatusChanged;

        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        bool Connected { get; set; }

        void GetContacts(Action<IOperationResult<IEnumerable<Contact>>> callback);

        void SendMessage(Contact contact, string message, Action<IOperationResult> callback);
    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(Contact contact, string message)
        {
            this.Message = new ReceivedMessage(contact, message);
        }

        public ReceivedMessage Message { get; private set; }
    }
}
