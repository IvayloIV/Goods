using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Models.Dto
{
    public class StockSummaryDto
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Measure { get; set; }

        public int DeliveryCount { get; set; }

        public int TotalQuantity { get; set; }

        public StockSummaryDto(string name, double price, string measure, int deliveryCount, int totalQuantity)
        {
            this.Name = name;
            this.Price = price;
            this.Measure = measure;
            this.DeliveryCount = deliveryCount;
            this.TotalQuantity = totalQuantity;
        }
    }
}
