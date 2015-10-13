

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading;
using System.Windows.Data;
using System.Windows.Input;
using ViewSwitchingNavigation.Contacts.Model;
using ViewSwitchingNavigation.Infrastructure;

namespace ViewSwitchingNavigation.Contacts.ViewModels
{
    [Export]
    public class ContactsViewModel : BindableBase
    {
        private const string ComposeEmailViewName = "ComposeEmailView";
        private const string ToQueryItemName = "To";

        private readonly SynchronizationContext synchronizationContext = SynchronizationContext.Current ?? new SynchronizationContext();
        private readonly IRegionManager regionManager;
        private readonly ObservableCollection<Contact> contactsCollection;
        private readonly ICollectionView contactsView;
        private readonly DelegateCommand<object> emailContactCommand;

        [ImportingConstructor]
        public ContactsViewModel(IContactsService contactsService, IRegionManager regionManager)
        {
            this.emailContactCommand = new DelegateCommand<object>(this.EmailContact, this.CanEmailContact);

            this.contactsCollection = new ObservableCollection<Contact>();
            this.contactsView = new ListCollectionView(this.contactsCollection);
            this.contactsView.CurrentChanged += (s, e) => this.emailContactCommand.RaiseCanExecuteChanged();

            this.regionManager = regionManager;

            contactsService.BeginGetContacts((ar) =>
            {
                IEnumerable<Contact> newContacts = contactsService.EndGetContacts(ar);

                this.synchronizationContext.Post((state) =>
                {
                    foreach (var newContact in newContacts)
                    {
                        this.contactsCollection.Add(newContact);
                    }
                }, null);

            }, null);
        }

        public ICollectionView Contacts
        {
            get { return this.contactsView; }
        }

        public ICommand EmailContactCommand
        {
            get { return this.emailContactCommand; }
        }

        private void EmailContact(object ignored)
        {
            var contact = this.contactsView.CurrentItem as Contact;
            if (contact != null && !string.IsNullOrEmpty(contact.EmailAddress))
            {
                this.regionManager.RequestNavigate(RegionNames.MainContentRegion, ComposeEmailViewName + "?" + ToQueryItemName + "=" + contact.EmailAddress);
            }
        }

        private bool CanEmailContact(object ignored)
        {
            return this.contactsView.CurrentItem != null;
        }
    }
}
