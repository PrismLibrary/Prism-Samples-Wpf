

using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ViewSwitchingNavigation.Contacts.Views
{
    [Export("ContactAvatarView")]
    public partial class ContactAvatarView : UserControl
    {
        public ContactAvatarView()
        {
            InitializeComponent();
        }
    }
}
