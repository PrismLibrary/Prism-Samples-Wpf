

using StockTraderRI.Infrastructure.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace StockTraderRI.Modules.Position.Orders
{
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "This interface is used as a contract type in the MEF container.")]
    public interface IOrdersViewModel : IHeaderInfoProvider<string>
    {
    }
}