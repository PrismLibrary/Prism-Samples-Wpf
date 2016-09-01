// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcceptanceTestLibrary.Common;
using System.Windows.Automation;
using AcceptanceTestLibrary.TestEntityBase;
using AcceptanceTestLibrary.ApplicationHelper;
using System.Threading;
using AcceptanceTestLibrary.UIAWrapper;

namespace StockTraderRI.Tests.AcceptanceTest.TestEntities.Page
{
    public static class StockTraderRIPage<TApp>
        where TApp : AppLauncherBase, new()
    {
        
        public static AutomationElement Window
        {
            get { return PageBase<TApp>.Window; }
            set { PageBase<TApp>.Window = value; }
        }
               
        public static AutomationElement PositionSummaryTab
        {
            get { return PageBase<TApp>.FindControlByAutomationId("PositionSummaryTab"); }
        }
        public static AutomationElement PositionSummaryGrid
        {
            get { return PageBase<TApp>.FindControlByAutomationId("PositionSummaryGrid"); }
        }
        public static AutomationElement PositionBuySellTab
        {
            get { return PageBase<TApp>.FindControlByAutomationId("PositionBuySellTab"); }
        }
        public static AutomationElement ActionsBuyButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("ActionsBuyButton"); }
        }
        public static AutomationElement ActionsSellButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("ActionsSellButton"); }
        }
        public static AutomationElement HistoricalDataText
        {
            get { return PageBase<TApp>.FindControlByAutomationId("HistoricalDataTextBlock"); }
        }
        public static AutomationElement WatchListGrid
        {
            get
            {
                AutomationElement ae = StockTraderRIPage<TApp>.PositionSummaryTab;
                System.Windows.Point clickablePoint = new System.Windows.Point((int)Math.Floor(ae.Current.BoundingRectangle.X + 150), (int)Math.Floor(ae.Current.BoundingRectangle.Y + 15));
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)clickablePoint.X, (int)clickablePoint.Y);
                Thread.Sleep(1000);
                MouseEvents.Click();
                System.Threading.Thread.Sleep(3000);
                return PageBase<TApp>.FindControlByAutomationId("WatchListGrid");
            }
        }
        public static AutomationElement ActionsRemoveButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("ActionsRemoveButton"); }
        }
        public static AutomationElement NewsArticleText
        {
            get { return PageBase<TApp>.FindControlByAutomationId("NewsArticleText"); }
        }
        public static AutomationElement PieChartTextBlock
        {
            get { return PageBase<TApp>.FindControlByAutomationId("PieChartTextBlock"); }
        }
        public static AutomationElement SubmitButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("SubmitButton"); }
        }
        public static AutomationElement SubmitAllButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("SubmitAllButton"); }
        }
        public static AutomationElement CancelButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("CancelButton"); }
        }
        public static AutomationElement CancelAllButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("CancelAllButton"); }
        }
        public static AutomationElement AddToWatchListButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("AddToWatchListButton"); }
        }
        public static AutomationElement TextBoxBlock
        {
            get { return PageBase<TApp>.FindControlByAutomationId("TextBoxBlock"); }
        }
        public static AutomationElement TermComboBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("TermComboBox"); }
        }
        public static AutomationElement PriceLimitTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("PriceLimitTextBox"); }
        }
        public static AutomationElement SellRadioButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("SellRadioButton"); }
        }
        public static AutomationElement BuyRadioButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("BuyRadioButton"); }
        }
        public static AutomationElement SharesTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("SharesTextBox"); }
        }
        public static AutomationElement SymbolTextBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("SymbolTextBox"); }
        }
        public static AutomationElement OrderTypeComboBox
        {
            get { return PageBase<TApp>.FindControlByAutomationId("OrderTypeComboBox"); }
        }
        public static AutomationElement OrderCommandSubmit
        {
            get { return PageBase<TApp>.FindControlByContent("Submit"); }
        }
        public static AutomationElement OrderCommandCancel
        {
            get { return PageBase<TApp>.FindControlByContent("Cancel"); }
        }
        public static AutomationElement OrderCommandSubmitAllButton
        {
            get { return PageBase<TApp>.FindControlByContent("SubmitAll"); }
        }
        public static AutomationElement OrderCommandCancelAllButton
        {
            get { return PageBase<TApp>.FindControlByContent("CancelAll"); }
        }
        
        
    }
}
