// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using System.Diagnostics;

namespace AcceptanceTestLibrary.Common
{
    public abstract class AppLauncherBase
    {
        public abstract List<AutomationElement> LaunchApp(string applicationPath, string processTitle);
        public abstract void UnloadApp(Process p);
        public abstract void UnloadApp();
    }
}
