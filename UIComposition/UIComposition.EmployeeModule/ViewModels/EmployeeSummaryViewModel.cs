// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel;
using UIComposition.EmployeeModule.Models;

namespace UIComposition.EmployeeModule.ViewModels
{
    /// <summary>
    /// View model to support the Employee Summary view.
    /// </summary>
    public class EmployeeSummaryViewModel : INotifyPropertyChanged
    {
        public EmployeeSummaryViewModel()
        {
        }

        private Employee currentEmployee;

        public Employee CurrentEmployee
        {
            get { return this.currentEmployee; }
            set
            {
                this.currentEmployee = value;
                this.NotifyPropertyChanged("CurrentEmployee");
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