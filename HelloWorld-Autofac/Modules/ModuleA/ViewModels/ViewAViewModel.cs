using Prism.Mvvm;

namespace ModuleA.ViewModels
{
    // TODO: 10.  Create a ViewModel using the standard naming convention so the
    //              AutoWireViewModel can find it.
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
