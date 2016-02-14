// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
{
    public abstract class DataProviderBase<TEntity> : IDataProvider<TEntity>, IDisposable
    {
        XmlSerializer xmlSerializer = null;
        XmlTextReader xmlReader = null;

        protected DataProviderBase()
        {
            xmlSerializer = new XmlSerializer(typeof(List<TEntity>));
            xmlReader = new XmlTextReader(GetDataFilePath());
        }

        public virtual List<TEntity> GetData()
        {
            return (List<TEntity>)(xmlSerializer.Deserialize(xmlReader));
        }

        public virtual List<TEntity> GetDataForId(string id)
        {
            throw new NotImplementedException();
        }

        public virtual int GetCount()
        {
            return ((List<TEntity>)xmlSerializer.Deserialize(xmlReader)).Count;
        }

        public abstract string GetDataFilePath();

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != xmlSerializer)
                {
                    xmlSerializer = null;
                }

                if (null != xmlReader)
                {
                    xmlReader = null;
                }
            }
        }
    }
}
