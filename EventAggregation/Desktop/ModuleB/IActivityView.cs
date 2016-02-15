// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace ModuleB
{
    public interface IActivityView
    {
        void SetTitle(string title);

        void SetCustomerId(string customerId);

        void AddContent(string content);
    }
}
