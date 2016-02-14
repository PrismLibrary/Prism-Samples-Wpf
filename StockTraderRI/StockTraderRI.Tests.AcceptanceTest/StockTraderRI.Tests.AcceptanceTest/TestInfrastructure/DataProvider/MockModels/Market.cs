// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure.MockModels
{
    [Serializable]
    [XmlRoot("MarketItem")]
    public class Market
    {
        private string symbol;
        private decimal marketValue;
        private long volume;

        public Market() { }

        public Market(string symbol, decimal value)
        {
            this.symbol = symbol;
            this.marketValue = value;
        }

        public Market(string symbol, decimal value, long volume) 
        {
            this.symbol = symbol;
            this.marketValue = value;
            this.volume = volume;
        }

        [XmlAttribute("TickerSymbol")]
        public string TickerSymbol
        {
            get { return this.symbol; }
            set { this.symbol = value; }
        }

        [XmlAttribute("LastPrice")]
        public decimal LastPrice
        {
            get { return this.marketValue; }
            set { this.marketValue = value; }
        }

        [XmlAttribute("Volume")]
        public long Volume
        {
            get { return this.volume; }
            set { this.volume = value; }
        }
    }
}
