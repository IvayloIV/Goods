using goods.Models;
using goods.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Services
{
    public class DeliveryService
    {
        private static List<Delivery> deliveries = new List<Delivery>()
        {
            new Delivery() { ProviderId="1", StockId="2", DeliveryDate=DateTime.Now.AddDays(-1), DocumentNumber=103, Quantity=2 },
            new Delivery() { ProviderId="1", StockId="3", DeliveryDate=DateTime.Now.AddDays(-2), DocumentNumber=104, Quantity=3 },
            new Delivery() { ProviderId="1", StockId="4", DeliveryDate=DateTime.Now.AddDays(-3), DocumentNumber=105, Quantity=5 },
            new Delivery() { ProviderId="1", StockId="5", DeliveryDate=DateTime.Now.AddDays(-4), DocumentNumber=106, Quantity=22 },
            new Delivery() { ProviderId="2", StockId="1", DeliveryDate=DateTime.Now.AddDays(-5), DocumentNumber=107, Quantity=11 },
            new Delivery() { ProviderId="2", StockId="2", DeliveryDate=DateTime.Now.AddDays(-6), DocumentNumber=108, Quantity=23 },
            new Delivery() { ProviderId="3", StockId="1", DeliveryDate=DateTime.Now.AddDays(-7), DocumentNumber=109, Quantity=55 },
            new Delivery() { ProviderId="4", StockId="3", DeliveryDate=DateTime.Now.AddDays(-8), DocumentNumber=110, Quantity=32 },
            new Delivery() { ProviderId="5", StockId="5", DeliveryDate=DateTime.Now.AddDays(-9), DocumentNumber=111, Quantity=7 },
            new Delivery() { ProviderId="5", StockId="2", DeliveryDate=DateTime.Now.AddDays(-10), DocumentNumber=112, Quantity=6 },
        };

        public void Save(Delivery delivery)
        {
            deliveries.Add(delivery);
        }

        public void Update(Delivery delivery)
        {
            Delivery currentDelivery = FindByProviderIdAndStockId(delivery.ProviderId, delivery.StockId);
            currentDelivery.DeliveryDate = delivery.DeliveryDate;
            currentDelivery.DocumentNumber = delivery.DocumentNumber;
            currentDelivery.Quantity = delivery.Quantity;
        }

        public Delivery FindByProviderIdAndStockId(string providerId, string stockId)
        {
            return deliveries.Find(d => d.ProviderId.Equals(providerId) && d.StockId.Equals(stockId));
        }

        public List<Delivery> FindByProviderId(string providerId)
        {
            return deliveries.Where(d => d.ProviderId.Equals(providerId)).ToList();
        }

        public List<Delivery> GetAll()
        {
            return deliveries;
        }

        public List<DeliveryOilDto> GetDeliveryOilDtos(string stockName, DateTime deliveryDate, string providerId)
        {
            StockService stockService = new StockService();
            ProviderService providerService = new ProviderService();
            List<DeliveryOilDto> deliveriesDto = new List<DeliveryOilDto>();

            foreach (Delivery delivery in deliveries)
            {
                Stock stock = stockService.FindById(delivery.StockId);
                Provider provider = providerService.FindById(delivery.ProviderId);

                if ((stockName == null || stock.Name.Equals(stockName))
                    && delivery.DeliveryDate.CompareTo(deliveryDate) < 0
                    && (providerId == null || provider.Id.Equals(providerId)))
                {
                    //TODO: order collection by delivery.Quantity DESC
                    DeliveryOilDto dod = new DeliveryOilDto();
                    dod.ProviderId = delivery.ProviderId;
                    dod.StockName = stock.Name;
                    dod.PriceVAT = stock.Price * 1.2;
                    dod.QuantityValue = $"{delivery.Quantity} {stock.Measure}";
                    dod.DeliveryDate = delivery.DeliveryDate;
                    deliveriesDto.Add(dod);
                }
            }

            return deliveriesDto;
        }
    }
}
