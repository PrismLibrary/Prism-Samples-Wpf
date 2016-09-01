// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using AcceptanceTestLibrary.Common;
using System.Windows.Automation;
using AcceptanceTestLibrary.TestEntityBase;
using AcceptanceTestLibrary.ApplicationHelper;

namespace EventAggregation.Tests.AcceptanceTest.TestEntities.Page
{
    public static class EventAggregationPage<TApp>
       where TApp : AppLauncherBase, new()
    {
        private static AutomationElement window;

        public static AutomationElement DesktopWindow
        {
            get { return window; }
            set { window = value; }
        }

        public static AutomationElement CustomerComboBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("CustomerCombo"); }
        }

        public static AutomationElement FundComboBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("FundCombo"); }
        }

        public static AutomationElement AddButton
        {
             get { return PageBase<TApp>.FindControlByAutomationId("AddButton"); }
        }

        public static AutomationElement ActivityLabel
        {
             get { return PageBase<TApp>.FindControlByAutomationId("ActivityLabel"); }
        }

        public static AutomationElement GetFundsLabel(string fundsText)
        {
             return PageBase<TApp>.FindControlByContent(fundsText);
        }

        public static AutomationElementCollection GetAllFundsLabels(string fundsText)
        {
            return PageBase<TApp>.FindAllControlsByContent(fundsText);
        }

        public static AutomationElement GetFundsLabelByAutomationId(string fundsText, string automationId)
        {
           
            PropertyCondition cond = new PropertyCondition(AutomationElement.NameProperty,
                  fundsText);
            PropertyCondition cond1 = new PropertyCondition(AutomationElement.AutomationIdProperty, automationId);
            AndCondition andCond = new AndCondition(cond, cond1);
            return Window.FindFirst(TreeScope.Descendants, andCond);         

        }

        public static AutomationElementCollection ElementsInActivityView
        {
            //get { return DesktopWindow.AutomationElement.FindSiblingsInTreeByName(GetDataFromResourceFile("ActivityView")); }
            get { return PageBase<TApp>.FindControlByType("ActivityView"); }
        }

    

        private static string GetDataFromResourceFile(string keyName)
        {
            return new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue(keyName);
        }

        public static AutomationElement Window
        {
            get { return PageBase<TApp>.Window; }
            set { PageBase<TApp>.Window = value; }
        }

        public static AutomationElementCollection CustomerCombo
        {           
            get { return Window.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, GetDataFromResourceFile("CustomerCombo"))); } 
        }

        public static AutomationElement CustomerCombo1
        {
            get { return PageBase<TApp>.FindControlByAutomationId("CustomerCombo"); } 
        }
        public static AutomationElementCollection CustomerComboItems
        {
         get
            {
                AutomationElement elementList = null;
                // Set up the CacheRequest.
                CacheRequest cacheRequest = new CacheRequest();
                cacheRequest.Add(AutomationElement.ControlTypeProperty);
                cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;

                using (cacheRequest.Activate())
                {
                   PropertyCondition c = new PropertyCondition(
                       AutomationElement.ControlTypeProperty, ControlType.ComboBox);
                   PropertyCondition c1 = new PropertyCondition(AutomationElement.AutomationIdProperty,
                       GetDataFromResourceFile("CustomerCombo"));

                   AndCondition andCond = new AndCondition(c, c1);

                    elementList = Window.FindFirst(TreeScope.Descendants, andCond);
                }
                return elementList.CachedChildren;
            }


         }

        public static AutomationElementCollection FundComboItems
        {
            get
            {
                AutomationElement elementList = null;
                // Set up the CacheRequest.
                CacheRequest cacheRequest = new CacheRequest();
                cacheRequest.Add(AutomationElement.ControlTypeProperty);
                cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;

                using (cacheRequest.Activate())
                {
                    PropertyCondition c = new PropertyCondition(
                        AutomationElement.ControlTypeProperty, ControlType.ComboBox);
                    PropertyCondition c1 = new PropertyCondition(AutomationElement.AutomationIdProperty,
                        GetDataFromResourceFile("FundCombo"));

                    AndCondition andCond = new AndCondition(c, c1);

                    elementList = Window.FindFirst(TreeScope.Descendants, andCond);
                }
                return elementList.CachedChildren;
            }
        }

        public static AutomationElement FundCombo
        {
             get { return PageBase<TApp>.FindControlByAutomationId("FundCombo"); }
        }

        public static AutomationElement AddFundButton
        {
             get { return PageBase<TApp>.FindControlByAutomationId("AddButton"); }
        }

        public static AutomationElement ActivityLabelElement
        {
             get { return PageBase<TApp>.FindControlByAutomationId("ActivityLabel"); }
        }

        public static AutomationElementCollection AllTextBoxes
        {
            get
            {
                PropertyCondition findText = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text);
                return Window.FindAll(TreeScope.Descendants, findText);
            }
        }

    }
}
