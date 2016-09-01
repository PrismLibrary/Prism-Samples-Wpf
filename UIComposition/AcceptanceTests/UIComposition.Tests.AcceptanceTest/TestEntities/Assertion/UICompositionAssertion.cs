// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


using System;
using AcceptanceTestLibrary.Common;
using UIComposition.Tests.AcceptanceTest.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestLibrary.Common.Desktop;
using UIComposition.Tests.AcceptanceTest.TestEntities.Page;
using System.Windows.Automation;
using System.Drawing;
using System.Threading;
using AcceptanceTestLibrary.UIAWrapper;

namespace UIComposition.Tests.AcceptanceTest.TestEntities.Assertion
{
    public static class UICompositionAssertion<TApp>
        where TApp : AppLauncherBase, new()
    {
        // Time in milliseconds to wait
        static int TIMEWAIT = 500; 
 
        #region Desktop Assertion
        public static void AssertEmployeeSelection()
        {
            AutomationElement employeesListGrid = UICompositionPage<TApp>.EmployeesListGrid;           
            Assert.IsNotNull(employeesListGrid, "Could not find employees list view");
        }

        public static void AssertEmployeeGeneralData()
        {
            AutomationElement employeesListGrid = UICompositionPage<TApp>.EmployeesListGrid;           

             AutomationElementCollection aeEmployeesList = UICompositionPage<TApp>.EmployeesGridItems;         
            aeEmployeesList[1].Select();
            Thread.Sleep(TIMEWAIT);

            for(int count=0; count<aeEmployeesList.Count-1; count++)
            {
                aeEmployeesList[count].Select();
                Thread.Sleep(TIMEWAIT);
                
                AutomationElement firstName = UICompositionPage<TApp>.FirstNameTextBox;
                AutomationElement lastName = UICompositionPage<TApp>.LastNameTextBox;
                AutomationElement phoneText = UICompositionPage<TApp>.PhoneTextBox;
                AutomationElement emailText = UICompositionPage<TApp>.EmailTextBox;

                Assert.IsNotNull(firstName, "Could not find first name text box");
                Assert.IsNotNull(lastName, "Could not find last name text box");
                Assert.IsNotNull(phoneText, "Could not find phone text box");
                Assert.IsNotNull(emailText, "Could not find email text box");
                Employee emp = GetEmployeeId((count+1).ToString());
                Assert.AreEqual(firstName.GetValue(), emp.FirstName);
                Assert.AreEqual(lastName.GetValue(), emp.LastName);
                Assert.AreEqual(phoneText.GetValue(), emp.Phone);
                Assert.AreEqual(emailText.GetValue(), emp.Email);

            }

        }

        public static void AssertEmployeeCurrentProjects()
        {
            AutomationElement employeesListGrid = UICompositionPage<TApp>.EmployeesListGrid;

            AutomationElementCollection aeEmployeesList = UICompositionPage<TApp>.EmployeesGridItems;
            aeEmployeesList[1].Select();
            Thread.Sleep(TIMEWAIT);

           
            for (int count = 0; count < aeEmployeesList.Count-1; count++)
            {
                aeEmployeesList[count].Select();
                Thread.Sleep(TIMEWAIT);

                AutomationElement employeeTabControl = UICompositionPage<TApp>.EmployeeSummaryTabControl;
                AutomationElementCollection employeeTabItems = UICompositionPage<TApp>.EmployeeTabItems;

                employeeTabItems[1].Select();
                Thread.Sleep(TIMEWAIT);

                //Check if the current projects grid is loaded.
                AutomationElement currentProjectsGrid = UICompositionPage<TApp>.ProjectsGrid;
                GridPattern projectPattern = currentProjectsGrid.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
                Assert.AreEqual(TestDataInfrastructure.GetTestInputData("ProjectsRowCount"+(count+1).ToString()), projectPattern.Current.RowCount.ToString());
            }
        }
        #endregion

        #region Private Helper methods

   
        private static Employee GetEmployeeId(string count)
        {
            Employee emp = new Employee(1)
            {
                FirstName = TestDataInfrastructure.GetTestInputData("Emp_"+count+"_FirstName"),
                LastName = TestDataInfrastructure.GetTestInputData("Emp_" +count+ "_LastName"),
                Phone = TestDataInfrastructure.GetTestInputData("Emp_" + count + "_Phone"),
                Email = TestDataInfrastructure.GetTestInputData("Emp_" + count + "_Email")
            };

            return emp;
        }
       
        #endregion

    }
}