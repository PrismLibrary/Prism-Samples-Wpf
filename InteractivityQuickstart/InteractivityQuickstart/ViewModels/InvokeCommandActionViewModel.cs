using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace InteractivityQuickstart.ViewModels
{
    public class InvokeCommandActionViewModel : BindableBase
    {
        private string selectedItemText;

        public InvokeCommandActionViewModel()
        {
            this.Items = new List<string>();

            this.Items.Add("Item1");
            this.Items.Add("Item2");
            this.Items.Add("Item3");
            this.Items.Add("Item4");
            this.Items.Add("Item5");

            // This command will be executed when the selection of the ListBox in the view changes.
            this.SelectedCommand = new DelegateCommand<object[]>(this.OnItemSelected);
        }

        public IList<string> Items { get; private set; }

        public ICommand SelectedCommand { get; private set; }

        public string SelectedItemText
        {
            get
            {
                return this.selectedItemText;
            }
            private set
            {
                this.SetProperty(ref this.selectedItemText, value);
            }
        }

        private void OnItemSelected(object[] obj)
        {
            if (obj != null && obj.Count() > 0)
            {
                this.SelectedItemText = obj.FirstOrDefault().ToString();
            }
        }
    }
}
