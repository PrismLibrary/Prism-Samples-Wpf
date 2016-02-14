// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace AcceptanceTestLibrary.UIAWrapper
{
    public static class InvokePatternExtensions
    {
        public static void Click(this AutomationElement Control)
        {
                //check if the buttonControl is indeed a handle to a button-based control
                InvokePattern invPattern = ValidateButtonControl(Control);

                //click the button control
                invPattern.Invoke();                 
         
        }

        private static InvokePattern ValidateButtonControl(AutomationElement element)
        {
            object invPattern = null;
            bool isValid = element.TryGetCurrentPattern(InvokePattern.Pattern,out invPattern);

            if (isValid)
            {
                return (InvokePattern)invPattern;
            }
            else
            {
                throw new InvalidOperationException("Invalid operation");
            }
        }
    }
}
