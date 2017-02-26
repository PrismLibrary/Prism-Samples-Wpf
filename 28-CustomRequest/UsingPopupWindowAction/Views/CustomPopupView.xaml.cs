using Prism.Interactivity.InteractionRequest;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UsingPopupWindowAction.Views
{
    /// <summary>
    /// Interaction logic for CustomPopupView.xaml
    /// </summary>
    public partial class CustomPopupView : UserControl, IInteractionRequestAware
    {
        public CustomPopupView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FinishInteraction?.Invoke();
        }

        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }
    }
}
