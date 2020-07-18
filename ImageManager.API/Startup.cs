using AutoMapper;
using Catel.Configuration;
using ImageManager.API.Mapper;
using ImageManager.BLL.ImageStorageServices;
using ImageManager.BLL.Interfaces.ImageStorageServices;
using ImageManager.BLL.Mapper;
using ImageManager.DAL;
using ImageManager.DAL.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAutoMapper(typeof(AssemblyReferenceBLL).Assembly, typeof(AssemblyReferenceAPI).Assembly);

            var connectionString = Configuration.GetConnectionString("Connection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, s => s.MigrationsAssembly("ImageManager.DAL"));

            });



            //services
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IImageStorageService, ImageStorageService>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
