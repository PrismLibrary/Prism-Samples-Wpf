

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Events;
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;
using System.ComponentModel.Composition;
using System;

namespace StockTraderRI.Modules.Position.PositionSummary
{
    [Export(typeof(IObservablePosition))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ObservablePosition : IObservablePosition
    {
        private IAccountPositionService accountPositionService;
        private IMarketFeedService marketFeedService;

        public ObservableCollection<PositionSummaryItem> Items { get; private set; }

        [ImportingConstructor]
        public ObservablePosition(IAccountPositionService accountPositionService, IMarketFeedService marketFeedService, IEventAggregator eventAggregator)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }

            this.Items = new ObservableCollection<PositionSummaryItem>();

            this.accountPositionService = accountPositionService;
            this.marketFeedService = marketFeedService;

            eventAggregator.GetEvent<MarketPricesUpdatedEvent>().Subscribe(MarketPricesUpdated, ThreadOption.UIThread);

            PopulateItems();

            this.accountPositionService.Updated += PositionSummaryItems_Updated;
        }

        public void MarketPricesUpdated(IDictionary<string, decimal> tickerSymbolsPrice)
        {
            if (tickerSymbolsPrice == null)
            {
                throw new ArgumentNullException("tickerSymbolsPrice");
            }

            foreach (PositionSummaryItem position in this.Items)
            {
                if (tickerSymbolsPrice.ContainsKey(position.TickerSymbol))
                {
                    position.CurrentPrice = tickerSymbolsPrice[position.TickerSymbol];
                }
            }
        }

        private void PositionSummaryItems_Updated(object sender, AccountPositionModelEventArgs e)
        {
            if (e.AcctPosition != null)
            {
                PositionSummaryItem positionSummaryItem = this.Items.First(p => p.TickerSymbol == e.AcctPosition.TickerSymbol);

                if (positionSummaryItem != null)
                {
                    positionSummaryItem.Shares = e.AcctPosition.Shares;
                    positionSummaryItem.CostBasis = e.AcctPosition.CostBasis;
                }
            }
        }

        private void PopulateItems()
        {
            PositionSummaryItem positionSummaryItem;
            foreach (AccountPosition accountPosition in this.accountPositionService.GetAccountPositions())
            {
                positionSummaryItem = new PositionSummaryItem(accountPosition.TickerSymbol, accountPosition.CostBasis, accountPosition.Shares, this.marketFeedService.GetPrice(accountPosition.TickerSymbol));
                this.Items.Add(positionSummaryItem);
            }
        }
    }
}
