

using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewSwitchingNavigation.Contacts.Model;
using ViewSwitchingNavigation.Contacts.ViewModels;
using ViewSwitchingNavigation.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Threading.Tasks;

namespace ViewSwitchingNavigation.Contacts.Tests
{
    [TestClass]
    public class ContactsViewModelFixture
    {
        [TestMethod]
        public void WhenCreated_ThenRequestsContacts()
        {
            var contactsServiceMock = new Mock<IContactsService>();
            var regionManager = new RegionManager();

            var viewModel = new ContactsViewModel(contactsServiceMock.Object, regionManager);

            contactsServiceMock.Verify(svc => svc.GetContactsAsync());
            Assert.IsTrue(viewModel.Contacts.IsEmpty);
        }

        [TestMethod]
        public void WhenContactIsSelected_ThenEmailContactCommandIsEnabledAndNotifiesChange()
        {
            var contactsServiceMock = new Mock<IContactsService>();
            AsyncCallback callback = null;
            var resultMock = new Mock<IAsyncResult>();
            IEnumerable<Contact> contacts = new[] { new Contact { }, new Contact { } };
            contactsServiceMock
                .Setup(svc => svc.GetContactsAsync())
                .Returns(Task.FromResult(contacts));

            var regionManager = new RegionManager();

            var viewModel = new TestContactsViewModel(contactsServiceMock.Object, regionManager, contacts.ToArray());

            var notified = false;
            viewModel.EmailContactCommand.CanExecuteChanged += (s, o) => notified = true;
            Assert.IsFalse(viewModel.EmailContactCommand.CanExecute(null));
            viewModel.Contacts.MoveCurrentToFirst();
            Assert.IsTrue(viewModel.EmailContactCommand.CanExecute(null));
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void WhenSendingEmail_ThenNavigatesWithAToQueryParameter()
        {
            var contactsServiceMock = new Mock<IContactsService>();
            AsyncCallback callback = null;
            var resultMock = new Mock<IAsyncResult>();
            var contacts = new[] { new Contact { EmailAddress = "email" }, new Contact { } };
            contactsServiceMock
                .Setup(svc => svc.GetContactsAsync())
                .Returns(Task.FromResult(contacts.AsEnumerable()));

            Mock<IRegionManager> regionManagerMock = new Mock<IRegionManager>();
            regionManagerMock.Setup(x => x.RequestNavigate(RegionNames.MainContentRegion, @"ComposeEmailView?To=email"))
                .Verifiable();

            IRegionManager regionManager = regionManagerMock.Object;

            var viewModel = new TestContactsViewModel(contactsServiceMock.Object, regionManager, contacts);

            viewModel.Contacts.MoveCurrentToFirst();

            viewModel.EmailContactCommand.Execute(null);

            regionManagerMock.VerifyAll();
        }
    }

    class TestContactsViewModel : ContactsViewModel
    {
        public TestContactsViewModel(IContactsService contactsService, IRegionManager regionManager, Contact[] contacts) : 
            base(contactsService, regionManager)
        {
            var viewCollection = this.Contacts as ListCollectionView;

            foreach(var contact in contacts)
            {
                viewCollection.AddNewItem(contact);
            }

            viewCollection.MoveCurrentTo(null);
        }
    }
}
