

using Prism.Mvvm;
using StockTraderRI.Infrastructure;

namespace StockTraderRI.Modules.Position.Models
{
    public class TransactionInfo : BindableBase
    {
        private string tickerSymbol;
        private TransactionType transactionType;

        public TransactionInfo()
        {
        }

        public TransactionInfo(string tickerSymbol, TransactionType transactionType)
        {
            this.tickerSymbol = tickerSymbol;
            this.transactionType = transactionType;
        }

        public string TickerSymbol
        {
            get
            {
                return this.tickerSymbol;
            }

            set
            {
                SetProperty(ref this.tickerSymbol, value);
            }
        }

        public TransactionType TransactionType
        {
            get
            {
                return this.transactionType;
            }

            set
            {
                SetProperty(ref this.transactionType, value);
            }
        }
    }
}