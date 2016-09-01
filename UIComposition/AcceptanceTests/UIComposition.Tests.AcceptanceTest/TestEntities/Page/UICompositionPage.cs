// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.TestEntityBase;
using UIComposition.Tests.AcceptanceTest.TestInfrastructure;
using System.Windows.Automation;
using AcceptanceTestLibrary.ApplicationHelper;

namespace UIComposition.Tests.AcceptanceTest.TestEntities.Page
{
    public static class UICompositionPage<TApp>
        where TApp : AppLauncherBase, new()
    {
        #region Desktop
        public static AutomationElement Window
        {
            get { return PageBase<TApp>.Window; }
            set { PageBase<TApp>.Window = value; }
        }

      //  public static AutomationElement GarageImage
        //{
        //    get { return PageBase<TApp>.FindControlByAutomationId("GarageImgAutomation"); }
        //}

        //public static AutomationElementCollection EmployeesListGrid1
        //{

        //    get
        //    {
        //        AutomationElement elementList = null;
        //        // Set up the CacheRequest.
        //        CacheRequest cacheRequest = new CacheRequest();
        //        cacheRequest.Add(AutomationElement.ControlTypeProperty);
        //        cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;

        //        using (cacheRequest.Activate())
        //        {
        //            PropertyCondition c = new PropertyCondition(
        //                AutomationElement.ControlTypeProperty, ControlType.DataGrid);
        //            PropertyCondition c1 = new PropertyCondition(AutomationElement.AutomationIdProperty,
        //                "EmployeesListGrid");

        //            AndCondition andCond = new AndCondition(c, c1);

        //            elementList = Window.FindFirst(TreeScope.Descendants, andCond);
        //        }
        //        return elementList.CachedChildren;
        //    }
               
        //}
        public static AutomationElement EmployeesListGrid
        {
            get
            {
                return PageBase<TApp>.FindControlByAutomationId("EmployeesList");
            }
        }

      
        public static AutomationElement EmployeeSummaryTabControl
        {
            get { return PageBase<TApp>.FindControlByAutomationId("EmployeeSummaryTabControl"); }
        }
        public static AutomationElement FirstNameTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("FirstNameText"); }
        }

        public static AutomationElement PhoneTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("PhoneText"); }
        }

        public static AutomationElement LastNameTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("LastNameText"); }
        }

        public static AutomationElement EmailTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("EmailText"); }
        }

        public static AutomationElement ProjectsGrid
        {
            get { return PageBase<TApp>.FindControlByAutomationId("ProjectsList"); }
        }

        public static AutomationElementCollection AllTextBoxes
        {
            get
            {
                PropertyCondition findText = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text);
                return Window.FindAll(TreeScope.Descendants, findText);
            }
        }

        public static AutomationElementCollection EmployeesGridItems
        {
            get
            {
              
                PropertyCondition cond1 = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem);                
                return EmployeesListGrid.FindAll(TreeScope.Descendants, cond1);
            }
        }

        public static AutomationElementCollection EmployeeTabItems
        {
            get
            {

                PropertyCondition cond1 = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
                return EmployeeSummaryTabControl.FindAll(TreeScope.Descendants, cond1);
            }
        }
        #endregion

       
    }
}
