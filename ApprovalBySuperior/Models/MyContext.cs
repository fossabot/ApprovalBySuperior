using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;


namespace ApprovalBySuperior.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Departments> Departments { get; set; }

        // FluentAPI Configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            //リレーションは不要になったためコメントアウト

            // configures one-to-many(Postions-to-Users) relationship
            //１つの役職には複数のユーザがいる
            /*modelBuilder.Entity<Positions>()
                        .HasMany(p => p.Users)
                        .WithOne(u => u.Positions)
                        .IsRequired();*/


            // configures one-to-many(Departments-to-Users) relationship
            //１つの部署には複数のユーザが所属している
            /*modelBuilder.Entity<Departments>()
                        .HasMany(d => d.Users)
                        .WithOne(u => u.Departments)
                        .IsRequired();*/
                        
        }

    }
}
