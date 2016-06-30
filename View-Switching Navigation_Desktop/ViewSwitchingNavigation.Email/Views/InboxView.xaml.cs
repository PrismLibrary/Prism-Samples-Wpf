

using System.ComponentModel.Composition;
using System.Windows.Controls;
using ViewSwitchingNavigation.Email.ViewModels;

namespace ViewSwitchingNavigation.Email.Views
{
    [Export("InboxView")]
    public partial class InboxView : UserControl
    {
        public InboxView()
        {
            InitializeComponent();
        }

        [Import]
        public InboxViewModel ViewModel
        {
            get { return this.DataContext as InboxViewModel; }
            set { this.DataContext = value; }
        }
    }
}
