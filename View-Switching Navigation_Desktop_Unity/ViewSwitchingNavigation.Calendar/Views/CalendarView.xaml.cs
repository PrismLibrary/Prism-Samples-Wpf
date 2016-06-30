

using System.Windows.Controls;
using ViewSwitchingNavigation.Calendar.ViewModels;

namespace ViewSwitchingNavigation.Calendar.Views
{
    public partial class CalendarView : UserControl
    {
        public CalendarView(CalendarViewModel viewModel)
        {
            InitializeComponent();

            this.ViewModel = viewModel;
        }

        public CalendarViewModel ViewModel
        {
            set { this.DataContext = value; }
        }
    }
}
