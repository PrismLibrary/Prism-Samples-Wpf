

using System;

namespace ViewSwitchingNavigation.Email.Model
{
    public class EmailDocument
    {
        public EmailDocument()
            : this(Guid.NewGuid())
        {
        }

        public EmailDocument(Guid id)
        {
            this.Id = id;
        }

        public string From { get; set;}

        public string To { get; set;}

        public string Subject { get; set;}

        public string Text { get; set;}

        public Guid Id { get; private set;}
    }
}
