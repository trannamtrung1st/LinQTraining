namespace LinQTraining
{
    public static class Data
    {
        public static IEnumerable<Product> Products => new[]
        {
            new Product { Id = "apple", Name = "apple", CategoryId = "fruit" },
            new Product { Id = "banana", Name = "banana", CategoryId = "fruit" },
            new Product { Id = "orange", Name = "orange", CategoryId = "fruit" },
            new Product { Id = "grape", Name = "grape", CategoryId = "fruit" },
            new Product { Id = "mango", Name = "mango", CategoryId = "fruit" },
            new Product { Id = "television", Name = "television", CategoryId = "electronic" },
            new Product { Id = "laptop", Name = "laptop", CategoryId = "electronic" },
            new Product { Id = "keyboard", Name = "keyboard", CategoryId = "electronic" },
            new Product { Id = "monitor", Name = "monitor", CategoryId = "electronic" },
            new Product { Id = "knife", Name = "knife", CategoryId = "household" },
            new Product { Id = "spoon", Name = "spoon", CategoryId = "household" },
            new Product { Id = "bowl", Name = "bowl", CategoryId = "household" },
            new Product { Id = "chopsticks", Name = "chopsticks", CategoryId = "household" },
        };

        public static IEnumerable<ProductCategory> Categories => new[]
        {
            new ProductCategory { Id = "fruit", Name = "fruit" },
            new ProductCategory { Id = "electronic", Name = "electronic" },
            new ProductCategory { Id = "household", Name = "household" },
            new ProductCategory { Id = "misc", Name = "misc" },
        };
    }

    public class ProductCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }

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
