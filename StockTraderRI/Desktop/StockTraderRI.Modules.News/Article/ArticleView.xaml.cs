

using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using StockTraderRI.Infrastructure;
using StockTraderRI.Modules.News.Controllers;

namespace StockTraderRI.Modules.News.Article
{
    /// <summary>
    /// Interaction logic for ArticleView.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.ResearchRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ArticleView : UserControl
    {
        // Note - this import is here so that the controller is created and gets wired to the article and news reader
        // view models, which are shared instances
        [Import]
#pragma warning disable 169
        private INewsController newsController;
#pragma warning restore 169

        public ArticleView()
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
        ArticleViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.NewsList.SelectedItem != null)
            {
                var storyboard = (Storyboard)this.Resources["Details"];
                storyboard.Begin();
            }
            else
            {
                var storyboard = (Storyboard)this.Resources["List"];
                storyboard.Begin();
            }
        }
    }
}
