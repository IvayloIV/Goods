using goods.Commands;
using goods.Models;
using goods.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace goods.ViewModels
{
    public class StockFormViewModel : BaseViewModel
    {
        private Stock stock;
        private string[] measures;
        private string selectedMeasure;

        public RelayCommand CreateCommand { get; }
        public ICommand NavigationBackCommand { get; }

        public StockFormViewModel(NavigationStore navigationStore)
        {
            CreateCommand = new RelayCommand(Create);
            NavigationBackCommand = new NavigateCommand<FormHomeViewModel>(navigationStore, (n) => new FormHomeViewModel(n));
            measures = Enum.GetNames(typeof(StockMeasure));
            selectedMeasure = measures[0];
            stock = new Stock();
            stock.CreationDate = DateTime.Now;
        }

        public Stock Stock
        {
            get { return stock; }
            set { stock = value; OnPropertyChanged(nameof(Stock)); }
        }

        public string[] Measures
        {
            get { return measures; }
        }

        public string SelectedMeasure
        {
            get { return selectedMeasure; }
            set { selectedMeasure = value; OnPropertyChanged(nameof(SelectedMeasure)); }
        }

        private void Create()
        {
            Console.WriteLine();
        }
    }
}
