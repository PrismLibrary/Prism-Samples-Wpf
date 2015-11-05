

using System;
using System.ComponentModel.Composition;

namespace StockTraderRI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [MetadataAttribute]
    public sealed class ViewExportAttribute : ExportAttribute, IViewRegionRegistration
    {
        public ViewExportAttribute()
            : base(typeof(object))
        { }

        public ViewExportAttribute(string viewName)
            : base(viewName, typeof(object))
        { }

        public string ViewName { get { return base.ContractName; } }

        public string RegionName { get; set; }
    }
}
