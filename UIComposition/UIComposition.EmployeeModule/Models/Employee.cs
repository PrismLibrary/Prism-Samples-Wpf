// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace UIComposition.EmployeeModule.Models
{
    /// <summary>
    /// Employee entity class.
    /// </summary>
    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}