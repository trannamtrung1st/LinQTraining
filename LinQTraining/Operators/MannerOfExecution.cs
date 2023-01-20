namespace LinQTraining.Operators
{
    public static class MannerOfExecution
    {
        public static void Run()
        {
            IEnumerable<string> dataStream = DataStream();

            Console.WriteLine("Immediate execution");
            var count = dataStream.Count();
            Console.WriteLine($"Count: {count}");
            Console.WriteLine();

            Console.WriteLine("Deferred: Streaming");
            var firstLetterOnly = dataStream.Select(s => s[0]);
            foreach (var data in firstLetterOnly) Console.WriteLine(data);
            Console.WriteLine();

            Console.WriteLine("Deferred: Non-Streaming");
            var orderByDesc = dataStream.OrderByDescending(s => s);
            foreach (var data in orderByDesc) Console.WriteLine(data);
            Console.WriteLine();
        }

        static IEnumerable<string> DataStream()
        {
            yield return "A123";

            Console.WriteLine("Processing ..."); Thread.Sleep(1000);

            yield return "B123";

            Console.WriteLine("Processing ..."); Thread.Sleep(1000);

            yield return "C123";
        }
    }
}
