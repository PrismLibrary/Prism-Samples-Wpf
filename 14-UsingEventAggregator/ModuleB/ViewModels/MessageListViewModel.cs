namespace ModuleB.ViewModels
{
    using System.Collections.ObjectModel;

    using Prism.Events;
    using Prism.Mvvm;

    using UsingEventAggregator.Core;

    /// <summary>
    ///     Class MessageListViewModel.
    ///     Implements the <see cref="BindableBase" />
    /// </summary>
    /// <seealso cref="BindableBase" />
    public class MessageListViewModel : BindableBase
    {
        /// <summary>
        ///     The Event Aggregator interface
        /// </summary>
        private readonly IEventAggregator _ea;

        /// <summary>
        ///     The messages
        /// </summary>
        private ObservableCollection<string> _messages;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageListViewModel" /> class.
        /// </summary>
        /// <param name="ea">The ea.</param>
        public MessageListViewModel(IEventAggregator ea)
        {
            _ea = ea;
            Messages = new ObservableCollection<string>();

            _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }

        /// <summary>
        ///     Gets or sets the messages.
        /// </summary>
        /// <value>The messages.</value>
        public ObservableCollection<string> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        /// <summary>
        ///     Messages the received.
        /// </summary>
        /// <param name="message">The message.</param>
        private void MessageReceived(string message)
        {
            Messages.Add(message);
        }
    }
}