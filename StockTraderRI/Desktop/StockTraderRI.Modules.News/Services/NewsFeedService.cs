

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.News.Properties;
using System.ComponentModel.Composition;

namespace StockTraderRI.Modules.News.Services
{
    [Export(typeof(INewsFeedService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NewsFeedService : INewsFeedService
    {
        readonly Dictionary<string, List<NewsArticle>> newsData = new Dictionary<string, List<NewsArticle>>();

        public NewsFeedService()
        {
            var document = XDocument.Parse(Resources.News);
            newsData = document.Descendants("NewsItem")
                .GroupBy(x => x.Attribute("TickerSymbol").Value,
                x => new NewsArticle
                {
                    PublishedDate = DateTime.Parse(x.Attribute("PublishedDate").Value, CultureInfo.InvariantCulture),
                    Title = x.Element("Title").Value,
                    Body = x.Element("Body").Value,
                    IconUri = x.Attribute("IconUri") != null ? x.Attribute("IconUri").Value : null
                })
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        #region INewsFeed Members

        public IList<NewsArticle> GetNews(string tickerSymbol)
        {
            List<NewsArticle> articles = new List<NewsArticle>();
            newsData.TryGetValue(tickerSymbol, out articles);
            return articles;
        }

        public event EventHandler<NewsFeedEventArgs> Updated = delegate { };

        public bool HasNews(string tickerSymbol)
        {
            return newsData.ContainsKey(tickerSymbol);
        }

        #endregion
    }
}
