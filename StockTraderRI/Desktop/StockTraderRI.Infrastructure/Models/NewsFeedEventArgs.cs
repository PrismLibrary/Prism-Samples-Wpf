

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockTraderRI.Infrastructure.Models
{
    public class NewsFeedEventArgs : EventArgs
    {
        public NewsFeedEventArgs(string tickerSymbol, string newsHeadline)
        {
            TickerSymbol = tickerSymbol;
            NewsHeadline = newsHeadline;
        }

        public string TickerSymbol { get; set; }
        public string NewsHeadline { get; set; }
    }
}
