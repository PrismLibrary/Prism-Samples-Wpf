using System;
using ModuleA.Business;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ModuleA.ViewModels
{
    public class PersonDetailViewModel : BindableBase, INavigationAware
    {
        private Person _selectedPerson;
        IRegionNavigationJournal _journal;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetProperty(ref _selectedPerson, value); }
        }

        public DelegateCommand GoBackCommand { get; set; }

        public PersonDetailViewModel()
        {
            GoBackCommand = new DelegateCommand(GoBack);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;

            var person = navigationContext.Parameters["person"] as Person;
            if (person != null)
                SelectedPerson = person;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var person = navigationContext.Parameters["person"] as Person;
            if (person != null)
                return SelectedPerson != null && SelectedPerson.LastName == person.LastName;
            else
                return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        private void GoBack()
        {
            _journal.GoBack();
        }
    }
}
