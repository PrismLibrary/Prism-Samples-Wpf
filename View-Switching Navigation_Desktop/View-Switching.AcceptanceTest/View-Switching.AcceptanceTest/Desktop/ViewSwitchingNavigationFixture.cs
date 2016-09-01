// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.Common.Desktop;
using System.Reflection;
using View_Switching.AcceptanceTest.TestEntities.Page;
using System.Threading;
using AcceptanceTestLibrary.TestEntityBase;
using AcceptanceTestLibrary.ApplicationHelper;
using System.IO;
using System.Text.RegularExpressions;
using View_Switching.AcceptanceTest.TestEntities.Assertion;

namespace View_Switching.AcceptanceTest
{
    #if DEBUG

   [DeploymentItem(@"TestData", "TestData")]
   [DeploymentItem(@"..\..\..\..\ViewSwitchingNavigation\bin\Debug", "WPF")]
#else
    [DeploymentItem(@"TestData", "TestData")]
    [DeploymentItem(@"..\..\..\..\ViewSwitchingNavigation\bin\Release", "WPF")]
  
#endif
    [TestClass]
    public class ViewSwitchingNavigationFixture : FixtureBase<WpfAppLauncher>
    {

        #region Additional test attributes

        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {

            string currentOutputPath = (new System.IO.DirectoryInfo(Assembly.GetExecutingAssembly().Location)).Parent.FullName;
            ViewSwitchingNavigationPage<WpfAppLauncher>.Window = base.LaunchApplication(currentOutputPath + GetDesktopApplication(), GetDesktopApplicationTitle())[0];
            ViewSwitchingNavigationPage<WpfAppLauncher>.Window.SetFocus();
            Thread.Sleep(500);
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            PageBase<WpfAppLauncher>.DisposeWindow();
            UnloadApplication(); 
        }
        #endregion

        [TestMethod]
        public void RegionNavigationLaunch()
        {
            Assert.IsNotNull(ViewSwitchingNavigationPage<WpfAppLauncher>.Window, "Region Navigation application is not launched.");
        }

        [TestMethod]
        public void RegionNavigationMailRegionLoad()
        {
            ViewSwitchingNavigationAssertion<WpfAppLauncher>.AssertRN_MailRegionOnLoad();
        }

        [TestMethod]
        public void RegionNavigationSelectAMessage()
        {
            ViewSwitchingNavigationAssertion<WpfAppLauncher>.AssertRN_SelectAMessage();
        }

        [TestMethod]
        public void RegionNavigationLoadCalendar()
        {
            ViewSwitchingNavigationAssertion<WpfAppLauncher>.AssertRN_LoadCalendar();
        }

        [TestMethod]
        public void RegionNavigationLoadContactDetails()
        {
            ViewSwitchingNavigationAssertion<WpfAppLauncher>.AssertRN_LoadContactDetails();
        }

        [TestMethod]
        public void RegionNavigationLoadContactAvatars()
        {
            ViewSwitchingNavigationAssertion<WpfAppLauncher>.AssertRN_LoadContactAvatars();
        }

        [TestMethod]
        public void RegionNavigationSendEmailFromContactDetailsPage()
        {
            ViewSwitchingNavigationAssertion<WpfAppLauncher>.AssertRN_SendEmailFromContactDetailsPage();
        }

        [TestMethod]
        public void RegionNavigationSendEmailFromContactsAvatarsPage()
        {
            ViewSwitchingNavigationAssertion<WpfAppLauncher>.AssertRN_SendEmailFromContactsAvatarPage();
        }
        [TestMethod]
        public void RegionNavigationSendNewEmail()
        {
            ViewSwitchingNavigationAssertion<WpfAppLauncher>.AssertRN_SendNewEmail();
        }
        [TestMethod]
        public void RegionNavigationReplyAnEmail()
        {
            ViewSwitchingNavigationAssertion<WpfAppLauncher>.AssertRN_ReplyAnEmail();
        }

        public void RegionNavigationClickHyperlinkOnCalendar()
        {
            ViewSwitchingNavigationAssertion<WpfAppLauncher>.AssertRN_ClickHyperlinkOnCalendar();
        }
        [TestMethod]
        public void RegionNavigationOpenEmail()
        {
            ViewSwitchingNavigationAssertion<WpfAppLauncher>.AssertRN_OpenEmail();
        }
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
