// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using AcceptanceTestLibrary.Common.Desktop;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.UIAWrapper;

namespace AcceptanceTestLibrary.ApplicationHelper
{
    public static class UIAExtensions
    {
        private static List<AutomationElement> automationElementList = new List<AutomationElement>();
        #region Find Element Methods

        public static AutomationElement FindElementById(this AutomationElement ae, string automationId)
        {
            if (ae == null || String.IsNullOrEmpty(automationId))
            {
                throw new InvalidOperationException("invalid operation");
            }

            // Set a property condition that will be used to find the control.
            Condition c = new PropertyCondition(
                AutomationElement.AutomationIdProperty,
                automationId,
                PropertyConditionFlags.IgnoreCase);

            // Find the element.
            TreeWalker tw = new TreeWalker(c);
            return tw.GetFirstChild(ae);
        }

        public static AutomationElementCollection FindAllChildElements(this AutomationElement ae, AutomationElement parent)
        {
            if (parent == null || ae == null)
            {
                throw new InvalidOperationException("invalid operation");
            }
            Condition c1 = new PropertyCondition(AutomationElement.IsControlElementProperty, true);       
          // Find all children that match the specified conditions.
            return parent.FindAll(TreeScope.Children, c1);
        }

        public static AutomationElementCollection FindElementByControlType(this AutomationElement ae, string controlType)
        {
            Condition c = null;
            AutomationElement elementList = null;            
            if (ae == null || String.IsNullOrEmpty(controlType))
            {
                throw new InvalidOperationException("invalid operation");
            }
            // Set up the CacheRequest.
            CacheRequest cacheRequest = new CacheRequest();
            cacheRequest.Add(AutomationElement.ControlTypeProperty);
            cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;

            using (cacheRequest.Activate())
            {
                switch(controlType)
                {
                    case "List":
                // Set a property condition that will be used to find the control.
                        c = new PropertyCondition(
                        AutomationElement.ControlTypeProperty, ControlType.List);
                        elementList = ae.FindFirst(TreeScope.Descendants, c);
                        break;
                    case "Tree":
                        // Set a property condition that will be used to find the control.
                        c = new PropertyCondition(
                        AutomationElement.ControlTypeProperty, ControlType.Tree);
                        elementList = ae.FindFirst(TreeScope.Descendants, c);
                        break;
                    case "Grid":
                        // Set a property condition that will be used to find the control.
                        c = new PropertyCondition(
                        AutomationElement.ControlTypeProperty, ControlType.DataGrid);
                        elementList = ae.FindFirst(TreeScope.Descendants, c);
                        break;
                    case "Tab":
                        // Set a property condition that will be used to find the control.
                        c = new PropertyCondition(
                        AutomationElement.ControlTypeProperty, ControlType.Tab);
                        elementList = ae.FindFirst(TreeScope.Descendants, c);
                        break;
                    case "Combo":
                        // Set a property condition that will be used to find the control.
                        c = new PropertyCondition(
                        AutomationElement.ControlTypeProperty, ControlType.ComboBox);
                        elementList = ae.FindFirst(TreeScope.Descendants, c);
                        elementList.Expand();
                        return elementList.FindAllChildElements(elementList);
                      
                    default:
                        break;
               
                }

                // Find the element.
                return elementList.CachedChildren;
            }

        }

       
        public static AutomationElementCollection FindAllElementsById(this AutomationElement ae, string automationid)
        {
            if (ae == null || String.IsNullOrEmpty(automationid))
            {
                throw new InvalidOperationException("invalid operation");
            }

            // Set a property condition that will be used to find the control.
            Condition c = new PropertyCondition(
                AutomationElement.AutomationIdProperty,
                automationid,
                PropertyConditionFlags.IgnoreCase);

            // Find the element.          
            return ae.FindAll(TreeScope.Descendants, c);
        }
        public static AutomationElement FindElementByContent(this AutomationElement ae, string content)
        {
            if (ae == null || String.IsNullOrEmpty(content))
            {
                throw new InvalidOperationException("invalid operation");
            }

            // Set a property condition that will be used to find the control.
            Condition c = new PropertyCondition(
                AutomationElement.NameProperty,
                content);          
            // Find the element.
            TreeWalker tw = new TreeWalker(c);
            return tw.GetFirstChild(ae);
        }

        public static AutomationElementCollection FindAllElementsByContent(this AutomationElement ae, string content)
        {
            if (ae == null || String.IsNullOrEmpty(content))
            {
                throw new InvalidOperationException("invalid operation");
            }

            // Set a property condition that will be used to find the control.
            Condition c = new PropertyCondition(
                AutomationElement.NameProperty,
                content);
            // Find the element.
            return ae.FindAll(TreeScope.Descendants, c);
        }
        public static List<AutomationElement> FindSiblingsInTreeByName(this AutomationElement rootElement, string name)
        {
            // Clear the automation element list.
            automationElementList.Clear();
            AutomationElement parentElement = rootElement;

            WalkThroughRawTreeAndPopulateAEList(parentElement, name);

            return automationElementList;
        }

        public static AutomationElement SearchInRawTreeByName(this AutomationElement rootElement, string name)
        {
            AutomationElement elementNode = TreeWalker.RawViewWalker.GetFirstChild(rootElement);

            while (elementNode != null)
            {
                if (name.Equals(elementNode.Current.Name, StringComparison.OrdinalIgnoreCase)
                      || (name.Equals(elementNode.Current.AutomationId, StringComparison.OrdinalIgnoreCase)))
                {
                    return elementNode;
                }
                AutomationElement returnedAutomationElement = elementNode.SearchInRawTreeByName(name);
                if (returnedAutomationElement != null)
                {
                    return returnedAutomationElement;
                }
                elementNode = TreeWalker.RawViewWalker.GetNextSibling(elementNode);
            }
            return null;
        }
        #endregion

