// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace EventAggregation
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            Shell shell = Container.Resolve<Shell>();
            shell.Show();

            return shell;
        }

        protected override void InitializeModules()
        {
            IModule moduleA = Container.Resolve<ModuleA.ModuleA>();
            IModule moduleB = Container.Resolve<ModuleB.ModuleB>();

            moduleA.Initialize();
            moduleB.Initialize();
        }
    }
}
