

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Prism.Common;
using Prism.Commands;
using Prism.Regions;
using ViewSwitchingNavigation.Calendar.Model;
using ViewSwitchingNavigation.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace ViewSwitchingNavigation.Calendar.ViewModels
{
    [Export]
    public class CalendarViewModel : BindableBase
    {
        private readonly DelegateCommand<Meeting> openMeetingEmailCommand;
        private readonly IRegionManager regionManager;
        private readonly ICalendarService calendarService;

        private const string GuidNumericFormatSpecifier = "N";
        private const string EmailViewName = "EmailView";
        private const string EmailIdName = "EmailId";

        private ObservableCollection<Meeting> meetings;

        [ImportingConstructor]
        public CalendarViewModel(ICalendarService calendarService, IRegionManager regionManager)
        {
            this.openMeetingEmailCommand = new DelegateCommand<Meeting>(this.OpenMeetingEmail);

            this.calendarService = calendarService;
            this.regionManager = regionManager;
            var task = this.LoadMeetings();
        }

        public ObservableCollection<Meeting> Meetings
        {
            get { return meetings; }
            set { SetProperty(ref meetings, value); }
        }

        public ICommand OpenMeetingEmailCommand
        {
            get { return this.openMeetingEmailCommand; }
        }

        private async Task LoadMeetings()
        {
            this.meetings = new ObservableCollection<Meeting>(await this.calendarService.GetMeetingsAsync());
        }

        private void OpenMeetingEmail(Meeting meeting)
        {
            // todo: 12 - Opening an email
            //
            // This view initiates navigation using the RegionManager.
            // The RegionManager will find the region and delegate the
            // navigation request to the region specified.
            //
            // This navigation request also includes additional navigation context, an 'EmailId', to
            // allow the Email view to orient to the right item.
            var parameters = new NavigationParameters();
            parameters.Add(EmailIdName, meeting.EmailId.ToString(GuidNumericFormatSpecifier));

            this.regionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(EmailViewName + parameters, UriKind.Relative));
        }
    }
}
