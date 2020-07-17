using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ImageManager.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.AddConfiguration<User, UserConfiguration>();
            //builder.AddConfiguration<Role, RoleConfiguration>();

            base.OnModelCreating(builder);

            //  builder.ApplyConfiguration(new UserConfiguration());



        }

    }
}
