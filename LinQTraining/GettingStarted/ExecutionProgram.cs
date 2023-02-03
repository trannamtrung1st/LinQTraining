namespace LinQTraining.GettingStarted
{
    public static class ExecutionProgram
    {
        public static void Run()
        {
            IEnumerable<int> data = new List<int> { -1, 0, 1, 2, 3, 4, 5 };

            IEnumerable<int> filteredData = data.Where(x =>
            {
                Console.WriteLine($"Filtering: {x}");
                return x > 0;
            });

            // Deferred execution
            foreach (var num in filteredData) Console.WriteLine(num + " ");
            Console.WriteLine();
            foreach (var num in filteredData) Console.WriteLine(num + " ");
            Console.WriteLine();

            // Immediate execution
            IEnumerable<int> cachedData = filteredData.ToList();
            foreach (var num in cachedData) Console.WriteLine(num + " ");
            Console.WriteLine();
            foreach (var num in cachedData) Console.WriteLine(num + " ");
            Console.WriteLine();
        }
    }
}
