

using System.ComponentModel.Composition;
using System.Windows.Controls;
using ViewSwitchingNavigation.Email.ViewModels;

namespace ViewSwitchingNavigation.Email.Views
{
    [Export("EmailView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class EmailView : UserControl
    {
        public EmailView()
        {
            InitializeComponent();
        }

        [Import]
        public EmailViewModel ViewModel
        {
            set { this.DataContext = value; }
        }
    }
}
