using Microsoft.EntityFrameworkCore;

namespace LinQTraining.EFCoreAdvanced
{
    public static class ComplexQueryOperators
    {
        public static void Run()
        {
            Joins();

            SubQueries();

            CrossApplyAndOuterApply();

            SelectMany();

            Unions();

            GroupByHaving();

            SelectLatestItemsGroupedByParent();

            SelectWithRowNumbers();

            SplitQueriesAndInMemoryProcessing();
        }

        public static void GroupByHaving()
        {
            using LinQContext context = new LinQContext();

            var productsCountQuery = from product in context.Product
                                     group product by product.CategoryId into grouping
                                     select new
                                     {
                                         CategoryId = grouping.Key,
                                         ProductCount = grouping.Count()
                                     };
            string queryText = productsCountQuery.ToQueryString();
            Console.WriteLine(queryText);
            var data = productsCountQuery.ToList();

            var productsCountHavingQuery = from product in context.Product
                                           group product by product.CategoryId into grouping
                                           where grouping.Count() > 4
                                           select new
                                           {
                                               CategoryId = grouping.Key,
                                               ProductCount = grouping.Count()
                                           };
            queryText = productsCountHavingQuery.ToQueryString();
            Console.WriteLine(queryText);
            data = productsCountHavingQuery.ToList();
        }

        public static void Unions()
        {
            using LinQContext context = new LinQContext();

            var query1 = from product in context.Product
                         where product.Name.Contains("a")
                         select new
                         {
                             product.Name
                         };
            var query2 = from product in context.Product
                         where product.Name.Contains("b")
                         select new
                         {
                             product.Name
                         };

            var union = query1.Union(query2);
            string queryText = union.ToQueryString();
            Console.WriteLine(queryText);
            var data = union.ToList();

            var unionAll = query1.Concat(query2);
            queryText = unionAll.ToQueryString();
            Console.WriteLine(queryText);
            data = unionAll.ToList();
        }

        public static void SelectMany()
        {
            using LinQContext context = new LinQContext();

            IQueryable<Product> productsOfCategories = (from category in context.Category
                                                        where category.Products.Count() < 5
                                                        select category)
                                       .SelectMany(category => category.Products);
            string queryText = productsOfCategories.ToQueryString();
            Console.WriteLine(queryText);
            List<Product> data = productsOfCategories.ToList();
        }

        public static void CrossApplyAndOuterApply()
        {
            using LinQContext context = new LinQContext();

            // Demo using SQL Server, APPLY can use value from left table's columns in right table
            var query = from company in context.Company
                        from product in context.Product
                            .Where(p => p.Category.Name.Contains("elec"))
                            .Where(p => company.ProductCategories.Any(c => c.Id == p.CategoryId))
                        select new { company, product };
            string queryText = query.ToQueryString();
            Console.WriteLine(queryText);
            var data = query.ToList();

            query = from company in context.Company
                    from product in context.Product
                        .Where(p => p.Category.Name.Contains("elec"))
                        .Where(p => company.ProductCategories.Any(c => c.Id == p.CategoryId))
                        .DefaultIfEmpty()
                    select new { company, product };
            queryText = query.ToQueryString();
            Console.WriteLine(queryText);
            data = query.ToList();
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
