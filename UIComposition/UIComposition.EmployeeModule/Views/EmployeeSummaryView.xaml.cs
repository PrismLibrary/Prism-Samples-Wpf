// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Windows.Controls;
using UIComposition.EmployeeModule.ViewModels;

namespace UIComposition.EmployeeModule.Views
{
    public partial class EmployeeSummaryView : UserControl
    {
        // TODO: 06 - The EmployeeSummaryView contains a Tab control which defines a region named TabRegion (EmployeeSummaryView.xaml).
        // TODO: 07 - The TabRegion defines a RegionContext which provides a reference to the currently selected employee to all child views (EmployeeSummaryView.xaml).
        public EmployeeSummaryView( EmployeeSummaryViewModel viewModel )
        {
            this.InitializeComponent();

            // Set the ViewModel as this View's data context.
            this.DataContext = viewModel;
        }
    }
}