// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIComposition.Tests.AcceptanceTest.TestInfrastructure
{
    public class Employee
    {
        public Employee()
        { }

        public Employee(int employeeId)
        {
            this.EmployeeId = employeeId;
        }

        public int EmployeeId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
