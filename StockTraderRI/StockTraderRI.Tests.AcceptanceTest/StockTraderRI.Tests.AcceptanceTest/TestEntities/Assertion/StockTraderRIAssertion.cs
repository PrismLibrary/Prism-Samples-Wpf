// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcceptanceTestLibrary.Common;
using System.Windows.Automation;
using System.Windows.Controls;
using StockTraderRI.Tests.AcceptanceTest.TestEntities.Page;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestLibrary.ApplicationHelper;
using System.Windows.Forms;
using System.Globalization;
using AcceptanceTestLibrary.UIAWrapper;
using StockTraderRI.Tests.AcceptanceTest.TestInfrastructure;
using System.Threading;

namespace StockTraderRI.Tests.AcceptanceTest.TestEntities.Assertion
{
    public static class StockTraderRIAssertion<TApp>
        where TApp : AppLauncherBase, new()
    {
        #region Position Summary Module Assert
        private static TestDataInfrastructure testDataInfrastructure = new TestDataInfrastructure();
        public static void AssertPositionSummaryTab()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.PositionSummaryTab, "Position Summary tab is not loaded.");
        }

        public static void AssertPositionSummaryGrid()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.PositionSummaryGrid, "Position Summary grid is not loaded.");
        }

        public static void AssertPositionSummaryColumnCount()
        {
            GridPattern gridPattern = GetGridPattern();
            Assert.AreEqual(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("PositionSummaryColumnCount"), gridPattern.Current.ColumnCount.ToString());
        }

        public static void DesktopAssertPositionSummaryColumnCount()
        {
            GridPattern gridPattern = GetGridPattern();
            Assert.AreEqual("7", gridPattern.Current.ColumnCount.ToString());
        }

        public static void AssertPositionSummaryRowCount()
        {
            GridPattern gridPattern = GetGridPattern();
            //read number of account positions from the AccountPosition.xml data file
            string positionRowCount = new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("PositionSummaryRowCount");
            //testDataInfrastructure.GetCount<AccountPositionDataProvider, AccountPosition>();
            Assert.AreEqual(positionRowCount, gridPattern.Current.RowCount.ToString());
        }

        public static void AssertPositionSummaryValuesForSymbol()
        {
            //For each row, get the column values and test it the new value
            GridPattern gridPattern = GetGridPattern();

            string share;
            string lastPrice;
            string marketPrice;
            string gainLoss;
            string costBasis;
            string symbol;

            //Test for the number of rows displayed in the Position summary table in the UI
            for (int rowCountIndex = 0; rowCountIndex < gridPattern.Current.RowCount; rowCountIndex++)
            {
                // Get the Stock name 
                symbol = gridPattern.GetItem(rowCountIndex, 0).Current.Name; // Column 0 is for Stock

                
                //input columns
                share = double.Parse(gridPattern.GetItem(rowCountIndex, 1).Current.Name, NumberStyles.Currency).ToString(); // Column 1 is for number of share
                lastPrice = double.Parse(gridPattern.GetItem(rowCountIndex, 2).Current.Name, NumberStyles.Currency).ToString(); // Column 2 is for last price
                costBasis = double.Parse(gridPattern.GetItem(rowCountIndex, 3).Current.Name, NumberStyles.Currency).ToString(); // Column 3 is for cost

                //computed columns
                marketPrice = double.Parse(gridPattern.GetItem(rowCountIndex, 4).Current.Name, NumberStyles.Currency).ToString(); // Column 3 is for cost
                //marketPrice = gridPattern.GetItem(rowCountIndex, 4).Current.Name.Replace('$', '0'); // Column 4 is for market price for symbol
                gainLoss = gridPattern.GetItem(rowCountIndex, 5).Current.Name; // Column 5 is for Gain Loss % for symbol
                gainLoss = gainLoss.Remove(gainLoss.Length - 1);
                double tempValue;
                Assert.IsTrue(Double.TryParse(share, out tempValue), String.Format(CultureInfo.CurrentCulture, "Number of shares {0} is not numeric", symbol));
                Assert.IsTrue(Double.TryParse(lastPrice, out tempValue), String.Format(CultureInfo.CurrentCulture, "Lastprice for {0} is not numeric", symbol));
                Assert.IsTrue(Double.TryParse(costBasis, out tempValue), String.Format(CultureInfo.CurrentCulture, "Cost basis Value for {0} is not numeric", symbol));
                Assert.IsTrue(Double.TryParse(marketPrice, out tempValue), String.Format(CultureInfo.CurrentCulture, "Market price for {0} is not numeric", symbol));
                Assert.IsTrue(Double.TryParse(gainLoss, out tempValue), String.Format(CultureInfo.CurrentCulture, "Gainloss % Value for {0} is not numeric", symbol));
            }
        }

        #endregion

        #region Common Methods
        private static void InternalAssertControl(AutomationElement control, string message)
        {
            Assert.IsNotNull(control, message);
        }
        #endregion

        #region Market Trend Module Assert

        public static void AssertHistoricalDataText()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.HistoricalDataText, "Historical Data Text block is not loaded.");
        }

        public static void AssertPieChartTextBlock()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.PieChartTextBlock, "Pie Chart Text block is not loaded.");
        }
        #endregion

        #region WatchList Module Assert

        public static void AssertWatchListGrid()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.WatchListGrid, "WatchList Grid is not loaded.");
        }
        #endregion

        #region NewsArticle Module Assert

        public static void AssertNewsArticleText()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.NewsArticleText, "News Article Text block is not loaded.");
        }
        #endregion

        #region Orders ToolBar Module Assert

        public static void AssertSubmitButton()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.SubmitButton, "Submit Button in Orders Tool bar is not loaded.");
        }

        public static void AssertSubmitAllButton()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.SubmitAllButton, "Submit All Button in Orders Tool bar is not loaded.");
        }

        public static void AssertCancelButton()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.CancelButton, "Cancel Button in Orders Tool bar is not loaded.");
        }

        public static void AssertCancelAllButton()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.CancelAllButton, "Cancel All Button in Orders Tool bar is not loaded.");
        }

        public static void AssertAddtoWatchListButton()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.AddToWatchListButton, "Add to Watch List Button in Orders Tool bar is not loaded..");
        }

        public static void AssertTextBoxBlock()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.TextBoxBlock, "TextBox in Orders Tool bar is not loaded.");
        }

        public static void AssertStockAddedinWatchListTextBox()
        {
            AutomationElement aeTextBoxBlock = StockTraderRIPage<TApp>.TextBoxBlock;
            InternalAssertControl(aeTextBoxBlock, "TextBox in Orders Tool bar is not loaded.");
            aeTextBoxBlock.SetValue(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("StockName"));
            aeTextBoxBlock.SetFocus();

            SendKeys.SendWait("{ENTER}");
            System.Threading.Thread.Sleep(1000);
        }
        public static void AssertStockRemovedfromWatchListTextBox()
        {
            AutomationElement aeRemoveButton = StockTraderRIPage<TApp>.ActionsRemoveButton;
            aeRemoveButton.Click();
            GridPattern gridPattern = GetGridPatternWatchList();
            Assert.AreEqual(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("DefaultValue"), gridPattern.Current.RowCount.ToString());

        }

        public static void AssertWatchListRowCount()
        {
            GridPattern gridPattern = GetGridPatternWatchList();
            Assert.AreEqual(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("WatchListRowCount"), gridPattern.Current.RowCount.ToString());
        }

        #endregion

        #region PositionBuySellTab Assert

        public static void AssertPositionBuySellTab()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.PositionBuySellTab, "Position Buy & Sell tab is not loaded.");
        }

        public static void AssertPositionBuyButtonClickTest(string option)
        {
            if (option.Equals("Buy"))
            {
                System.Threading.Thread.Sleep(3000);
                AutomationElement ActionBuyButton = StockTraderRIPage<TApp>.ActionsBuyButton;
                ActionBuyButton.Click();
            }
            else if (option.Equals("Sell"))
            {
                System.Threading.Thread.Sleep(3000);
                AutomationElement ActionsellButton = StockTraderRIPage<TApp>.ActionsSellButton;
                ActionsellButton.Click();
            }
        }

        public static void AssertTermComboBox()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.TermComboBox, "Term ComboBox is not loaded.");
        }

        public static void AssertPriceLimitTextBox()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.PriceLimitTextBox, "PriceLimit TextBox is not loaded.");
        }

        public static void AssertSellRadioButton()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.SellRadioButton, "Sell RadioButton is not loaded.");
        }

        public static void AssertBuyRadioButton()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.BuyRadioButton, "Buy RadioButton is not loaded.");
        }

        public static void AssertSharesTextBox()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.SharesTextBox, "Shares TextBox is not loaded.");
        }

        public static void AssertSymbolTextBox()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.SymbolTextBox, "Symbol TextBox is not loaded.");
        }

        public static void AssertOrderTypeComboBox()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.OrderTypeComboBox, "OrderType ComboBox is not loaded.");
        }

        public static void AssertOrderCommandSubmit()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.OrderCommandSubmit, "OrderCommand Submit Button is not loaded.");
        }

        public static void AssertOrderCommandCancel()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.OrderCommandCancel, "Order Command Cancel Button is not loaded.");
        }

        public static void AssertOrderCommandSubmitAllButton()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.OrderCommandSubmitAllButton, "Order Command SubmitAll Button is not loaded.");
        }

        public static void AssertOrderCommandCancelAllButton()
        {
            InternalAssertControl(StockTraderRIPage<TApp>.OrderCommandCancelAllButton, "Order Command CancelAll Button is not loaded.");
        }

        public static void AssertAttemptBuySellOrderWithValidData()
        {
            SLPopulateValidStockDetails();
            System.Threading.Thread.Sleep(3000);
            AutomationElement SubmitButton = StockTraderRIPage<TApp>.OrderCommandSubmit;
            Assert.IsTrue(SubmitButton.Current.IsEnabled, "Submit Button disabled for valid Shares details");
            SubmitButton.Click();
            System.Threading.Thread.Sleep(3000);
        }

        public static void AssertAttemptBuySellOrderWithInValidData()
        {
            SLPopulateInvalidStockDetails();
            System.Threading.Thread.Sleep(3000);
            AutomationElement SubmitButton = StockTraderRIPage<TApp>.OrderCommandSubmit;
            Assert.IsFalse(SubmitButton.Current.IsEnabled, "Submit Button enabled for Invalid Shares details");
        }

        public static void AssertAttemptOrderCancelByCancelButton()
        {
            AutomationElement SharesTextBoxValue = StockTraderRIPage<TApp>.SharesTextBox;
            SharesTextBoxValue.SetValue(GetDataFromTestDataFile("DefaultShares"));
            AutomationElement PriceLimitValue = StockTraderRIPage<TApp>.PriceLimitTextBox;
            PriceLimitValue.SetValue(GetDataFromTestDataFile("DefaultPriceLimit"));
            AutomationElement TermComboBoxValue = StockTraderRIPage<TApp>.TermComboBox;
            AutomationElement CancelButton = StockTraderRIPage<TApp>.OrderCommandCancel;
            Assert.IsTrue(CancelButton.Current.IsEnabled, "Cancel Button disabled");
            CancelButton.Click();
         }

        public static void AssertProcessMultipleStockBuySellBySubmitAllButtonforValidData()
        {
            SLPopulateValidStockDetails();
            System.Threading.Thread.Sleep(3000);
            AutomationElement submitAllButton = StockTraderRIPage<TApp>.OrderCommandSubmitAllButton;
            submitAllButton.Click();
            System.Threading.Thread.Sleep(3000);
            Assert.IsFalse(submitAllButton.Current.IsEnabled, "Submit All Button Enabled  even after Submitting All the Details");

        }
        public static void AssertProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
        {
            SLPopulateValidStockDetails();

            MultipleStockSelectionforSubmitAllCancelAll();
            System.Threading.Thread.Sleep(4000);
            SLPopulateInvalidStockDetails();
            System.Threading.Thread.Sleep(4000);
            AutomationElement submitAllButton = StockTraderRIPage<TApp>.OrderCommandSubmitAllButton;
            Assert.IsFalse(submitAllButton.Current.IsEnabled, "Submit All Button Enabled  forInvalid Stock Details");

        }
        public static void AssertProcessMultipleStockBuySellByCancelAllButton()
        {
            SLPopulateValidStockDetails();

            MultipleStockSelectionforSubmitAllCancelAll();
            SLPopulateValidStockDetails();
            System.Threading.Thread.Sleep(4000);
            AutomationElement CancelAllButton = StockTraderRIPage<TApp>.OrderCommandCancelAllButton;
            CancelAllButton.Click();
            System.Threading.Thread.Sleep(4000);
            Assert.IsFalse(CancelAllButton.Current.IsEnabled, "Submit All Button Enabled  even after Submitting All the Details");
        }
        #endregion

        #region Private method GridPatterns

        private static GridPattern GetGridPatternWatchList()
        {
            return StockTraderRIPage<TApp>.WatchListGrid.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
        }
        private static GridPattern GetGridPattern()
        {
            return StockTraderRIPage<TApp>.PositionSummaryGrid.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
        }
        private static string GetDataFromTestDataFile(string keyName)
        {
            return new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue(keyName);
        }

        private static void SLPopulateValidStockDetails()
        {
            AutomationElement SharesTextBoxValue = StockTraderRIPage<TApp>.SharesTextBox;
            SharesTextBoxValue.SetValue(GetDataFromTestDataFile("DefaultShares"));
            AutomationElement PriceLimitValue = StockTraderRIPage<TApp>.PriceLimitTextBox;
            PriceLimitValue.SetValue(GetDataFromTestDataFile("DefaultPriceLimit"));
            AutomationElement TermComboBoxValue = StockTraderRIPage<TApp>.TermComboBox;
            TermComboBoxValue.Expand();
            System.Threading.Thread.Sleep(3000);
            AutomationElement TermValue = TermComboBoxValue.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, GetDataFromTestDataFile("DefaultTerm")));
            //AutomationElementCollection orderviews = aeorderView.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem));
            Assert.IsNotNull(TermValue, "Could not find items in Term combobox");

            System.Windows.Point clickablePoint = new System.Windows.Point((int)Math.Floor(TermValue.Current.BoundingRectangle.X), (int)Math.Floor(TermValue.Current.BoundingRectangle.Y));
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)clickablePoint.X, (int)clickablePoint.Y);
            Thread.Sleep(1000);
            MouseEvents.Click();
        }


        private static void SLPopulateInvalidStockDetails()
        {
            AutomationElement SharesTextBoxValue = StockTraderRIPage<TApp>.SharesTextBox;
            SharesTextBoxValue.SetValue(GetDataFromTestDataFile("DefaultInvalidShares"));
            AutomationElement PriceLimitValue = StockTraderRIPage<TApp>.PriceLimitTextBox;
            PriceLimitValue.SetValue(GetDataFromTestDataFile("DefaultInvalidPriceLimit"));
            AutomationElement TermComboBoxValue = StockTraderRIPage<TApp>.TermComboBox;
            TermComboBoxValue.Expand();
            System.Threading.Thread.Sleep(3000);
            AutomationElement TermValue = TermComboBoxValue.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, GetDataFromTestDataFile("DefaultTerm")));
            Assert.IsNotNull(TermValue, "Could not find Order item in OrderType combobox");

            System.Windows.Point clickablePoint = new System.Windows.Point((int)Math.Floor(TermValue.Current.BoundingRectangle.X), (int)Math.Floor(TermValue.Current.BoundingRectangle.Y));
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)clickablePoint.X, (int)clickablePoint.Y);
            Thread.Sleep(1000);
            MouseEvents.Click();

        }

        private static void MultipleStockSelectionforSubmitAllCancelAll()
        {
            System.Threading.Thread.Sleep(2000);
            AutomationElement PositionSummaryTabvalue = StockTraderRIPage<TApp>.PositionSummaryTab;
            System.Threading.Thread.Sleep(2000);

            System.Windows.Point clickablePoint = new System.Windows.Point((int)Math.Floor(PositionSummaryTabvalue.Current.BoundingRectangle.X), (int)Math.Floor(PositionSummaryTabvalue.Current.BoundingRectangle.Y));
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)clickablePoint.X, (int)clickablePoint.Y);
            Thread.Sleep(1000);
            MouseEvents.Click();

            System.Threading.Thread.Sleep(3000);
            AutomationElement PositionSummaryGridValue = StockTraderRIPage<TApp>.PositionSummaryGrid;

            System.Windows.Point clickablePoint1 = new System.Windows.Point((int)Math.Floor(PositionSummaryGridValue.Current.BoundingRectangle.X + 500), (int)Math.Floor(PositionSummaryGridValue.Current.BoundingRectangle.Y + 100));
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)clickablePoint1.X, (int)clickablePoint1.Y);
            Thread.Sleep(1000);
            MouseEvents.Click();
            System.Threading.Thread.Sleep(3000);
        }
        #endregion
    }
}
