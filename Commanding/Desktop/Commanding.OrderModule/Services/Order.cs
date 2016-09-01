// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;

namespace Commanding.Modules.Order.Services
{
    public class Order
    {
        public Order()
        {
            DeliveryDate = DateTime.Now;
        }

        public string   Name         { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int?     Quantity     { get; set; }
        public decimal? Price        { get; set; }
        public decimal? Shipping     { get; set; }
        //...
    }
}