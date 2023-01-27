using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LinQTraining.LinqExtensions
{
    public static class LinQInEFCore
    {
        public static async Task Run()
        {
            await Init();

            using var context = new LinQContext();

            IQueryable<Data> query = from data in context.Data
                                     select data;
            Console.WriteLine(query.ToQueryString());
            Data[] allData = query.ToArray();
            Console.WriteLine(allData.Length);

            query = from data in context.Data
                    where data.Id > 50
                    select data;
            Console.WriteLine(query.ToQueryString());
            Data[] filtered = query.ToArray();
            Console.WriteLine(filtered.Length);
        }

        public static async Task Init()
        {
            using var context = new LinQContext();
            await context.Database.MigrateAsync();
        }
    }

    class Data
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    class LinQContext : DbContext
    {
        public LinQContext(DbContextOptions options) : base(options)
        {
        }

        public LinQContext()
        {
        }

        public virtual DbSet<Data> Data { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=LinQTraining;Trusted_Connection=False;User Id=sa;Password=zaQ@123456!;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Data>(builder =>
            {
                builder.HasData(Enumerable.Range(1, 100).Select(i => new Data { Id = i, Value = $"Data {i}" }));
            });
        }
    }

    class LinQContextFactory : IDesignTimeDbContextFactory<LinQContext>
    {
        public LinQContext CreateDbContext(string[] args)
        {
            return new LinQContext();
        }
    }
}
