namespace ModuleB
{
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;

    using Views;

    /// <summary>
    ///     Class ModuleBModule.
    ///     Implements the <see cref="IModule" />
    /// </summary>
    /// <seealso cref="IModule" />
    public class ModuleBModule : IModule
    {
        /// <summary>
        ///     Notifies the module that it has be initialized.
        /// </summary>
        /// <param name="containerProvider">The container provider.</param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("RightRegion", typeof(MessageList));
        }

        /// <summary>
        ///     Used to register types with the container that will be used by your application.
        /// </summary>
        /// <param name="containerRegistry">The container registry.</param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}