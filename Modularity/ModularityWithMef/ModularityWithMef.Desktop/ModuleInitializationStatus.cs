// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace ModularityWithMef.Desktop
{
    /// <summary>
    /// The current status of the module used by ModuleTrackingState.
    /// </summary>
    public enum ModuleInitializationStatus
    {
        /// <summary>
        /// The module is in its initial state.
        /// </summary>
        NotStarted,

        /// <summary>
        /// The module is in the process of being downloaded.
        /// </summary>
        Downloading,

        /// <summary>
        /// The module has been downloaded.
        /// </summary>
        Downloaded,

        /// <summary>
        /// The module has been constructed.
        /// </summary>
        Constructed,

        /// <summary>
        /// The module has been initialized.
        /// </summary>
        Initialized,
    }
}