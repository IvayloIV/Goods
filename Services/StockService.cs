using goods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Services
{
    public class StockService
    {
        private static List<Stock> stocks = new List<Stock>()
        {
            new Stock() { Id="1", Name="Олио", CreationDate=DateTime.Now.AddDays(-3), DaysValidTo=7, Price=4.5, Measure=StockMeasure.Литри },
            new Stock() { Id="2", Name="Яйце", CreationDate=DateTime.Now.AddDays(-5), DaysValidTo=10, Price=0.4, Measure=StockMeasure.Брой },
            new Stock() { Id="3", Name="Хляб", CreationDate=DateTime.Now.AddDays(-2), DaysValidTo=5, Price=1.2, Measure=StockMeasure.Килограми },
            new Stock() { Id="4", Name="Сирене", CreationDate=DateTime.Now.AddDays(-2), DaysValidTo=3, Price=6.2, Measure=StockMeasure.Килограми },
            new Stock() { Id="5", Name="Бира", CreationDate=DateTime.Now.AddDays(-1), DaysValidTo=20, Price=2.5, Measure=StockMeasure.Литри }
        };

        public void Save(Stock stock)
        {
            stock.Id = (stocks.Count + 1).ToString();
            stocks.Add(stock);
        }

        public void Update(Stock stock)
        {
            Stock currentStock = FindById(stock.Id);
            currentStock.Name = stock.Name;
            currentStock.CreationDate = stock.CreationDate;
            currentStock.DaysValidTo = stock.DaysValidTo;
            currentStock.Price = stock.Price;
            currentStock.Measure = stock.Measure;
        }

        public Stock FindById(string id)
        {
            return stocks.Find(s => s.Id == id);
        }

        public List<Stock> GetAll()
        {
            return stocks;
        }
    }
}
