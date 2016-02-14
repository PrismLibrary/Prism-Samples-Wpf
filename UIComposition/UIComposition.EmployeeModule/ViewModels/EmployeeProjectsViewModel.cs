// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using System.Windows.Data;
using UIComposition.EmployeeModule.Models;
using UIComposition.EmployeeModule.Services;

namespace UIComposition.EmployeeModule.ViewModels
{
    /// <summary>
    /// View model to support the Employee Projects view.
    /// </summary>
    public class EmployeeProjectsViewModel : INotifyPropertyChanged
    {
        public EmployeeProjectsViewModel(IEmployeeDataService dataService)
        {
            if (dataService == null) throw new ArgumentNullException("dataService");

            // Initialize a CollectionView for the project list.
            this.Projects = new ListCollectionView(dataService.GetProjects());
        }

        public string ViewName
        {
            get { return "Employee Projects"; }
        }

        private Employee currentEmployee;

        public Employee CurrentEmployee
        {
            get { return this.currentEmployee; }
            set
            {
                this.currentEmployee = value;

                // Filter the list of projects to those that are assigned to the current employee.
                if (this.CurrentEmployee != null)
                    this.Projects.Filter = obj => ((Project) obj).Id == this.CurrentEmployee.Id;
                this.Projects.Refresh();

                this.NotifyPropertyChanged("CurrentEmployee");
                this.NotifyPropertyChanged("Projects");
            }
        }

        public ICollectionView Projects { get; private set; }

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