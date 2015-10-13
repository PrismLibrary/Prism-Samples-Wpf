// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace ModularityWithMef.Desktop
{
    /// <summary>
    /// Describes when a module is downloaded.
    /// </summary>
    public enum DownloadTiming
    {
        /// <summary>
        /// The module is downloaded with the application.
        /// </summary>
        WithApplication,

        /// <summary>
        /// The module is downloaded in the background either after the application starts, or on-demand.
        /// </summary>
        InBackground
    }
}