using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LinQTraining.EFCoreAdvanced
{
    public static class Pagination
    {
        public static void Run()
        {
            OffsetPagination();

            OffsetPaginationProblem();

            KeysetPagination();

            NonUniqueOrderProblem();
        }

        public static void OffsetPagination()
        {
            using LinQContext context = new LinQContext();

            bool hasMore = true;
            int offset = 0; int take = 5;

            while (hasMore)
            {
                IQueryable<Product> productsQuery = (from product in context.Product
                                                     orderby product.Id descending
                                                     select product).Skip(offset).Take(take);
                string query = productsQuery.ToQueryString();
                Console.WriteLine(query);
                List<Product> products = productsQuery.ToList();
                foreach (var product in products) Console.WriteLine(product);
                if (products.Count < take) hasMore = false;

                offset += take;
            }
        }

        public static void OffsetPaginationProblem()
        {
            using LinQContext context = new LinQContext();
            using IDbContextTransaction trans = context.Database.BeginTransaction();

            bool hasMore = true;
            int offset = 0; int take = 5;

            while (hasMore)
            {
                IQueryable<Product> productsQuery = (from product in context.Product
                                                     orderby product.Id descending
                                                     select product).Skip(offset).Take(take);
                string query = productsQuery.ToQueryString();
                Console.WriteLine(query);
                List<Product> products = productsQuery.ToList();
                foreach (var product in products) Console.WriteLine(product);
                if (products.Count < take) hasMore = false;

                offset += take;

                // Demo concurrent changes
                if (offset == take)
                {
                    context.Product.Add(new Product
                    {
                        CategoryId = "fruit",
                        Name = "a-new-fruit",
                        CompanyId = "company1"
                    });

                    context.SaveChanges();
                }
            }

            trans.Rollback();
        }

        public static void KeysetPagination()
        {
            using LinQContext context = new LinQContext();
            using IDbContextTransaction trans = context.Database.BeginTransaction();

            bool hasMore = true;
            int lastId = 0; int take = 5;

            while (hasMore)
            {
                IQueryable<Product> productsQuery = (from product in context.Product
                                                     orderby product.Id descending
                                                     where lastId == 0 || product.Id < lastId
                                                     select product).Take(take);
                string query = productsQuery.ToQueryString();
                Console.WriteLine(query);
                List<Product> products = productsQuery.ToList();
                foreach (var product in products) Console.WriteLine(product);
                if (products.Count < take) hasMore = false;

                // Demo concurrent changes
                if (lastId == 0)
                {
                    context.Product.Add(new Product
                    {
                        CategoryId = "fruit",
                        Name = "a-new-fruit",
                        CompanyId = "company1"
                    });

                    context.SaveChanges();
                }

                lastId = products.Last().Id;
            }

            trans.Rollback();
        }

        public static void NonUniqueOrderProblem()
        {
            using LinQContext context = new LinQContext();
            var productsQuery = (from product in context.Product
                                 join company in context.Company on product.CompanyId equals company.Id
                                 orderby product.CategoryId descending
                                 //orderby product.CategoryId descending, product.Id
                                 select new
                                 {
                                     product.Name,
                                     Company = company.Name
                                 }).Take(5);
            string query = productsQuery.ToQueryString();
            Console.WriteLine(query);
            var records = productsQuery.ToList();
            foreach (var record in records) Console.WriteLine(record);

            var newProductsQuery = (from product in context.Product
                                    join company in context.Company on product.CompanyId equals company.Id
                                    join category in context.Category on product.CategoryId equals category.Id
                                    orderby product.CategoryId descending
                                    //orderby product.CategoryId descending, product.Id
                                    select new
                                    {
                                        product.Name,
                                        Company = company.Name,
                                        Category = category.Name,
                                    }).Take(5);
            query = newProductsQuery.ToQueryString();
            Console.WriteLine(query);
            var newRecords = newProductsQuery.ToList();
            foreach (var record in newRecords) Console.WriteLine(record);
        }
    }
}
