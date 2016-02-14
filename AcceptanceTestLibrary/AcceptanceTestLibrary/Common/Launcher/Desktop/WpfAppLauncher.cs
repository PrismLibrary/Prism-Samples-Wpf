// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Automation;
using System.Threading;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.ApplicationHelper;
using AcceptanceTestLibrary.ApplicationObserver;
using System.IO;

namespace AcceptanceTestLibrary.Common.Desktop
{
    public class WpfAppLauncher : AppLauncherBase, IStateObserver
    {
        private static Process targetProcess;

        // Time in milliseconds to wait for the application to start.
        static int MAXTIME = 5000;
        // Time in milliseconds to wait before trying to find the application.
        static int TIMEWAIT = 100; 


        public override List<AutomationElement> LaunchApp(string applicationName, string processName)
        {
            ApplicationProcessName = processName;
            StateDiagnosis.Instance.StartDiagnosis(this);
            List<AutomationElement> aeList = new List<AutomationElement>();

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(applicationName);
                startInfo.WorkingDirectory = Path.GetDirectoryName(applicationName);
                startInfo.UseShellExecute = false;

                targetProcess = Process.Start(startInfo);
                int runningTime = 0;
                while (targetProcess.MainWindowHandle.Equals(IntPtr.Zero))
                {
                    if (runningTime > MAXTIME)
                        throw new Exception("Could not find " + applicationName);

                    Thread.Sleep(TIMEWAIT);
                    runningTime += TIMEWAIT;

                    targetProcess.Refresh();
                }


                AutomationElement aeWindow = AutomationElement.FromHandle(targetProcess.MainWindowHandle);
                StateDiagnosis.Instance.StopDiagnosis(this);
                aeList.Add(aeWindow);
                return aeList;
            }
            catch (Exception)
            {
                UnloadApp(targetProcess);
                return null;
            }
        }

        public static Process GetCurrentAppProcess()
        {
            return Process.GetProcesses().First<Process>(proc => proc.ProcessName.Equals(ApplicationProcessName));
        }

        public static string ApplicationProcessName
        { get; set; }

        public override void UnloadApp(Process p)
        {
            p.Kill();
            p.Dispose();
        }

        public override void UnloadApp()
        {
            targetProcess.Kill();
            targetProcess.Dispose();
        }

        #region IStateObserver Members

        public void Notify()
        {
            UnloadApp(GetCurrentAppProcess());
        }

        #endregion
    }
}
