using goods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Services
{
    public class ProviderService
    {
        private List<Provider> providers = new List<Provider>()
        {
            new Provider() { Id="1", Name="Иванов ЕОД", Address="Николаевска 12", Phone="0892423124", ContactPerson="Иван Иванов" },
            new Provider() { Id="2", Name="Стефанови ЕОД", Address="Ивановска 44", Phone="0883442334", ContactPerson="Стефан Петров" },
            new Provider() { Id="3", Name="Петрови ЕОД", Address="Стамболийска 18", Phone="0893452367", ContactPerson="Петър Иванов" },
            new Provider() { Id="4", Name="Мая ЕОД", Address="Петровска 23", Phone="0897005345", ContactPerson="Мая Маринова" },
            new Provider() { Id="5", Name="Георгиев ЕОД", Address="Обеля 11", Phone="0893442576", ContactPerson="Георги Георгиев" }
        };

        private List<Delivery> diliveries = new List<Delivery>()
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


    }
}
