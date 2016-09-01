

using System;

namespace ViewSwitchingNavigation.Calendar.Model
{
    public class Meeting
    {
        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public Guid EmailId { get; set; }
    }
}
