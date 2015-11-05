

using StockTraderRI.Modules.Position.Orders;
using System.Diagnostics.CodeAnalysis;

namespace StockTraderRI.Modules.Position.Interfaces
{
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification="This interface is used as a contract type in the MEF container.")]
    public interface IOrdersView
    {
        // The OrdersController.ShowOrdersView method adds an OrdersView to the action region.
        // The OrdersView implements this interface and is exported in the container.
        // Therefore, the OrdersController is abstracted of the concrete implementation of the view,
        // while still being able of retrieving it from the container and adding it to a region.
    }
}
