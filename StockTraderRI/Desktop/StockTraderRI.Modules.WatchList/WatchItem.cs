

using Prism.Mvvm;
using System.ComponentModel;

namespace StockTraderRI.Modules.Watch
{
    public class WatchItem : BindableBase
    {
        private decimal? _currentPrice;

        public WatchItem(string tickerSymbol, decimal? currentPrice)
        {
            TickerSymbol = tickerSymbol;
            CurrentPrice = currentPrice;
        }

        public string TickerSymbol { get; set; }

        public decimal? CurrentPrice
        {
            get { return _currentPrice; }
            set
            {
                SetProperty(ref _currentPrice, value);
            }
        }
    }
}
