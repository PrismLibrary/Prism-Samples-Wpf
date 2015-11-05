

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Prism.Mvvm;
using Prism.Events;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Infrastructure;

namespace StockTraderRI.Modules.Market.TrendLine
{
    [Export(typeof(TrendLineViewModel))]
    public class TrendLineViewModel : BindableBase
    {
        private readonly IMarketHistoryService marketHistoryService;

        private string tickerSymbol;

        private MarketHistoryCollection historyCollection;

        [ImportingConstructor]
        public TrendLineViewModel(IMarketHistoryService marketHistoryService, IEventAggregator eventAggregator)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }

            this.marketHistoryService = marketHistoryService;
            eventAggregator.GetEvent<TickerSymbolSelectedEvent>().Subscribe(this.TickerSymbolChanged);
        }

        public void TickerSymbolChanged(string newTickerSymbol)
        {
            MarketHistoryCollection newHistoryCollection = this.marketHistoryService.GetPriceHistory(newTickerSymbol);

            this.TickerSymbol = newTickerSymbol;
            this.HistoryCollection = newHistoryCollection;
        }

        public string TickerSymbol
        {
            get
            {
                return this.tickerSymbol;
            }
            set
            {
                SetProperty(ref this.tickerSymbol, value);
            }
        }

        public MarketHistoryCollection HistoryCollection
        {
            get
            {
                return historyCollection;
            }
            private set
            {
                SetProperty(ref this.historyCollection, value);
            }
        }
    }
}
