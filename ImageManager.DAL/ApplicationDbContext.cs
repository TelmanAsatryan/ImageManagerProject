using ImageManager.DAL.Configurations.ImageStoreConfigurations;
using ImageManager.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ImageManager.DAL
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
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

            builder.ApplyConfiguration(new ImageStoreConfiguration());



        }

    }
}
