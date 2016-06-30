

using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Prism.Regions;
using ViewSwitchingNavigation.Infrastructure;

namespace ViewSwitchingNavigation.Calendar.Views
{
    [Export]
    [ViewSortHint("02")]
    public partial class CalendarNavigationItemView : UserControl, IPartImportsSatisfiedNotification
    {
        private static Uri calendarViewUri = new Uri("CalendarView", UriKind.Relative);

        [Import]
        public IRegionManager regionManager;

        public CalendarNavigationItemView()
        {
            InitializeComponent();
        }

        void IPartImportsSatisfiedNotification.OnImportsSatisfied()
        {
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
