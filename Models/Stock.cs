using System;

namespace goods.Models
{
    public class Stock : BaseModel
    {
        private string id;
        private string name;
        private DateTime creationDate;
        private int daysValidTo;
        private double price;
        private StockMeasure measure;

        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; OnPropertyChanged(nameof(CreationDate)); }
        }

        public int DaysValidTo
        {
            get { return daysValidTo; }
            set { daysValidTo = value; OnPropertyChanged(nameof(DaysValidTo)); }
        }

        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(nameof(Price)); }
        }

        public StockMeasure Measure
        {
            get { return measure; }
            set { measure = value; OnPropertyChanged(nameof(Measure)); }
        }
    }
}
