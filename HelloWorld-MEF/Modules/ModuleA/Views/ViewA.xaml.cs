using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ModuleA.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    // TODO: 09a. Create a WPF User control for your view.
    //             Must be identified with the [Export]
    //              attribute so MEF can inject it when needed.
    [Export(typeof(ViewA))]
    public partial class ViewA : UserControl
    {
        public ViewA()
        {
            InitializeComponent();
        }
    }
}
