namespace LinQTraining.CustomDataSource
{
    public class FamilyMember
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString() => $"{Name} | {Age}";
    }
}
