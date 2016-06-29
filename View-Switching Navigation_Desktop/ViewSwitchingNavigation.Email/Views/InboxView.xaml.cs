

using System.Windows.Controls;
using ViewSwitchingNavigation.Email.ViewModels;

namespace ViewSwitchingNavigation.Email.Views
{
    public partial class InboxView : UserControl
    {
        public InboxView(InboxViewModel viewModel)
        {
            InitializeComponent();

            this.ViewModel = viewModel;
        }

        public InboxViewModel ViewModel
        {
            get { return this.DataContext as InboxViewModel; }
            set { this.DataContext = value; }
        }
    }
}
