namespace LinQTraining.Operators
{
    public static class FilteringData
    {
        public static void Run()
        {
            string[] words = { "the", "quick", "brown", "fox", "jumps" };

            IEnumerable<string> query = from word in words
                                        where word.Length == 3
                                        select word;

            foreach (string str in query)
                Console.WriteLine(str);

            object[] objects = { "string", 1, 1.0, new Exception(), new List<string>(), "another string" };

            IEnumerable<string> stringOnly = objects.OfType<string>();

            foreach (string str in stringOnly)
                Console.WriteLine(str);
        }
    }
}
