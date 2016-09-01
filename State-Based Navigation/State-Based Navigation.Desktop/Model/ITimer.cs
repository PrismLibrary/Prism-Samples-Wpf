// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;

namespace StateBasedNavigation.Desktop.Model
{
    public interface ITimer
    {
        event EventHandler Tick;

        void Start();

        void Stop();
    }
}
