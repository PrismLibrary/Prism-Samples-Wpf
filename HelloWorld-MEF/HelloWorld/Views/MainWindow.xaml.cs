using System.ComponentModel.Composition;
using System.Windows;

namespace HelloWorld.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //TODO: 02a. Add the [Export] attribute to your shell window so MEF can
    //           inject it when necessary
    [Export(typeof(MainWindow))]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
