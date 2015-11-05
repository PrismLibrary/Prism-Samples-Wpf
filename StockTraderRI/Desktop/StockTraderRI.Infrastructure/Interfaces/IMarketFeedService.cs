


namespace StockTraderRI.Infrastructure.Interfaces
{
    public interface IMarketFeedService
    {
        decimal GetPrice(string tickerSymbol);
        long GetVolume(string tickerSymbol);
        bool SymbolExists(string tickerSymbol);
    }
}
