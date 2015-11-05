

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockTraderRI.Infrastructure.Models
{
    public class AccountPositionModelEventArgs : EventArgs
    {
        public AccountPositionModelEventArgs(AccountPosition position)
        {
            AcctPosition = position;
        }

        public AccountPosition AcctPosition { get; set; }
    }
}
