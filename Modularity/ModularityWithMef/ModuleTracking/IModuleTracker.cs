// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace ModuleTracking
{
    /// <summary>
    /// Provides ability for modules to inform the quickstart of their state.
    /// </summary>
    public interface IModuleTracker
    {
        /// <summary>
        /// Records the module is loading.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        /// <param name="bytesReceived">The number of bytes downloaded.</param>
        void RecordModuleDownloading(string moduleName, long bytesReceived, long totalBytesToReceive);

        /// <summary>
        /// Records the module has been loaded.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        void RecordModuleLoaded(string moduleName);

        /// <summary>
        /// Records the module has been constructed.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        void RecordModuleConstructed(string moduleName);

        /// <summary>
        /// Records the module has been initialized.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        void RecordModuleInitialized(string moduleName);
    }
}