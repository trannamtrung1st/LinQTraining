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

        public static void BasicOperations()
        {
            var names = new[] { "Kid", "David", "Diana", "Anna", "Chris", "Wayne", "John" };

            // IEnumerable<string>
            var allNames = from name in names
                           select name;

            allNames = names;

            var filteredNames = from name in names
                                where name.Contains("D") || name.Contains("C")
                                where name.Length > 2
                                select name;

            filteredNames = names
                .Where(name => name.Contains("D") || name.Contains("C"))
                .Where(name => name.Length > 2);

            var orderedNames = from name in names
                               orderby name descending
                               select name;

            orderedNames = from name in names
                           orderby name.Length, name descending
                           select name;

            orderedNames = names
                .OrderBy(name => name.Length)
                .ThenByDescending(name => name);

            IEnumerable<IGrouping<char, string>> groupedNames = from name in names
                                                                group name by name[0];

            groupedNames = names.GroupBy(name => name[0]);

            foreach (var nameGroup in groupedNames)
            {
                Console.WriteLine(nameGroup.Key); // Key: A
                Console.WriteLine(string.Join(',', nameGroup)); // Values: An, Anh, Anna, etc
            }

            IEnumerable<char> groupNamesOnly = from name in names
                                               group name by name[0] into nameGroups
                                               select nameGroups.Key;


            var validNameLengths = new int[] { 3, 5 };

            var joinedNames = from name in names
                              join validLength in validNameLengths on name.Length equals validLength
                              select new
                              {
                                  Name = name,
                                  Length = validLength
                              } into nameAndLength
                              group nameAndLength by nameAndLength.Length;

            joinedNames = names
                .Join(validNameLengths,
                    name => name.Length,
                    length => length,
                    (name, length) => new
                    {
                        Name = name,
                        Length = length,
                    })
                .GroupBy(nameAndLength => nameAndLength.Length);

            var complexNames = from name in names
                               select new
                               {
                                   Name = name,
                                   FirstLetter = name[0],
                                   LastLetter = name[name.Length - 1],
                                   NameLength = name.Length,
                                   IsValidLength = (from nameLength in validNameLengths select nameLength)
                                                   .Contains(name.Length)
                               };

            string[] allNamesQuery = names;

            IEnumerable<string> containsAQuery = names.Where(n => n.Contains("A"));

            IOrderedEnumerable<string> containsAOrderByLengthAsc = containsAQuery.OrderBy(n => n.Length);

            IEnumerable<IGrouping<char, string>> groupedByFirstLetter =
                containsAOrderByLengthAsc.GroupBy(n => n[0]);
        }

        public static void RunQueryableConvertedToEnumerable()
        {
            IQueryable<string> query = new MyQueryable();

            // Query processing is handled by IQueryable object (provided by specific LinQ providers)
            query = query.Where(s => s.Contains("A"));
            query = query.Where(s => s != "ABC");

            IEnumerable<string> inMemoryQuery1 = query;
            IEnumerable<string> inMemoryQuery2 = query.AsEnumerable();

            // From this point, query logic is handled by LinQ in-memory operators
            inMemoryQuery1 = inMemoryQuery1.Skip(2).Take(100);
            inMemoryQuery2 = inMemoryQuery2.Skip(2).Take(100);

            /**
             * When inMemoryQuery1/inMemoryQuery2 object is enumerated:
             * 1. Handled by IQueryable: fetch from data source, .Where(...)
             * 2. Converted to enumerable object
             * 3. Handled by LinQ in-memory: .Skip(), .Take() 
             */
            string[] result = inMemoryQuery1.ToArray();
            result = inMemoryQuery2.ToArray();
            Console.WriteLine(string.Join(',', result));
        }
    }
}
