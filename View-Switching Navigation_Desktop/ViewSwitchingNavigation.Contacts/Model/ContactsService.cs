using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace ViewSwitchingNavigation.Contacts.Model
{
    [Export(typeof(IContactsService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ContactsService : IContactsService
    {
        private const string Avatar1Uri = @"/ViewSwitchingNavigation.Contacts;component/Avatars/MC900432625.PNG";
        private const string Avatar2Uri = @"/ViewSwitchingNavigation.Contacts;component/Avatars/MC900433938.PNG";
        private const string Avatar3Uri = @"/ViewSwitchingNavigation.Contacts;component/Avatars/MC900433946.PNG";
        private const string Avatar4Uri = @"/ViewSwitchingNavigation.Contacts;component/Avatars/MC900434899.PNG";

        private static IEnumerable<Contact> CreateContacts()
        {
            List<Contact> contacts = new List<Contact>();
            contacts.Add(new Contact() { Name = "Kim Abercrombie", Company = "Contoso", EmailAddress = "jka@contoso.com", Address = "45 North St.", AvatarUri = Avatar3Uri });
            contacts.Add(new Contact() { Name = "Thomas Hamborg", Company = "TailSpin", EmailAddress = "th@tailspintoys.com", Address = "123 Main Street", AvatarUri = Avatar1Uri });
            contacts.Add(new Contact() { Name = "Mark Harrington", Company = "Fabrikam", EmailAddress = "mh@fabrikam.com", Address = "789 West Place", AvatarUri = Avatar4Uri });
            contacts.Add(new Contact() { Name = "Grigory Pogulsky", Company = "TailSpin", EmailAddress = "gp@tailspintoys.com", Address = "345 Regus Ave", AvatarUri = Avatar2Uri });
            return contacts;
        }

        public Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return Task.FromResult(CreateContacts());
        }
    }
}
