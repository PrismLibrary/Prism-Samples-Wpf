// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using System.Diagnostics;
using System.Threading;

using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Text;
using System.Windows.Automation.Provider;

using AcceptanceTestLibrary.ApplicationHelper;
using AcceptanceTestLibrary.Common;
using StockTraderRI.Tests.AcceptanceTest.TestEntities.Page;
using StockTraderRI.Tests.AcceptanceTest.TestEntities.Assertion;
using StockTraderRI.Tests.AcceptanceTest.TestEntities.Action;
using AcceptanceTestLibrary.ApplicationObserver;
using AcceptanceTestLibrary.Common.Desktop;
using System.Reflection;
using AcceptanceTestLibrary.TestEntityBase;

namespace StockTraderRI.Tests.AcceptanceTest.Desktop
{
    /// <summary>
    /// Acceptance test fixture for WPF application
    /// </summary>
#if DEBUG
    [DeploymentItem(@"..\..\..\..\Desktop\StockTraderRI\bin\Debug", "WPF")]
    [DeploymentItem(@"TestData", "TestData")]
#else
    [DeploymentItem(@"..\..\..\..\Desktop\StockTraderRI\bin\Release", "WPF")]
    [DeploymentItem(@"TestData","TestData")]
#endif

    [TestClass]
    public class StockTraderRIDesktopFixture : FixtureBase<WpfAppLauncher>
    {
        #region Additional test attributes

        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            string currentOutputPath = (new System.IO.DirectoryInfo(Assembly.GetExecutingAssembly().Location)).Parent.FullName;
            StockTraderRIPage<WpfAppLauncher>.Window = base.LaunchApplication(currentOutputPath + GetDesktopApplication(), GetDesktopApplicationProcess())[0];
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            PageBase<WpfAppLauncher>.DisposeWindow();
            Process p = WpfAppLauncher.GetCurrentAppProcess();
            base.UnloadApplication(p);
        }

        #endregion

        #region Test Methods

        #region Application Launch Test
        [TestMethod]
        public void DesktopApplicationLoadTest()
        {
            Assert.IsNotNull(StockTraderRIPage<WpfAppLauncher>.Window, "StockTraderRI is not launched.");
        }
        #endregion

        #endregion

        #region Position summary Module Load Test

        /// <summary>
        /// Tests if position summary details are loaded.
        /// </summary>
        [TestMethod]
        public void DesktopApplicationPositionSummaryTest()
        {
            InvokePositionSummaryAssert();
        }

        /// <summary>
        /// Tests the number of columns from position summary view table. 
        /// </summary>
        /// 

        [TestMethod]
        public void DesktopApplicationPositionSummaryColumnCountTest()
        {

            //For now the test data is hardcoded in resource file. But if the datasource is available it will be read from the datasource
            StockTraderRIAssertion<WpfAppLauncher>.DesktopAssertPositionSummaryColumnCount();
        }

        /// <summary>
        /// Tests the number of rows from position summary view table. 
        /// </summary>

