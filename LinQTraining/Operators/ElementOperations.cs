namespace LinQTraining.Operators
{
    public static class ConvertingDataTypes
    {
        public static void Run()
        {
            try
            {
                IEnumerable<object> objects = new object[] { 1, 2, "Hello", new object() };

                IQueryable<object> iqueryable = objects.AsQueryable();

                IEnumerable<object> enumerable = iqueryable.AsEnumerable();

                IEnumerable<string> cast = from string s in objects
                                           select s;

                cast = objects.Cast<string>();

                IEnumerable<string> ofType = from s in objects.OfType<string>()
                                             select s;

                object[] array = objects.ToArray();
                List<object> list = objects.ToList();
                Dictionary<Type, object> dictionary = objects.ToDictionary(e => e.GetType());
                ILookup<Type, object> lookup = objects.ToLookup(e => e.GetType());
            }
            catch { }
        }
    }
}
