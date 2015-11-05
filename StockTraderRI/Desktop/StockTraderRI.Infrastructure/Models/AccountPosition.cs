

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace StockTraderRI.Infrastructure.Models
{
    public class AccountPosition
    {
        public event EventHandler<AccountPositionEventArgs> Updated = delegate { };

        public AccountPosition() {}

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
                if (value == null)
                {
                    value = string.Empty;
                }

                if (!value.Equals(_tickerSymbol))
                {
                    _tickerSymbol = value;
                    Updated(this, new AccountPositionEventArgs());
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
                    Updated(this, new AccountPositionEventArgs());
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
                    Updated(this, new AccountPositionEventArgs());
                }
            }
        }
    }
}
