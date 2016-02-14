// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Windows.Controls;
using Commanding.Modules.Order.PresentationModels;

namespace Commanding.Modules.Order.Views
{
    /// <summary>
    /// Interaction logic for OrdersEditorView.xaml
    /// </summary>
    public partial class OrdersEditorView : UserControl
    {
        public OrdersEditorView( OrdersEditorViewModel viewModel )
        {
            InitializeComponent();

            // Set the presentation model as this views data context.
            DataContext = viewModel;
        }
    }
}