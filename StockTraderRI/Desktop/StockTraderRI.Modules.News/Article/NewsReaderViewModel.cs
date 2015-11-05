

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Prism.Mvvm;
using StockTraderRI.Infrastructure.Models;

namespace StockTraderRI.Modules.News.Article
{
    [Export(typeof(NewsReaderViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NewsReaderViewModel : BindableBase
    {
        private NewsArticle newsArticle;

        public NewsArticle NewsArticle        
        {
            get
            {
                return this.newsArticle;
            }
            set
            {
                SetProperty(ref this.newsArticle, value);
            }
        }

    }
}
