// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using StateBasedNavigation.Desktop.Infrastructure;

namespace StateBasedNavigation.Desktop.Model
{
    public class ChatService : IChatService
    {
        private static readonly string Avatar1Uri = @"/StateBasedNavigation.Desktop;component/Avatars/MC900432625.PNG";
        private static readonly string Avatar2Uri = @"/StateBasedNavigation.Desktop;component/Avatars/MC900433938.PNG";
        private static readonly string Avatar3Uri = @"/StateBasedNavigation.Desktop;component/Avatars/MC900433946.PNG";
        private static readonly string Avatar4Uri = @"/StateBasedNavigation.Desktop;component/Avatars/MC900434899.PNG";

        private static readonly string[] receivedMessages =
            new[]
            {
                "Hi, how are you?",
                "You will not believe this!",
                "So far so good",
                "Hope you're doing ok...",
                "Yes",
                "No",
                "Sometimes",
                "Is that all?",
                "Can't right now..."
            };

        private readonly Dispatcher dispatcher;
        private readonly ITimer timer;
        private readonly ReadOnlyCollection<Contact> contacts;
        private readonly Random random;
        private bool connected;

        public ChatService()
            : this(new DispatcherTimerWrapper(new TimeSpan(0, 0, 1)))
        {
        }

        public ChatService(ITimer timer)
        {
            this.dispatcher = Application.Current.Dispatcher;
            this.random = new Random();
            this.timer = timer;
            this.timer.Tick += this.OnTimerTick;
            this.timer.Start();

            this.contacts =
                new ReadOnlyCollection<Contact>(
                    new[]
                    {
                        new Contact { Name = "Friend #1", AvatarUri = Avatar1Uri, PersonalMessage = "Enjoying my vacations!" },
                        new Contact { Name = "Friend #2", AvatarUri = Avatar3Uri },
                        new Contact { Name = "Friend #3", AvatarUri = Avatar2Uri },
                        new Contact { Name = "Friend #4", AvatarUri = Avatar1Uri, PersonalMessage = "Work work work work work" },
                        new Contact { Name = "Friend #5", AvatarUri = Avatar4Uri },
                        new Contact { Name = "Friend #6", AvatarUri = Avatar2Uri },
                        new Contact { Name = "Friend #7", AvatarUri = Avatar4Uri },
                        new Contact { Name = "Friend #8", AvatarUri = Avatar2Uri },
                        new Contact { Name = "Friend #9", AvatarUri = Avatar3Uri },
                        new Contact { Name = "Friend #10", AvatarUri = Avatar1Uri }
                    });
        }

        public event EventHandler ConnectionStatusChanged;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public bool Connected
        {
            get
            {
                return this.connected;
            }

            set
            {
                if (this.connected != value)
                {
                    this.connected = value;
                    var handler = this.ConnectionStatusChanged;
                    if (handler != null)
                    {
                        handler(this, EventArgs.Empty);
                    }
                }
            }
        }

        public void GetContacts(Action<IOperationResult<IEnumerable<Contact>>> callback)
        {
            this.dispatcher.BeginInvoke(
                new Action(() =>
                {
                    callback(new GetContactsOperationResult(this.contacts));
                }));
        }

        public void SendMessage(Contact contact, string message, Action<IOperationResult> callback)
        {
            Timer timer = null;
            timer =
                new Timer(
                    state =>
                    {
                        this.dispatcher.BeginInvoke(
                            new Action(() =>
                            {
                                timer.Dispose();
                                Debug.WriteLine("Sent message to '{0}': {1}", contact.Name, message);
                                callback(new OperationResult());
                            }));
                    },
                    null,
                    3000,
                    Timeout.Infinite);
        }

        private void OnTimerTick(object sender, EventArgs args)
        {
            if (this.Connected)
            {
                var coinToss = this.random.Next(3);
                if (coinToss == 0)
                {
                    this.ReceiveMessage(
                        this.GetRandomMessage(this.random.Next(receivedMessages.Length)),
                        this.GetRandomContact(this.random.Next(this.contacts.Count)));
                }
                else
                {
                    coinToss = this.random.Next(150);
                    if (coinToss == 0)
                    {
                        this.Connected = false;
                    }
                }
            }
        }

        private void ReceiveMessage(string message, Contact contact)
        {
            var handler = this.MessageReceived;
            if (handler != null)
            {
                handler(this, new MessageReceivedEventArgs(contact, message));
            }
        }

        private string GetRandomMessage(int messageIndex)
        {
            return receivedMessages[messageIndex];
        }

        private Contact GetRandomContact(int contactIndex)
        {
            return this.contacts[contactIndex];
        }

        private class GetContactsOperationResult : OperationResult<IEnumerable<Contact>>
        {
            public GetContactsOperationResult(IEnumerable<Contact> contacts)
            {
                this.Result = contacts;
            }
        }

        private class DispatcherTimerWrapper : ITimer
        {
            private readonly DispatcherTimer timer;

            public DispatcherTimerWrapper(TimeSpan interval)
            {
                this.timer = new DispatcherTimer { Interval = interval };
            }

            public event EventHandler Tick
            {
                add { this.timer.Tick += value; }

                remove { this.timer.Tick -= value; }
            }

            public void Start()
            {
                this.timer.Start();
            }

            public void Stop()
            {
                this.timer.Stop();
            }
        }
    }
}
