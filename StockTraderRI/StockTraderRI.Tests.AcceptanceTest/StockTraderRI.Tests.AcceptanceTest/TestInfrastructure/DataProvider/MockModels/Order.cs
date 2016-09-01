// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
{
    [Serializable]
    [XmlRoot("Order")]
    public class Order
    {
        private string symbol;
        private decimal limitprice;
        private string orderType;
        private int shares;
        private string timeInForce;
        private string transactionType;

        public Order()
        { }

        /// <summary>
        /// Instantiate Order for symbol
        /// </summary>
        /// <param name="symbol">Symbol for which the model is created</param>
        public Order(string symbol)
        {
            this.symbol = symbol;
        }

        /// <summary>
        /// Instantiate Order with given parameter values
        /// </summary>
        /// <param name="symbol">Symbol for which the model is created</param>
        /// <param name="limitprice">Limit/Stop price for buying / selling the stock</param>
        /// <param name="orderType">type of the order</param>
        /// <param name="shares">number of shares to be bought / sold</param>
        /// <param name="timeInForce">time in force</param>
        public Order(string symbol, decimal limitPrice, string orderType, int shares, string timeInForce, string transactionType)
        {
            this.symbol = symbol;
            this.limitprice = limitPrice;
            this.orderType = orderType;
            this.shares = shares;
            this.timeInForce = timeInForce;
            this.transactionType = transactionType;
        }

        [XmlAttribute("TickerSymbol")]
        public string Symbol
        {
            get { return this.symbol; }
            set { this.symbol = value; }
        }

        [XmlAttribute("StopLimitPrice")]
        public decimal LimitStopPrice
        {
            get { return this.limitprice; }
            set { this.limitprice = value; }
        }

        [XmlAttribute("OrderType")]
        public string OrderType
        {
            get { return this.orderType; }
            set { this.orderType = value; }
        }

        [XmlAttribute("Shares")]
        public int NumberOfShares
        {
            get { return this.shares; }
            set { this.shares = value; }
        }
        
        [XmlAttribute("TimeInForce")]
        public string TimeInForce
        {
            get { return this.timeInForce; }
            set { this.timeInForce = value; }
        }

        public string FormattedTimeInForce
        {
            get 
            {
                if (this.timeInForce == TestDataInfrastructure.GetTestInputData("TimeInForceEndOfDay"))
                {
                    return TestDataInfrastructure.GetTestInputData("FormattedTimeInForceEndOfDay");
                }
                else
                {
                    return String.Empty;
                }
            }
            set { this.timeInForce = value; }
        }

        [XmlAttribute("TransactionType")]
        public string TransactionType
        {
            get { return this.transactionType; }
            set { this.transactionType = value; }
        }

        public override bool Equals(object obj)
        {
            Order o = obj as Order;

            return (
                this.symbol.ToUpperInvariant().Equals(o.Symbol.ToUpperInvariant()) &&
                this.limitprice.Equals(o.LimitStopPrice) &&
                this.orderType.ToUpperInvariant().Equals(o.OrderType.ToUpperInvariant()) &&
                this.shares.Equals(o.NumberOfShares) &&
                this.timeInForce.ToUpperInvariant().Equals(o.TimeInForce.ToUpperInvariant()) &&
                this.transactionType.ToUpperInvariant().Equals(o.TransactionType.ToUpperInvariant())
                    );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
