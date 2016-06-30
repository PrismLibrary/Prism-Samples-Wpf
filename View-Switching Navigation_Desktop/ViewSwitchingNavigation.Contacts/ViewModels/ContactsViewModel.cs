using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ViewSwitchingNavigation.Contacts.Model;
using ViewSwitchingNavigation.Infrastructure;

namespace ViewSwitchingNavigation.Contacts.ViewModels
{
    [Export]
    public class ContactsViewModel : BindableBase
    {
        private const string ComposeEmailViewName = "ComposeEmailView";
        private const string ToQueryItemName = "To";

        private readonly IRegionManager regionManager;
        private readonly ObservableCollection<Contact> contactsCollection;
        private readonly ICollectionView contactsView;
        private readonly DelegateCommand<object> emailContactCommand;
        private readonly IContactsService contactsService;

        [ImportingConstructor]
        public ContactsViewModel(IContactsService contactsService, IRegionManager regionManager)
        {
            this.contactsService = contactsService;
            this.emailContactCommand = new DelegateCommand<object>(this.EmailContact, this.CanEmailContact);

            this.contactsCollection = new ObservableCollection<Contact>();
            this.contactsView = new ListCollectionView(this.contactsCollection);
            this.contactsView.CurrentChanged += (s, e) => this.emailContactCommand.RaiseCanExecuteChanged();

            this.regionManager = regionManager;
            var task = Initialize();
        }

        private async Task Initialize()
        {
            var contacts = await this.contactsService.GetContactsAsync();
            contacts.ToList().ForEach(c => this.contactsCollection.Add(c));
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
