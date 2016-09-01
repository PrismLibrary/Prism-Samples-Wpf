// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EventAggregation.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleB.Tests.Mocks;
using Prism.Events;

namespace ModuleB.Tests
{
    [TestClass]
    public class ActivityPresenterFixture
    {
        [TestMethod]
        public void PresenterAddsFundOnEvent()
        {
            var view = new MockActivityView();
            var mockEventAggregator = new MockEventAggregator();

            MockFundAddedEvent mockEvent = new MockFundAddedEvent();
            mockEventAggregator.AddMapping<FundAddedEvent>(mockEvent);

            ActivityPresenter presenter = new ActivityPresenter(mockEventAggregator);
            presenter.View = view;
            string customerId = "ALFKI";
            presenter.CustomerId = customerId;
            FundOrder payload = new FundOrder() { CustomerId = customerId, TickerSymbol = "MSFT" };
            mockEvent.Subscribe(delegate { new FundOrder() { CustomerId = customerId, TickerSymbol = "MSFT" }; }, ThreadOption.UIThread, true, delegate { return true; });
            mockEvent.Publish(payload);
            StringAssert.Contains(view.AddContentArgumentContent, "MSFT");
        }

        [TestMethod]
        public void PresenterSubscribesToFundAddedForCorrectCustomer()
        {
            var mockEventAggregator = new MockEventAggregator();

            MockFundAddedEvent mockEvent = new MockFundAddedEvent();
            mockEventAggregator.AddMapping<FundAddedEvent>(mockEvent);
            ActivityPresenter presenter = new ActivityPresenter(mockEventAggregator);
            presenter.View = new MockActivityView();

            string customerId = "ALFKI";
            presenter.CustomerId = customerId;

            FundOrder payload = new FundOrder() { CustomerId = customerId };
            mockEvent.Subscribe(delegate { new FundOrder() { CustomerId = customerId }; }, ThreadOption.UIThread, true, delegate{
                if (customerId == "ALFKI"){
                    return true;
                }
                else{
                    return false;
                }
                                                                                                                        });
            mockEvent.Publish(payload);

            Assert.IsTrue(mockEvent.SubscribeArgumentFilter(new FundOrder { CustomerId = "ALFKI" }));
            Assert.AreEqual(ThreadOption.UIThread, mockEvent.ThreadOption);
            customerId = "FilteredOutCustomer";
            mockEvent.Subscribe(delegate { new FundOrder() { CustomerId = customerId }; }, ThreadOption.UIThread, true, delegate {
                if (customerId == "ALFKI")
                {
                    return true;
                }
                else {
                    return false;
                }
            });
            Assert.IsFalse(mockEvent.SubscribeArgumentFilter(new FundOrder { CustomerId = "FilteredOutCustomer" }));
        }

        [TestMethod]
        public void PresenterUnsubcribesFromFundAddedEventIfCustomerIDIsSetTwice()
        {
            var mockEventAggregator = new MockEventAggregator();

            MockFundAddedEvent mockEvent = new MockFundAddedEvent();
            string customerId = "ALFKI";
            FundOrder payload = new FundOrder() { CustomerId = customerId, TickerSymbol = "MSFT" };
            mockEvent.Subscribe(delegate { new FundOrder() { CustomerId = customerId, TickerSymbol = "MSFT" }; }, ThreadOption.UIThread, true, delegate { return true; });
            mockEventAggregator.AddMapping<FundAddedEvent>(mockEvent);
            ActivityPresenter presenter = new ActivityPresenter(mockEventAggregator);
            presenter.View = new MockActivityView();


            presenter.CustomerId = "ALFKI";
            presenter.CustomerId = "ALFKI";

            Assert.AreEqual(1, mockEvent.SubscribeCount);
        }
    }

    internal class MockFundAddedEvent : FundAddedEvent
    {
        public Action<FundOrder> SubscribeArgumentAction;
        public Predicate<FundOrder> SubscribeArgumentFilter;
        public ThreadOption ThreadOption;
        public int SubscribeCount;

        public new SubscriptionToken Subscribe(Action<FundOrder> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<FundOrder> filter)
        {
            SubscribeArgumentAction = action;
            SubscribeArgumentFilter = filter;
            ThreadOption = threadOption;
            SubscribeCount++;
            return new SubscriptionToken(t=> { });
        }

        //public override void Unsubscribe(SubscriptionToken subscriptionToken)
        //{
        //    SubscribeCount--;
        //}
    }

    class MockActivityView : IActivityView
    {
        public string AddContentArgumentContent;

        public void SetTitle(string title)
        {

        }

        public void SetCustomerId(string customerId)
        {

        }

        public void AddContent(string content)
        {
            AddContentArgumentContent = content;
        }
    }
}
