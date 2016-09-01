// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using AcceptanceTestLibrary.ApplicationHelper;
using AcceptanceTestLibrary.Common;

namespace AcceptanceTestLibrary.TestEntityBase
{
    public abstract class PageBase<TApp>
        where TApp : AppLauncherBase, new()
    {
        private static AutomationElement window;

        public static AutomationElement Window
        {
            get
            {
                if (window == null)
                {
                    throw new InvalidOperationException("Window object not set exception");
                }

                return window;
            }
            set
            {
                window = value;
            }
        }

        public static AutomationElement FindControlByAutomationId(string control)
        {
            AutomationElement aeControl;

            //find control using AutomationElement of window 
            aeControl = window.GetHandleById<TApp>(control);
            return aeControl;
        }

        public static AutomationElementCollection FindControlByType(string control)
        {
            AutomationElementCollection aeControl;

            //find control using AutomationElement of window 
            aeControl = window.GetHandleByControlType<TApp>(control);
            return aeControl;
        }

        public static AutomationElementCollection FindAllControlsByAutomationId(string control)
        {
            AutomationElementCollection aeControl;

            //find control using AutomationElement of window 
            aeControl = window.GetAllHandlesById<TApp>(control);
            return aeControl;
        }

        public static AutomationElement FindControlByContent(string content)
        {
            AutomationElement aeControl;

            //find control using AutomationElement of window 
            aeControl = window.GetHandleByContent<TApp>(content);
            return aeControl;
        }

        public static AutomationElementCollection FindAllControlsByContent(string content)
        {
            AutomationElementCollection aeControl;

            //find control using AutomationElement of window 
            aeControl = window.GetAllHandlesByContent<TApp>(content);
            return aeControl;
        }

        public static AutomationElementCollection FindControlsByParent(AutomationElement parent)
        {
            AutomationElementCollection aeControlCollection;

            //find control using AutomationElement of window 
            aeControlCollection = window.GetHandleByParent<TApp>(parent);
            return aeControlCollection;
        }
        /// <summary>
        /// This method disposes the static variable window.
        /// </summary>
        public static void DisposeWindow()
        {
            window = null;
        }
    }
}
