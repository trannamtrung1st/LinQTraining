namespace LinQTraining.Operators
{
    public static class PartitioningData
    {
        public static void Run()
        {
            Chunk();

            SkipTake();
        }

        static void Chunk()
        {
            int chunkNumber = 1;
            foreach (int[] chunk in Enumerable.Range(0, 8).Chunk(3))
            {
                Console.WriteLine($"Chunk {chunkNumber++}:");
                foreach (int item in chunk)
                {
                    Console.WriteLine($"    {item}");
                }

                Console.WriteLine();
            }
        }

        static void SkipTake()
        {
            var range = Enumerable.Range(1, 100);

            var query = range.Skip(10).Take(10);

            Console.WriteLine(string.Join(',', query));

            query = range.SkipWhile(num => num < 10).TakeWhile(num => num < 50);

            Console.WriteLine(string.Join(',', query));
        }

    }
}
