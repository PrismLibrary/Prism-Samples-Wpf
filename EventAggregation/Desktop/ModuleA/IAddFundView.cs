// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModuleA
{
    public interface IAddFundView
    {
        event EventHandler AddFund;
        string Customer { get;}
        string Fund { get;}
    }
}
