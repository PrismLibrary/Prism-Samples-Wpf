// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.UIAWrapper;
using AcceptanceTestLibrary.ApplicationHelper;
using System.Windows.Automation;
using Interactivity.Tests.AcceptanceTest.TestEntities.Page;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interactivity.Tests.AcceptanceTest.TestEntities.Assertion
{
    public static class InteractivityAssertion<TApp>
   where TApp : AppLauncherBase, new()
    {
        // Time in milliseconds to wait
        static int TIMEWAIT = 500; 

        # region Desktop
   
        public static void AssertIR_RaiseDefaultNotification()
        {
            AutomationElement interactivityTabControl = InteractivityPage<TApp>.InteractivityTabControl;
            AutomationElementCollection tabItems = InteractivityPage<TApp>.InteractivityTabItems;
            tabItems[1].Select();

            Assert.IsNotNull(InteractivityPage<TApp>.NotificationButton, "Raise Default Notification Button Not loaded");
            InteractivityPage<TApp>.NotificationButton.Click();

            PropertyCondition okButtonPropertyCondition = new PropertyCondition(AutomationElement.NameProperty, "OK");
            AutomationElement okButton = InteractivityPage<TApp>.DefaultNotificationWindow.FindFirst(TreeScope.Children, okButtonPropertyCondition);

            Assert.IsNotNull(okButton, "Default Notification OK Button not loaded");
            okButton.Click();

            Thread.Sleep(TIMEWAIT);

            AutomationElement resultLabel = InteractivityPage<TApp>.ResultTextBlock;
            Assert.IsNotNull(resultLabel, "Result Label not loaded");
            Assert.AreEqual("The user was notified.", resultLabel.Current.Name);
        }

        public static void AssertIR_RaiseDefaultConfirmationAcceptTest()
        {
            AutomationElement interactivityTabControl = InteractivityPage<TApp>.InteractivityTabControl;
            AutomationElementCollection tabItems = InteractivityPage<TApp>.InteractivityTabItems;
            tabItems[1].Select();

            Assert.IsNotNull(InteractivityPage<TApp>.NotificationButton, "Raise Default Confirmation Button Not loaded");
            InteractivityPage<TApp>.ConfirmationButton.Click();

            PropertyCondition acceptButtonPropertyCondition = new PropertyCondition(AutomationElement.NameProperty, "OK");
            AutomationElement acceptButton = InteractivityPage<TApp>.DefaultConfirmationWindow.FindFirst(TreeScope.Children, acceptButtonPropertyCondition);

            Assert.IsNotNull(acceptButton, "Default Confirmation Window Accept Button not loaded");
            acceptButton.Click();

            Thread.Sleep(TIMEWAIT);

            AutomationElement resultLabel = InteractivityPage<TApp>.ResultTextBlock;
            Assert.IsNotNull(resultLabel, "Result Label not loaded");
            Assert.AreEqual("The user accepted.", resultLabel.Current.Name);
        }

        public static void AssertIR_RaiseDefaultConfirmationCancelTest()
        {
            AutomationElement interactivityTabControl = InteractivityPage<TApp>.InteractivityTabControl;
            AutomationElementCollection tabItems = InteractivityPage<TApp>.InteractivityTabItems;
            tabItems[1].Select();

            Assert.IsNotNull(InteractivityPage<TApp>.NotificationButton, "Raise Default Confirmation Button Not loaded");
            InteractivityPage<TApp>.ConfirmationButton.Click();

            PropertyCondition cancelButtonPropertyCondition = new PropertyCondition(AutomationElement.NameProperty, "Cancel");
            AutomationElement cancelButton = InteractivityPage<TApp>.DefaultConfirmationWindow.FindFirst(TreeScope.Children, cancelButtonPropertyCondition);

            Assert.IsNotNull(cancelButton, "Default Confirmation Window Cancel Button not loaded");
            cancelButton.Click();

            Thread.Sleep(TIMEWAIT);

            AutomationElement resultLabel = InteractivityPage<TApp>.ResultTextBlock;
            Assert.IsNotNull(resultLabel, "Result Label not loaded");
            Assert.AreEqual("The user cancelled.", resultLabel.Current.Name);
        }
        public static void AssertIR_RaiseCustomPopupViewTest()
        {
            AutomationElement interactivityTabControl = InteractivityPage<TApp>.InteractivityTabControl;
            AutomationElementCollection tabItems = InteractivityPage<TApp>.InteractivityTabItems;
            tabItems[1].Select();

            Assert.IsNotNull(InteractivityPage<TApp>.CustomPopupButton, "Raise Custom Popup View Button not Loaded");
            InteractivityPage<TApp>.CustomPopupButton.Click();

            AutomationElement closeButton = InteractivityPage<TApp>.CustomPopupWindow.GetHandleById<TApp>("CustomPopupCloseButton");
            Assert.IsNotNull(closeButton, "Custom Popup Window Close Button not loaded");

            closeButton.Click();

            Thread.Sleep(TIMEWAIT);

            AutomationElement resultLabel = InteractivityPage<TApp>.ResultTextBlock;
            Assert.IsNotNull(resultLabel, "Result Label not loaded");
            Assert.AreEqual("", resultLabel.Current.Name);
        }
        public static void AssertIR_RaiseItemSelectionPopupCancelTest()
        {
            AutomationElement interactivityTabControl = InteractivityPage<TApp>.InteractivityTabControl;
            AutomationElementCollection tabItems = InteractivityPage<TApp>.InteractivityTabItems;
            tabItems[1].Select();

            Assert.IsNotNull(InteractivityPage<TApp>.ItemSelectionButton, "Raise Item Selection Popup Button not Loaded");
            InteractivityPage<TApp>.ItemSelectionButton.Click();

            AutomationElement cancelButton = InteractivityPage<TApp>.ItemSelectionPopupWindow.GetHandleById<TApp>("ItemsCancelButton");
            Assert.IsNotNull(cancelButton, "Item Selecction Popup Window Cancel Button not loaded");

            cancelButton.Click();

            Thread.Sleep(TIMEWAIT);

            AutomationElement resultLabel = InteractivityPage<TApp>.ResultTextBlock;
            Assert.IsNotNull(resultLabel, "Result Label not loaded");
            Assert.AreEqual("The user cancelled the operation or didn't select an item.", resultLabel.Current.Name);
        }

        public static void AssertIR_RaiseItemSelectionPopupSelectTest()
        {
            AutomationElement interactivityTabControl = InteractivityPage<TApp>.InteractivityTabControl;
            AutomationElementCollection tabItems = InteractivityPage<TApp>.InteractivityTabItems;
            tabItems[1].Select();

            Assert.IsNotNull(InteractivityPage<TApp>.ItemSelectionButton, "Raise Item Selection Popup Button not Loaded");
            InteractivityPage<TApp>.ItemSelectionButton.Click();

            AutomationElement itemSelectionPopupWindow = InteractivityPage<TApp>.ItemSelectionPopupWindow;
            AutomationElementCollection itemsList = itemSelectionPopupWindow.GetHandleByControlType<TApp>("ItemsList");

            Assert.IsTrue(itemsList.Count > 0, "Items in the list box of Item Selection Popup window is empty");
            itemsList[1].Select();

            Thread.Sleep(TIMEWAIT);

            AutomationElement selectButton = itemSelectionPopupWindow.GetHandleById<TApp>("ItemsSelectButton");
            Assert.IsNotNull(selectButton, "Item Selecction Popup Window Select Button not loaded");

            selectButton.Click();

            Thread.Sleep(TIMEWAIT);

            AutomationElement resultLabel = InteractivityPage<TApp>.ResultTextBlock;
            Assert.IsNotNull(resultLabel, "Result Label not loaded");
            Assert.AreEqual("The user selected: Item2", resultLabel.Current.Name);
        }
        public static void AssertInvokeCommandActionViewTest()
        {
            AutomationElement interactivityTabControl = InteractivityPage<TApp>.InteractivityTabControl;
            AutomationElementCollection tabItems = InteractivityPage<TApp>.InteractivityTabItems;
            tabItems[2].Select();

            AutomationElementCollection itemsList = InteractivityPage<TApp>.InvokeCommandList;
            

            Assert.IsTrue(itemsList.Count > 0, "Items in the list box of InvokeCommand View is empty");
            itemsList[1].Select();

            Thread.Sleep(TIMEWAIT);

            AutomationElement resultLabel = InteractivityPage<TApp>.SelectedItemTextBlock;
            Assert.IsNotNull(resultLabel, "Selected Result Label not loaded");
            Assert.AreEqual("Item2", resultLabel.Current.Name);
        }

    
        # endregion


    }

}