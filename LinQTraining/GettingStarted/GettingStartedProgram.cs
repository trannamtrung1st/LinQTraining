namespace LinQTraining.GettingStarted
{
    public static class GettingStartedProgram
    {
        private static void PrintList<T>(IEnumerable<T> list,
            Func<T, string> toString)
        {
            Console.WriteLine();
            foreach (var item in list) Console.WriteLine(toString(item));
            Console.WriteLine();
        }

        public static void Run()
        {
            GetAllProductsAndCategories();

            GetAllProductsWithCategory();

            GetProductById();

            GetAllProductsHavingNameStartWithA();

            GetAllProductsHavingCategoryNameContainsE();

            GetAllProductsGroupedByCategory();

            GetProductOrderByCategoryNameDescAndProductNameAsc();

            GetCountOfProductsGroupedByCategoryAndTheCategoryName();
        }

        private static void GetAllProductsAndCategories()
        {
            Console.WriteLine("\n=== GetAllProductsAndCategories ===");

            var products = Data.Products.ToList();
            var categories = Data.Categories.ToList();
            PrintList(products, i => i.ToString()); PrintList(categories, i => i.ToString());

            Console.WriteLine("=== GetAllProductsAndCategories ===\n");
        }

        private static void GetAllProductsWithCategory()
        {
            Console.WriteLine("\n=== GetAllProductsWithCategory ===");

            var productsWithCategory = from product in Data.Products
                                       join category in Data.Categories on product.CategoryId equals category.Id
                                       select new Product
                                       {
                                           Id = product.Id,
                                           Name = product.Name,
                                           CategoryId = product.CategoryId,
                                           Category = category
                                       };

            PrintList(productsWithCategory, i => $"{i} | {i.Category}");

            Console.WriteLine("=== GetAllProductsWithCategory ===\n");
        }

        private static void GetProductById()
        {
            Console.WriteLine("\n=== GetProductById ===");

            var productById = (from product in Data.Products
                               where product.Id == "banana"
                               select product).FirstOrDefault();

            Console.WriteLine(productById);

            Console.WriteLine("=== GetProductById ===\n");
        }

        private static void GetAllProductsHavingNameStartWithA()
        {
            Console.WriteLine("\n=== GetAllProductsHavingNameStartWithA ===");

            var products = from product in Data.Products
                           where product.Name.StartsWith("A", StringComparison.OrdinalIgnoreCase)
                           select product;

            PrintList(products, i => i.ToString());

            Console.WriteLine("=== GetAllProductsHavingNameStartWithA ===\n");
        }

        private static void GetAllProductsHavingCategoryNameContainsE()
        {
            Console.WriteLine("\n=== GetAllProductsHavingCategoryNameContainsE ===");

            var products = from product in Data.Products
                           join category in Data.Categories on product.CategoryId equals category.Id
                           where category.Name.Contains("E", StringComparison.OrdinalIgnoreCase)
                           select new
                           {
                               Product = product,
                               Category = category
                           };

            PrintList(products, i => $"{i.Product} | {i.Category}");

            Console.WriteLine("=== GetAllProductsHavingCategoryNameContainsE ===\n");
        }

        private static void GetAllProductsGroupedByCategory()
        {
            Console.WriteLine("\n=== GetAllProductsGroupedByCategory ===");

            var groups = from product in Data.Products
                         group product by product.CategoryId;

            foreach (var group in groups)
            {
                Console.WriteLine($"Group: {group.Key}");

                PrintList(group, i => i.ToString());
            }

            Console.WriteLine("=== GetAllProductsGroupedByCategory ===\n");
        }

        private static void GetProductOrderByCategoryNameDescAndProductNameAsc()
        {
            Console.WriteLine("\n=== GetProductOrderByCategoryNameDescAndProductNameAsc ===");

            var products = from product in Data.Products
                           join category in Data.Categories on product.CategoryId equals category.Id
                           orderby category.Name descending, product.Name ascending
                           select new
                           {
                               Product = product,
                               Category = category
                           };

            PrintList(products, i => $"{i.Product} | {i.Category}");

            Console.WriteLine("=== GetProductOrderByCategoryNameDescAndProductNameAsc ===\n");
        }

        private static void GetCountOfProductsGroupedByCategoryAndTheCategoryName()
        {
            Console.WriteLine("\n=== GetCountOfProductsGroupedByCategoryAndTheCategoryName ===");

            var products = from product in Data.Products
                           group product by product.CategoryId into groups
                           join category in Data.Categories on groups.Key equals category.Id
                           select new
                           {
                               Category = category,
                               Count = groups.Count()
                           };

            PrintList(products, i => $"{i.Category} | Product count: {i.Count}");

            Console.WriteLine("=== GetCountOfProductsGroupedByCategoryAndTheCategoryName ===\n");
        }
    }
}
