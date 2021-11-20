using goods.Commands;
using goods.Models;
using goods.Models.Dto;
using goods.Models.Enums;
using goods.Services;
using goods.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace goods.ViewModels
{
    public class ProviderFormViewModel : BaseViewModel
    {
        private const string ADD_NEW_PROVIDER = "-- Добави нов доставчик --";

        private Provider provider;
        private ProviderValidation providerValidation;
        private string successMessage;
        private ProviderService providerService;
        private DeliveryService deliveryService;
        private ObservableCollection<string> providerValues;
        private string selectedProviderValue;
        private string labelText;
        private ObservableCollection<DeliveryOilDto> deliveryOilDtos; //TODO: show them in the view layer

        public RelayCommand CreateCommand { get; }
        public ICommand NavigationBackCommand { get; }

        public ProviderFormViewModel(NavigationStore navigationStore)
        {
            CreateCommand = new RelayCommand(Create);
            NavigationBackCommand = new NavigateCommand<FormHomeViewModel>(navigationStore, (n) => new FormHomeViewModel(n));
            providerService = new ProviderService();
            deliveryService = new DeliveryService();
            providerValidation = new ProviderValidation();
            InitProvider();
            InitDeliveryOilDtos();
        }

        private void InitProvider()
        {
            Provider = new Provider();
            UpdateProviderValus(ADD_NEW_PROVIDER);
            LabelText = Enum.GetName(typeof(OperationType), OperationType.Създаване);
        }

        private void InitDeliveryOilDtos()
        {
            //TODO: create button for delivery date
            if (Provider.Id != null)
            {
                List<DeliveryOilDto> deliveries = deliveryService.GetDeliveryOilDtos(null, DateTime.MaxValue, Provider.Id);
                DeliveryOilDtos = new ObservableCollection<DeliveryOilDto>(deliveries);
            }
            else
            {
                DeliveryOilDtos = new ObservableCollection<DeliveryOilDto>();
            }
        }

        private void UpdateProviderValus(string defaultValue)
        {
            ProviderValues = new ObservableCollection<string>(providerService.GetAll().Select(a => $"{a.Id} - {a.Name}").ToList());
            ProviderValues.Insert(0, ADD_NEW_PROVIDER);
            SelectedProviderValue = defaultValue;
        }

        public Provider Provider
        {
            get { return provider; }
            set { provider = value; OnPropertyChanged(nameof(Provider)); }
        }

        public ProviderValidation ProviderValidation
        {
            get { return providerValidation; }
            set { providerValidation = value; OnPropertyChanged(nameof(ProviderValidation)); }
        }

        public string SuccessMessage
        {
            get { return successMessage; }
            set { successMessage = value; OnPropertyChanged(nameof(SuccessMessage)); }
        }

        public ObservableCollection<string> ProviderValues
        {
            get { return providerValues; }
            set { providerValues = value; OnPropertyChanged(nameof(ProviderValues)); }
        }

        public string LabelText
        {
            get { return labelText; }
            set { labelText = value; OnPropertyChanged(nameof(LabelText)); }
        }

        public ObservableCollection<DeliveryOilDto> DeliveryOilDtos
        {
            get { return deliveryOilDtos; }
            set { deliveryOilDtos = value; OnPropertyChanged(nameof(DeliveryOilDtos)); }
        }

        public string SelectedProviderValue
        {
            get { return selectedProviderValue; }
            set
            {
                selectedProviderValue = value;
                OnPropertyChanged(nameof(SelectedProviderValue));
                SwapProvider(selectedProviderValue);
                InitDeliveryOilDtos();
            }
        }

        private void SwapProvider(string selectedProviderValue)
        {
            SuccessMessage = string.Empty;
            ProviderValidation = new ProviderValidation();

            if (!selectedProviderValue.Equals(ADD_NEW_PROVIDER))
            {
                string providerId = selectedProviderValue.Split('-')[0].Trim();
                Provider = providerService.FindById(providerId);
                LabelText = Enum.GetName(typeof(OperationType), OperationType.Редактиране);
            }
            else
            {
                Provider = new Provider();
                LabelText = Enum.GetName(typeof(OperationType), OperationType.Създаване);
            }
        }

        private void Create()
        {
            if (!ProviderValidation.ValidateProvider(Provider))
            {
                if (!selectedProviderValue.Equals(ADD_NEW_PROVIDER))
                {
                    providerService.Update(Provider);
                    UpdateProviderValus($"{Provider.Id} - {Provider.Name}");
                    SuccessMessage = "Успешно редактирахте доставчика!";
                }
                else
                {
                    providerService.Save(Provider);
                    InitProvider();
                    SelectedProviderValue = ADD_NEW_PROVIDER;
                    SuccessMessage = "Успешно създадохте нов доставчик!";
                }
            }
            else
            {
                SuccessMessage = string.Empty;
            }
        }
    }
}
