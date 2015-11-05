

using Prism.Commands;

namespace StockTraderRI.Modules.Position.Controllers
{

    public interface IOrdersController
    {
        DelegateCommand<string> BuyCommand { get; }
        DelegateCommand<string> SellCommand { get; }
    }
}
