

using System.Collections.Generic;

namespace StockTraderRI.Modules.Position.Orders
{
    public interface IValueDescriptionList<T> : IList<ValueDescription<T>> where T : struct
    {

    }
}
