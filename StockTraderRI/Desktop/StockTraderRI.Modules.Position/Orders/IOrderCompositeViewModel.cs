

using System;
using System.Windows.Input;
using StockTraderRI.Modules.Position.Models;

namespace StockTraderRI.Modules.Position.Orders
{
    public interface IOrderCompositeViewModel
    {
        event EventHandler CloseViewRequested;

        ICommand SubmitCommand { get; }
        ICommand CancelCommand { get; }
        TransactionInfo TransactionInfo { get; set; }
        int Shares { get; }
    }
}