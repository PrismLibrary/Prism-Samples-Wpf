

using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Modules.Position.Models;

namespace StockTraderRI.Modules.Position.Orders
{
    [Export(typeof(IOrderCompositeViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class OrderCompositeViewModel : DependencyObject, IOrderCompositeViewModel, IHeaderInfoProvider<string>
    {
        private readonly IOrderDetailsViewModel orderDetailsViewModel;

        public static readonly DependencyProperty HeaderInfoProperty =
            DependencyProperty.Register("HeaderInfo", typeof(string), typeof(OrderCompositeViewModel), null);

        [ImportingConstructor]
        public OrderCompositeViewModel(IOrderDetailsViewModel orderDetailsViewModel)
        {
            if (orderDetailsViewModel == null)
            {
                throw new ArgumentNullException("orderDetailsViewModel");
            }

            this.orderDetailsViewModel = orderDetailsViewModel;
            this.orderDetailsViewModel.CloseViewRequested += _orderViewModel_CloseViewRequested;
        }

        void _orderViewModel_CloseViewRequested(object sender, EventArgs e)
        {
            OnCloseViewRequested(sender, e);
        }

        partial void SetTransactionInfo(TransactionInfo transactionInfo);

        private void OnCloseViewRequested(object sender, EventArgs e)
        {
            CloseViewRequested(sender, e);
        }

        public event EventHandler CloseViewRequested = delegate { };

        public TransactionInfo TransactionInfo
        {
            get { return this.orderDetailsViewModel.TransactionInfo; }
            set { SetTransactionInfo(value); }

        }

        public ICommand SubmitCommand
        {
            get { return this.orderDetailsViewModel.SubmitCommand; }
        }

        public ICommand CancelCommand
        {
            get { return this.orderDetailsViewModel.CancelCommand; }
        }

        public int Shares
        {
            get { return this.orderDetailsViewModel.Shares ?? 0; }
        }

        public object OrderDetails
        {
            get { return this.orderDetailsViewModel; }
        }
    }
}