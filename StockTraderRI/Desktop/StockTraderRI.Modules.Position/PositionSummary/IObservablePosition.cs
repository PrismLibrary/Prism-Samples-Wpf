

using System.Collections.ObjectModel;

namespace StockTraderRI.Modules.Position.PositionSummary
{
    public interface IObservablePosition
    {
        ObservableCollection<PositionSummaryItem> Items { get; }
    }
}