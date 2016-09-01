// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.UIAWrapper;
using AcceptanceTestLibrary.ApplicationHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Windows.Automation;
using StateBasedNavigation.Tests.AcceptanceTest.TestEntities.Page;

namespace StateBasedNavigation.Tests.AcceptanceTest.TestEntities.Assertion
{
    public static class StateBasedNavigation_Assertion<TApp>
        where TApp : AppLauncherBase, new()
    {
        // Time in milliseconds to wait
        static int TIMEWAIT = 1500; 

        #region Desktop
        /// <summary>
        /// Validations on State-Based Navigation App launch
        /// </summary>
        public static void StateBasedNavigation_OnLoad()
        {
            //Check that List button is loaded
            AutomationElement aeListButton = StateBasedNavigationPage<TApp>.ListButton;
            Assert.IsNotNull(aeListButton, "List Button is not loaded");

            //Check that Avatars button is loaded
            Assert.IsFalse(StateBasedNavigationPage<TApp>.AvatarButton.Current.IsOffscreen, "Avatar button is not loaded");
            AutomationElementCollection aeFriendsList = StateBasedNavigationPage<TApp>.FriendList; 
            Assert.IsTrue(aeFriendsList.Count > 0, "Friends list is not loaded");
            foreach (AutomationElement aeFriend in aeFriendsList)
            {
                Assert.IsFalse(aeFriend.Current.IsOffscreen, "Friend " + aeFriend.Current.Name + "is not visible");
            }
           
            //Check that Combobox with Available and Unavailable items is loaded
            AutomationElementCollection aeComboItems = StateBasedNavigationPage<TApp>.ComboBoxItems;
            Assert.IsTrue(aeComboItems.Count == 2, "Available Combo box Item is not loaded");
            Assert.IsTrue(aeComboItems[0].Current.Name.Equals("Available"), "Available selection is unavailable");
            Assert.IsTrue(aeComboItems[1].Current.Name.Equals("Unavailable"), "Unavailable selection is unavailable");            
        }

        /// <summary>
        /// Validations on clicking avatars button
        /// </summary>
        public static void StateBasedNavigation_ClickAvatars()
        {
            //Select available from combobox
            AutomationElementCollection aeComboItems = StateBasedNavigationPage<TApp>.ComboBoxItems;
            aeComboItems[0].Select();
            Thread.Sleep(TIMEWAIT);

            //Find Avatas button and click it
            AutomationElement aeAvatarButton = StateBasedNavigationPage<TApp>.AvatarButton;
            aeAvatarButton.Select();
            Thread.Sleep(TIMEWAIT);

            //Check that Avatar view is loaded
            AutomationElement aeAvatarsView = StateBasedNavigationPage<TApp>.AvatarView;
            Assert.IsNotNull(aeAvatarsView, "Avatar View is not loaded");  
        }

        /// <summary>
        /// Validations on selecting Unavailable From Combobox
        /// </summary>
        public static void StateBasedNavigation_SelectUnavailable()
        {
            //Select unavailable from combobox
            AutomationElementCollection aeComboItems = StateBasedNavigationPage<TApp>.ComboBoxItems;
            aeComboItems[1].Select();
            Thread.Sleep(TIMEWAIT);
            //Check each item in friends list is disabled.
            foreach (AutomationElement aeFriend in StateBasedNavigationPage<TApp>.FriendList)
            {
                Assert.IsFalse(aeFriend.Current.IsEnabled, aeFriend.Current.Name + " Icon is available");
            }
            //Check that list and Avatar buttons are disabled
            AutomationElement aeListButton = StateBasedNavigationPage<TApp>.ListButton;
            Assert.IsFalse(aeListButton.Current.IsEnabled, "List button is enabled");
            AutomationElement aeAvatarButton = StateBasedNavigationPage<TApp>.AvatarButton;
            Assert.IsFalse(aeAvatarButton.Current.IsEnabled, "Avatar button is enabled");

            AutomationElement aeDisconnectedImage = StateBasedNavigationPage<TApp>.DisconnectedImage;
            Assert.IsFalse(aeDisconnectedImage.Current.IsOffscreen, "Image is not loaded on disconnection");

        }

        /// <summary>
        /// Validations on clicking "Show Details" 
        /// </summary>
        public static void StateBasedNavigation_ClickDetails()
        {
            //Clicks on Show Details of a friend in list
            AssertClickDetails();
            //Check Details view and send message button are available 
            AutomationElement aeDetailsHeading = StateBasedNavigationPage<TApp>.DetailsHeading;
            Assert.IsFalse(aeDetailsHeading.Current.IsOffscreen, "Details are not loaded");

            AutomationElement aeFriendName = StateBasedNavigationPage<TApp>.FriendImage;
            Assert.IsFalse(aeFriendName.Current.IsOffscreen, "Friend Image is not displayed in details");

            AutomationElement aeSendMessage = StateBasedNavigationPage<TApp>.SendMessageButton;
            Assert.IsFalse(aeSendMessage.Current.IsOffscreen, "Send Message button is not loaded");
           
        }

        public static void StateBasedNavigation_ClickDetailsInAvatarView()
        {
            AssertClickDetailsInAvatarsView();
            //Check Details view and send message button are available 
            AutomationElement aeDetailsHeading = StateBasedNavigationPage<TApp>.DetailsHeading;
            Assert.IsFalse(aeDetailsHeading.Current.IsOffscreen, "Details are not loaded");

            //Check if the Friends Name is Displayed in Detail View
            AutomationElement aeFriendName = StateBasedNavigationPage<TApp>.FriendImage;
            Assert.IsFalse(aeFriendName.Current.IsOffscreen, "Friend Name is not displayed in details");

            AutomationElement aeSendMessage = StateBasedNavigationPage<TApp>.SendMessageButton;
            Assert.IsFalse(aeSendMessage.Current.IsOffscreen, "Send Message button is not loaded");
        }
        /// <summary>
        /// Validations On clicking "Send Message" button
        /// </summary>
        public static void StateBasedNavigation_SendMessage()
        {
            //Clicks on Show Details of a friend in list
            AssertClickDetails();
            //Click on send message button
            AutomationElement aeSendMessage = StateBasedNavigationPage<TApp>.SendMessageButton;
            Assert.IsNotNull(aeSendMessage, "Send Message button is not loaded");
            aeSendMessage.Click();
            Thread.Sleep(TIMEWAIT);

            //Check that send message child window is loaded
            AutomationElement aeSendMessageWindow = StateBasedNavigationPage<TApp>.SendMessageWindow;
            Assert.IsNotNull(aeSendMessageWindow, "Send Message window is not found");

            //Type text  in send message text box 
            AutomationElement aeMessageTextBox = aeSendMessageWindow.GetHandleById<TApp>("TextBoxMessage");
            Assert.IsNotNull(aeMessageTextBox, "Send Message Window Message Textbox not loaded.");
            aeMessageTextBox.SetFocus();
            aeMessageTextBox.SetValue(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("MessageText"));
            Thread.Sleep(TIMEWAIT);

            //Click OK button displayed in Send Message Popup Window
            AutomationElement aeSendButton = aeSendMessageWindow.GetHandleById<TApp>("SendMessageOkButton");
            Assert.IsNotNull(aeSendButton, "Send Message Window Send button not loaded.");
            aeSendButton.Click();
            Thread.Sleep(TIMEWAIT);
         
            AutomationElement aeSendingProgressbar = StateBasedNavigationPage<TApp>.SendingProgressbar;
            Assert.IsNotNull(aeSendingProgressbar, "Sending message progress bar is not shown");
        }

     
        #endregion

        /// <summary>
        /// Clicks on show details button of a friend in list
        /// </summary>
        public static void AssertClickDetails()
        {
            //Select available from combobox
            AutomationElementCollection aeComboItems = StateBasedNavigationPage<TApp>.ComboBoxItems;
            aeComboItems[0].Select();
            Thread.Sleep(TIMEWAIT);

            AutomationElementCollection aeFriendsList = StateBasedNavigationPage<TApp>.FriendList;
            // Select 3rd item in Friends list
            aeFriendsList[2].Select();
            Thread.Sleep(TIMEWAIT);           

            //Find Show Details button and click on it
            AutomationElement aeContactDetails = StateBasedNavigationPage<TApp>.ContactDetails;     
            aeContactDetails.Click();
            Thread.Sleep(TIMEWAIT);
            
        }

        /// <summary>
        /// Clicks on show details button of a friend in list
        /// </summary>
        public static void AssertClickDetailsInAvatarsView()
        {
            //Select available from combobox
            AutomationElementCollection aeComboItems = StateBasedNavigationPage<TApp>.ComboBoxItems;
            aeComboItems[0].Select();
            Thread.Sleep(TIMEWAIT);

            AutomationElementCollection aeFriendsList = StateBasedNavigationPage<TApp>.AvatarViewFriends;

            //Right click 3rd item in Friends list
            aeFriendsList[2].Select();
            Thread.Sleep(TIMEWAIT);          

            //Find Show Details button and click on it
            AutomationElement aeContactDetails = StateBasedNavigationPage<TApp>.ContactDetails;
            aeContactDetails.Click();
            Thread.Sleep(TIMEWAIT);
           

        }
    }
}
