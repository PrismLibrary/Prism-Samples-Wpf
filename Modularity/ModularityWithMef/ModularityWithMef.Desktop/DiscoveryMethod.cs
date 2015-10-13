// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace ModularityWithMef.Desktop
{
    /// <summary>
    /// Describes how a module is discovered.
    /// </summary>
    public enum DiscoveryMethod
    {
        /// <summary>
        /// The module is directly referenced by the application.
        /// </summary>
        ApplicationReference,

        /// <summary>
        /// The module is listed in a XAML manifest file.
        /// </summary>
        XamlManifest,

        /// <summary>
        /// The module is listed in a configuration file.
        /// </summary>
        ConfigurationManifest,

        /// <summary>
        /// The module is discovered by examining the assemblies in a directory.
        /// </summary>
        DirectorySweep
    }
}