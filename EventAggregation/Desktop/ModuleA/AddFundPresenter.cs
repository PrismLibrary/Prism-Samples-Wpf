// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EventAggregation.Infrastructure;
using Prism.Events;

namespace ModuleA
{
    public class AddFundPresenter
    {
        private IAddFundView _view;
        private IEventAggregator eventAggregator;

        public AddFundPresenter(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        void AddFund(object sender, EventArgs e)
        {
            FundOrder fundOrder = new FundOrder();
            fundOrder.CustomerId = View.Customer;
            fundOrder.TickerSymbol = View.Fund;

            if (!string.IsNullOrEmpty(fundOrder.CustomerId) && !string.IsNullOrEmpty(fundOrder.TickerSymbol))
                eventAggregator.GetEvent<FundAddedEvent>().Publish(fundOrder);
        }

        public IAddFundView View
        {
            get { return _view; }
            set
            {
                _view = value;
                _view.AddFund += AddFund;
            }
        }

    }
}
