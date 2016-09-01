// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace StateBasedNavigation.Desktop.Infrastructure
{
    public class OperationResult<T> : OperationResult, IOperationResult<T>
    {
        public T Result
        {
            get;
            protected internal set;
        }
    }
}
