// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Microsoft.Practices.Unity;

using Commanding.Modules.Order.Views;
using Commanding.Modules.Order.Services;
using Prism.Modularity;
using Prism.Regions;

namespace Commanding.Modules.Order
{
    public class OrderModule : IModule
    {
        private readonly IRegionManager  regionManager;
        private readonly IUnityContainer container;

        public OrderModule( IUnityContainer container, IRegionManager regionManager )
        {
            this.container     = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            this.container.RegisterType<IOrdersRepository, OrdersRepository>(new ContainerControlledLifetimeManager());

            // Show the Orders Editor view in the shell's main region.
            this.regionManager.RegisterViewWithRegion( "MainRegion",
                                                       () => this.container.Resolve<OrdersEditorView>() );

            // Show the Orders Toolbar view in the shell's toolbar region.
            this.regionManager.RegisterViewWithRegion( "GlobalCommandsRegion",
                                                       () => this.container.Resolve<OrdersToolBar>() );
        }
    }
}

