// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StateBasedNavigation.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for InfoTipToggleButton.xaml
    /// </summary>
    public partial class InfoTipToggleButton : ToggleButton
    {
        private static WeakReference weakOpenToggleButton;
        public InfoTipToggleButton()
        {
            this.InitializeComponent();

            this.IsThreeState = false;
            this.Checked += InfoTipToggleButton_Checked;
            this.Unchecked += InfoTipToggleButton_Checked;
        }

        public static readonly DependencyProperty PopupProperty =
            DependencyProperty.Register("PopupProperty", typeof(Popup), typeof(InfoTipToggleButton));

        public Popup Popup
        {
            get { return (Popup)this.GetValue(PopupProperty); }
            set { this.SetValue(PopupProperty, value); }
        }

        void InfoTipToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((weakOpenToggleButton != null) &&
                (weakOpenToggleButton.IsAlive) &&
                (weakOpenToggleButton.Target != this))
            {
                ((ToggleButton)weakOpenToggleButton.Target).IsChecked = false;
            }

            if (this.Popup != null)
            {
                if (this.IsChecked.HasValue && this.IsChecked.Value)
                {
                    this.Popup.PlacementTarget = this;
                    this.Popup.Placement = PlacementMode.Bottom;
                    this.Popup.IsOpen = true;
                    weakOpenToggleButton = new WeakReference(this);
                }
                else
                {
                    this.Popup.IsOpen = false;
                    if ((weakOpenToggleButton != null) &&
                        (weakOpenToggleButton.IsAlive) &&
                        (weakOpenToggleButton.Target == this))
                    {
                        weakOpenToggleButton = null;
                    }
                }
            }

        }
    }
}
