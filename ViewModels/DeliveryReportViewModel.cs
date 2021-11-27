using goods.Commands;
using goods.Models.Dto;
using goods.Services;
using goods.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace goods.ViewModels
{
    public class DeliveryReportViewModel : BaseViewModel
    {
        public ICommand NavigationBackCommand { get; }

        private DeliveryService deliveryService;
        private List<DeliveryOilDto> deliveriesOilDto;

        private string stockName;
        private DateTime deliveryDate;

        public DeliveryReportViewModel(NavigationStore navigationStore)
        {
            NavigationBackCommand = new NavigateCommand<ReportHomeViewModel>(navigationStore, (n) => new ReportHomeViewModel(n));
            deliveryService = new DeliveryService();
            DeliveryDate = DateTime.Now;
            DeliveriesOilDto = deliveryService.GetDeliveryOilDtos(null, DeliveryDate, 0);
        }

        public List<DeliveryOilDto> DeliveriesOilDto
        {
            get { return deliveriesOilDto; }
            set { deliveriesOilDto = value; OnPropertyChanged(nameof(DeliveriesOilDto)); }
        }

        public string StockName
        {
            get { return stockName; }
            set { stockName = value; OnPropertyChanged(nameof(StockName)); }
        }

        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; OnPropertyChanged(nameof(DeliveryDate)); }
        }
    }
}