        #region Get Handle Methods

        public static AutomationElement GetHandleById(this AutomationElement ae, string controlId)
        {
            if (ae == null || String.IsNullOrEmpty(controlId))
            {
                throw new InvalidOperationException("invalid operation");
            }

            return ae.FindElementById(
                new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue(controlId));
        }

        public static AutomationElementCollection GetHandleByParent(this AutomationElement ae, AutomationElement parent)
        {
            if (parent == null || ae == null)
            {
                throw new InvalidOperationException("invalid operation");
            }

            return ae.FindAllChildElements(parent);
        }

      

        public static AutomationElementCollection GetHandleByControlType(this AutomationElement ae, string controlType)
        {
            if (ae == null || String.IsNullOrEmpty(controlType))
            {
                throw new InvalidOperationException("invalid operation");
            }

            return ae.FindElementByControlType(
                new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue(controlType));
        }

        public static AutomationElementCollection GetAllHandlesById(this AutomationElement ae, string controlId)
        {
            if (ae == null || String.IsNullOrEmpty(controlId))
            {
                throw new InvalidOperationException("invalid operation");
            }

            return ae.FindAllElementsById(
                new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue(controlId));
        }
      

        public static AutomationElement GetHandleById<TApp>(this AutomationElement ae, string controlId)
            where TApp : AppLauncherBase
        {
            if (ae == null || String.IsNullOrEmpty(controlId))
            {
                throw new InvalidOperationException("invalid operation");
            }

            controlId = GetAppSpecificString<TApp>(controlId);
            return ae.GetHandleById(controlId);
        }

        public static AutomationElementCollection GetHandleByParent<TApp>(this AutomationElement ae, AutomationElement parent)
          where TApp : AppLauncherBase
        {
            if (ae == null || parent == null)
            {
                throw new InvalidOperationException("invalid operation");
            }

          //  controlId = GetAppSpecificString<TApp>(controlId);
            return ae.GetHandleByParent(parent);
        }

        public static AutomationElementCollection GetHandleByControlType<TApp>(this AutomationElement ae, string controlType)
            where TApp : AppLauncherBase
        {
            if (ae == null || String.IsNullOrEmpty(controlType))
            {
                throw new InvalidOperationException("invalid operation");
            }

            controlType = GetAppSpecificString<TApp>(controlType);
            return ae.GetHandleByControlType(controlType);
        }       
        public static AutomationElementCollection GetAllHandlesById<TApp>(this AutomationElement ae, string controlId)
          where TApp : AppLauncherBase
        {
            if (ae == null || String.IsNullOrEmpty(controlId))
            {
                throw new InvalidOperationException("invalid operation");
            }

            controlId = GetAppSpecificString<TApp>(controlId);
            return ae.GetAllHandlesById(controlId);
        }
        public static AutomationElement GetHandleByContent(this AutomationElement ae, string content)
        {
            if (ae == null || String.IsNullOrEmpty(content))
            {
                throw new InvalidOperationException("invalid operation");
            }

            return ae.FindElementByContent(
                new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue(content));
        }

        public static AutomationElementCollection GetAllHandlesByContent(this AutomationElement ae, string content)
        {
            if (ae == null || String.IsNullOrEmpty(content))
            {
                throw new InvalidOperationException("invalid operation");
            }

            return ae.FindAllElementsByContent(
                new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue(content));
        }

        public static AutomationElement GetHandleByContent<TApp>(this AutomationElement ae, string content)
            where TApp : AppLauncherBase
        {
            if (ae == null || String.IsNullOrEmpty(content))
            {
                throw new InvalidOperationException("invalid operation");
            }

            content = GetAppSpecificString<TApp>(content);
            return ae.GetHandleByContent(content);
        }

        public static AutomationElementCollection GetAllHandlesByContent<TApp>(this AutomationElement ae, string content)
           where TApp : AppLauncherBase
        {
            if (ae == null || String.IsNullOrEmpty(content))
            {
                throw new InvalidOperationException("invalid operation");
            }

            content = GetAppSpecificString<TApp>(content);
            return ae.GetAllHandlesByContent(content);
        }

        #endregion

        #region Private Helper Methods

        private static void WalkThroughRawTreeAndPopulateAEList(AutomationElement parentElement, string name)
        {
            AutomationElement element = SearchInRawTreeByName(parentElement, name);
            AutomationElement elementNode = null;
            if (element != null)
            {
                // Add the element to the list;
                automationElementList.Add(element);

                elementNode = TreeWalker.RawViewWalker.GetNextSibling(element);
                while (elementNode != null)
                {
                    // Add the elementNode to the list
                    if (elementNode.Current.AutomationId.Equals(name))
                        automationElementList.Add(elementNode);
                    else
                    {
                        WalkThroughRawTreeAndPopulateAEList(elementNode, name);
                    }
                    elementNode = TreeWalker.RawViewWalker.GetNextSibling(elementNode);
                }
            }
        }

        /// <summary>
        /// Private helper method for modifying the string (resource file key string) based on the app type
        /// </summary>
        /// <typeparam name="TApp"></typeparam>
        /// <param name="controlId"></param>
        /// <returns></returns>
        private static string GetAppSpecificString<TApp>(string controlId)
            where TApp : AppLauncherBase
        {
            if (typeof(WpfAppLauncher).Equals(typeof(TApp)))
            {
                controlId += "_Wpf";
            }
        
            return controlId;
        }

        #endregion
    }
}
