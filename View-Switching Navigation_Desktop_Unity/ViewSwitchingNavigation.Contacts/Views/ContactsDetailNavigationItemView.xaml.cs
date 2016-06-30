

using System;
using System.Windows;
using System.Windows.Controls;
using Prism.Regions;

namespace ViewSwitchingNavigation.Contacts.Views
{
    [ViewSortHint("03")]
    public partial class ContactsDetailNavigationItemView : UserControl
    {
        private const string mainContentRegionName = "MainContentRegion";

        // todo: 17b - ContactsView Details Option
        // This naigation uri provides additional query data to indicate the 'Details' view should be shown.
        private static Uri contactsDetailsViewUri = new Uri("ContactsView?Show=Details", UriKind.Relative);

        private IRegionManager regionManager;

        public ContactsDetailNavigationItemView(IRegionManager regionManager)
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
            this.NavigateToContactDetailsRadioButton.IsChecked = (uri == contactsDetailsViewUri);
        }

        private void NavigateToContactDetailsRadioButton_Click(object sender, RoutedEventArgs e)
        {
            this.regionManager.RequestNavigate(mainContentRegionName, contactsDetailsViewUri);
        }
    }
}
