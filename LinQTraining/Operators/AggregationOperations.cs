namespace LinQTraining.Operators
{
    public static class AggregationOperations
    {
        public static void Run()
        {
            int[] numbers = new[] { 1, 2, 3, 4 };

            string aggregate = numbers.Aggregate(string.Empty, (result, current) => result + current);

            double avg = numbers.Average();
            avg = numbers.Average(e => e * 2);

            int count = numbers.Count();

            int max = numbers.Max();
            int maxBy = numbers.MaxBy(e => e % 2);
            int min = numbers.Min();
            int minBy = numbers.MinBy(e => e % 2);

            int sum = numbers.Sum();
            sum = numbers.Sum(e => e * 2);
        }
    }
}
