// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using StockTraderRI.Tests.AcceptanceTest.TestInfrastructure.MockModels;
using System.Data;
using System.Globalization;
using AcceptanceTestLibrary.ApplicationHelper;

namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure.DataProvider.ModuleDataProviders
{
    public class NewsDataProvider: DataProviderBase<News>
    {
        public NewsDataProvider()
            : base()
        { }

        public override string GetDataFilePath()
        {
            return ConfigHandler.GetValue("NewsDataFile");
        }

        public override List<News> GetDataForId(string id)
        {
            List<News> news = new List<News>();

            XDocument xDoc = XDocument.Load(GetDataFilePath());
            foreach (XElement newsItem in xDoc.Descendants("NewsItems").Descendants("NewsItem")
                .Where(newsItem => newsItem.Attribute(TestDataInfrastructure.GetTestInputData("TickerSymbol")).Value.Equals(id)))
            {
                news.Add(
                    new News(
                        id,
                        newsItem.Attribute(TestDataInfrastructure.GetTestInputData("IconUri")).Value,
                        DateTime.Parse(newsItem.Attribute(TestDataInfrastructure.GetTestInputData("PublishedDate")).Value, CultureInfo.InvariantCulture),
                        newsItem.Elements("Title").ToList<XElement>()[0].Value,
                        newsItem.Elements("Body").ToList<XElement>()[0].Value
                        )
                    );
            }

            return news;
        }
    }
}
