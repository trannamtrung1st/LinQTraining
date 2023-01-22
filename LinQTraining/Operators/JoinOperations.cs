namespace LinQTraining.Operators
{
    public static class JoinOperations
    {
        public static void Run()
        {
            Join();

            GroupJoin();

            LeftRightOuterJoin();
        }

        static void Join()
        {
            var productsWithCategory = from product in Data.Products
                                       join category in Data.Categories on product.CategoryId equals category.Id
                                       select new Product
                                       {
                                           Id = product.Id,
                                           Name = product.Name,
                                           CategoryId = product.CategoryId,
                                           Category = category
                                       };

            foreach (var product in productsWithCategory)
            {
                Console.WriteLine($"{product} - {product.Category}");
            }
        }

        static void GroupJoin()
        {
            // Join categories and product based on CategoryId and grouping result
            var productGroups = from category in Data.Categories
                                join product in Data.Products on category.Id equals product.CategoryId
                                into productGroup
                                select productGroup;

        }

        static void LeftRightOuterJoin()
        {
            var leftTableProduct = Data.Products;
            var rightTableCategory = Data.Categories;

            var leftJoin = from product in leftTableProduct
                           join category in rightTableCategory on product.CategoryId equals category.Id into categoryGroup
                           from category in categoryGroup.DefaultIfEmpty()
                           select new
                           {
                               Category = category,
                               Product = product
                           };

            foreach (var record in leftJoin)
            {
                Console.WriteLine($"{record.Product} - {record.Category}");
            }

            var rightJoin = from category in rightTableCategory
                            join product in leftTableProduct on category.Id equals product.CategoryId into productGroup
                            from product in productGroup.DefaultIfEmpty()
                            select new
                            {
                                Category = category,
                                Product = product
                            };

            foreach (var record in rightJoin)
            {
                Console.WriteLine($"{record.Category} - {record.Product}");
            }
        }
    }
}
