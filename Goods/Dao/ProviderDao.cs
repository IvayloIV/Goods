using Goods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Dao
{
    public class ProviderDao
    {
        private readonly IGoodsContext goodsContext;

        public ProviderDao()
        {
            goodsContext = GoodsContextSingleton.GetContext();
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
