// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EventAggregation.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleA.Tests.Mocks;

namespace ModuleA.Tests
{
    [TestClass]
    public class AddFundPresenterFixture
    {
        [TestMethod]
        public void PresenterPublishesFundAddedOnViewAddClick()
        {
            var view = new MockAddFundView();
            var EventAggregator = new MockEventAggregator();
            var mockFundAddedEvent = new MockFundAddedEvent();
            EventAggregator.AddMapping<FundAddedEvent>(mockFundAddedEvent);
            var presenter = new AddFundPresenter(EventAggregator);
            presenter.View = view;
            view.Customer = "99";
            view.Fund = "TestFund";

            view.PublishAddClick();

            Assert.IsTrue(mockFundAddedEvent.PublishCalled);
            Assert.AreEqual("99", mockFundAddedEvent.PublishArgumentPayload.CustomerId);
            Assert.AreEqual("TestFund", mockFundAddedEvent.PublishArgumentPayload.TickerSymbol);
        }


    }

    class MockAddFundView : IAddFundView
    {
        public void PublishAddClick()
        {
            AddFund(this, EventArgs.Empty);
        }

        public event EventHandler AddFund;

        public string Customer { get; set; }

        public string Fund { get; set; }
    }
}
