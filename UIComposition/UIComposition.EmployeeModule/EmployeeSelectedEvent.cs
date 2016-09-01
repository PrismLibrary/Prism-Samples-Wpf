// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


using Prism.Events;

namespace UIComposition.EmployeeModule
{
    /// <summary>
    /// Defines an event to communicate that an employee has been selected.
    /// The event payload is the employee id.
    /// </summary>
    public class EmployeeSelectedEvent : PubSubEvent<string>
    {
    }
}