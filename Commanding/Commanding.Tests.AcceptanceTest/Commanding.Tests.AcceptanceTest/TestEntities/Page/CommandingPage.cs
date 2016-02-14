// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcceptanceTestLibrary.Common;
using System.Windows.Automation;
using AcceptanceTestLibrary.TestEntityBase;
using AcceptanceTestLibrary.ApplicationHelper;

namespace Commanding.Tests.AcceptanceTest.TestEntities.Page
{
    public static class CommandingPage<TApp>
        where TApp : AppLauncherBase, new()
    {
        #region Desktop
        public static AutomationElement Window
        {
            get { return PageBase<TApp>.Window; }
            set { PageBase<TApp>.Window = value; }
        }   

        public static AutomationElement SaveAllToolBarButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("SaveAllToolBarButton"); }
        }
        public static AutomationElement SaveButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("SaveButton"); }
        }
        public static AutomationElementCollection OrderListView
        {
            get { return PageBase<TApp>.FindControlByType("OrderListView"); }
        }
        public static AutomationElement DateLabel
        {
            get { return PageBase<TApp>.FindControlByAutomationId("DateLabel"); }
        }
        public static AutomationElement OrderNameLabel
        {
            get { return PageBase<TApp>.FindControlByAutomationId("OrderNameLabel"); }
        }
        public static AutomationElement PriceLabel
        {
            get { return PageBase<TApp>.FindControlByAutomationId("PriceLabel"); }
        }
        public static AutomationElement QuantityLabel
        {
            get { return PageBase<TApp>.FindControlByAutomationId("QuantityLabel"); }
        }
        public static AutomationElement ShippingLabel
        {
            get { return PageBase<TApp>.FindControlByAutomationId("ShippingLabel"); }
        }
        public static AutomationElement TotalLabel
        {
            get { return PageBase<TApp>.FindControlByAutomationId("TotalLabel"); }
        }
        public static AutomationElement DeliveryDateTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("DeliveryDateTextBox");}
        }
        public static AutomationElement PriceTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("PriceTextBox");}
        }
        public static AutomationElement QuantityTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("QuantityTextBox");}
        }
        public static AutomationElement ShippingTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("ShippingTextBox");}
        }
        public static AutomationElement TotalTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("TotalTextBox");}
        }
        
        #endregion

        public static AutomationElementCollection aeOrderListView
        {
            //get { return Window.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, GetDataFromResourceFile("OrderListView"))); }
            get { return PageBase<TApp>.FindControlByType("OrderListView"); }
        }

    }
}
