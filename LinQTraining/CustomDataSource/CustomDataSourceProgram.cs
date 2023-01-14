namespace LinQTraining.CustomDataSource
{
    public class CustomDataSourceProgram
    {
        public static void RunEnumerable()
        {
            var family = new MyFamily();

            Console.WriteLine("All members:");
            var allMembers = family;
            foreach (var member in allMembers) Console.WriteLine(member);
            Console.WriteLine();

            Console.WriteLine("Dad:");
            var dad = (from members in family
                       where members.Name == "Dad"
                       select members).FirstOrDefault();
            Console.WriteLine(dad);
            Console.WriteLine();

            Console.WriteLine("Members with age > 20:");
            var ageGreaterThan20 = family.Where(m => m.Age > 20);
            foreach (var member in ageGreaterThan20) Console.WriteLine(member);
            Console.WriteLine();
        }

        public static void RunQueryable()
        {
            var query = new MyQueryable();

            var letterAQuery = query.Where(s => s.Contains("A"));
            letterAQuery = letterAQuery.Where(s => s.Contains("B"));
            letterAQuery = letterAQuery.Where(s => s.Contains("C"));
            letterAQuery = letterAQuery.Where(s => s != "B");

            Console.WriteLine((letterAQuery as MyQueryable).QueryText);

            Console.WriteLine("Filtered:");
            var filteredList = letterAQuery.ToList();
            Console.WriteLine(string.Join(',', filteredList));
            Console.WriteLine();

            Console.WriteLine("ABC only:");
            var letterA = letterAQuery.FirstOrDefault(e => e == "ABC");
            Console.WriteLine(letterA);
            Console.WriteLine();

            Console.WriteLine("All:");
            var allData = query.ToList();
            Console.WriteLine(string.Join(',', allData));
            Console.WriteLine();
        }
    }
}
