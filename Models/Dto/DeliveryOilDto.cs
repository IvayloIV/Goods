using System;

namespace goods.Models.Dto
{
    public class DeliveryOilDto
    {
        public string ProviderId { get; set; }
        public string StockName { get; set; }
        public double PriceVAT { get; set; }
        public string QuantityValue { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
