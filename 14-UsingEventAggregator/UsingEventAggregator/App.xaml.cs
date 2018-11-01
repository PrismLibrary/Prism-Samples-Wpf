namespace UsingEventAggregator
{
    using System.Windows;

    using ModuleA;

    using ModuleB;

    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Unity;

    using Views;

    /// <summary>
    ///     Interaction logic for App.xaml
    ///     Class App.
    ///     Implements the <see cref="PrismApplication" />
    /// </summary>
    /// <seealso cref="PrismApplication" />
    public partial class App : PrismApplication
    {
        /// <summary>
        ///     Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        ///     Used to register types with the container that will be used by your application.
        /// </summary>
        /// <param name="containerRegistry">The container registry.</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        /// <summary>
        ///     Configures the <see cref="T:Prism.Modularity.IModuleCatalog" /> used by Prism.
        /// </summary>
        /// <param name="moduleCatalog">The module catalog.</param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleAModule>();
            moduleCatalog.AddModule<ModuleBModule>();
        }
    }
}