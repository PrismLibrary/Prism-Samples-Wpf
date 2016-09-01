// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;

namespace StateBasedNavigation.Desktop.Infrastructure
{
    public interface IOperationResult
    {
        Exception Error { get; }
    }
}
