

using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.News.Article;
using System.ComponentModel.Composition;
using System.ComponentModel;
using System;

namespace StockTraderRI.Modules.News.Controllers
{
    [Export(typeof(INewsController))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NewsController : INewsController
    {
        private readonly ArticleViewModel articleViewModel;
        private readonly NewsReaderViewModel newsReaderViewModel;
        
        [ImportingConstructor]
        public NewsController(ArticleViewModel articleViewModel, NewsReaderViewModel newsReaderViewModel)
        {
            this.articleViewModel = articleViewModel;         
            this.newsReaderViewModel = newsReaderViewModel;
            if (articleViewModel != null)
            {
                this.articleViewModel.PropertyChanged += this.ArticleViewModel_PropertyChanged;
            }
        }

        private void ArticleViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedArticle":
                    this.newsReaderViewModel.NewsArticle = this.articleViewModel.SelectedArticle;
                    break;
            }
        }
    }
}
