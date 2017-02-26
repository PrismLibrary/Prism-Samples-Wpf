using Prism.Mvvm;

namespace ViewModelLocator.ViewModels
{
    public class CustomViewModel : BindableBase
    {
        private string _title = "Custom ViewModel Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public CustomViewModel()
        {

        }
    }
}
