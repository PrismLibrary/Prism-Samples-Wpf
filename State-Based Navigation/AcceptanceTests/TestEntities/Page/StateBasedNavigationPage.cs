// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using AcceptanceTestLibrary.Common;
using System.Windows.Automation;
using AcceptanceTestLibrary.TestEntityBase;
using AcceptanceTestLibrary.ApplicationHelper;

namespace StateBasedNavigation.Tests.AcceptanceTest.TestEntities.Page
{
    public static class StateBasedNavigationPage<TApp>
         where TApp : AppLauncherBase, new()
    {
        #region Desktop
        public static AutomationElement Window
        {
            get { return PageBase<TApp>.Window; }
            set { PageBase<TApp>.Window = value; }
        }
        public static AutomationElement ListButton
        {
            get
            {
                return PageBase<TApp>.FindControlByAutomationId("ListButton");
                
            }
        }

        public static AutomationElement AvatarButton
        {
            get
            {
                return PageBase<TApp>.FindControlByAutomationId("AvatarButton");
            }
        }

        public static AutomationElement SendMessageButton
        {
            get
            {
                return PageBase<TApp>.FindControlByAutomationId("SendMessageButton");
            }
        }
        public static AutomationElementCollection FriendList
        {
            get
            {
                return PageBase<TApp>.FindControlByType("FriendsList");
            }
        }

        public static AutomationElementCollection ComboBoxItems
        {
            get
            {
                return PageBase<TApp>.FindControlByType("Combo");
            }
        }

        public static AutomationElement AvatarView
        {
            get
            {
                return PageBase<TApp>.FindControlByAutomationId("AvatarsView");
            }
        }


        public static AutomationElement DisconnectedImage
        {
            get
            {
                return PageBase<TApp>.FindControlByAutomationId("DisconnectedImage");
            }
        }

        public static AutomationElement ContactDetails
        {
            get
            {
                return PageBase<TApp>.FindControlByContent("ContactDetails");
            }
        }

        public static AutomationElement DetailsHeading
        {
            get
            {
                return PageBase<TApp>.FindControlByAutomationId("DetailsHeading");
            }
        }

        public static AutomationElement FriendImage
        {
            get
            {
                return PageBase<TApp>.FindControlByAutomationId("FriendImage");
            }
        }

        public static AutomationElement SendMessageWindow
        {
            get
            {
                CacheRequest cacheRequest = new CacheRequest();
                cacheRequest.Add(AutomationElement.NameProperty);
                cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;

                AutomationElement sendMessageWindow;
                using (cacheRequest.Activate())
                {
                    AutomationElement ae1 = AutomationElement.RootElement;
                    var pc = new PropertyCondition(AutomationElement.NameProperty, "Send message to Friend #3");
                    sendMessageWindow = ae1.FindFirst(TreeScope.Children, pc);
                }

                return sendMessageWindow;
            }
        }

        public static AutomationElementCollection AvatarViewFriends
        {
            get
            {
                // Set up the CacheRequest.
                CacheRequest cacheRequest = new CacheRequest();
                cacheRequest.Add(AutomationElement.ControlTypeProperty);
                cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;


                using (cacheRequest.Activate())
                {
                    PropertyCondition cond = new PropertyCondition(AutomationElement.AutomationIdProperty,
                       new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("AvatarsView"));
                    PropertyCondition cond1 = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.List);
                    AndCondition andCond = new AndCondition(cond, cond1);
                    return Window.FindFirst(TreeScope.Descendants, andCond).CachedChildren;
                    // return PageBase<TApp>.FindControlByAutomationId("AvatarsView");
                }
            }
        }

        public static AutomationElement SendingProgressbar
        {
            get
            {
                PropertyCondition cond = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ProgressBar);
                return Window.FindFirst(TreeScope.Descendants, cond);
            }
        }

        #endregion
    }
}
