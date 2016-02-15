// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Globalization;
using EventAggregation.Infrastructure;
using ModuleB.Properties;
using Prism.Events;

namespace ModuleB
{
    public class ActivityPresenter
    {
        private string _customerId;
        private IEventAggregator eventAggregator;
        private SubscriptionToken subscriptionToken;

        public ActivityPresenter(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public void FundAddedEventHandler(FundOrder fundOrder)
        {
            Debug.Assert(View != null);
            View.AddContent(fundOrder.TickerSymbol);
        }

        public bool FundOrderFilter(FundOrder fundOrder)
        {
            return fundOrder.CustomerId == _customerId;
        }

        public IActivityView View { get; set; }

        public string CustomerId
        {
            get
            {
                return _customerId;
            }

            set
            {
                _customerId = value;

                FundAddedEvent fundAddedEvent = eventAggregator.GetEvent<FundAddedEvent>();

                if (subscriptionToken != null)
                {
                    fundAddedEvent.Unsubscribe(subscriptionToken);
                }

                subscriptionToken = fundAddedEvent.Subscribe(FundAddedEventHandler, ThreadOption.UIThread, false, FundOrderFilter);

                View.SetTitle(string.Format(CultureInfo.CurrentCulture, Resources.ActivityTitle, CustomerId));
            }
        }
    }
}
