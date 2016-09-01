using System;
using Prism.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewSwitchingNavigation.Calendar.Model;
using ViewSwitchingNavigation.Calendar.ViewModels;
using ViewSwitchingNavigation.Infrastructure;

namespace ViewSwitchingNavigation.Calendar.Tests
{
    [TestClass]
    public class CalendarViewModelFixture
    {
        [TestMethod]
        public void WhenCreated_ThenRequestsMeetingsToService()
        {
            var calendarServiceMock = new Mock<ICalendarService>();
            var requested = false;
            calendarServiceMock
                .Setup(svc => svc.GetMeetingsAsync())
                .Callback(() => requested = true);

            var viewModel = new CalendarViewModel(calendarServiceMock.Object, new Mock<IRegionManager>().Object);

            Assert.IsTrue(requested);
        }

        [TestMethod]
        public void WhenExecutingTheGoToEmailCommand_ThenNavigatesToEmailView()
        {
            var meeting = new Meeting { EmailId = Guid.NewGuid() };

            var calendarServiceMock = new Mock<ICalendarService>();

            Mock<IRegionManager> regionManagerMock = new Mock<IRegionManager>();
            regionManagerMock.Setup(x => x.RequestNavigate(RegionNames.MainContentRegion, new Uri(@"EmailView?EmailId=" + meeting.EmailId.ToString("N"), UriKind.Relative))).Verifiable();

            var viewModel = new CalendarViewModel(calendarServiceMock.Object, regionManagerMock.Object);

            viewModel.OpenMeetingEmailCommand.Execute(meeting);

            regionManagerMock.VerifyAll();
        }
    }
}
