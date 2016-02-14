// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace AcceptanceTestLibrary.UIAWrapper
{
    public static class TogglePatternExtensions
    {
        public static void Toggle(this AutomationElement Control)
        {
            //check if the buttonControl is indeed a handle to a button-based control
            TogglePattern togglePattern = ValidateControlForTogglePattern(Control);

            //click the button control
            togglePattern.Toggle();

        }

        private static TogglePattern ValidateControlForTogglePattern(AutomationElement element)
        {
            object togglePattern = null;
            bool isValid = element.TryGetCurrentPattern(TogglePattern.Pattern, out togglePattern);

            if (isValid)
            {
                return (TogglePattern)togglePattern;
            }
            else
            {
                throw new InvalidOperationException("Invalid operation");
            }
        }
    }
}
