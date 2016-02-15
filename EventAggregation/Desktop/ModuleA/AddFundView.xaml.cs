// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;

namespace ModuleA
{
    /// <summary>
    /// Interaction logic for AddFundView.xaml
    /// </summary>
    public partial class AddFundView : UserControl, IAddFundView
    {
        private AddFundPresenter _presenter;

        public AddFundView()
        {
            InitializeComponent();
            this.AddButton.Click += new RoutedEventHandler(AddButton_Click);
        }

        public AddFundView(AddFundPresenter presenter) : this()
        {
            _presenter = presenter;
            _presenter.View = this;
        }

        void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddFund(this, null);
        }

        #region IAddFundView Members

        public event EventHandler AddFund = delegate { };

        public string Customer
        {
            get
            {
                ComboBoxItem selectedItem = this.CustomerCbx.SelectedItem as ComboBoxItem;
                if (selectedItem == null)
                    return string.Empty;
                
                return selectedItem.Content.ToString();


            }
        }

        public string Fund
        {
            get
            {
                ComboBoxItem selectedItem = this.FundCbx.SelectedItem as ComboBoxItem;
                if (selectedItem == null)
                    return string.Empty;

                return selectedItem.Content.ToString();
            }
        }

        #endregion
    }
}
