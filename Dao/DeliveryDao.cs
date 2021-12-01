﻿using goods.Models;
using goods.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Dao
{
    public class DeliveryDao
    {
        private readonly GoodsContext goodsContext;

        public DeliveryDao()
        {
            goodsContext = GoodsContextSingleton.GetContext();
        }

        public void Save(Delivery delivery)
        {
            goodsContext.Deliveries.Add(delivery);
            goodsContext.SaveChanges();
        }

        public void Update(Delivery delivery)
        {
            Delivery currentDelivery = FindByProviderIdAndStockId(delivery.ProviderId, delivery.StockId);
            currentDelivery.DeliveryDate = delivery.DeliveryDate;
            currentDelivery.DocumentNumber = delivery.DocumentNumber;
            currentDelivery.Quantity = delivery.Quantity;
            goodsContext.SaveChanges();
        }

        public Delivery FindByProviderIdAndStockId(long providerId, long stockId)
        {
            return goodsContext.Deliveries
                .Where(d => d.ProviderId.Equals(providerId) && d.StockId.Equals(stockId))
                .FirstOrDefault();
        }

        public List<Delivery> FindByProviderId(long providerId)
        {
            return goodsContext.Deliveries
                .Where(d => d.ProviderId.Equals(providerId))
                .ToList();
        }

        public List<Delivery> GetAll()
        {
            return goodsContext.Deliveries.ToList();
        }

        public List<DeliveryOilDto> GetDeliveryOilDtos(string stockName, DateTime deliveryDate, long providerId)
        {
            IQueryable<Delivery> query = goodsContext.Deliveries
                .Include("Stock")
                .Where(d => d.DeliveryDate.CompareTo(deliveryDate) <= 0);

            if (stockName != null && stockName.Length > 0)
            {
                query = query.Where(d => d.Stock.Name.Equals(stockName));
            }

            if (providerId != 0)
            {
                query = query.Where(d => d.ProviderId.Equals(providerId));
            }

            return query.OrderByDescending(d => d.Quantity)
                .ToList()
                .Select(d =>
                {
                    DeliveryOilDto dod = new DeliveryOilDto();
                    dod.ProviderId = d.ProviderId;
                    dod.StockName = d.Stock.Name;
                    dod.PriceVAT = d.Stock.Price * 1.2;
                    dod.QuantityValue = $"{d.Quantity} {d.Stock.Measure}";
                    dod.DeliveryDate = d.DeliveryDate;
                    return dod;
                })
                .ToList();
        }
    }
}
