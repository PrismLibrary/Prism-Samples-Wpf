// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using UIComposition.EmployeeModule.Models;

namespace UIComposition.EmployeeModule.Services
{
    /// <summary>
    /// Data service interface.
    /// </summary>
    public interface IEmployeeDataService
    {
        Employees GetEmployees();
        Projects GetProjects();
    }
}