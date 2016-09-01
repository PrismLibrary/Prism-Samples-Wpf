// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace StateBasedNavigation.Desktop.Infrastructure
{
    public interface IOperationResult<T> : IOperationResult
    {
        T Result { get; }
    }
}
