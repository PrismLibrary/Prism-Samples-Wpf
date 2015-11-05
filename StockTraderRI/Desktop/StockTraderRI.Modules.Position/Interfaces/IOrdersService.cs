

using StockTraderRI.Modules.Position.Models;

namespace StockTraderRI.Modules.Position.Interfaces
{
    public interface IOrdersService
    {
        void Submit(Order order);
    }
}