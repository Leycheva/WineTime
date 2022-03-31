namespace WineTime.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Manufacture> Manufactures { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Degustation> Degustations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            base.OnModelCreating(modelBuilder);
        }
    }
}