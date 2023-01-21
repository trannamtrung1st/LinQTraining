namespace LinQTraining.Operators
{
    public static class SortingData
    {
        public static void Run()
        {
            PrimaryAscendingSort();

            PrimaryDescendingSort();

            SecondaryAscendingSort();

            SecondaryDescendingSort();
        }

        static void PrimaryAscendingSort()
        {
            string[] words = { "the", "quick", "brown", "fox", "jumps" };

            IEnumerable<string> query = from word in words
                                        orderby word.Length
                                        select word;

            foreach (string str in query)
                Console.WriteLine(str);
        }

        static void PrimaryDescendingSort()
        {
            string[] words = { "the", "quick", "brown", "fox", "jumps" };

            IEnumerable<string> query = from word in words
                                        orderby word.Substring(0, 1) descending
                                        select word;

            foreach (string str in query)
                Console.WriteLine(str);
        }

        static void SecondaryAscendingSort()
        {
            string[] words = { "the", "quick", "brown", "fox", "jumps" };

            IEnumerable<string> query = from word in words
                                        orderby word.Length, word.Substring(0, 1)
                                        select word;

            foreach (string str in query)
                Console.WriteLine(str);
        }

        static void SecondaryDescendingSort()
        {
            string[] words = { "the", "quick", "brown", "fox", "jumps" };

            IEnumerable<string> query = from word in words
                                        orderby word.Length, word.Substring(0, 1) descending
                                        select word;

            foreach (string str in query)
                Console.WriteLine(str);
        }

    }
}
