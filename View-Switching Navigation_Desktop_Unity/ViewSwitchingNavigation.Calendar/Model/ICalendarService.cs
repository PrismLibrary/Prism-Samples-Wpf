using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewSwitchingNavigation.Calendar.Model
{
    public interface ICalendarService
    {
        Task<IEnumerable<Meeting>> GetMeetingsAsync();
    }
}
