

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.Market.Properties;
using System.ComponentModel.Composition;

namespace StockTraderRI.Modules.Market.Services
{
    [Export(typeof(IMarketHistoryService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MarketHistoryService : IMarketHistoryService
    {
        private Dictionary<string, MarketHistoryCollection> _marketHistory;

        public MarketHistoryService()
        {
            InitializeMarketHistory();
        }

        private void InitializeMarketHistory()
        {
            XDocument document = XDocument.Parse(Resources.MarketHistory);

            _marketHistory = document.Descendants("MarketHistoryItem")
                .GroupBy(x => x.Attribute("TickerSymbol").Value,
                         x => new MarketHistoryItem
                                  {
                                      DateTimeMarker = DateTime.Parse(x.Attribute("Date").Value, CultureInfo.InvariantCulture),
                                      Value = Decimal.Parse(x.Value, NumberStyles.Float, CultureInfo.InvariantCulture)
                                  })
                .ToDictionary(group => group.Key, group => new MarketHistoryCollection(group));
        }

        public MarketHistoryCollection GetPriceHistory(string tickerSymbol)
        {
            MarketHistoryCollection items = _marketHistory[tickerSymbol];
            return items;
        }
    }
}
