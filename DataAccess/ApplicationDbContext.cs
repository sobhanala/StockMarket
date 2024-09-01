using api.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace api.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options){}
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Portfolio>(x => x.HasKey(p => new {p.UserId, p.StockId}));
            modelBuilder.Entity<Portfolio>()
            .HasOne(u => u.User)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Portfolio>()
            .HasOne(u => u.Stock)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.StockId);

        }
    }
}
