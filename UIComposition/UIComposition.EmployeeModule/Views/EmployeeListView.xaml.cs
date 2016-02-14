// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Windows.Controls;
using UIComposition.EmployeeModule.ViewModels;

namespace UIComposition.EmployeeModule.Views
{
    public partial class EmployeeListView : UserControl
    {
        public EmployeeListView(EmployeeListViewModel viewModel)
        {
            this.InitializeComponent();

            // Set the ViewModel as this view's data context.
            this.DataContext = viewModel;
        }
    }
}