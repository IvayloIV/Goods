using System;

namespace Goods.Models.Dto
{
    public class DeliveryOilDto
    {
        public long ProviderId { get; set; }
        public string StockName { get; set; }
        public double PriceVAT { get; set; }
        public string QuantityValue { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
