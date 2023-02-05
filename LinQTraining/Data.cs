namespace LinQTraining
{
    public static class Data
    {
        public static IEnumerable<Company> Companies => Enumerable.Range(1, 200)
            .Select(i => new Company { Id = $"company{i}", Name = $"company{i}" });

        public static IEnumerable<Product> Products => new[]
        {
            new Product { Id = 1, Name = "apple", CategoryId = "fruit", CompanyId = "company1" },
            new Product { Id = 2, Name = "banana", CategoryId = "fruit", CompanyId = "company1" },
            new Product { Id = 3, Name = "orange", CategoryId = "fruit", CompanyId = "company1" },
            new Product { Id = 4, Name = "grape", CategoryId = "fruit", CompanyId = "company1" },
            new Product { Id = 5, Name = "mango", CategoryId = "fruit", CompanyId = "company2" },
            new Product { Id = 6, Name = "television", CategoryId = "electronic", CompanyId = "company2" },
            new Product { Id = 7, Name = "laptop", CategoryId = "electronic", CompanyId = "company2" },
            new Product { Id = 8, Name = "keyboard", CategoryId = "electronic", CompanyId = "company2" },
            new Product { Id = 9, Name = "monitor", CategoryId = "electronic", CompanyId = "company2" },
            new Product { Id = 10, Name = "knife", CategoryId = "household", CompanyId = "company3" },
            new Product { Id = 11, Name = "spoon", CategoryId = "household", CompanyId = "company3" },
            new Product { Id = 12, Name = "bowl", CategoryId = "household", CompanyId = "company3" },
            new Product { Id = 13, Name = "chopsticks", CategoryId = "household", CompanyId = "company3" },
        };

        public static IEnumerable<ProductCategory> Categories => new[]
        {
            new ProductCategory { Id = "fruit", Name = "fruit", CompanyId = "company1" },
            new ProductCategory { Id = "electronic", Name = "electronic", CompanyId = "company2" },
            new ProductCategory { Id = "household", Name = "household", CompanyId = "company2" },
            new ProductCategory { Id = "misc", Name = "misc", CompanyId = "company3" },
        };
    }

    public class ProductCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }

        public override string ToString() => $"Category: {Name}";
    }

    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
        public virtual IEnumerable<ProductCategory> ProductCategories { get; set; }

        public override string ToString() => $"Company: {Name}";
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ProductCategory Category { get; set; }

        public override string ToString() => $"Product: {Name}";
    }
}
