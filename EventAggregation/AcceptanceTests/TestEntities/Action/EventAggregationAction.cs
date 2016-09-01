// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcceptanceTestLibrary.Common;

namespace EventAggregation.Tests.AcceptanceTest.TestEntities.Action
{
    public static class EventAggregationAction<TApp>
        where TApp : AppLauncherBase, new()
    {
    }
}
