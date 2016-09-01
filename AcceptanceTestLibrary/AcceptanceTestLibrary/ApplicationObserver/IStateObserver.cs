// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcceptanceTestLibrary.ApplicationObserver
{
    /// <summary>
    /// Interface class for Observers. 
    /// </summary>
    public interface IStateObserver
    {
        void Notify();
    }
}
