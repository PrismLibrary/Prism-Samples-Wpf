// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcceptanceTestLibrary.ApplicationHelper;

namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
{
    public class TestDataInfrastructure
    {
        public int GetCount<T, TEntity>()
            where T : IDataProvider<TEntity>, new()
        {
            return new T().GetCount();
        }

        public List<TEntity> GetData<T, TEntity>()
            where T : IDataProvider<TEntity>, new()
        {
            return new T().GetData();
        }

        public List<TEntity> GetDataForId<T, TEntity>(string id)
            where T : IDataProvider<TEntity>, new()
        {
            return new T().GetDataForId(id);
        }
        public static string GetTestInputData(string key)
        {
            ResXConfigHandler testInputHandler = new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile"));
            return testInputHandler.GetValue(key);
        }

        public static string GetControlId(string key)
        {
            ResXConfigHandler testInputHandler = new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile"));
            return testInputHandler.GetValue(key);
        }
    }
}
