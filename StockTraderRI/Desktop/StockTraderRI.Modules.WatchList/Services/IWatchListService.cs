

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StockTraderRI.Modules.Watch.Services
{
    public interface IWatchListService
    {
        ObservableCollection<string> RetrieveWatchList();
        ICommand AddWatchCommand { get; set; }
    }
}