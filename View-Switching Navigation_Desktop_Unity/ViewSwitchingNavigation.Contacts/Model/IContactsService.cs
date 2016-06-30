using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewSwitchingNavigation.Contacts.Model
{
    public interface IContactsService
    {
        Task<IEnumerable<Contact>> GetContactsAsync();
    }
}
