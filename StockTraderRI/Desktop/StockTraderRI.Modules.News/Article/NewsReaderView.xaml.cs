

using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using StockTraderRI.Infrastructure;

namespace StockTraderRI.Modules.News.Article
{
    /// <summary>
    /// Interaction logic for NewsReader.xaml
    /// </summary>
    [ViewExport("NewsReaderView")]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class NewsReaderView : UserControl
    {
        public NewsReaderView()
        {
            InitializeComponent();
        }

        public static string Title
        {
            get
            {
                return Properties.Resources.NewsReaderViewTitle;
            }
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
        NewsReaderViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }
    }
}
