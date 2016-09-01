// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
{
    public interface IDataProvider<TEntity>
    {
        string GetDataFilePath();
        List<TEntity> GetData();
        List<TEntity> GetDataForId(string id);
        int GetCount();
    }
}
