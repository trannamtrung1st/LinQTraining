using Microsoft.EntityFrameworkCore;

namespace LinQTraining.EFCoreAdvanced
{
    public static class N1Queries
    {
        public static void Run()
        {
            Issue();

            Solution();
        }

        public static LinQContext GetLazyLoadingEnabledContext()
        {
            var options = new DbContextOptionsBuilder<LinQContext>()
                .UseSqlServer(LinQContext.DefaultConnectionString)
                .UseLazyLoadingProxies()
                .LogTo(Console.WriteLine, minimumLevel: Microsoft.Extensions.Logging.LogLevel.Information)
                .Options;
            var context = new LinQContext(options);
            return context;
        }

        public static void Issue()
        {
            using LinQContext context = GetLazyLoadingEnabledContext();

            var products = context.Product;

            foreach (var product in products)
            {
                var category = product.Category;

                Console.WriteLine($"{product} - {category}");
            }
        }

        public static void Solution()
        {
            using LinQContext context = GetLazyLoadingEnabledContext();

            IQueryable<Product> products = context.Product
                .Include(p => p.Category);

            foreach (var product in products)
            {
                var category = product.Category;

                Console.WriteLine($"{product} - {category}");
            }

            products = context.Product.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category
            });

            foreach (var product in products)
            {
                var category = product.Category;

                Console.WriteLine($"{product} - {category}");
            }
        }
    }
}
