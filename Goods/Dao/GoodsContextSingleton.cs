using System.Linq;

namespace Goods.Dao
{
    public sealed class GoodsContextSingleton
    {
        private static IGoodsContext goodsContext;

        private GoodsContextSingleton() { }

        public static void InitContext(IGoodsContext goodsContext)
        {
            GoodsContextSingleton.goodsContext = goodsContext;
            if (goodsContext.Database != null)
            {
                goodsContext.Database.SqlQuery<int>("select 1").First();
            }
        }

        public static void DestroyContext()
        {
            goodsContext.Dispose();
        }

        public static IGoodsContext GetContext()
        {
            return goodsContext;
        }
    }
}
