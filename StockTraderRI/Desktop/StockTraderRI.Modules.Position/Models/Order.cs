

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockTraderRI.Infrastructure;
using StockTraderRI.Modules.Position.Orders;

namespace StockTraderRI.Modules.Position.Models
{
    public class Order
    {
        public int Shares { get; set; }
        public TimeInForce TimeInForce { get; set; }
        public string TickerSymbol { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal StopLimitPrice { get; set; }
        public OrderType OrderType { get; set; }
    }
}
