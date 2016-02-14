// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;

namespace StateBasedNavigation.Desktop.Infrastructure
{
    public class OperationResult : IOperationResult
    {
        public Exception Error
        {
            get;
            protected internal set;
        }
    }
}
