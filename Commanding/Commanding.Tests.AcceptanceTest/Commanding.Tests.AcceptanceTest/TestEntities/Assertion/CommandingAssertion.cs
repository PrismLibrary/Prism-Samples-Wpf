// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.UIAWrapper;
using AcceptanceTestLibrary.ApplicationHelper;
using System.Windows.Automation;
using Commanding.Tests.AcceptanceTest.TestEntities.Page;
using System.Globalization;
using System.Threading;

namespace Commanding.Tests.AcceptanceTest.TestEntities.Assertion
{

    public static class CommandingAssertion<TApp>
        where TApp : AppLauncherBase, new()
    {
        // Time in milliseconds to wait
        static int TIMEWAIT = 1000; 

        #region Desktop
        public static void AssertDesktopControlLoad()
        {
            AutomationElementCollection aeorderViews = CommandingPage<TApp>.aeOrderListView;
            aeorderViews[0].Select();
            Thread.Sleep(TIMEWAIT);

            //const int initialOrdersCount = 3;
            Assert.IsNotNull(CommandingPage<TApp>.SaveAllToolBarButton,"Save All Button Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.SaveButton, "Save Button Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.DateLabel, "Delivery Date Label Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.DeliveryDateTextBox, "Delivery Date TextBox Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.OrderNameLabel, "Order Name Label Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.PriceLabel, "Price Label Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.PriceTextBox, "Price Text Box Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.QuantityTextBox, "Quantity Text Box Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.QuantityLabel, "Quantity Label Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.ShippingTextBox, "Shipping Text Box Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.ShippingLabel, "Shipping Label Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.TotalTextBox, "Total Text Box Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.TotalLabel, "Total Label Not loaded");
            Assert.IsNotNull(CommandingPage<TApp>.OrderListView,"OrderListView is Null");
            //Assert.AreEqual(CommandingPage<TApp>.OrderListView.Count, initialOrdersCount,"Row Count of the List View Not Equal");
        }

        public static void AssertProcessOrderByClickingSave()
        {
            AutomationElementCollection aeorderViews = CommandingPage<TApp>.aeOrderListView;
            aeorderViews[0].Select();
            Thread.Sleep(TIMEWAIT);

            PopulateOrderDetailsWithData();
            AutomationElementCollection orderView = CommandingPage<TApp>.OrderListView;
            AutomationElement saveButton = CommandingPage<TApp>.SaveButton;
            System.Threading.Thread.Sleep(TIMEWAIT);
            int orderCount = orderView.Count;
            //check if the save button is enabled
            Assert.IsTrue(saveButton.Current.IsEnabled, "Save Button Disabled for valid Order Details");
            //click on the save button
            saveButton.Click();
            AutomationElementCollection orderView1 = CommandingPage<TApp>.OrderListView;
            Assert.AreEqual(orderView1.Count, orderCount - 1, "ProcessOrderByClickingSave Failed as the count of Orders is not decreased");
        }

        public static void AssertAttemptSaveAfterMakingAnOrderInvalid()
        {
            AutomationElementCollection aeorderViews = CommandingPage<TApp>.aeOrderListView;
            aeorderViews[0].Select();
            Thread.Sleep(TIMEWAIT);

            //Populate the order details fileds with valid data
            PopulateOrderDetailsWithData();

            //Get the hanlde of the save button
            AutomationElement saveButton = CommandingPage<TApp>.SaveButton;
            //check if the save button is enabled
            Assert.IsTrue(saveButton.Current.IsEnabled,"Save Button is Disabled for Valid Order details");

            //Populate the order details fileds with invalid data
            PopulateOrderDetailsWithInvalidData();

            //check if the save button is disabled
            Assert.IsFalse(saveButton.Current.IsEnabled, "Save Button is Enabled for InValid Order details");
        }

        public static void AssertProcessMultipleOrdersByClickingToolBarSaveAll()
        {
            int count = 0;
            //Get the hanlde of the Order list view
            AutomationElementCollection orderView = CommandingPage<TApp>.OrderListView;
            int orderCount = orderView.Count;

            //Select the orders in the list view one by one and POpulate valid order details for every order
            while (count < orderCount)
            {
                orderView[count].SetFocus();              
                orderView[count].Select();
                Thread.Sleep(TIMEWAIT);
                AutomationElement orderNameLabel = CommandingPage<TApp>.OrderNameLabel;
                Assert.IsNotNull(orderNameLabel);
                PopulateOrderDetailsWithData();
                count++;
            }

            System.Threading.Thread.Sleep(TIMEWAIT);

            //Get the hanlde of the toolbar Save all button
            AutomationElement saveAllButton = CommandingPage<TApp>.SaveAllToolBarButton;

            //check if the toolbar save all button is enabled
            Assert.IsTrue(saveAllButton.Current.IsEnabled, "SaveAll Button is Disabled for Valid order details");
            //click on the tollbar save all button
            saveAllButton.Click();

            //check if all the orders are saved and removed from the listview
            AutomationElementCollection orderView1 = CommandingPage<TApp>.OrderListView;

            Assert.AreEqual(orderView1.Count.ToString(CultureInfo.InvariantCulture), new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("DefaultData"),"Save All Button Functionality Dosent works for Valid Order details.");
        }

        public static void AssertAttemptToolBarSaveAllForMultipleValidOrdersAndOneInvalidOrder()
        {
            int count = 0;

            //Get the hanlde of the order list view
            AutomationElementCollection orderView = CommandingPage<TApp>.OrderListView;
            int orderCount = orderView.Count;

            //Select the orders in the list view one by one and POpulate valid order details for every order
            while (count < orderCount)
            {
                orderView[count].SetFocus();
                orderView[count].Select();
                Thread.Sleep(TIMEWAIT);                            
                AutomationElement orderNameLabel = CommandingPage<TApp>.OrderNameLabel;
                Assert.IsNotNull(orderNameLabel,"Order Name is Null");
                PopulateOrderDetailsWithData();
                count++;
            }

            System.Threading.Thread.Sleep(TIMEWAIT);

            //Get the hanlde of te toolbar save all button
            AutomationElement saveAllButton = CommandingPage<TApp>.SaveAllToolBarButton;

            //check if the toolbar save all button is enabled
            Assert.IsTrue(saveAllButton.Current.IsEnabled, "SaveAll Button is Disabled for Valid order details");

            //POpulate the selected ordere details with invalid data
            PopulateOrderDetailsWithInvalidData();

            //check if the toolbar save all button is disabled
            Assert.IsFalse(saveAllButton.Current.IsEnabled,"SaveAll Button is Enabled for Invalid order details");
        }

        public static void AssertDesktopSaveButton()
        {
            Assert.IsNull(CommandingPage<TApp>.SaveButton, "Save Button Enabled Even after all orders were Saved");
        }

        public static void AssertDesktopAttemptSaveAfterChangingQuantityNull()
        {
            AutomationElementCollection aeorderViews = CommandingPage<TApp>.aeOrderListView;
            aeorderViews[0].Select();
            Thread.Sleep(TIMEWAIT);
            PopulateOrderDetailsWithData();
            CommandingPage<TApp>.QuantityTextBox.SetValue(string.Empty);
            CommandingPage<TApp>.PriceTextBox.SetFocus();
            Assert.IsFalse(CommandingPage<TApp>.SaveButton.Current.IsEnabled, "Save Button Enabled for Null Quantity");
        }
        public static void AssertDesktopAttemptSaveAfterChangingPriceNull()
        {
            AutomationElementCollection aeorderViews = CommandingPage<TApp>.aeOrderListView;
            aeorderViews[0].Select();
            Thread.Sleep(TIMEWAIT);

            PopulateOrderDetailsWithData();
            CommandingPage<TApp>.PriceTextBox.SetValue(string.Empty);
            CommandingPage<TApp>.QuantityTextBox.SetFocus();
            Assert.IsFalse(CommandingPage<TApp>.SaveButton.Current.IsEnabled, "Save Button Enabled for Null Price");
        }
        #endregion

        #region Desktop Private Methods
        private static void PopulateOrderDetailsWithData()
        {
            CommandingPage<TApp>.QuantityTextBox.SetValue(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("DefaultQuantity"));
            CommandingPage<TApp>.PriceTextBox.SetValue(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("DefaultPrice"));
            CommandingPage<TApp>.ShippingTextBox.SetValue(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("DefaultShipping"));
        }

        private static void PopulateOrderDetailsWithInvalidData()
        {
            CommandingPage<TApp>.QuantityTextBox.SetValue(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("InvalidQuantity"));
            CommandingPage<TApp>.PriceTextBox.SetValue(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("InvalidPrice"));
        }
        #endregion
    }
}
