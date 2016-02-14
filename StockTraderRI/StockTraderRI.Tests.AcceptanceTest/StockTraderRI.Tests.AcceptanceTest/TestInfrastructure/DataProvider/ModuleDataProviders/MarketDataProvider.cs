// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StockTraderRI.Tests.AcceptanceTest.TestInfrastructure.MockModels;
using System.Globalization;
using AcceptanceTestLibrary.ApplicationHelper;

namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
{
    public class MarketDataProvider : DataProviderBase<Market>
    {
        public MarketDataProvider()
            : base()
        { }

        public override string GetDataFilePath()
        {
            return ConfigHandler.GetValue("MarketDataFile");
        }

        public override List<Market> GetData()
        {
            DataSet ds = new DataSet();
            ds.Locale = CultureInfo.CurrentCulture;
            ds.ReadXml(GetDataFilePath());
            DataRow dr = null;

            List<Market> market = new List<Market>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dr = ds.Tables[1].Rows[i];
                market.Add(
                    new Market(
                        dr[TestDataInfrastructure.GetTestInputData("TickerSymbol")].ToString(), 
                        decimal.Parse(dr[TestDataInfrastructure.GetTestInputData("LastPrice")].ToString(), CultureInfo.InvariantCulture)));
            }

            return market;
        }
    }
}
