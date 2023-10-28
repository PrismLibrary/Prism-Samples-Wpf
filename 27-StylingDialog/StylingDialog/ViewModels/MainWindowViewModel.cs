using Prism.Commands;
using Prism.Mvvm;
using Prism.Dialogs;

namespace StylingDialog.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private DelegateCommand _showDialogCommand;
        public DelegateCommand ShowDialogCommand =>
            _showDialogCommand ?? (_showDialogCommand = new DelegateCommand(ShowDialog));

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private IDialogService _dialogService;

        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        private void ShowDialog()
        {
            var message = "This is a message that should be shown in the dialog.";
            //using the dialog service as-is
            _dialogService.ShowDialog("NotificationDialog", new DialogParameters($"message={message}"), r =>
            {
                if (r.Result == ButtonResult.None)
                    Title = "Result is None";
                else if (r.Result == ButtonResult.OK)
                    Title = "Result is OK";
                else if (r.Result == ButtonResult.Cancel)
                    Title = "Result is Cancel";
                else
                    Title = "I Don't know what you did!?";
            });
        }
    }
}
