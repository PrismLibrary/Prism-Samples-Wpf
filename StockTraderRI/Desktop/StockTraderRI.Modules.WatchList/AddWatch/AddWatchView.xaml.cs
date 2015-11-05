

using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using StockTraderRI.Infrastructure;

namespace StockTraderRI.Modules.Watch.AddWatch
{
    /// <summary>
    /// Interaction logic for AddWatchControl.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainToolBarRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AddWatchView : UserControl
    {
        public AddWatchView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the ViewModel.
        /// </summary>
        /// <remarks>
        /// This set-only property is annotated with the <see cref="ImportAttribute"/> so it is injected by MEF with
        /// the appropriate view model.
        /// </remarks>
        [Import]
        [SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly", Justification = "Needs to be a property to be composed by MEF")]
        AddWatchViewModel ViewModel
        {          
            set
            {
                this.DataContext = value;
            }
        }
    }
}
