namespace LinQTraining.Operators
{
    public static class ProjectionOperations
    {
        public static void Run()
        {
            Select();

            SelectMany();

            Zip();
        }

        static void Select()
        {
            List<string> words = new() { "an", "apple", "a", "day" };

            var query = from word in words
                        select word[..1];

            foreach (string s in query)
                Console.WriteLine(s);
        }

        static void SelectMany()
        {
            List<string> phrases = new() { "an apple a day", "the quick brown fox" };

            var query = from phrase in phrases
                        from word in phrase.Split(' ')
                        select word;

            foreach (string s in query)
                Console.WriteLine(s);

            query = phrases.SelectMany(phrase => phrase.Split(' '));

            foreach (string s in query)
                Console.WriteLine(s);
        }

        static void Zip()
        {
            // An int array with 7 elements.
            IEnumerable<int> numbers = new[]
            {
                1, 2, 3, 4, 5, 6, 7
            };
            // A char array with 6 elements.
            IEnumerable<char> letters = new[]
            {
                'A', 'B', 'C', 'D', 'E', 'F'
            };

            foreach ((int number, char letter) in numbers.Zip(letters))
            {
                Console.WriteLine($"Number: {number} zipped with letter: '{letter}'");
            }

            foreach (string zipResult in numbers.Zip(letters, (number, letter) => $"{number} - {letter}"))
            {
                Console.WriteLine(zipResult);
            }
        }
    }
}
