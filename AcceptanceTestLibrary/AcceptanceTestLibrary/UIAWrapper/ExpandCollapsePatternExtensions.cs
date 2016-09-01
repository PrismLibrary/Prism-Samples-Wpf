// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace AcceptanceTestLibrary.UIAWrapper
{
    public static class ExpandCollapsePatternExtensions
    {
        public static void Expand(this AutomationElement expandControl)
        {
            //check if the buttonControl is indeed a handle to a button-based control
            ExpandCollapsePattern expColPattern = ValidateExpandCollapseControl(expandControl);

            //expand
            expColPattern.Expand();
        }

        public static void Collapse(this AutomationElement expandControl)
        {
            //check if the buttonControl is indeed a handle to a button-based control
            ExpandCollapsePattern expColPattern = ValidateExpandCollapseControl(expandControl);

            //Collapse
            expColPattern.Collapse();
        }

        private static ExpandCollapsePattern ValidateExpandCollapseControl(AutomationElement element)
        {
            object expColPattern = null;
            bool isValid = element.TryGetCurrentPattern(ExpandCollapsePattern.Pattern, out expColPattern);

            if (isValid)
            {
                return (ExpandCollapsePattern)expColPattern;
            }
            else
            {
                throw new InvalidOperationException("Invalid operation");
            }
        }
    }
}
