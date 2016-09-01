

using System.Windows.Controls;
using ViewSwitchingNavigation.Email.ViewModels;

namespace ViewSwitchingNavigation.Email.Views
{
    public partial class ComposeEmailView : UserControl
    {
        public ComposeEmailView(ComposeEmailViewModel viewModel)
        {
            InitializeComponent();

            this.ViewModel = viewModel;
        }

        public ComposeEmailViewModel ViewModel
        {
            set { this.DataContext = value; }
        }
    }
}
