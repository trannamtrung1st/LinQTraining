using LinQTraining.LinqExtensions;
using Microsoft.EntityFrameworkCore;

namespace LinQTraining.EFCoreAdvanced
{
    public static class ClientServerEvaluation
    {
        public static void Run()
        {
            ClientEvalTopLevelProjection();

            UnsupportedClientEval();

            ExplicitClientEval();

            PotentialMemoryLeak();

            GC.Collect();

            Thread.Sleep(10000);
        }

        public static void ClientEvalTopLevelProjection()
        {
            using LinQContext context = new LinQContext();

            var query = from product in context.Product
                        where product.Name.Contains("a")
                        select new
                        {
                            ProductName = product.Name,
                            ProductId = product.Id,
                            FirstChar = product.Name[0]
                        };

            Console.WriteLine(query.ToQueryString());
            var result = query.ToList();

            foreach (var product in result) Console.WriteLine(product);
        }

        public static void UnsupportedClientEval()
        {
            try
            {
                using LinQContext context = new LinQContext();

                var query = context.Product
                    .Where(product => product.Name.Contains("a") || product.Name[0] == 'a')
                    .Select(product => new
                    {
                        ProductName = product.Name,
                        ProductId = product.Id,
                        FirstChar = product.Name[0]
                    });

                Console.WriteLine(query.ToQueryString());
                var result = query.ToList();

                foreach (var product in result) Console.WriteLine(product);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        public static void ExplicitClientEval()
        {
            try
            {
                using LinQContext context = new LinQContext();

                var query = context.Product
                    .Where(product => product.Name.Contains("a"))
                    .AsEnumerable()
                    .Where(product => product.Name[0] == 'a')
                    .Select(product => new
                    {
                        ProductName = product.Name,
                        ProductId = product.Id,
                        FirstChar = product.Name[0]
                    });

                var result = query.ToList();

                foreach (var product in result) Console.WriteLine(product);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        public static void PotentialMemoryLeak()
        {
            var myClass = new MyClass();

            try
            {
                myClass.LeakQuery();
                myClass.LeakQuery();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }

            myClass.SafeQuery();
            myClass.SafeQuery();
        }

        class MyClass
        {
            public string GetName(string productName) => productName;

            ~MyClass()
            {
                Console.WriteLine("MyClass destructor");
            }

            public void LeakQuery()
            {
                Console.WriteLine("START MEMORY LEAK QUERY");
                using LinQContext context = new LinQContext();

                var query = from product in context.Product
                            where product.Name.Contains("a")
                            select new
                            {
                                ProductName = product.Name,
                                ProductId = product.Id,
                                Value = this.GetName(product.Name)
                            };

                Console.WriteLine(query.ToQueryString());
                var result = query.ToList();

                foreach (var product in result) Console.WriteLine(product);
            }

            public void SafeQuery()
            {
                Console.WriteLine("START SAFE QUERY");
                using LinQContext context = new LinQContext();

                Func<string, string> GetProductName = (name) => this.GetName(name);

                var query = from product in context.Product
                            where product.Name.Contains("a")
                            select new
                            {
                                ProductName = product.Name,
                                ProductId = product.Id,
                                Value = GetProductName(product.Name)
                            };

                Console.WriteLine(query.ToQueryString());
                var result = query.ToList();

                foreach (var product in result) Console.WriteLine(product);
            }
        }
    }
}
