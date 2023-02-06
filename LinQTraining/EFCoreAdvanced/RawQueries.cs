using Microsoft.EntityFrameworkCore;

namespace LinQTraining.EFCoreAdvanced
{
    public static class RawQueries
    {
        public static void Run()
        {
            RawQueryWithEntity();

            RawQueryWithoutEntity();
        }

        public static void RawQueryWithEntity()
        {
            using LinQContext context = new LinQContext();

            IQueryable<Product> query = context.Product.FromSqlInterpolated($"SELECT * FROM Product");
            string queryText = query.ToQueryString();
            Console.WriteLine(queryText);
            List<Product> data = query.ToList();
            foreach (var product in data) Console.WriteLine(product);
        }

        public static void RawQueryWithoutEntity()
        {
            using LinQContext context = new LinQContext();

            IQueryable<GetProductCountByCategoryView> query = context.GetProductCountByCategoryView
                .FromSqlInterpolated(
$@"
SELECT p.CategoryId, COUNT(*) as ProductCount 
FROM Product p
GROUP BY p.CategoryId");

            string queryText = query.ToQueryString();
            Console.WriteLine(queryText);
            List<GetProductCountByCategoryView> data = query.ToList();
            foreach (var record in data) Console.WriteLine(record);
        }
    }
}
