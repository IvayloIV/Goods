using goods.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Dao
{
    public class GoodsContext : DbContext
    {
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        public GoodsContext() : base(nameOrConnectionString: "GoodsDbConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("goods");
            base.OnModelCreating(modelBuilder);
        }
    }
}
