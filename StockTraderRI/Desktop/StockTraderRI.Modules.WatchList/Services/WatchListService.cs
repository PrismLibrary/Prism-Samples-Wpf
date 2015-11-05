

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using StockTraderRI.Infrastructure.Interfaces;

namespace StockTraderRI.Modules.Watch.Services
{
    [Export(typeof(IWatchListService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class WatchListService : IWatchListService
    {
        private readonly IMarketFeedService marketFeedService;

        private ObservableCollection<string> WatchItems { get; set; }

        [ImportingConstructor]
        public WatchListService(IMarketFeedService marketFeedService)
        {
            this.marketFeedService = marketFeedService;
            WatchItems = new ObservableCollection<string>();

            AddWatchCommand = new DelegateCommand<string>(AddWatch);
        }

        public ObservableCollection<string> RetrieveWatchList()
        {
            return WatchItems;
        }

        private void AddWatch(string tickerSymbol)
        {
            if (!String.IsNullOrEmpty(tickerSymbol))
            {
                string upperCasedTrimmedSymbol = tickerSymbol.ToUpper(CultureInfo.InvariantCulture).Trim();
                if (!WatchItems.Contains(upperCasedTrimmedSymbol))
                {
                    if (marketFeedService.SymbolExists(upperCasedTrimmedSymbol))
                    {
                        WatchItems.Add(upperCasedTrimmedSymbol);
                    }
                }
            }
        }

        public ICommand AddWatchCommand { get; set; }
    }
}
