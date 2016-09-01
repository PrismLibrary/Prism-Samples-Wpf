// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIComposition.Tests.AcceptanceTest.TestInfrastructure
{
    public class Project
    {
        public Project()
        { }

        public Project(string projectName, string role)
        {
            this.ProjectName = projectName;
            this.Role = role;
        }

        public string ProjectName { get; set; }
        public string Role { get; set; }
    }
}
