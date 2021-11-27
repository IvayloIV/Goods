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
        private readonly GoodsContext goodsContext;

        public ProviderService()
        {
            goodsContext = new GoodsContext();
        }

        public void Save(Provider provider)
        {
            goodsContext.Providers.Add(provider);
            goodsContext.SaveChanges();
        }

        public void Update(Provider provider)
        {
            Provider currentProvider = FindById(provider.Id);
            currentProvider.Name = provider.Name;
            currentProvider.Address = provider.Address;
            currentProvider.Phone = provider.Phone;
            currentProvider.ContactPerson = provider.ContactPerson;
            goodsContext.SaveChanges();
        }

        public Provider FindById(long id)
        {
            return goodsContext.Providers.Find(id);
        }

        public List<Provider> GetAll()
        {
            return goodsContext.Providers.ToList();
        }
    }
}
