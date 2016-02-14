// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using StockTraderRI.Tests.AcceptanceTest.TestInfrastructure.MockModels;
using System.Data;
using System.Globalization;
using AcceptanceTestLibrary.ApplicationHelper;

namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
{
    public class MarketHistoryDataProvider : DataProviderBase<MarketHistoryItem>
    {
        public MarketHistoryDataProvider()
            : base()
        { }

        public override string GetDataFilePath()
        {
            return ConfigHandler.GetValue("MarketHistoryDataFile");
        }

        public override List<MarketHistoryItem> GetData()
        {
            DataSet ds = new DataSet();
            ds.Locale = CultureInfo.CurrentCulture;
            ds.ReadXml(GetDataFilePath());
            DataRow dr = null;

            List<MarketHistoryItem> history = new List<MarketHistoryItem>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                history.Add(
                    new MarketHistoryItem(
                        dr[TestDataInfrastructure.GetTestInputData("TickerSymbol")].ToString(),
                        DateTime.Parse(dr[TestDataInfrastructure.GetTestInputData("Date")].ToString(), CultureInfo.InvariantCulture),
                        decimal.Parse(dr[2].ToString(), CultureInfo.InvariantCulture)
                        ));
            }

            return history;
        }
    }
}
