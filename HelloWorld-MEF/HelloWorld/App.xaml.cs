using System.Windows;

namespace HelloWorld
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //TODO: 04a. Override the OnStartup method on the WPF Application class.
        //            instantiate the bootstrapper and call the run method.
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Bootstrapper bs = new Bootstrapper();
            bs.Run();
        }
    }
}
