using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LinQTraining
{
    public class LinQContext : DbContext
    {
        public const string DefaultConnectionString = "Server=.;Database=LinQTraining;Trusted_Connection=False;User Id=sa;Password=zaQ@123456!;MultipleActiveResultSets=true";

        public LinQContext(DbContextOptions<LinQContext> options) : base(options)
        {
        }

        public LinQContext()
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> Category { get; set; }
        public virtual DbSet<Company> Company { get; set; }

        #region Views
        public virtual DbSet<GetProductCountByCategoryView> GetProductCountByCategoryView { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DefaultConnectionString)
                    //.LogTo(Console.WriteLine);
                    .LogTo(Console.WriteLine, minimumLevel: Microsoft.Extensions.Logging.LogLevel.Information);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GetProductCountByCategoryView>().HasNoKey().ToView(null);

            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasOne(e => e.Category)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.CategoryId)
                    .IsRequired(false);

                builder.HasOne(e => e.Company)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.CompanyId)
                    .IsRequired(false);

                builder.HasData(Data.Products);
            });

            modelBuilder.Entity<ProductCategory>(builder =>
            {
                builder.HasOne(e => e.Company)
                    .WithMany(e => e.ProductCategories)
                    .HasForeignKey(e => e.CompanyId)
                    .IsRequired(false);

                builder.HasData(Data.Categories);
            });

            modelBuilder.Entity<Company>(builder =>
            {
                builder.HasData(Data.Companies);
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
