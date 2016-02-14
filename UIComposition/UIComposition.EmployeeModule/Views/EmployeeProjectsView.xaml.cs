// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Windows.Controls;
using Prism.Regions;
using UIComposition.EmployeeModule.Models;
using UIComposition.EmployeeModule.ViewModels;

namespace UIComposition.EmployeeModule.Views
{
    public partial class EmployeeProjectsView : UserControl
    {
        public EmployeeProjectsView(EmployeeProjectsViewModel employeeProjectsViewModel)
        {
            this.InitializeComponent();

            // Set the ViewModel as this View's data context.
            this.DataContext = employeeProjectsViewModel;

            // TODO: 09 - Whenever the RegionContext value is changed, the newly selected employee is set on the view model for each child window.
            // This view is displayed in a region with a region context.
            // The region context is defined as the currently selected employee
            // When the region context is changed, we need to propogate the
            // change to this view's view model.
            RegionContext.GetObservableContext(this).PropertyChanged += (s, e)
                                                                        =>
                                                                        employeeProjectsViewModel.CurrentEmployee =
                                                                        RegionContext.GetObservableContext(this).Value
                                                                        as Employee;
        }
    }
}