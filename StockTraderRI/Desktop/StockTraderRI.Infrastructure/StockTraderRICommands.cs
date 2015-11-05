

using Prism.Commands;
using System.ComponentModel.Composition;

namespace StockTraderRI.Infrastructure
{

    public static class StockTraderRICommands
    {
        private static CompositeCommand submitOrderCommand = new CompositeCommand(true);
        private static CompositeCommand cancelOrderCommand = new CompositeCommand(true);
        private static CompositeCommand submitAllOrdersCommand = new CompositeCommand();
        private static CompositeCommand cancelAllOrdersCommand = new CompositeCommand();

        public static CompositeCommand SubmitOrderCommand
        {
            get { return submitOrderCommand; }
            set { submitOrderCommand = value; }
        }

        public static CompositeCommand CancelOrderCommand
        {
            get { return cancelOrderCommand; }
            set { cancelOrderCommand = value; }
        }

        public static CompositeCommand SubmitAllOrdersCommand
        {
            get { return submitAllOrdersCommand; }
            set { submitAllOrdersCommand = value; }
        }

        public static CompositeCommand CancelAllOrdersCommand
        {
            get { return cancelAllOrdersCommand; }
            set { cancelAllOrdersCommand = value; }
        }
    }

    [Export]    
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StockTraderRICommandProxy
    {
        virtual public CompositeCommand SubmitOrderCommand
        {
            get { return StockTraderRICommands.SubmitOrderCommand; }
        }

        virtual public CompositeCommand CancelOrderCommand
        {
            get { return StockTraderRICommands.CancelOrderCommand; }
        }

        virtual public CompositeCommand SubmitAllOrdersCommand
        {
            get { return StockTraderRICommands.SubmitAllOrdersCommand; }
        }

        virtual public CompositeCommand CancelAllOrdersCommand
        {
            get { return StockTraderRICommands.CancelAllOrdersCommand; }
        }
    }
}
