using Microsoft.EntityFrameworkCore;

namespace LinQTraining.EFCoreAdvanced
{
    public static class SplitQueries
    {
        public static void Run()
        {
            CartesianExplosion();

            DataDuplication();

            SplitQuery();
        }

        public static void CartesianExplosion()
        {
            using LinQContext context = new LinQContext();

            // Occur
            var query1 = context.Company
                .Include(p => p.Products)
                .Include(p => p.ProductCategories);

            Console.WriteLine(query1.ToQueryString());
            query1.ToList();

            // Not occur
            var query2 = context.Company
                .Include(p => p.ProductCategories)
                .ThenInclude(p => p.Products);

            Console.WriteLine(query2.ToQueryString());
            query2.ToList();
        }

        public static void DataDuplication()
        {
            using LinQContext context = new LinQContext();

            var query1 = context.Company
                .Include(p => p.Products);

            // Big data column could cause performance issues
            Console.WriteLine(query1.ToQueryString());
            query1.ToList();
        }

        public static void SplitQuery()
        {
            using LinQContext context = new LinQContext();

            var query1 = context.Company
                .Include(p => p.Products)
                .AsSplitQuery();

            query1.ToList();

            var query2 = context.Company
                .Include(p => p.Products)
                .Include(p => p.ProductCategories)
                .AsSplitQuery();

            query2.ToList();
        }
    }
}
