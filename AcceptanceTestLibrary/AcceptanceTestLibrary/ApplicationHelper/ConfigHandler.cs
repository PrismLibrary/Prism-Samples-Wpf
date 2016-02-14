// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace AcceptanceTestLibrary.ApplicationHelper
{
    /// <summary>
    /// Class use for handling the application config file
    /// </summary>
    public static class ConfigHandler
    {
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? String.Empty;
        }

        public static NameValueCollection GetConfigSection(string name)
        {
            return (NameValueCollection)ConfigurationManager.GetSection(name) ?? null;
        }
    }
}
