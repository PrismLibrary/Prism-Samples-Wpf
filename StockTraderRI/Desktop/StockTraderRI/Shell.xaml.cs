

using System.Windows;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace StockTraderRI
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>   
    [Export]
    public partial class Shell : Window
    {
        public Shell()
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
        ShellViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }        
    }
}
