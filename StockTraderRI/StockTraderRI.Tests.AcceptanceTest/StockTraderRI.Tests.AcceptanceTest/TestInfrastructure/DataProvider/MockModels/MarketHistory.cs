// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure.MockModels
{
    [Serializable]
    [XmlRoot("MarketHistoryItem")]
    public class MarketHistoryItem
    {
        private string symbol;
        private DateTime date;
        private decimal item;

        public MarketHistoryItem()
        { }

        public MarketHistoryItem(string symbol, DateTime date, decimal item)
        {
            this.symbol = symbol;
            this.date = date;
            this.item = item;
        }

        [XmlAttribute("TickerSymbol")]
        public string TickerSymbol 
        {
            get { return this.symbol; }
            set { this.symbol = value; }
        }

        [XmlAttribute("Date")]
        public DateTime Date
        {
            get { return this.date; }
            set { this.date = value; }
        }

        [XmlElement("MarketHistoryItem")]
        public decimal MarketItem
        {
            get { return this.item; }
            set { this.item = value; }
        }
    }
}
