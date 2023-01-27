using LinQTraining.SameQueryForMultiProviders.DataSources;

namespace LinQTraining.SameQueryForMultiProviders
{
    public static class SameQueryForMultiProvidersProgram
    {
        public static void Run()
        {
            Query(ObjectsDataSource.Products.AsQueryable(), "Objects");

            using LinqExtensions.LinQContext context = EFCoreDataSource.Context;
            Query(context.Product, "EFCore");
        }

        static IEnumerable<Product> Query(IQueryable<Product> dataSource, string sourceName)
        {
            IQueryable<Product> query = from product in dataSource
                                        where product.Category.Name == "fruit"
                                        select product;
            Product[] filteredProducts = query.ToArray();

            Console.WriteLine($"======{sourceName}======");
            foreach (Product product in filteredProducts) { Console.WriteLine(product); }
            Console.WriteLine();


            Product[] orderedProducts = (from product in query
                                         orderby product.Name
                                         select product).ToArray();
            foreach (Product product in orderedProducts) { Console.WriteLine(product); }
            Console.WriteLine();

            return filteredProducts;
        }
    }
}
