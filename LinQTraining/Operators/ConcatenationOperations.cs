namespace LinQTraining.Operators
{
    public static class ConcatenationOperations
    {
        public static void Run()
        {
            int[] a = new[] { 1, 2, 3, 4 };
            int[] b = new[] { 4, 3, 2, 1 };

            var join = string.Join(',', a.Concat(b));

            Console.WriteLine(join);
        }
    }
}
