namespace LinQTraining.Operators
{
    public static class ElementOperations
    {
        public static void Run()
        {
            List<int> numbers = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };
            IEnumerable<int> empty = Enumerable.Empty<int>();
            IEnumerable<int> singleList = new[] { 1 };

            try
            {
                var elementAt = numbers.ElementAtOrDefault(0);
                elementAt = numbers.ElementAt(0);
                elementAt = empty.ElementAtOrDefault(10);
                elementAt = empty.ElementAt(10);
            }
            catch { Console.WriteLine("Element at error"); }

            try
            {
                var first = numbers.FirstOrDefault();
                first = numbers.First();
                first = empty.FirstOrDefault();
                first = empty.First();
            }
            catch { Console.WriteLine("First error"); }

            try
            {
                var last = numbers.LastOrDefault();
                last = numbers.Last();
                last = empty.LastOrDefault();
                last = empty.Last();
            }
            catch { Console.WriteLine("Last error"); }

            var single = singleList.SingleOrDefault();
            single = singleList.Single();

            try
            {
                single = empty.SingleOrDefault();
                single = empty.Single();
            }
            catch { Console.WriteLine("Single error"); }

            try
            {
                single = numbers.SingleOrDefault();
                single = numbers.Single();
            }
            catch { Console.WriteLine("Single error"); }
        }

    }
}
