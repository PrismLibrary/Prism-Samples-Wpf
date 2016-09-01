// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Commanding.Modules.Order.Services
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> GetOrdersToEdit();
    }
}