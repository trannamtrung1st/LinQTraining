namespace LinQTraining.Operators
{
    public static class GroupingData
    {
        public static void Run()
        {
            GroupBy();

            ToLookup();
        }

        static void GroupBy()
        {
            List<int> numbers = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };

            IEnumerable<IGrouping<int, int>> query = from number in numbers
                                                     group number by number % 2;

            foreach (var group in query)
            {
                Console.WriteLine(group.Key == 0 ? "\nEven numbers:" : "\nOdd numbers:");

                foreach (int i in group)
                    Console.WriteLine(i);
            }
        }

        static void ToLookup()
        {
            List<int> numbers = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };

            ILookup<string, int> query = numbers.ToLookup(n => n % 2 == 0 ? "even" : "odd");

            Console.WriteLine("Even numbers:");

            var even = query["even"];

            foreach (int i in even)
                Console.WriteLine(i);
        }
    }
}
