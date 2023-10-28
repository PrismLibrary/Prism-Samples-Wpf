using Prism.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UsingCustomWindow.Views
{
    /// <summary>
    /// MyCustomWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MyCustomWindow : Window, IDialogWindow
    {
        public IDialogResult Result { get; set; }

        public MyCustomWindow()
        {
            InitializeComponent();
        }
    }
}
