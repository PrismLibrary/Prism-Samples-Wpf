// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.Common.Desktop;
using AcceptanceTestLibrary.TestEntityBase;
using AcceptanceTestLibrary.ApplicationHelper;
using System.Reflection;
using System.Threading;
using System.IO;
using StateBasedNavigation.Tests.AcceptanceTest.TestEntities.Assertion;
using StateBasedNavigation.Tests.AcceptanceTest.TestEntities.Page;

namespace StateBasedNavigation.Tests.AcceptanceTest.Desktop
{
#if DEBUG
    [DeploymentItem(@"TestData", "TestData")]
    [DeploymentItem(@"..\..\..\State-Based Navigation.Desktop\Bin\Debug", "WPF")]
#else
    [DeploymentItem(@"TestData", "TestData")]
    [DeploymentItem(@"..\..\..\State-Based Navigation.Desktop\Bin\Release", "WPF")]
#endif

    [TestClass]
    public class StateBasedNavigationFixture: FixtureBase<WpfAppLauncher>
    {

        #region Additional test attributes

        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            string currentOutputPath = (new System.IO.DirectoryInfo(Assembly.GetExecutingAssembly().Location)).Parent.FullName;
            StateBasedNavigationPage<WpfAppLauncher>.Window = base.LaunchApplication(currentOutputPath + GetDesktopApplication(), GetDesktopApplicationTitle())[0];
            StateBasedNavigationPage<WpfAppLauncher>.Window.SetFocus();
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

        /// <summary>
        /// StateBasedNavigation Launch
        /// </summary>
        [TestMethod]
        public void StateBasedNavigation_NavigationLaunch()
        {
            Assert.IsNotNull(StateBasedNavigationPage<WpfAppLauncher>.Window, "Navigation Prototype application is not launched.");
        }

        /// <summary>
        /// Validations on State Based Navigation App launch
        /// </summary>
        [TestMethod]
        public void StateBasedNavigation_OnLoad()
        {
            StateBasedNavigation_Assertion<WpfAppLauncher>.StateBasedNavigation_OnLoad();         
        }
        /// <summary>
        ///  Validations on clicking avatars button
        /// </summary>
        [TestMethod]        
        public void StateBasedNavigation_ClickAvatars()
        {
            StateBasedNavigation_Assertion<WpfAppLauncher>.StateBasedNavigation_ClickAvatars();      
        }
        /// <summary>
        ///  Validations on selecting Unavailable From Combobox
        /// </summary>
        [TestMethod]
        public void StateBasedNavigation_SelectUnavailable()
        {
            StateBasedNavigation_Assertion<WpfAppLauncher>.StateBasedNavigation_SelectUnavailable();
        }
        /// <summary>
        /// Validations on clicking "Show Details" 
        /// </summary>
        [TestMethod]
        public void StateBasedNavigation_ClickDetails()
        {
            StateBasedNavigation_Assertion<WpfAppLauncher>.StateBasedNavigation_ClickDetails(); 
        }

        /// <summary>
        /// Validations on clicking "Show Details" in Avatars View
        /// </summary>
        [TestMethod]
        public void StateBasedNavigation_ClickDetailsInAvatarsView()
        {
            StateBasedNavigation_Assertion<WpfAppLauncher>.StateBasedNavigation_ClickAvatars();
            StateBasedNavigation_Assertion<WpfAppLauncher>.StateBasedNavigation_ClickDetailsInAvatarView();
        }

        /// <summary>
        ///  Validations On clicking "Send Message" button
        /// </summary>
        [TestMethod]
        public void StateBasedNavigation_SendMessage()
        {
            StateBasedNavigation_Assertion<WpfAppLauncher>.StateBasedNavigation_SendMessage();
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
