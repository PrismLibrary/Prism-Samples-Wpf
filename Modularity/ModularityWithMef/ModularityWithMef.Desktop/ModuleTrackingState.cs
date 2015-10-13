// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel;
using Prism.Modularity;

namespace ModularityWithMef.Desktop
{
    /// <summary>
    /// Provides tracking of a module's state for the quickstart.
    /// </summary>
    /// <remarks>
    /// This class is for demonstration purposes for the quickstart and not expected to be used in a real world application.
    /// </remarks>
    public class ModuleTrackingState : INotifyPropertyChanged
    {
        private string moduleName;
        private ModuleInitializationStatus moduleInitializationStatus;
        private DiscoveryMethod expectedDiscoveryMethod;
        private InitializationMode expectedInitializationMode;
        private DownloadTiming expectedDownloadTiming;
        private string configuredDependencies = "(none)";
        private long bytesReceived;
        private long totalBytesToReceive;

        /// <summary>
        /// Raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <remarks>
        /// This is a display string.
        /// </remarks>
        public string ModuleName
        {
            get
            {
                return this.moduleName;
            }

            set
            {
                if (this.moduleName != value)
                {
                    this.moduleName = value;
                    this.RaisePropertyChanged(PropertyNames.ModuleName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the current initialization status of the module.
        /// </summary>
        /// <value>A ModuleInitializationStatus value.</value>
        public ModuleInitializationStatus ModuleInitializationStatus
        {
            get
            {
                return this.moduleInitializationStatus;
            }

            set
            {
                if (this.moduleInitializationStatus != value)
                {
                    this.moduleInitializationStatus = value;
                    this.RaisePropertyChanged(PropertyNames.ModuleInitializationStatus);
                }
            }
        }

        /// <summary>
        /// Gets or sets how the module is expected to be discovered.
        /// </summary>
        /// <value>A DiscoveryMethod value.</value>
        /// <remarks>
        /// The actual discovery method is determined by the ModuleCatalog.
        /// </remarks>
        public DiscoveryMethod ExpectedDiscoveryMethod
        {
            get
            {
                return this.expectedDiscoveryMethod;
            }

            set
            {
                if (this.expectedDiscoveryMethod != value)
                {
                    this.expectedDiscoveryMethod = value;
                    this.RaisePropertyChanged(PropertyNames.ExpectedDiscoveryMethod);
                }
            }
        }

        /// <summary>
        /// Gets or sets how the module is expected to be initialized.
        /// </summary>
        /// <value>An InitializationModev value.</value>
        /// <remarks>
        /// The actual initialization mode is determiend by the ModuleCatalog.
        /// </remarks>
        public InitializationMode ExpectedInitializationMode
        {
            get
            {
                return this.expectedInitializationMode;
            }

            set
            {
                if (this.expectedInitializationMode != value)
                {
                    this.expectedInitializationMode = value;
                    this.RaisePropertyChanged(PropertyNames.ExpectedInitializationMode);
                }
            }
        }

        /// <summary>
        /// Gets or sets how the module is expected to be downloaded.
        /// </summary>
        /// <value>A DownloadTiming value.</value>
        /// <remarks>
        /// The actual download timing is determiend by the ModuleCatalog.
        /// </remarks>        
        public DownloadTiming ExpectedDownloadTiming
        {
            get
            {
                return this.expectedDownloadTiming;
            }

            set
            {
                if (this.expectedDownloadTiming != value)
                {
                    this.expectedDownloadTiming = value;
                    this.RaisePropertyChanged(PropertyNames.ExpectedDownloadTiming);
                }
            }
        }

        /// <summary>
        /// Gets or sets the list of modules the module depends on.
        /// </summary>
        /// <value>A string description of module dependencies.</value>
        /// <remarks>
        /// This is a display string.
        /// </remarks>
        public string ConfiguredDependencies
        {
            get
            {
                return this.configuredDependencies;
            }

            set
            {
                if (this.configuredDependencies != value)
                {
                    this.configuredDependencies = value;
                    this.RaisePropertyChanged(PropertyNames.ConfiguredDependencies);
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the number of bytes received as the module is loaded.
        /// </summary>
        /// <value>A number of bytes.</value>
        public long BytesReceived
        {
            get
            {
                return this.bytesReceived;
            }

            set
            {
                if (this.bytesReceived != value)
                {
                    this.bytesReceived = value;
                    this.RaisePropertyChanged(PropertyNames.BytesReceived);
                    this.RaisePropertyChanged(PropertyNames.DownloadProgressPercentage);
                }
            }
        }

        /// <summary>
        /// Gets or sets the total bytes to receive to load the module.
        /// </summary>
        /// <value>A number of bytes.</value>
        public long TotalBytesToReceive
        {
            get
            {
                return this.totalBytesToReceive;
            }

            set
            {
                if (this.totalBytesToReceive != value)
                {
                    this.totalBytesToReceive = value;
                    this.RaisePropertyChanged(PropertyNames.TotalBytesToReceive);
                    this.RaisePropertyChanged(PropertyNames.DownloadProgressPercentage);
                }
            }
        }

        /// <summary>
        /// Gets the percentage of BytesReceived/TotalByteToReceive.
        /// </summary>
        /// <value>A percentage number between 0 and 100.</value>
        public int DownloadProgressPercentage
        {
            get
            {
                if (this.bytesReceived < this.totalBytesToReceive)
                {
                    return (int)(this.bytesReceived * 100.0 / this.totalBytesToReceive);
                }
                else
                {
                    return 100;
                }
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Property names used with INotifyPropertyChanged.
        /// </summary>
        public static class PropertyNames
        {
            public const string ModuleName = "ModuleName";
            public const string ModuleInitializationStatus = "ModuleInitializationStatus";
            public const string ExpectedDiscoveryMethod = "ExpectedDiscoveryMethod";
            public const string ExpectedInitializationMode = "ExpectedInitializationMode";
            public const string ExpectedDownloadTiming = "ExpectedDownloadTiming";
            public const string ConfiguredDependencies = "ConfiguredDependencies";
            public const string BytesReceived = "BytesReceived";
            public const string TotalBytesToReceive = "TotalBytesToReceive";
            public const string DownloadProgressPercentage = "DownloadProgressPercentage";
        }
    }
}
