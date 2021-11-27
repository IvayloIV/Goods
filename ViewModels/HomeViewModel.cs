using goods.Commands;
using goods.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace goods.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand NavigationFormInputCommand { get; }
        public ICommand NavigationReportsCommand { get; }
        public ICommand NavigationExitCommand { get; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigationFormInputCommand = new NavigateCommand<FormHomeViewModel>(navigationStore, (n) => new FormHomeViewModel(n));
            NavigationReportsCommand = new NavigateCommand<ReportHomeViewModel>(navigationStore, (n) => new ReportHomeViewModel(n));
        }
    }
}
