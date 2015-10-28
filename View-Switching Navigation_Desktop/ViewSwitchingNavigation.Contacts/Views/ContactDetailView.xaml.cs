

using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ViewSwitchingNavigation.Contacts.Views
{
    [Export("ContactDetailView")]
    public partial class ContactDetailView : UserControl
    {
        public ContactDetailView()
        {
            InitializeComponent();
        }
    }
}
