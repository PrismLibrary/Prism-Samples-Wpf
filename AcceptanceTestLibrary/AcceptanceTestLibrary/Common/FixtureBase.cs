// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using System.Diagnostics;
using AcceptanceTestLibrary.ApplicationObserver;
using System.Text.RegularExpressions;
using AcceptanceTestLibrary.ApplicationHelper;
using System.IO;
using System.Security.Policy;

namespace AcceptanceTestLibrary.Common
{
    public class FixtureBase<TApp> 
        where TApp : AppLauncherBase, new()
    {
        TApp appLauncher = new TApp();

        private Process appProcess = null;

        public List<AutomationElement> LaunchApplication(string applicationPath, string processTitle)
        {
            return appLauncher.LaunchApp(applicationPath, processTitle);
        }

        public void UnloadApplication(Process p)
        {
            appLauncher.UnloadApp(p);
        }

        public void UnloadApplication()
        {
            appLauncher.UnloadApp();
           
        }
        #region Start - Stop Cassini Server
        /// <summary>
        /// Start the Cassini server from the given host path
        /// </summary>
        /// <param name="portNumber">Prt number of the server </param>
        /// <param name="hostPath">Host path to launch the server from.</param>
        public virtual void StartWebServer(string portNumber, string hostPath)
        {
            const string WEBSERVERPATH = @"\Common Files\microsoft shared\DevServer\10.0\webdev.webserver40.exe";

            appProcess = Process.Start(
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + WEBSERVERPATH,
                    "/port" + portNumber + " /path:\"" + hostPath + "\"");
        }

        public virtual void StopWebServer()
        {
            appProcess.Kill();
            appProcess = null;
        }

        
        #endregion
    }
}
