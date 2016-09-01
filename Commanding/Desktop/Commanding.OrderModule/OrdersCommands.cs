// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


using Prism.Commands;

namespace Commanding.Modules.Order
{
    /// <summary>
    /// Defines the SaveAll command. This command is defined as a static so
    /// that it can be easily accessed throughout the application.
    /// </summary>
    public static class OrdersCommands
    {
        //TODO: 03 - The application defines a global SaveAll command which invokes the Save command on all registered Orders. It is enabled only when all orders can be saved.
        public static CompositeCommand SaveAllOrdersCommand = new CompositeCommand();
    }

    /// <summary>
    /// Provides a class wrapper around the static SaveAll command.
    /// </summary>
    public class OrdersCommandProxy
    {
        public virtual CompositeCommand SaveAllOrdersCommand
        {
            get { return OrdersCommands.SaveAllOrdersCommand; }
        }
    }
}
