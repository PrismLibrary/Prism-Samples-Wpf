

using System;
using System.Windows;
using System.Windows.Controls;
using Prism.Regions;

namespace ViewSwitchingNavigation.Contacts.Views
{
    [ViewSortHint("04")]
    public partial class ContactsAvatarNavigationItemView : UserControl
    {
        private const string mainContentRegionName = "MainContentRegion";

        // todo: 17a - ContactsView Avatar Option
        // This navigation uri provides additional query data to indicate the 'Avatar' view should be shown.
        private static Uri contactsAvatarsViewUri = new Uri("ContactsView?Show=Avatars", UriKind.Relative);

        private IRegionManager regionManager;

        public ContactsAvatarNavigationItemView(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            InitializeComponent();

            IRegion mainContentRegion = this.regionManager.Regions[mainContentRegionName];
            if (mainContentRegion != null && mainContentRegion.NavigationService != null)
            {
                mainContentRegion.NavigationService.Navigated += this.MainContentRegion_Navigated;
            }
        }

        public void MainContentRegion_Navigated(object sender, RegionNavigationEventArgs e)
        {
            this.UpdateNavigationButtonState(e.Uri);
        }

        private void UpdateNavigationButtonState(Uri uri)
        {
            this.NavigateToContactAvatarsRadioButton.IsChecked = (uri == contactsAvatarsViewUri);
        }

        private void NavigateToContactAvatarsRadioButton_Click(object sender, RoutedEventArgs e)
        {
            this.regionManager.RequestNavigate(mainContentRegionName, contactsAvatarsViewUri);
        }
    }
}
