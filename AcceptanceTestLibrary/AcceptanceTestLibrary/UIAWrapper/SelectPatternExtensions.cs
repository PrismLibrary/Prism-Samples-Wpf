// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace AcceptanceTestLibrary.UIAWrapper
{
    public static class SelectPatternExtensions
    {
        public static void Select(this AutomationElement Control)
        {
            //check if the buttonControl is indeed a handle to a button-based control
            SelectionItemPattern selPattern = ValidateControlForSelectionPattern(Control);

            //click the button control
            selPattern.Select();
          
        }

        private static SelectionItemPattern ValidateControlForSelectionPattern(AutomationElement element)
        {
            object selPattern = null;
            bool isValid = element.TryGetCurrentPattern(SelectionItemPattern.Pattern, out selPattern);

            if (isValid)
            {
                return (SelectionItemPattern)selPattern;
            }
            else
            {
                throw new InvalidOperationException("Invalid operation");
            }
        }
    }
}
