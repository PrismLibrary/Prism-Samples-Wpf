

namespace ModularityWithUnity.Desktop
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