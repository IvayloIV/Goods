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
        private static List<Provider> providers = new List<Provider>()
        {
            new Provider() { Id="1", Name="Иванов ЕОД", Address="Николаевска 12", Phone="0892423124", ContactPerson="Иван Иванов" },
            new Provider() { Id="2", Name="Стефанови ЕОД", Address="Ивановска 44", Phone="0883442334", ContactPerson="Стефан Петров" },
            new Provider() { Id="3", Name="Петрови ЕОД", Address="Стамболийска 18", Phone="0893452367", ContactPerson="Петър Иванов" },
            new Provider() { Id="4", Name="Мая ЕОД", Address="Петровска 23", Phone="0897005345", ContactPerson="Мая Маринова" },
            new Provider() { Id="5", Name="Георгиев ЕОД", Address="Обеля 11", Phone="0893442576", ContactPerson="Георги Георгиев" }
        };

        public void Save(Provider provider)
        {
            provider.Id = (providers.Count + 1).ToString();
            providers.Add(provider);
        }

        public void Update(Provider provider)
        {
            Provider currentProvider = FindById(provider.Id);
            currentProvider.Name = provider.Name;
            currentProvider.Address = provider.Address;
            currentProvider.Phone = provider.Phone;
            currentProvider.ContactPerson = provider.ContactPerson;
        }

        public Provider FindById(string id)
        {
            return providers.Find(s => s.Id == id);
        }

        public List<Provider> GetAll()
        {
            return providers;
        }
    }
}
