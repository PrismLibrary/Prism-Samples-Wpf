// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Prism.Events;

namespace ModuleB.Tests.Mocks
{
    public class MockEventAggregator : IEventAggregator
    {
        Dictionary<Type, object> events = new Dictionary<Type, object>();
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new(){
            var type = typeof (TEventType);
            return (TEventType)events[typeof(TEventType)];
        }

        public void AddMapping<TEventType>(TEventType mockEvent)
        {
            var type = typeof(TEventType);
            events.Add(typeof(TEventType), mockEvent);
        }
    }
}
