using Microsoft.EntityFrameworkCore;
using MyPasswords.Models.Entities;

namespace MyPasswords.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Credential> Credentials { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasColumnType("varchar(50)");
                entity.Property(u => u.Email).IsRequired().HasColumnType("varchar(100)");
                entity.Property(u => u.Password).IsRequired().HasColumnType("varchar(255)");

            });

            modelBuilder.Entity<Credential>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.ServiceName).IsRequired().HasColumnType("varchar(50)");
                entity.Property(c => c.UserName).IsRequired().HasColumnType("varchar(100)");
                entity.Property(c => c.Password).IsRequired().HasColumnType("varchar(255)");
                entity.Property(c => c.CreatedAt);

                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
