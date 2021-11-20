using System;

namespace goods.Models
{
    public class Delivery : BaseModel
    {
        private string stockId;
        private string providerId;
        private long documentNumber;
        private DateTime deliveryDate;
        private long quantity;

        public string StockId
        {
            get { return stockId; }
            set { stockId = value; OnPropertyChanged(nameof(StockId)); }
        }

        public string ProviderId
        {
            get { return providerId; }
            set { providerId = value; OnPropertyChanged(nameof(ProviderId)); }
        }

        public long DocumentNumber
        {
            get { return documentNumber; }
            set { documentNumber = value; OnPropertyChanged(nameof(DocumentNumber)); }
        }

        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; OnPropertyChanged(nameof(DeliveryDate)); }
        }

        public long Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }
    }
}