        [TestMethod]
        public void DesktopApplicationPositionSummaryRowCountTest()
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertPositionSummaryRowCount();
        }

        /// <summary>
        /// Tests the computed value (Market value & Gain Loss %) with the value loaded in the screen
        /// </summary>
        /// 

        [TestMethod]
        public void DesktopApplicationPositionSummaryDataTest()
        {
            //For each Stock or Symbol take the old value and get the value from Web service and monitor that

            StockTraderRIAssertion<WpfAppLauncher>.AssertPositionSummaryValuesForSymbol();
        }

        #endregion

        #region Market Trend Test
        /// <summary>
        /// Tests the Historical Data Block is loaded 
        /// </summary>
        /// 

        [TestMethod]
        public void DesktopApplicationMarketTrendTest()
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertHistoricalDataText();
        }

        /// <summary>
        /// Tests the Pie Chart Data Block is loaded 
        /// </summary>
        /// 

        [TestMethod]
        public void DesktopApplicationPieChartTextLoadTest()
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertPieChartTextBlock();
        }
        #endregion

        #region PositionBuySellTab Test
        [TestMethod]
        public void DesktopPositionBuySellTabControlsLoadTest()
        {
            InvokeDesktopPositionBuySellTabControlsLoad("Buy");
        }


        [TestMethod]
        public void DesktopAttemptBuyStockWithValidData()
        {
            InvokeAttemptBuySellOrderWithValidData("Buy");
        }

        [TestMethod]
        public void DesktopAttemptBuyStockWithInValidData()
        {
            InvokeAttemptBuySellOrderWithInValidData("Buy");
        }
        [TestMethod]
        public void DesktopAttemptSellStockWithValidData()
        {
            InvokeAttemptBuySellOrderWithValidData("Sell");
        }
        [TestMethod]
        public void DesktopAttemptSellStockWithInValidData()
        {
            InvokeAttemptBuySellOrderWithInValidData("Sell");
        }
        [TestMethod]
        public void DesktopAttemptStockBuySellCancelByCancelButton()
        {
            InvokeAttemptOrderCancelByCancelButton();
        }
        [TestMethod]
        public void DesktopProcessMultipleStockBuySellBySubmitAllButtonforValidData()
        {
            InvokeProcessMultipleStockBuySellBySubmitAllButtonforValidData();
        }
        [TestMethod]
        public void DesktopProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
        {
            InvokeProcessMultipleStockBuySellBySubmitAllButtonforInValidData();
        }
        [TestMethod]
        public void DesktopProcessMultipleStockBuySellByCancelAllButton()
        {
            InvokeProcessMultipleStockBuySellByCancelAllButton();
        }

        #endregion

        #region NewsArticle Module Load Test
        /// <summary>
        /// Tests the News Articles Data Block is loaded 
        /// </summary>
        /// 

        [TestMethod]
        public void DesktopApplicationNewsArticleTextLoadTest()
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertNewsArticleText();
        }
        #endregion

        #region Watch List Module Test
        
        /// <summary>
        /// Tests the AddtoWatchList Button and the text Box is loaded
        /// </summary>
        /// 

        [TestMethod]
        public void DesktopApplicationAddtoWatchListTextBoxLoadTest()
        {
            InvokeAddtoWatchListAssert();
        }


        [TestMethod]
        public void DesktopStockRemovedfromWatchListTextBoxTest()
        {
            InvokeStockRemovedfromWatchListTextBoxAssert();
        }

        /// <summary>
        /// Tests the stock added in the TextBox gets added to the Watch List Grid on Clicking the AddtoWatchList Button
        /// </summary>
        /// 
        [TestMethod]
        public void DesktopApplicationStockAddedinWatchListTextBoxTest()
        {
            InvokeStockAddedinWatchListTextBoxAssert();
        }
        #endregion

        #region private methods
        private static string GetDesktopApplicationProcess()
        {
            return ConfigHandler.GetValue("WpfAppProcessName");
        }

        private static string GetDesktopApplication()
        {
            return ConfigHandler.GetValue("WpfAppLocation");
        }

        private void InvokePositionSummaryAssert()
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertPositionSummaryTab();
            StockTraderRIAssertion<WpfAppLauncher>.AssertPositionSummaryGrid();
        }
        private void InvokeOrderToolBarAssert()
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertSubmitButton();
            StockTraderRIAssertion<WpfAppLauncher>.AssertSubmitAllButton();
            StockTraderRIAssertion<WpfAppLauncher>.AssertCancelButton();
            StockTraderRIAssertion<WpfAppLauncher>.AssertCancelAllButton();
        }
        private void InvokeAddtoWatchListAssert()
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertTextBoxBlock();
        }
        private void InvokeStockAddedinWatchListTextBoxAssert()
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertStockAddedinWatchListTextBox();
            StockTraderRIAssertion<WpfAppLauncher>.AssertWatchListRowCount();
        }

        private void InvokeStockRemovedfromWatchListTextBoxAssert()
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertStockAddedinWatchListTextBox();
            StockTraderRIAssertion<WpfAppLauncher>.AssertWatchListRowCount();
            StockTraderRIAssertion<WpfAppLauncher>.AssertStockRemovedfromWatchListTextBox();
        }

        private void InvokeDesktopPositionBuySellTabControlsLoad(string option)
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertPositionSummaryTab();
            StockTraderRIAssertion<WpfAppLauncher>.AssertPositionBuyButtonClickTest(option);

            StockTraderRIAssertion<WpfAppLauncher>.AssertTermComboBox();
            StockTraderRIAssertion<WpfAppLauncher>.AssertPriceLimitTextBox();
            StockTraderRIAssertion<WpfAppLauncher>.AssertSellRadioButton();
            StockTraderRIAssertion<WpfAppLauncher>.AssertBuyRadioButton();
            StockTraderRIAssertion<WpfAppLauncher>.AssertSharesTextBox();

            StockTraderRIAssertion<WpfAppLauncher>.AssertOrderTypeComboBox();
            StockTraderRIAssertion<WpfAppLauncher>.AssertOrderCommandSubmit();
            StockTraderRIAssertion<WpfAppLauncher>.AssertOrderCommandCancel();
            StockTraderRIAssertion<WpfAppLauncher>.AssertOrderCommandSubmitAllButton();
            StockTraderRIAssertion<WpfAppLauncher>.AssertOrderCommandCancelAllButton();
        }
        private void InvokeAttemptBuySellOrderWithValidData(string option)
        {
            InvokeDesktopPositionBuySellTabControlsLoad(option);
            StockTraderRIAssertion<WpfAppLauncher>.AssertAttemptBuySellOrderWithValidData();
        }

        private void InvokeAttemptBuySellOrderWithInValidData(string option)
        {
            InvokeDesktopPositionBuySellTabControlsLoad(option);
            StockTraderRIAssertion<WpfAppLauncher>.AssertAttemptBuySellOrderWithInValidData();
        }

        private void InvokeAttemptOrderCancelByCancelButton()
        {
            InvokeDesktopPositionBuySellTabControlsLoad("Buy");
            StockTraderRIAssertion<WpfAppLauncher>.AssertAttemptOrderCancelByCancelButton();
        }

        private void InvokeProcessMultipleStockBuySellBySubmitAllButtonforValidData()
        {
            InvokeDesktopPositionBuySellTabLoad();
            
            StockTraderRIAssertion<WpfAppLauncher>.AssertProcessMultipleStockBuySellBySubmitAllButtonforValidData();
        }

        private void InvokeDesktopPositionBuySellTabLoad()
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertPositionSummaryTab();
            StockTraderRIAssertion<WpfAppLauncher>.AssertPositionBuyButtonClickTest("Buy");
        }


        private void InvokeProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
        {
            InvokeSilverLightPositionBuySellTabLoad();
            StockTraderRIAssertion<WpfAppLauncher>.AssertProcessMultipleStockBuySellBySubmitAllButtonforInValidData();
        }
        private void InvokeSilverLightPositionBuySellTabLoad()
        {
            StockTraderRIAssertion<WpfAppLauncher>.AssertPositionSummaryTab();
            StockTraderRIAssertion<WpfAppLauncher>.AssertPositionBuyButtonClickTest("Buy");
        }


        private void InvokeProcessMultipleStockBuySellByCancelAllButton()
        {
            InvokeDesktopPositionBuySellTabLoad();
            StockTraderRIAssertion<WpfAppLauncher>.AssertProcessMultipleStockBuySellByCancelAllButton();
        }
        #endregion
    }
}
