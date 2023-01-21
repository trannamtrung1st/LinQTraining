namespace LinQTraining.Operators
{
    public static class SetOperations
    {
        public static void Run()
        {
            DistinctAndDistinctBy();

            ExceptAndExceptBy();

            IntersectAndIntersectBy();

            UnionAndUnionBy();
        }

        static void DistinctAndDistinctBy()
        {
            string[] planets = { "Mercury", "Venus", "Venus", "Earth", "Mars", "Earth" };

            IEnumerable<string> query = from planet in planets.Distinct()
                                        select planet;

            foreach (var str in query)
            {
                Console.WriteLine(str);
            }

            query = from planet in planets.DistinctBy(o => o[0])
                    select planet;

            foreach (var str in query)
            {
                Console.WriteLine(str);
            }
        }

        static void ExceptAndExceptBy()
        {
            var planetsObj = new[] { new { Name = "Mercury" }, new { Name = "Jupiter" }, new { Name = "Moon" } };
            string[] planets1 = { "Mercury", "Venus", "Earth", "Jupiter" };
            string[] planets2 = { "Mercury", "Earth", "Mars", "Jupiter" };

            IEnumerable<string> query = from planet in planets1.Except(planets2)
                                        select planet;

            foreach (var str in query)
            {
                Console.WriteLine(str);
            }

            var objQuery = from planet in planetsObj.ExceptBy(planets2, e => e.Name)
                           select planet;

            foreach (var obj in objQuery)
            {
                Console.WriteLine(obj.Name);
            }
        }

        static void IntersectAndIntersectBy()
        {
            var planetsObj = new[] { new { Name = "Mercury" }, new { Name = "Jupiter" }, new { Name = "Moon" } };
            string[] planets1 = { "Mercury", "Venus", "Earth", "Jupiter" };
            string[] planets2 = { "Mercury", "Earth", "Mars", "Jupiter" };

            IEnumerable<string> query = from planet in planets1.Intersect(planets2)
                                        select planet;

            foreach (var str in query)
            {
                Console.WriteLine(str);
            }

            var objQuery = from planet in planetsObj.IntersectBy(planets2, e => e.Name)
                           select planet;

            foreach (var obj in objQuery)
            {
                Console.WriteLine(obj.Name);
            }
        }

        static void UnionAndUnionBy()
        {
            var planetsObj = new[] { new { Name = "Mercury" }, new { Name = "Jupiter" }, new { Name = "Moon" } };
            var planetsObj2 = new[] { new { Name = "Mycure" }, new { Name = "Jupiter" }, new { Name = "Moon" } };
            string[] planets1 = { "Mercury", "Venus", "Earth", "Jupiter" };
            string[] planets2 = { "Mercury", "Earth", "Mars", "Jupiter" };

            IEnumerable<string> query = from planet in planets1.Union(planets2)
                                        select planet;

            foreach (var str in query)
            {
                Console.WriteLine(str);
            }

            var objQuery = from planet in planetsObj.UnionBy(planetsObj2, e => e.Name)
                           select planet;

            foreach (var obj in objQuery)
            {
                Console.WriteLine(obj.Name);
            }
        }
    }
}
