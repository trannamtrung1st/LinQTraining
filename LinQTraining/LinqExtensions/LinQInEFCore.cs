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

            IQueryable<Product> query = from product in context.Product
                                        where product.Category.Name == "fruit"
                                        select product;
            Console.WriteLine(query.ToQueryString());
            Product[] products = query.ToArray();
            Console.WriteLine(products.Length);
        }

        public static async Task Init()
        {
            using var context = new LinQContext();
            await context.Database.MigrateAsync();
        }
    }

    public class LinQContext : DbContext
    {
        public LinQContext(DbContextOptions options) : base(options)
        {
        }

        public LinQContext()
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> Category { get; set; }

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

            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasOne(e => e.Category)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.CategoryId)
                    .IsRequired(false);

                builder.HasData(Data.Products);
            });

            modelBuilder.Entity<ProductCategory>(builder =>
            {
                builder.HasData(Data.Categories);
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
