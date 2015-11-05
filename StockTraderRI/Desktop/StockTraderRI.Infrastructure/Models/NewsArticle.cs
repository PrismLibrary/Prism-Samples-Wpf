

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockTraderRI.Infrastructure.Models
{
    public class NewsArticle
    {
        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string IconUri { get; set; }
    }
}
