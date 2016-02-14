// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace AcceptanceTestLibrary.UIAWrapper
{
    public static class ValuePatternExtensions
    {
        public static string GetValue(this AutomationElement textControl)
        {
            //check if the textControl is indeed a handle to a text-based control
            ValuePattern valPattern = ValidateTextControl(textControl);

            //get value from the texbox
            return valPattern.Current.Value;
        }

        public static void SetValue(this AutomationElement textControl, string value)
        {
            //check if the textControl is indeed a handle to a text-based control
            ValuePattern valPattern = ValidateTextControl(textControl);

            valPattern.SetValue(value);
        }

        private static ValuePattern ValidateTextControl(AutomationElement element)
        {
            object valPattern = null;
            bool isValid = element.TryGetCurrentPattern(ValuePattern.Pattern,out valPattern);

            if (isValid)
            {
                return (ValuePattern)valPattern;
            }
            else
            {
                throw new InvalidOperationException("Invalid operation");
            }
        }
    }
}
