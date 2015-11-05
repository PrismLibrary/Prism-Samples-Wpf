

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using StockTraderRI.Infrastructure;
using Prism.Events;

namespace StockTraderRI.Modules.Position.PositionSummary
{
    /// <summary>
    /// Interaction logic for PositionPieChartView.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.ResearchRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PositionPieChartView : UserControl
    {
        public event EventHandler<DataEventArgs<string>> PositionSelected = delegate { };

        public PositionPieChartView()
        {
            InitializeComponent();
        }

        [Import]
        public IPositionPieChartViewModel Model
        {
            get
            {
                return this.DataContext as IPositionPieChartViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
