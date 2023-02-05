using Microsoft.EntityFrameworkCore;

namespace LinQTraining.EFCoreAdvanced
{
    public static class ComplexQueryOperators
    {
        public static void Run()
        {
            Joins();

            SubQueries();

            SelectLatestItemsGroupedByParent();

            SelectWithRowNumbers();

            SplitQueriesAndInMemoryProcessing();
        }

        public static void Joins()
        {
            using LinQContext context = new LinQContext();

            var inner = from product in context.Product
                        join category in context.Category on product.CategoryId equals category.Id
                        select new
                        {
                            Product = product,
                            Category = category
                        };
            string queryText = inner.ToQueryString();
            Console.WriteLine(queryText);
            inner.ToList();

            var left = from product in context.Product
                       join category in context.Category on product.CategoryId equals category.Id into grouping
                       from category in grouping.DefaultIfEmpty()
                       select new
                       {
                           Product = product,
                           Category = category
                       };
            queryText = left.ToQueryString();
            Console.WriteLine(queryText);
            left.ToList();

            left = from product in context.Product
                   from category in context.Category.Where(c => c.Id == product.CategoryId).DefaultIfEmpty()
                   select new
                   {
                       Product = product,
                       Category = category
                   };
            queryText = left.ToQueryString();
            Console.WriteLine(queryText);
            left.ToList();

            var cross = from product in context.Product
                        from category in context.Category
                        select new
                        {
                            Product = product,
                            Category = category
                        };
            queryText = cross.ToQueryString();
            Console.WriteLine(queryText);
            cross.ToList();
        }

        public static void SubQueries()
        {
            using LinQContext context = new LinQContext();

            IQueryable<string> filteredCategoryIds = from category in context.Category
                                                     where category.Name.Contains("a")
                                                     select category.Id;
            IQueryable<Product> subQuery = from product in context.Product
                                           where filteredCategoryIds.Contains(product.CategoryId)
                                           select product;
            string queryText = subQuery.ToQueryString();
            Console.WriteLine(queryText);
            subQuery.ToList();

            IQueryable<ProductCategory> filteredCategories = from category in context.Category
                                                             where category.Name.Contains("a")
                                                             select category;
            var joinAndSubQuery = from product in context.Product
                                  join category in filteredCategories on product.CategoryId equals category.Id
                                  select new
                                  {
                                      Product = product,
                                      Category = category
                                  };
            queryText = joinAndSubQuery.ToQueryString();
            Console.WriteLine(queryText);
            joinAndSubQuery.ToList();
        }

        public static void SelectLatestItemsGroupedByParent()
        {
            using LinQContext context = new LinQContext();

            var subQuery = from product in context.Product
                           group product by product.CategoryId into grouping
                           select grouping.OrderBy(p => p.Name).ThenByDescending(p => p.Id).Skip(0).Take(1).Select(p => p.Id).FirstOrDefault();
            Console.WriteLine(subQuery.ToQueryString());

            IQueryable<Product> query = from product in context.Product
                                        join latestProductId in subQuery on product.Id equals latestProductId
                                        where product.Name != null
                                        select product;
            string queryText = query.ToQueryString();
            Console.WriteLine(queryText);
            var data = query.ToList();
        }

        public static void SelectWithRowNumbers()
        {
            using LinQContext context = new LinQContext();

            IQueryable<Product> query = context.Product.FromSqlInterpolated(
$@"SELECT * FROM (
    SELECT *, ROW_NUMBER() OVER(PARTITION BY CategoryId ORDER BY Name ASC, Id DESC) AS RowNumber
    FROM Product
) Product WHERE RowNumber = 1");
            query = query.Where(p => p.Name != null);
            string queryText = query.ToQueryString();
            Console.WriteLine(queryText);
            var data = query.ToList();
        }

        public static void SplitQueriesAndInMemoryProcessing()
        {
            using LinQContext context = new LinQContext();

            // Get all companies which have categories with more than 4 products
            var validCategoryQuery = from product in context.Product
                                     group product by product.CategoryId into grouping
                                     where grouping.Count() > 4
                                     select grouping.Key;

            var categoryQuery = from category in context.Category
                                join validCategory in validCategoryQuery on category.Id equals validCategory
                                select category;

            var query = from company in context.Company
                        join category in categoryQuery on company.Id equals category.CompanyId
                        select company;

            var queryText = query.ToQueryString();
            Console.WriteLine(queryText);
            var data = query.ToList();

            // split queries and in-memory processing
            validCategoryQuery = from product in context.Product
                                 group product by product.CategoryId into grouping
                                 where grouping.Count() > 4
                                 select grouping.Key;

            List<string> validCategoryIds = validCategoryQuery.ToList();

            query = from company in context.Company
                    where company.ProductCategories.Any(c => validCategoryIds.Contains(c.Id))
                    select company;
            queryText = query.ToQueryString();
            Console.WriteLine(queryText);
            data = query.ToList();
        }
    }
}
