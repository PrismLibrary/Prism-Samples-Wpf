using System.Text;
using System.Windows;
using Prism.Ioc;
using Serilog;
using LoggingWithSerilog.Views;

namespace LoggingWithSerilog
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(path: "DemoLog.txt", encoding: Encoding.UTF8)
                .CreateLogger();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.CloseAndFlush();

            base.OnExit(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSerilog();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
