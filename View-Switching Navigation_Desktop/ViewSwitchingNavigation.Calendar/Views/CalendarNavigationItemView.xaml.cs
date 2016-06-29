

using System;
using System.Windows;
using System.Windows.Controls;
using Prism.Regions;
using ViewSwitchingNavigation.Infrastructure;

namespace ViewSwitchingNavigation.Calendar.Views
{
    [ViewSortHint("02")]
    public partial class CalendarNavigationItemView : UserControl
    {
        private static Uri calendarViewUri = new Uri("CalendarView", UriKind.Relative);

        private IRegionManager regionManager;

        public CalendarNavigationItemView(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            InitializeComponent();

            IRegion mainContentRegion = this.regionManager.Regions[RegionNames.MainContentRegion];
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
            this.NavigateToCalendarRadioButton.IsChecked = (uri == calendarViewUri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.regionManager.RequestNavigate(RegionNames.MainContentRegion, calendarViewUri);
        }
    }
}
