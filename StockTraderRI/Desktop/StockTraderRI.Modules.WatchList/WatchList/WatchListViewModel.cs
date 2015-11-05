

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using StockTraderRI.Modules.Watch.Services;
using System.ComponentModel.Composition;
using StockTraderRI.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Events;
using Prism.Regions;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Modules.Watch.Properties;

namespace StockTraderRI.Modules.Watch.WatchList
{
    [Export(typeof(WatchListViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WatchListViewModel : BindableBase
    {
        private readonly IMarketFeedService marketFeedService;
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;
        private readonly ObservableCollection<string> watchList;
        private ICommand removeWatchCommand;
        private ObservableCollection<WatchItem> watchListItems;
        private WatchItem currentWatchItem;

        [ImportingConstructor]
        public WatchListViewModel(IWatchListService watchListService, IMarketFeedService marketFeedService, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            if (watchListService == null)
            {
                throw new ArgumentNullException("watchListService");
            }

            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }

            this.HeaderInfo = Resources.WatchListTitle;
            this.WatchListItems = new ObservableCollection<WatchItem>();

            this.marketFeedService = marketFeedService;
            this.regionManager = regionManager;

            this.watchList = watchListService.RetrieveWatchList();
            this.watchList.CollectionChanged += delegate { this.PopulateWatchItemsList(this.watchList); };
            this.PopulateWatchItemsList(this.watchList);

            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<MarketPricesUpdatedEvent>().Subscribe(this.MarketPricesUpdated, ThreadOption.UIThread);

            this.removeWatchCommand = new DelegateCommand<string>(this.RemoveWatch);

            this.watchListItems.CollectionChanged += this.WatchListItems_CollectionChanged;
        }       

        public ObservableCollection<WatchItem> WatchListItems
        {
            get
            {
                return this.watchListItems;
            }

            private set
            {
                SetProperty(ref this.watchListItems, value);
            }
        }

        public WatchItem CurrentWatchItem
        {
            get
            {
                return this.currentWatchItem;
            }

            set
            {
                if (value != null)
                {
                    SetProperty(ref this.currentWatchItem, value);
                    this.eventAggregator.GetEvent<TickerSymbolSelectedEvent>().Publish(this.currentWatchItem.TickerSymbol);
                }
            }
        }

        public string HeaderInfo { get; set; }

        public ICommand RemoveWatchCommand { get { return this.removeWatchCommand; } }

        private void MarketPricesUpdated(IDictionary<string, decimal> updatedPrices)
        {
            if (updatedPrices == null)
            {
                throw new ArgumentNullException("updatedPrices");
            }

            foreach (WatchItem watchItem in this.WatchListItems)
            {
                if (updatedPrices.ContainsKey(watchItem.TickerSymbol))
                {
                    watchItem.CurrentPrice = updatedPrices[watchItem.TickerSymbol];
                }
            }
        }

        private void RemoveWatch(string tickerSymbol)
        {
            this.watchList.Remove(tickerSymbol);
        }

        private void PopulateWatchItemsList(IEnumerable<string> watchItemsList)
        {
            this.WatchListItems.Clear();
            foreach (string tickerSymbol in watchItemsList)
            {
                decimal? currentPrice;
                try
                {
                    currentPrice = this.marketFeedService.GetPrice(tickerSymbol);
                }
                catch (ArgumentException)
                {
                    currentPrice = null;
                }

                this.WatchListItems.Add(new WatchItem(tickerSymbol, currentPrice));
            }
        }

        private void WatchListItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                regionManager.Regions[RegionNames.MainRegion].RequestNavigate("/WatchListView", nr => { });
            }
        }
    }
}
