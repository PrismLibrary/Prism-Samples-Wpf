

using StockTraderRI.Infrastructure.Models;

namespace StockTraderRI.Infrastructure.Interfaces
{
    public interface IMarketHistoryService
    {
        MarketHistoryCollection GetPriceHistory(string tickerSymbol);
    }
}
