// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EventAggregation.Infrastructure;

namespace ModuleA.Tests.Mocks
{
    class MockFundAddedEvent : FundAddedEvent
    {
        public bool PublishCalled;
        public FundOrder PublishArgumentPayload;


        public override void Publish(FundOrder payload)
        {
            PublishCalled = true;
            PublishArgumentPayload = payload;
        }
    }
}
