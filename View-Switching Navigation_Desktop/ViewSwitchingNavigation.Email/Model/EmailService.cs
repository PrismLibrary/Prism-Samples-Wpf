using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViewSwitchingNavigation.Infrastructure;

namespace ViewSwitchingNavigation.Email.Model
{
    [Export(typeof(IEmailService))]
    public class EmailService : IEmailService
    {
        private readonly List<EmailDocument> emailDocuments;

        public EmailService()
        {
            this.emailDocuments =
                new List<EmailDocument>
                {
                    new EmailDocument { From = "Christine Koch", To = "Robert Zare", Subject = "Important news", Text = "Text" },
                    new EmailDocument { From = "Christine Koch", To = "Robert Zare", Subject = "RSVP", Text = "Text" },

                    new EmailDocument(Guid.Parse("5C5BC399-F03F-4301-B314-2D70C1FF2306")) 
                        { From = "Christine Koch", To = "Robert Zare", Subject = "Marketing plan", Text = "Let's discuss the marketing plan." },
                    new EmailDocument(Guid.Parse("D84FF2F9-144C-4357-8DC7-785394FC99A6"))
                        { From = "Christine Koch", To = "Robert Zare", Subject = "Product brainstorming", Text = "Text" },
                    new EmailDocument(Guid.Parse("DE7C62F9-E15E-4C9D-8500-BD3210C529B8"))
                        { From = "Christine Koch", To = "Robert Zare", Subject = "Marketing plan", Text = "Let's discuss the marketing plan. Again." },
                    new EmailDocument(Guid.Parse("687E0458-A3B2-4688-8CEE-BD0E63A01C10"))
                        { From = "Christine Koch", To = "Robert Zare", Subject = "Planning meeting", Text = "This is the agenda..." },
                };
        }

        public EmailDocument GetEmailDocument(Guid id)
        {
            return this.emailDocuments.FirstOrDefault(e => e.Id == id);
        }

        public Task<IEnumerable<EmailDocument>> GetEmailDocumentsAsync()
        {
            return Task.FromResult(new ReadOnlyCollection<EmailDocument>(this.emailDocuments) as IEnumerable<EmailDocument>);
        }

        public Task SendEmailDocumentAsync(EmailDocument email)
        {
            return Task.Delay(500);
        }
    }
}
