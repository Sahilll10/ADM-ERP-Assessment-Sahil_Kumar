using Microsoft.EntityFrameworkCore;

namespace ERPAssessmentApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(i => i.ParentItem)
                .WithMany(i => i.ChildItems)
                .HasForeignKey(i => i.ParentItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}