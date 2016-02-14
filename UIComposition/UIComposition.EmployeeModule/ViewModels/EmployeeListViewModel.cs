// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using System.Windows.Data;
using Prism.Events;
using UIComposition.EmployeeModule.Models;
using UIComposition.EmployeeModule.Services;

namespace UIComposition.EmployeeModule.ViewModels
{
    /// <summary>
    /// View model to support the Employee List view.
    /// </summary>
    public class EmployeeListViewModel : INotifyPropertyChanged
    {
        private readonly IEventAggregator eventAggregator;

        public EmployeeListViewModel(IEmployeeDataService dataService, IEventAggregator eventAggregator)
        {
            if (dataService == null) throw new ArgumentNullException("dataService");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            this.eventAggregator = eventAggregator;

            // Initialize the CollectionView for the underlying model
            // and track the current selection.
            this.Employees = new ListCollectionView(dataService.GetEmployees());
            this.Employees.CurrentChanged += new EventHandler(this.SelectedEmployeeChanged);
        }

        public ICollectionView Employees { get; private set; }

        private void SelectedEmployeeChanged(object sender, EventArgs e)
        {
            Employee employee = this.Employees.CurrentItem as Employee;
            if (employee != null)
            {
                // Publish the EmployeeSelectedEvent event.
                this.eventAggregator.GetEvent<EmployeeSelectedEvent>().Publish(employee.Id);
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}