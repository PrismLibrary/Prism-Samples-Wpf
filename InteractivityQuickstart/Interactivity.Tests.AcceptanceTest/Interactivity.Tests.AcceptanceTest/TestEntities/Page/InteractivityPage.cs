// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.TestEntityBase;
using AcceptanceTestLibrary.ApplicationHelper;
using System.Windows.Automation;


namespace Interactivity.Tests.AcceptanceTest.TestEntities.Page
{
    public static class InteractivityPage<TApp>
       where TApp : AppLauncherBase, new()
    {
        #region Desktop
        public static AutomationElement Window
        {
            get { return PageBase<TApp>.Window; }
            set { PageBase<TApp>.Window = value; }
        }

        public static AutomationElement ItemSelectionButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("ItemSelectionButton"); }
        }

        public static AutomationElement NotificationButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("NotificationButton"); }
        }

        public static AutomationElement CustomPopupButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("CustomPopupButton"); }
        }

        public static AutomationElement ConfirmationButton
        {
            get { return PageBase<TApp>.FindControlByAutomationId("ConfirmationButton"); }
        }

        public static AutomationElement InteractivityTabControl
        {
            get { return PageBase<TApp>.FindControlByAutomationId("InteractivityTabControl"); }
        }

        public static AutomationElement ResultTextBlock
        {
            get { return PageBase<TApp>.FindControlByAutomationId("ResultTextBlock"); }
        }


        public static AutomationElementCollection InteractivityTabItems
        { 
              get
            {

                PropertyCondition cond1 = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
                return InteractivityTabControl.FindAll(TreeScope.Descendants, cond1);
            }
                
        }

        public static AutomationElement DefaultNotificationWindow
        {
            get
            {
                CacheRequest cacheRequest = new CacheRequest();
                cacheRequest.Add(AutomationElement.NameProperty);
                cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;

                AutomationElement notificationWindow;
                using (cacheRequest.Activate())
                {
                    AutomationElement ae1 = AutomationElement.RootElement;
                    var pc = new PropertyCondition(AutomationElement.NameProperty, "Notification");
                    notificationWindow = ae1.FindFirst(TreeScope.Children, pc);
                }
                return notificationWindow;
            }
        }

          public static AutomationElement DefaultConfirmationWindow
        {
            get
            {
                CacheRequest cacheRequest = new CacheRequest();
                cacheRequest.Add(AutomationElement.NameProperty);
                cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;

                AutomationElement confirmationWindow;
                using (cacheRequest.Activate())
                {
                    AutomationElement ae1 = AutomationElement.RootElement;
                    var pc = new PropertyCondition(AutomationElement.NameProperty, "Confirmation");
                    confirmationWindow = ae1.FindFirst(TreeScope.Children, pc);
                }
                return confirmationWindow;
            }
        }

          public static AutomationElement CustomPopupWindow
          {
              get
              {
                  CacheRequest cacheRequest = new CacheRequest();
                  cacheRequest.Add(AutomationElement.NameProperty);
                  cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;

                  AutomationElement customPopupWindow;
                  using (cacheRequest.Activate())
                  {
                      AutomationElement ae1 = AutomationElement.RootElement;
                      var pc = new PropertyCondition(AutomationElement.NameProperty, "Custom Popup");
                      customPopupWindow = ae1.FindFirst(TreeScope.Children, pc);
                  }
                  return customPopupWindow;
              }
          }

          public static AutomationElement ItemSelectionPopupWindow
          {
              get
              {
                  CacheRequest cacheRequest = new CacheRequest();
                  cacheRequest.Add(AutomationElement.NameProperty);
                  cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;

                  AutomationElement itemSelectionPopupWindow;
                  using (cacheRequest.Activate())
                  {
                      AutomationElement ae1 = AutomationElement.RootElement;
                      var pc = new PropertyCondition(AutomationElement.NameProperty, "Items");
                      itemSelectionPopupWindow = ae1.FindFirst(TreeScope.Children, pc);
                  }
                  return itemSelectionPopupWindow;
              }
          }

        public static AutomationElementCollection InvokeCommandList
          {
              get { return PageBase<TApp>.FindControlByType("InvokeCommandItemList"); }
          }

        public static AutomationElement SelectedItemTextBlock
        {
            get
            {
                return PageBase<TApp>.FindControlByAutomationId("SelectedItemTextBlock");
            }
        }
        #endregion

    }
}

