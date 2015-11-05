

using System.ComponentModel.Composition;
using System.Windows.Controls;
using StockTraderRI.Infrastructure;

namespace StockTraderRI.Modules.Position.PositionSummary
{
    [ViewExport(RegionName = RegionNames.MainRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PositionSummaryView : UserControl
    {
        public PositionSummaryView()
        {
            InitializeComponent();
        }

        #region IPositionSummaryView Members

        [Import]
        public IPositionSummaryViewModel Model
        {
            get
            {
                return DataContext as IPositionSummaryViewModel;
            }
            set
            {
                DataContext = value;
            }
        }
        #endregion
    }
}
