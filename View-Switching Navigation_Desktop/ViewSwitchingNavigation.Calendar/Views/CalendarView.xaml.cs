

using System.ComponentModel.Composition;
using System.Windows.Controls;
using ViewSwitchingNavigation.Calendar.ViewModels;

namespace ViewSwitchingNavigation.Calendar.Views
{
    [Export("CalendarView")]
    public partial class CalendarView : UserControl
    {
        public CalendarView()
        {
            InitializeComponent();
        }

        [Import]
        public CalendarViewModel ViewModel
        {
            set { this.DataContext = value; }
        }
    }
}
