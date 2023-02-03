using Microsoft.EntityFrameworkCore;

namespace LinQTraining.LinqExtensions
{
    public static class LinQInEFCore
    {
        public static void Run()
        {
            using var context = new LinQContext();

            IQueryable<Product> query = from product in context.Product
                                        where product.Category.Name == "fruit"
                                        select product;
            Console.WriteLine(query.ToQueryString());
            Product[] products = query.ToArray();
            Console.WriteLine(products.Length);
        }
    }
}
