

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockTraderRI.Infrastructure.Models;

namespace StockTraderRI.Infrastructure.Interfaces
{
    public interface IAccountPositionService
    {
        event EventHandler<AccountPositionModelEventArgs> Updated;
        IList<AccountPosition> GetAccountPositions();
    }
}
