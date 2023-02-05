namespace LinQTraining
{
    public static class Data
    {
        public static IEnumerable<Company> Companies => Enumerable.Range(1, 200)
            .Select(i => new Company { Id = $"company{i}", Name = $"company{i}" });

        public static IEnumerable<Product> Products => new[]
        {
            new Product { Id = "apple", Name = "apple", CategoryId = "fruit", CompanyId = "company1" },
            new Product { Id = "banana", Name = "banana", CategoryId = "fruit", CompanyId = "company1" },
            new Product { Id = "orange", Name = "orange", CategoryId = "fruit", CompanyId = "company1" },
            new Product { Id = "grape", Name = "grape", CategoryId = "fruit", CompanyId = "company1" },
            new Product { Id = "mango", Name = "mango", CategoryId = "fruit", CompanyId = "company2" },
            new Product { Id = "television", Name = "television", CategoryId = "electronic", CompanyId = "company2" },
            new Product { Id = "laptop", Name = "laptop", CategoryId = "electronic", CompanyId = "company2" },
            new Product { Id = "keyboard", Name = "keyboard", CategoryId = "electronic", CompanyId = "company2" },
            new Product { Id = "monitor", Name = "monitor", CategoryId = "electronic", CompanyId = "company2" },
            new Product { Id = "knife", Name = "knife", CategoryId = "household", CompanyId = "company3" },
            new Product { Id = "spoon", Name = "spoon", CategoryId = "household", CompanyId = "company3" },
            new Product { Id = "bowl", Name = "bowl", CategoryId = "household", CompanyId = "company3" },
            new Product { Id = "chopsticks", Name = "chopsticks", CategoryId = "household", CompanyId = "company3" },
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
        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ProductCategory Category { get; set; }

        public override string ToString() => $"Product: {Name}";
    }
}
