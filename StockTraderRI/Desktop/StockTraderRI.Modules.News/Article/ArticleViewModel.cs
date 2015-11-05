

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Events;
using Prism.Regions;
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;

namespace StockTraderRI.Modules.News.Article
{
    [Export(typeof(ArticleViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ArticleViewModel : BindableBase
    {
        private string _companySymbol;
        private IList<NewsArticle> articles;
        private NewsArticle selectedArticle;
        private readonly INewsFeedService newsFeedService;
        private readonly IRegionManager regionManager;
        private readonly ICommand showArticleListCommand;
        private readonly ICommand showNewsReaderViewCommand;

        [ImportingConstructor]
        public ArticleViewModel(INewsFeedService newsFeedService, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            if (newsFeedService == null)
            {
                throw new ArgumentNullException("newsFeedService");
            }

            if (regionManager == null)
            {
                throw new ArgumentNullException("regionManager");
            }

            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }

            this.newsFeedService = newsFeedService;
            this.regionManager = regionManager;

            this.showArticleListCommand = new DelegateCommand(this.ShowArticleList);
            this.showNewsReaderViewCommand = new DelegateCommand(this.ShowNewsReaderView);

            eventAggregator.GetEvent<TickerSymbolSelectedEvent>().Subscribe(OnTickerSymbolSelected, ThreadOption.UIThread);
        }

        public string CompanySymbol
        {
            get
            {
                return this._companySymbol;
            }
            set
            {
                if (SetProperty(ref this._companySymbol, value))
                {
                    this.OnCompanySymbolChanged();
                }
            }
        }

        public NewsArticle SelectedArticle
        {
            get { return this.selectedArticle; }
            set
            {
                SetProperty(ref this.selectedArticle, value);
            }
        }

        public IList<NewsArticle> Articles
        {
            get { return this.articles; }
            private set
            {
                SetProperty(ref this.articles, value);
            }
        }

        public ICommand ShowNewsReaderCommand { get { return this.showNewsReaderViewCommand; } }

        public ICommand ShowArticleListCommand { get { return this.showArticleListCommand; } }

        private void OnTickerSymbolSelected(string companySymbol)
        {
            this.CompanySymbol = companySymbol;
        }

        private void OnCompanySymbolChanged()
        {
            this.Articles = newsFeedService.GetNews(_companySymbol);
        }

        private void ShowArticleList()
        {
            this.SelectedArticle = null;
        }

        private void ShowNewsReaderView()
        {
            this.regionManager.RequestNavigate(RegionNames.SecondaryRegion, new Uri("/NewsReaderView", UriKind.Relative));
        }
    }
}
