using Prism.Events;

namespace UsingEventAggregator.Core
{
    public class MessageSentEvent : PubSubEvent<string>
    {
    }
}
