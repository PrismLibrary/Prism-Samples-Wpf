

using Prism.Logging;

namespace StockTraderRI
{
    public partial class StockTraderRIBootstrapper
    {
        private readonly EnterpriseLibraryLoggerAdapter _logger = new EnterpriseLibraryLoggerAdapter();

        protected override ILoggerFacade CreateLogger()
        {
            return _logger;
        }
    }
}
