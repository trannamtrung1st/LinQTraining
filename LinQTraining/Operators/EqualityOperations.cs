namespace LinQTraining.Operators
{
    public static class EqualityOperations
    {
        public static void Run()
        {
            List<int> numbers = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };
            List<int> numbers1 = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };
            List<int> numbers2 = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 328, 446, 208 };

            Console.WriteLine(numbers.SequenceEqual(numbers1));
            Console.WriteLine(numbers.SequenceEqual(numbers2));
        }
    }
}
