

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Linq;
using Prism.Regions;
using ViewSwitchingNavigation.Contacts.ViewModels;

namespace ViewSwitchingNavigation.Contacts.Views
{
    [Export("ContactsView")]
    public partial class ContactsView : UserControl, INavigationAware
    {
        private const string ContactAvatarViewName = "ContactAvatarView";
        private const string ContactDetailViewName = "ContactDetailView";
        private const string ShowParameterName = "Show";
        private const string AvatarsValue = "Avatars";

        public ContactsView()
        {
            InitializeComponent();
        }

        [Import]
        public IRegionManager regionManager;

        [Import]
        public ContactsViewModel ViewModel
        {
            get
            {
                return (ContactsViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            // todo: 18 - Navigating an inner region based on context
            // The ContactsView will navigate an inner region based on the
            // value of the 'Show' query parameter.  If the value is 'Avatars'
            // we will navigate to the avatar view otherwise we'll use the details view.

            NavigationParameters parameters = navigationContext.Parameters;
            if (parameters != null && string.Equals(parameters[ShowParameterName].ToString(), AvatarsValue))
            {
                regionManager.RequestNavigate(ContactsRegionNames.ContactDetailsRegion, new Uri(ContactAvatarViewName, UriKind.Relative));
            }
            else
            {
                regionManager.RequestNavigate(ContactsRegionNames.ContactDetailsRegion, new Uri(ContactDetailViewName, UriKind.Relative));
            }
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            // This view will handle all navigation view requests for ContactsView, so we always return true.
            return true;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            // Intentionally not implemented
        }
    }
}
