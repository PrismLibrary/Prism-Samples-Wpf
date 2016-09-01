// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Prism.Interactivity.InteractionRequest;

namespace StateBasedNavigation.Desktop.Model
{
    public class ReceivedMessage : Notification
    {
        public ReceivedMessage(Contact contact, string message)
        {
            this.Contact = contact;
            this.Message = message;
        }

        public Contact Contact { get; private set; }

        public string Message { get; private set; }
    }
}
