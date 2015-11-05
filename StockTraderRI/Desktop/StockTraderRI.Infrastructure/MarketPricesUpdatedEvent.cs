

using System.Collections.Generic;
using Prism.Events;

namespace StockTraderRI.Infrastructure
{
    public class MarketPricesUpdatedEvent : PubSubEvent<IDictionary<string, decimal>>
    {
    }
}
