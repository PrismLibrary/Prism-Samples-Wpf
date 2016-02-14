// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
{
    public class AccountPosition
    {
        public AccountPosition() { }

        public AccountPosition(string tickerSymbol, decimal costBasis, long shares)
        {
            TickerSymbol = tickerSymbol;
            CostBasis = costBasis;
            Shares = shares;
        }

        private string _tickerSymbol;

        public string TickerSymbol
        {
            get
            {
                return _tickerSymbol;
            }
            set
            {
                if (!value.Equals(_tickerSymbol))
                {
                    _tickerSymbol = value;
                }
            }
        }


        private decimal _costBasis;

        public decimal CostBasis
        {
            get
            {
                return _costBasis;
            }
            set
            {
                if (!value.Equals(_costBasis))
                {
                    _costBasis = value;
                }
            }
        }


        private long _shares;

        public long Shares
        {
            get
            {
                return _shares;
            }
            set
            {
                if (!value.Equals(_shares))
                {
                    _shares = value;
                }
            }
        }
    }
}
