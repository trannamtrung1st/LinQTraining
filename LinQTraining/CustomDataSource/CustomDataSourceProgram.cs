namespace LinQTraining.CustomDataSource
{
    public class CustomDataSourceProgram
    {
        public static void Run()
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
    }
}
