using Prism.Ioc;
using Prism.Regions;
using System.Windows;

namespace ViewInjection.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IContainerExtension _container;
        IRegionManager _regionManager;

        public MainWindow(IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();
            _container = container;
            _regionManager = regionManager;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var view = _container.Resolve<ViewA>();
            IRegion region = _regionManager.Regions["ContentRegion"];
            region.Add(view);
        }
    }
}
