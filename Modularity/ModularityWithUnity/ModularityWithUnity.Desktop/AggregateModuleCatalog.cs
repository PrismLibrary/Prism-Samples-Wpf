

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Modularity;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ModularityWithUnity.Desktop
{
    /// <summary>
    /// A basic aggregation of IModuleCatalogs for quickstart purposes.
    /// </summary>
    public class AggregateModuleCatalog : IModuleCatalog
    {
        private List<IModuleCatalog> catalogs = new List<IModuleCatalog>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateModuleCatalog"/> class.
        /// </summary>
        public AggregateModuleCatalog()
        {
            this.catalogs.Add(new ModuleCatalog());
        }

        /// <summary>
        /// Gets the collection of catalogs.
        /// </summary>
        /// <value>A read-only collection of catalogs.</value>
        public ReadOnlyCollection<IModuleCatalog> Catalogs
        {
            get
            {
                return this.catalogs.AsReadOnly();
            }
        }

        /// <summary>
        /// Adds the catalog to the list of catalogs
        /// </summary>
        /// <param name="catalog">The catalog to add.</param>
        public void AddCatalog(IModuleCatalog catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException("catalog");
            }

            this.catalogs.Add(catalog);
        }


        /// <summary>
        /// Gets all the <see cref="ModuleInfo"/> classes that are in the <see cref="ModuleCatalog"/>.
        /// </summary>
        /// <value></value>
        public IEnumerable<ModuleInfo> Modules
        {
            get 
            {
                return this.Catalogs.SelectMany(x => x.Modules);
            }
        }

        /// <summary>
        /// Return the list of <see cref="ModuleInfo"/>s that <paramref name="moduleInfo"/> depends on.
        /// </summary>
        /// <param name="moduleInfo">The <see cref="ModuleInfo"/> to get the</param>
        /// <returns>
        /// An enumeration of <see cref="ModuleInfo"/> that <paramref name="moduleInfo"/> depends on.
        /// </returns>
        public IEnumerable<ModuleInfo> GetDependentModules(ModuleInfo moduleInfo)
        {
            var catalog = this.catalogs.Single(x => x.Modules.Contains(moduleInfo));
            return catalog.GetDependentModules(moduleInfo);
        }

        /// <summary>
        /// Returns the collection of <see cref="ModuleInfo"/>s that contain both the <see cref="ModuleInfo"/>s in
        /// <paramref name="modules"/>, but also all the modules they depend on.
        /// </summary>
        /// <param name="modules">The modules to get the dependencies for.</param>
        /// <returns>
        /// A collection of <see cref="ModuleInfo"/> that contains both all <see cref="ModuleInfo"/>s in <paramref name="modules"/>
        /// and also all the <see cref="ModuleInfo"/> they depend on.
        /// </returns>
        public IEnumerable<ModuleInfo> CompleteListWithDependencies(IEnumerable<ModuleInfo> modules)
        {
            var modulesGroupedByCatalog = modules.GroupBy<ModuleInfo, IModuleCatalog>(module => this.catalogs.Single(catalog => catalog.Modules.Contains(module)));
            return modulesGroupedByCatalog.SelectMany(x => x.Key.CompleteListWithDependencies(x));                      
        }

        /// <summary>
        /// Initializes the catalog, which may load and validate the modules.
        /// </summary>
        public void Initialize()
        {
            foreach (var catalog in this.Catalogs)
            {                
                catalog.Initialize();
            }
        }

        /// <summary>
        /// Adds a <see cref="ModuleInfo"/> to the <see cref="ModuleCatalog"/>.
        /// </summary>
        /// <param name="moduleInfo">The <see cref="ModuleInfo"/> to add.</param>
        public void AddModule(ModuleInfo moduleInfo)
        {
            this.catalogs[0].AddModule(moduleInfo);
        }
    }
}
