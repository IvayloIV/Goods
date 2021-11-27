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
    public class ReportHomeViewModel : BaseViewModel
    {
        public ICommand NavigationDeliveriesCommand { get; }
        public ICommand NavigationBackCommand { get; }

        public ReportHomeViewModel(NavigationStore navigationStore)
        {
            NavigationDeliveriesCommand = new NavigateCommand<DeliveryReportViewModel>(navigationStore, (n) => new DeliveryReportViewModel(n));
            NavigationBackCommand = new NavigateCommand<HomeViewModel>(navigationStore, (n) => new HomeViewModel(n));
        }
    }
}
