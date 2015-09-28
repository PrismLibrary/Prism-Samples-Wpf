using Prism.Mvvm;
using System.ComponentModel.Composition;

namespace ModuleA.ViewModels
{
    // TODO: 10.  Create a ViewModel using the standard naming convention so the
    //              AutoWireViewModel can find it.  Must be identified with the [Export]
    //              attribute so MEF can inject it when needed.
    [Export(typeof(ViewAViewModel))]
    public class ViewAViewModel : BindableBase
    {
        //  Add the appropriate properties/methods to implement your model
        private string _title = "Hello World";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        } 
    }
    // TODO: 11.  That's it.  Run the application to see your handywork in action!
}
