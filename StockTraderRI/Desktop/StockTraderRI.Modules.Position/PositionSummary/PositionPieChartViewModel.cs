

using System.ComponentModel.Composition;

namespace StockTraderRI.Modules.Position.PositionSummary
{
    [Export(typeof(IPositionPieChartViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PositionPieChartViewModel : IPositionPieChartViewModel
    {
        public IObservablePosition Position { get; private set; }

        [ImportingConstructor]
        public PositionPieChartViewModel(IObservablePosition observablePosition)
        {
            this.Position = observablePosition;
        }
    }
}
