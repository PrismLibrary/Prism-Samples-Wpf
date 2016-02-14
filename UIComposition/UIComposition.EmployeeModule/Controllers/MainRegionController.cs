// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;
using UIComposition.EmployeeModule.Models;
using UIComposition.EmployeeModule.Services;
using UIComposition.EmployeeModule.ViewModels;
using UIComposition.EmployeeModule.Views;

namespace UIComposition.EmployeeModule.Controllers
{
    /// <summary>
    /// Controllers are typically used to programmatically add
    /// and remove views to regions using view injection.
    /// This controller subscribes to a loosely coupled event,
    /// which is published by the EmployeeListViewModel when the
    /// user selects an employee. When an employee is selected,
    /// the EmployeeSummaryView is created and added to the main
    /// region using view injection.
    /// </summary>
    public class MainRegionController
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IEmployeeDataService dataService;

        public MainRegionController(IUnityContainer container,
                                    IRegionManager regionManager,
                                    IEventAggregator eventAggregator,
                                    IEmployeeDataService dataService)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (regionManager == null) throw new ArgumentNullException("regionManager");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            if (dataService == null) throw new ArgumentNullException("dataService");

            this.container = container;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.dataService = dataService;

            // Subscribe to the EmployeeSelectedEvent event.
            // This event is fired whenever the user selects an
            // employee in the EmployeeListView.
            this.eventAggregator.GetEvent<EmployeeSelectedEvent>().Subscribe(this.EmployeeSelected, true);
        }

        /// <summary>
        /// Called when a new employee is selected. This method uses
        /// view injection to programmatically 
        /// </summary>
        private void EmployeeSelected(string id)
        {
            if ( string.IsNullOrEmpty( id ) ) return;

            // Get the employee entity with the selected ID.
            Employee selectedEmployee = this.dataService.GetEmployees().FirstOrDefault(item => item.Id == id);

            // TODO: 05 - The MainRegionController displays the EmployeeSummaryView in the Main region when an employee is first selected.

            // Get a reference to the main region.
            IRegion mainRegion = this.regionManager.Regions[RegionNames.MainRegion];
            if (mainRegion == null) return;

            // Check to see if we need to create an instance of the view.
            EmployeeSummaryView view = mainRegion.GetView("EmployeeSummaryView") as EmployeeSummaryView;
            if (view == null)
            {
                // Create a new instance of the EmployeeDetailsView using the Unity container.
                view = this.container.Resolve<EmployeeSummaryView>();

                // Add the view to the main region. This automatically activates the view too.
                mainRegion.Add(view, "EmployeeSummaryView");
            }
            else
            {
                // The view has already been added to the region so just activate it.
                mainRegion.Activate(view);
            }

            // Set the current employee property on the view model.
            EmployeeSummaryViewModel viewModel = view.DataContext as EmployeeSummaryViewModel;
            if (viewModel != null)
            {
                viewModel.CurrentEmployee = selectedEmployee;
            }
        }
    }
}