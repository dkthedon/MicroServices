using Microsoft.EntityFrameworkCore;
using UsersAPI.Entities;

namespace UsersAPI
{
    public class UsersDbContext: DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options): base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(e => e.UserId);

            modelBuilder.Entity<User>()
                .Property(e => e.Name).HasMaxLength(32);
            modelBuilder.Entity<User>()
                .Property(e => e.Email).HasMaxLength(200);
        }

        public DbSet<User> Users { get; set; }
    }
}
