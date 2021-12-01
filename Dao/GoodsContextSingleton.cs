using System.Linq;

namespace goods.Dao
{
    public sealed class GoodsContextSingleton
    {
        private static GoodsContext goodsContext;

        private GoodsContextSingleton() { }

        public static void InitContext(GoodsContext goodsContext)
        {
            GoodsContextSingleton.goodsContext = goodsContext;
            goodsContext.Database.SqlQuery<int>("select 1").First();
        }

        public static void DestroyContext()
        {
            goodsContext.Dispose();
        }

        public static GoodsContext GetContext()
        {
            return goodsContext;
        }
    }
}
