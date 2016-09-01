// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Windows.Controls;

namespace ModuleB
{
    /// <summary>
    /// Interaction logic for ActivityView.xaml
    /// </summary>
    public partial class ActivityView : UserControl, IActivityView
    {
        private ActivityPresenter presenter;

        public ActivityView()
        {
            InitializeComponent();
        }

        public ActivityView(ActivityPresenter presenter)
            : this()
        {
            this.presenter = presenter;
            presenter.View = this;
        }

        #region IActivityView Members

        public void AddContent(string content)
        {
            this.ContentPanel.Children.Add(new TextBlock() { Text = content });
        }

        public void SetTitle(string title)
        {
            this.ActivityLabel.Content = title;
        }

        public void SetCustomerId(string customerId)
        {
            this.presenter.CustomerId = customerId;
        }

        #endregion
    }
}
