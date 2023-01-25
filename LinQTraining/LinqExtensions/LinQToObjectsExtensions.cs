namespace LinQTraining.LinqExtensions
{
    public static class LinQToObjectsExtensions
    {
        public static IEnumerable<T> NotDefault<T>(this IEnumerable<T> source)
            => source.Where(x => x != null && !x.Equals(default(T)));

        public static void Run()
        {
            var list = new string[] { "1", null, "2", "3", null };

            var notDefault = list.NotDefault();

            var originalResult = string.Join(',', list);
            var notDefaultResult = string.Join(',', notDefault);
            Console.WriteLine(originalResult);
            Console.WriteLine(notDefaultResult);
        }
    }
}
