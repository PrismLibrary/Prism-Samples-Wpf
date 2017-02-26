using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using UsingEventAggregator.Core;

namespace ModuleB.ViewModels
{
    public class MessageListViewModel : BindableBase
    {
        IEventAggregator _ea;

        private ObservableCollection<string> _messages;
        public ObservableCollection<string> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }

        public MessageListViewModel(IEventAggregator ea)
        {
            _ea = ea;
            Messages = new ObservableCollection<string>();

            _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }

        private void MessageReceived(string message)
        {
            Messages.Add(message);
        }
    }
}
