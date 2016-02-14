// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using AcceptanceTestLibrary.Common.Desktop;
using Interactivity.Tests.AcceptanceTest.TestEntities.Page;
using Interactivity.Tests.AcceptanceTest.TestEntities.Assertion;
using AcceptanceTestLibrary.ApplicationHelper;
using AcceptanceTestLibrary.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interactivity.Tests.AcceptanceTest
{

#if DEBUG
    [DeploymentItem(@"..\..\..\..\InteractivityQuickstart\bin\Debug", "WPF")]
    [DeploymentItem(@"TestData", "TestData")]
#else
    [DeploymentItem(@"..\..\..\..\InteractivityQuickstart\bin\Release", "WPF")]
    [DeploymentItem(@"TestData","TestData")]
#endif

    [TestClass]
   public class InteractivityFixture: FixtureBase<WpfAppLauncher>
    {
        #region Additional test attributes

        [TestInitialize]
        public void TestInitialize()
        {
            string currentOutputPath = (new System.IO.DirectoryInfo(Assembly.GetExecutingAssembly().Location)).Parent.FullName;
            InteractivityPage<WpfAppLauncher>.Window = base.LaunchApplication(currentOutputPath + GetDesktopApplication(), GetDesktopApplicationTitle())[0];
            InteractivityPage<WpfAppLauncher>.Window.SetFocus();
            Thread.Sleep(500);            
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            UnloadApplication();
        }
        #endregion

        [TestMethod]
        public void InteractivityQuickStartLaunchTest()
        {
            //check if window handle object is not null
            Assert.IsNotNull(InteractivityPage<WpfAppLauncher>.Window, "Interactivity QS is not launched.");
           
         }

        [TestMethod]
        public void InteractionRequestNotificationTest()
        {
            InteractivityAssertion<WpfAppLauncher>.AssertIR_RaiseDefaultNotification();
        }

        [TestMethod]
        public void InteractionRequestConfirmationAcceptTest()
        {
            InteractivityAssertion<WpfAppLauncher>.AssertIR_RaiseDefaultConfirmationAcceptTest();
        }

        [TestMethod]
        public void InteractionRequestConfirmationCancelTest()
        {
            InteractivityAssertion<WpfAppLauncher>.AssertIR_RaiseDefaultConfirmationCancelTest();
        }

         [TestMethod]
        public void InteractionRequestCustomPopupViewTest()
        {
            InteractivityAssertion<WpfAppLauncher>.AssertIR_RaiseCustomPopupViewTest();
        }

         [TestMethod]
         public void InteractionRequestItemSelectionPopupViewSelectTest()
         {
             InteractivityAssertion<WpfAppLauncher>.AssertIR_RaiseItemSelectionPopupSelectTest();
         }

         [TestMethod]
         public void InteractionRequestItemSelectionPopupViewCancelTest()
         {
             InteractivityAssertion<WpfAppLauncher>.AssertIR_RaiseItemSelectionPopupCancelTest();
         }

         [TestMethod]
         public void InvokeCommandActionViewTest()
         {
             InteractivityAssertion<WpfAppLauncher>.AssertInvokeCommandActionViewTest();
         }

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
