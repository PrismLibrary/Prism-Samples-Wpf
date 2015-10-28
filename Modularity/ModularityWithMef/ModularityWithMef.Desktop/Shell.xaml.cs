// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows;
using Prism.Logging;
using Prism.Modularity;
using ModuleTracking;

namespace ModularityWithMef.Desktop
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    [Export]
    public partial class Shell : Window, IPartImportsSatisfiedNotification
    {

#pragma warning disable 0649 // Imported by MEF
        // The shell imports IModuleTracker once to record updates as modules are downloaded.        
        [Import(AllowRecomposition = false)] private IModuleTracker moduleTracker;

        // The shell imports IModuleManager once to load modules on-demand.
        [Import(AllowRecomposition = false)] private IModuleManager moduleManager;

        // The shell imports the logger once to output logs to the UI.
        [Import(AllowRecomposition = false)] private CallbackLogger logger;
#pragma warning restore 0649 

        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Logs the specified message.  Called by the CallbackLogger.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="priority">The priority.</param>
        public void Log(string message, Category category, Priority priority)
        {
            this.TraceTextBox.AppendText(
                                            string.Format(
                                                        CultureInfo.CurrentUICulture, 
                                                        "[{0}][{1}] {2}\r\n", 
                                                        category,
                                                        priority, 
                                                        message));
        }

        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        public void OnImportsSatisfied()
        {
            // IPartImportsSatisfiedNotification is useful when you want to coordinate doing some work
            // with imported parts independent of when the UI is visible.

            // I use the IModuleTracker as the data-context for data-binding.
            // This quickstart only demonstrates modularity for Prism and does not use data-binding patterns such as MVVM.
            this.DataContext = this.moduleTracker;

            // I set this shell's Log method as the callback for receiving log messages.
            this.logger.Callback = this.Log;
            this.logger.ReplaySavedLogs();

            // I subscribe to events to help track module loading/loaded progress.
            // The ModuleManager uses the Async Events Pattern.
            this.moduleManager.LoadModuleCompleted += this.ModuleManager_LoadModuleCompleted;
            this.moduleManager.ModuleDownloadProgressChanged += this.ModuleManager_ModuleDownloadProgressChanged;
        }

        /// <summary>
        /// Handles the RequestModuleLoad event of the ModuleB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ModuleB_RequestModuleLoad(object sender, EventArgs e)
        {
            // The ModuleManager uses the Async Events Pattern.
            this.moduleManager.LoadModule(WellKnownModuleNames.ModuleB);
        }

        /// <summary>
        /// Handles the RequestModuleLoad event of the ModuleC control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ModuleC_RequestModuleLoad(object sender, EventArgs e)
        {
            // The ModuleManager uses the Async Events Pattern.
            this.moduleManager.LoadModule(WellKnownModuleNames.ModuleC);
        }

        /// <summary>
        /// Handles the RequestModuleLoad event of the ModuleE control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ModuleE_RequestModuleLoad(object sender, EventArgs e)
        {
            // The ModuleManager uses the Async Events Pattern.
            this.moduleManager.LoadModule(WellKnownModuleNames.ModuleE);
        }

        /// <summary>
        /// Handles the RequestModuleLoad event of the ModuleF control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ModuleF_RequestModuleLoad(object sender, EventArgs e)
        {
            // The ModuleManager uses the Async Events Pattern.
            this.moduleManager.LoadModule(WellKnownModuleNames.ModuleF);
        }

        /// <summary>
        /// Handles the LoadModuleProgressChanged event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Prism.Modularity.ModuleDownloadProgressChangedEventArgs"/> instance containing the event data.</param>
        private void ModuleManager_ModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
            this.moduleTracker.RecordModuleDownloading(e.ModuleInfo.ModuleName, e.BytesReceived, e.TotalBytesToReceive);
        }

        /// <summary>
        /// Handles the LoadModuleCompleted event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Prism.Modularity.LoadModuleCompletedEventArgs"/> instance containing the event data.</param>
        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            this.moduleTracker.RecordModuleLoaded(e.ModuleInfo.ModuleName);
        }
    }
}
