// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.Common.Desktop;
using System.Reflection;
using ModularityWithMef.Tests.AcceptanceTest.TestEntities.Page;
using System.Threading;
using AcceptanceTestLibrary.TestEntityBase;
using AcceptanceTestLibrary.ApplicationHelper;
using AcceptanceTestLibrary.UIAWrapper;
using System.Windows.Automation;
using System.Drawing;

namespace ModularityWithMef.Tests.AcceptanceTest
{

#if DEBUG

    [DeploymentItem(@"TestData", "TestData")]
    [DeploymentItem(@"..\..\..\..\ModularityWithMef\ModularityWithMef.Desktop\bin\Debug", "WPF")]
#else
    [DeploymentItem(@"TestData", "TestData")]
    [DeploymentItem(@"..\..\..\..\ModularityWithMef\ModularityWithMef.Desktop\bin\Release", "WPF")]
#endif
    [TestClass]
    public class MefDesktopTests : FixtureBase<WpfAppLauncher>
    {
        // Time in milliseconds to wait
        static int TIMEWAIT = 1000; 

        #region Additional test attributes
        [TestInitialize]
        public void TestInitialize()
        {
            string currentOutputPath = (new System.IO.DirectoryInfo(Assembly.GetExecutingAssembly().Location)).Parent.FullName;
            ModularityPage<WpfAppLauncher>.Window = base.LaunchApplication(currentOutputPath + GetDesktopApplication(), GetDesktopApplicationTitle())[0];
            ModularityPage<WpfAppLauncher>.Window.SetFocus();
            Thread.Sleep(TIMEWAIT);

        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            UnloadApplication();
        }
        #endregion
        [TestMethod]
        //[Ignore]
        public void DesktopModularityLaunchTest()
        {
            Assert.IsNotNull(ModularityPage<WpfAppLauncher>.Window, "Modularity QuickStart - Desktop with MEF is not launched");
        }

        [TestMethod]
        //[Ignore]
        public void DesktopModulesLoadedOnStartup()
        {
            AutomationElement aeModuleA = ModularityPage<WpfAppLauncher>.ModuleA;
            Thread.Sleep(TIMEWAIT);
            AutomationElement aeClickToloadModuleA = aeModuleA.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsTrue(aeClickToloadModuleA.Current.IsOffscreen, "Module A is not initialized at startup");

            AutomationElement aeModuleD = ModularityPage<WpfAppLauncher>.ModuleD;
            AutomationElement aeClickToloadModuleD = aeModuleD.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsTrue(aeClickToloadModuleD.Current.IsOffscreen, "Module D is not initialized at startup");
        }

        [TestMethod]
        //[Ignore]
        public void DesktopLoadModuleBOnDemand()
        {   
            //Check if Module B is already loaded
            AutomationElement aeModuleB = ModularityPage<WpfAppLauncher>.ModuleB;
            AutomationElement aeClickToloadModuleB = aeModuleB.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsFalse(aeClickToloadModuleB.Current.IsOffscreen, "Module B is initialized");
          
            //Get the clickable point and click on it.
            System.Windows.Point clickablePoint = aeModuleB.GetClickablePoint();
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)clickablePoint.X, (int)clickablePoint.Y);
            Thread.Sleep(TIMEWAIT);
            MouseEvents.Click();
            //Check if Module B is loaded
            AutomationElement aeClickToloadModB = aeModuleB.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsTrue(aeClickToloadModB.Current.IsOffscreen, "Module B is not initialized");         
            
        }

        [TestMethod]
        //[Ignore]
        public void DesktopLoadModuleCOnDemand()
        {
            //Check if Module C is already loaded
            AutomationElement aeModuleC = ModularityPage<WpfAppLauncher>.ModuleC;
            AutomationElement aeClickToloadModuleC = aeModuleC.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsFalse(aeClickToloadModuleC.Current.IsOffscreen, "Module C is initialized");

            //Get the clickable point and click on it.
            System.Windows.Point clickablePoint = aeModuleC.GetClickablePoint();
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)clickablePoint.X, (int)clickablePoint.Y);
            Thread.Sleep(TIMEWAIT);
            MouseEvents.Click();
          
            //Check if Module C is loaded
            AutomationElement aeClickToloadModC = aeModuleC.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsTrue(aeClickToloadModC.Current.IsOffscreen, "Module C is not initialized");

        }

        [TestMethod]
        public void DesktopLoadModuleEOnDemand()
        {
            //Check if Module E is already loaded
            AutomationElement aeModuleE = ModularityPage<WpfAppLauncher>.ModuleE;
            AutomationElement aeClickToloadModuleE = aeModuleE.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsFalse(aeClickToloadModuleE.Current.IsOffscreen, "Module E is initialized");

            //Get the clickable point and click on it.
            System.Windows.Point clickablePoint = aeModuleE.GetClickablePoint();
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)clickablePoint.X, (int)clickablePoint.Y);
            Thread.Sleep(TIMEWAIT);
            MouseEvents.Click();
            Thread.Sleep(TIMEWAIT);
            //Check if Module E is loaded
            AutomationElement aeClickToloadModE = aeModuleE.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsTrue(aeClickToloadModE.Current.IsOffscreen, "Module E is not initialized");

            //Check if Module F is also loaded
            AutomationElement aeModuleF = ModularityPage<WpfAppLauncher>.ModuleF;
            AutomationElement aeClickToloadModuleF = aeModuleF.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsFalse(aeClickToloadModuleF.Current.IsOffscreen, "Module F is also initialized");
        }

        [TestMethod]
        public void DesktopLoadModuleFAndDependency()
        {
            //Check if Module F is already loaded
            AutomationElement aeModuleF = ModularityPage<WpfAppLauncher>.ModuleF;
            AutomationElement aeClickToloadModuleF = aeModuleF.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsFalse(aeClickToloadModuleF.Current.IsOffscreen, "Module F is initialized");

            //Get the clickable point and click on it.
            System.Windows.Point clickablePoint = aeModuleF.GetClickablePoint();
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)clickablePoint.X, (int)clickablePoint.Y);
            Thread.Sleep(TIMEWAIT);
            MouseEvents.Click();
            Thread.Sleep(TIMEWAIT);
            //Check if depency module (Module E) is loaded
            AutomationElement aeModuleE = ModularityPage<WpfAppLauncher>.ModuleE;
            AutomationElement aeClickToloadModE = aeModuleE.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsTrue(aeClickToloadModE.Current.IsOffscreen, "Module E is not initialized");

            //Check if Module F is loaded           
            AutomationElement aeClickToloadModF = aeModuleF.FindElementByContent(new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("ClickToLoadTextBlock"));
            Assert.IsTrue(aeClickToloadModF.Current.IsOffscreen, "Module F is not initialized");
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
