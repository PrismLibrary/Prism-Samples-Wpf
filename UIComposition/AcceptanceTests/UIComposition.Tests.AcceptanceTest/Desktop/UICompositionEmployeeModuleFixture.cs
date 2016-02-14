// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.ApplicationHelper;
using System.IO;
using AcceptanceTestLibrary.Common.Desktop;
using AcceptanceTestLibrary.TestEntityBase;
using System.Reflection;
using UIComposition.Tests.AcceptanceTest.TestEntities.Page;
using UIComposition.Tests.AcceptanceTest.TestEntities.Assertion;
using System.Threading;
using System.Text.RegularExpressions;

namespace UIComposition.Tests.AcceptanceTest.Desktop
{
    /// <summary>
    /// Summary description for UI Composition
    /// </summary>
 #if DEBUG
    [DeploymentItem(@"TestData", "TestData")]
    [DeploymentItem(@"..\..\..\..\UIComposition_Desktop\bin\Debug", "WPF")]
#else
    [DeploymentItem(@"TestData", "TestData")]
    [DeploymentItem(@"..\..\..\..\UIComposition_Desktop\bin\Release", "WPF")]
#endif

    [TestClass]
    public class UICompositionEmployeeModuleFixture : FixtureBase<WpfAppLauncher>
    {

        [TestInitialize()]
        public void MyTestInitialize()
        {
            string currentOutputPath = (new System.IO.DirectoryInfo(Assembly.GetExecutingAssembly().Location)).Parent.FullName;
            UICompositionPage<WpfAppLauncher>.Window = base.LaunchApplication(currentOutputPath + GetDesktopApplication(), GetDesktopApplicationTitle())[0];
            UICompositionPage<WpfAppLauncher>.Window.SetFocus();
            Thread.Sleep(500);
        }

        /// <summary>
        /// TestCleanup performs clean-up activities after each test method execution
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup()
        {
            UnloadApplication();
        }


        #region Test Methods
        /// <summary>
        /// Tests if the Desktop UI composition View Discovery application is launched.
        /// </summary>
        [TestMethod]
        public void DesktopUICompositionLaunchTest()
        {
            Assert.IsNotNull(UICompositionPage<WpfAppLauncher>.Window, "UI composition application is not launched.");
        }

        /// <summary>
        /// Validate if the details view of the selected employee are displayed correctly.
        /// 
        /// Repro Steps:
        /// 1. Launch the QS application
        /// 2. Click on the first employee row in the Employee List table
        /// 3. Check if the Details View Tab is displayed, and the number of tab items is 3.
        /// 4. Check if the tab items headers match with "General", "Location" and "Current Projects"
        /// 
        /// Expected Result:
        /// Details View Tab is dispalyed with 2 tab items. 
        /// The tab items headers match with "General" and "Current Projects"
        /// </summary>
        [TestMethod]
        public void UICompositionValidateEmployeeSelection()
        {
            UICompositionAssertion<WpfAppLauncher>.AssertEmployeeSelection();
        }

        /// <summary>
        /// Validate General details in the General Tab for selected employee
        /// 
        /// Repro Steps:
        /// 1. Launch the QS Application
        /// 2. Select the first employee row in the Employee List table
        /// 3. Check if the details of the selected employee are displayed in the General tab
        /// 
        /// Expected results:
        /// Employee First Name, Last Name, Phone and Email are correctly displayed in the General Tab
        /// </summary>
        [TestMethod]
        public void UICompositionValidateGeneralDataForAllEmployees()
        {
            UICompositionAssertion<WpfAppLauncher>.AssertEmployeeGeneralData();
        }

        /// <summary>
        /// Validate Current Projects details in the Current Projects Tab for selected employee
        /// 
        /// Repro Steps:
        /// 1. Launch the QS Application
        /// 2. Select the first employee row in the Employee List table
        /// 3. Check if the Current Projects of the selected employee are displayed in the Current Projects tab
        /// 
        /// Expected results:
        /// Current Project and Role of the selected Employee are correctly displayed in the Current Projects Tab
        /// </summary>
        [TestMethod]
        public void UICompositionValidateProjects()
        {
            UICompositionAssertion<WpfAppLauncher>.AssertEmployeeCurrentProjects();
        }



        #endregion

        #region Helper Methods
        private static string GetDesktopApplication()
        {
            return ConfigHandler.GetValue("WpfAppLocation");
        }

        private static string GetDesktopApplicationTitle()
        {
            return new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("DesktopApplicationTitle");
        }
   
        #endregion
    }


}
