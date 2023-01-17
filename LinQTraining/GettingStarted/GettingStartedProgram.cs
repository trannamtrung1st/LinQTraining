namespace LinQTraining.GettingStarted
{
    public static class GettingStartedProgram
    {
        private static IEnumerable<Product> Products;
        private static IEnumerable<ProductCategory> Categories;

        private static void Init()
        {
            var productList = new List<Product>();
            var categoryList = new List<ProductCategory>();
            Products = productList;
            Categories = categoryList;

            productList.AddRange(new[]
            {
                new Product { Id = "apple", Name = "apple", CategoryId = "food" },
                new Product { Id = "banana", Name = "banana", CategoryId = "food" },
                new Product { Id = "orange", Name = "orange", CategoryId = "food" },
                new Product { Id = "grape", Name = "grape", CategoryId = "food" },
                new Product { Id = "television", Name = "television", CategoryId = "electronic" },
                new Product { Id = "laptop", Name = "laptop", CategoryId = "electronic" },
                new Product { Id = "keyboard", Name = "keyboard", CategoryId = "electronic" },
                new Product { Id = "monitor", Name = "monitor", CategoryId = "electronic" },
                new Product { Id = "knife", Name = "knife", CategoryId = "household" },
                new Product { Id = "spoon", Name = "spoon", CategoryId = "household" },
                new Product { Id = "bowl", Name = "bowl", CategoryId = "household" },
                new Product { Id = "chopsticks", Name = "chopsticks", CategoryId = "household" },
            });

            categoryList.AddRange(new[]
            {
                new ProductCategory { Id = "food", Name = "food" },
                new ProductCategory { Id = "electronic", Name = "electronic" },
                new ProductCategory { Id = "household", Name = "household" }
            });
        }

        private static void PrintList<T>(IEnumerable<T> list,
            Func<T, string> toString)
        {
            Console.WriteLine();
            foreach (var item in list) Console.WriteLine(toString(item));
            Console.WriteLine();
        }

        public static void Run()
        {
            Init();

            GetAllProductsAndCategories();

            GetAllProductsWithCategory();

            GetProductById();
        }

        private static void GetAllProductsAndCategories()
        {
            Console.WriteLine("\n=== SelectAllProductsAndCategories ===");

            var products = Products.ToList();
            var categories = Categories.ToList();
            PrintList(products, i => i.ToString()); PrintList(categories, i => i.ToString());

            Console.WriteLine("=== SelectAllProductsAndCategories ===\n");
        }

        private static void GetAllProductsWithCategory()
        {
            Console.WriteLine("\n=== SelectAllProductsWithCategory ===");

            var productsWithCategory = from product in Products
                                       join category in Categories on product.CategoryId equals category.Id
                                       select new Product
                                       {
                                           Id = product.Id,
                                           Name = product.Name,
                                           CategoryId = product.CategoryId,
                                           Category = category
                                       };

            PrintList(productsWithCategory, i => $"{i} | {i.Category}");

            Console.WriteLine("=== SelectAllProductsWithCategory ===\n");
        }

        private static void GetProductById()
        {
            Console.WriteLine("\n=== SelectProductById ===");

            var productById = (from product in Products
                               where product.Id == "banana"
                               select product).FirstOrDefault();

            Console.WriteLine(productById);

            Console.WriteLine("=== SelectProductById ===\n");
        }
    }

    public class ProductCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"Category: {Name}";
    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }

        public ProductCategory Category { get; set; }

        public override string ToString() => $"Product: {Name}";
    }
}
