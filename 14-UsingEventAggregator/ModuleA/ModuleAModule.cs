namespace ModuleA
{
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;

    using Views;

    /// <summary>
    ///     Class ModuleAModule.
    ///     Implements the <see cref="IModule" />
    /// </summary>
    /// <seealso cref="IModule" />
    public class ModuleAModule : IModule
    {
        /// <summary>
        ///     Notifies the module that it has be initialized.
        /// </summary>
        /// <param name="containerProvider">The container provider.</param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("LeftRegion", typeof(MessageView));
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