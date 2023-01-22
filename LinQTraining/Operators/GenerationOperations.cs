namespace LinQTraining.Operators
{
    public static class GenerationOperations
    {
        public static void Run()
        {
            List<int> numbers = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };

            IEnumerable<int> defaultIfEmpty = numbers.Where(n => n < 0).DefaultIfEmpty();

            Console.WriteLine(defaultIfEmpty.FirstOrDefault());

            IEnumerable<int> empty = Enumerable.Empty<int>();

            Console.WriteLine(empty.Count());

            IEnumerable<int> range = Enumerable.Range(0, 100);

            Console.WriteLine(string.Join(',', range));

            IEnumerable<int> repeat = Enumerable.Repeat(1, 10);

            Console.WriteLine(string.Join(',', repeat));
        }
    }
}
