// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Xml;
using System.Data;
using System.Xml.Serialization;
using StockTraderRI.Tests.AcceptanceTest.TestInfrastructure.MockModels;
using AcceptanceTestLibrary.ApplicationHelper;

namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
{
    public class AccountPositionDataProvider : DataProviderBase<AccountPosition>
    {
        public AccountPositionDataProvider()
            : base()
        { }

        public override string GetDataFilePath()
        {
            return ConfigHandler.GetValue("AccountPositionDataFile");
        }
    }
}
