using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewSwitchingNavigation.Email.Model
{
    public interface IEmailService
    {
        Task<IEnumerable<EmailDocument>> GetEmailDocumentsAsync();
        Task SendEmailDocumentAsync(EmailDocument email);
        EmailDocument GetEmailDocument(Guid id);
    }
}
