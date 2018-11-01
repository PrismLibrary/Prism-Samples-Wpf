namespace ModuleA.ViewModels
{
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    using UsingEventAggregator.Core;

    /// <summary>
    ///     Class MessageViewModel.
    ///     Implements the <see cref="BindableBase" />
    /// </summary>
    /// <seealso cref="BindableBase" />
    /// <summary>
    ///     Class MessageViewModel.
    ///     Implements the <see cref="BindableBase" />
    /// </summary>
    /// <seealso cref="BindableBase" />
    public class MessageViewModel : BindableBase
    {
        /// <summary>
        ///     The event aggregator interface
        /// </summary>
        /// <summary>
        ///     The ea
        /// </summary>
        private readonly IEventAggregator _ea;

        /// <summary>
        ///     The message
        /// </summary>
        /// <summary>
        ///     The message
        /// </summary>
        private string _message = "Message to Send";

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageViewModel" /> class.
        /// </summary>
        /// <param name="ea">The <see cref="IEventAggregator" />.</param>
        public MessageViewModel(IEventAggregator ea)
        {
            _ea = ea;
            SendMessageCommand = new DelegateCommand(SendMessage);
        }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        /// <summary>
        ///     Gets the send message command.
        /// </summary>
        /// <value>The send message command.</value>
        /// <summary>
        ///     Gets the send message command.
        /// </summary>
        /// <value>The send message command.</value>
        public DelegateCommand SendMessageCommand { get; }

        /// <summary>
        ///     Sends the message.
        /// </summary>
        private void SendMessage()
        {
            _ea.GetEvent<MessageSentEvent>().Publish(Message);
        }
    }
}