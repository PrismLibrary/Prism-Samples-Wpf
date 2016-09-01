// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestLibrary.Common.Desktop;
using EventAggregation.Tests.AcceptanceTest.TestEntities.Page;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.ApplicationHelper;
using EventAggregation.Tests.AcceptanceTest.TestEntities.Assertion;
using System.Reflection;
using System.Threading;

namespace EventAggregation.Tests.AcceptanceTest.Desktop
{
    /// <summary>
    /// Test class for desktop part of EventAggregation
    /// </summary>

#if DEBUG
    [DeploymentItem(@"..\..\..\Desktop\bin\Debug", "WPF")]
    [DeploymentItem(@"TestData", "TestData")]
#else
    [DeploymentItem(@"..\..\..\Desktop\bin\Release", "WPF")]
    [DeploymentItem(@"TestData","TestData")]
#endif

    [TestClass]
    public class EventAggregationAcceptanceTestDesktop : FixtureBase<WpfAppLauncher>
    {
        #region Additional test attributes
        [TestInitialize]
        public void TestInitialize()
        {
            string currentOutputPath = (new System.IO.DirectoryInfo(Assembly.GetExecutingAssembly().Location)).Parent.FullName;
            EventAggregationPage<WpfAppLauncher>.Window = base.LaunchApplication(currentOutputPath + GetDesktopApplication(), GetDesktopApplicationTitle())[0];
            EventAggregationPage<WpfAppLauncher>.Window.SetFocus();            
            Thread.Sleep(500);

        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            UnloadApplication();           
        }
        #endregion

        #region Test Methods
        [TestMethod]
        public void DesktopApplicationLoadTest()
        {
            //check if window handle object is not null
            Assert.IsNotNull(EventAggregationPage<WpfAppLauncher>.Window, "Event Aggregation is not launched.");
        }

        /// <summary>
        /// Check if each if repeated funds are getting added to Activity view
        /// /// 
        /// Repro Steps:
        /// 1. Launch the EventAgrregation QS        
        /// 2. For customers customer combo box,  add funds and check if they got added
        /// 
        /// Expected Result:
        /// For the customer the fund should have got added.
        [TestMethod]
        public void DesktopAddMultipleFundsToCustomer()
        {
            //Assert AddFund To Customer
            EventAggregationAssertion<WpfAppLauncher>.DesktopAssertAddMultipleFundsToCustomer();
        }

        /// <summary>
        /// Check if each if repeated funds are getting added to Activity view
        /// /// 
        /// Repro Steps:
        /// 1. Launch the EventAgrregation QS        
        /// 2. For a in the customer combo box,  add same funds repeatedly and check if they got added
        /// 
        /// Expected Result:
        /// For the selected customer fund should got added based on the number of AddButton clicks.
        /// </summary>
        [TestMethod]
        public void DesktopAddRepeatedFundsToCustomer()
        {
            //Assert AddFund To Customer
            EventAggregationAssertion<WpfAppLauncher>.DesktopAssertAddRepeatedFundsToCustomer();
        }

        /// <summary>
        /// Check if each of the customer in the customer combo box has an Article view.
        /// 
        /// Repro Steps:
        /// 1. Launch the EventAgrregation QS        
        /// 2. For every customer in the customer combo box, Check if a corresponding ActivityView is 
        /// displayed on the right side of the screen.
        /// 
        /// Expected Result:
        /// Every customer in the customer combo box should have a corresponding Article view displayed.      
        /// </summary>
        [TestMethod]
        public void DesktopEachCustomerShouldHaveAnActivityView()
        {
            EventAggregationAssertion<WpfAppLauncher>.DesktopAssertEachCustomerShouldHaveAnActivityView();
        }

        /// <summary>
        /// Check if the selected fund is added only to the selected customer.
        /// 
        /// Repro Steps:
        /// 1. Launch the EventAggregation QS
        /// 2. Select the customer from customer dropdown. 
        /// 3. Select the fund from fund dropdown.
        /// 4. Click on Add.
        /// 5. Repeat steps 2 to 4 by changing the customer and fund.
        /// 5. Check whether the added fund has been displayed correctly in the display area.
        /// 
        /// Expected Result:
        /// Fund should get added to the selected customer.
        /// </summary>
        [TestMethod]
        public void DesktopSelectedFundIsAddedOnlyToTheSelectedCustomer()
        {
            EventAggregationAssertion<WpfAppLauncher>.DesktopAssertSelectedFundIsAddedOnlyToTheSelectedCustomer();
        }
        #endregion

        #region Private methods

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
