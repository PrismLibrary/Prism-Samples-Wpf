// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using AcceptanceTestLibrary.ApplicationHelper;


namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
{
    public class OrderDataProvider : DataProviderBase<Order>
    {
        public OrderDataProvider()
            : base()
        { }

        public override string GetDataFilePath()
        {
            return ConfigHandler.GetValue("OrderProcessingFile");
        }

        public override List<Order> GetData()
        {
            List<Order> order = new List<Order>();
            string filepath = GetDataFilePath();

            if (File.Exists(filepath))
            {
                DataSet ds = new DataSet();
                ds.Locale = CultureInfo.CurrentCulture;
                ds.ReadXml(filepath);
                DataRow dr = null;


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    order.Add(
                        new Order(dr["TickerSymbol"].ToString(),
                        decimal.Parse(dr["StopLimitPrice"].ToString(), CultureInfo.InvariantCulture),
                        dr["OrderType"].ToString(),
                        int.Parse(dr["Shares"].ToString(), CultureInfo.InvariantCulture),
                        dr["TimeInForce"].ToString(),
                        dr["TransactionType"].ToString())
                        );
                }
            }
            return order;
        }
    }
}
