

using System.Windows.Controls;
using ViewSwitchingNavigation.Email.ViewModels;

namespace ViewSwitchingNavigation.Email.Views
{
    public partial class EmailView : UserControl
    {
        public EmailView(EmailViewModel viewModel)
        {
            InitializeComponent();

            this.ViewModel = viewModel;
        }

        public EmailViewModel ViewModel
        {
            set { this.DataContext = value; }
        }
    }
}
