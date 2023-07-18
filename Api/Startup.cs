using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AcmeStudios.ApiRefactor.Data;
using AcmeStudios.ApiRefactor.Business;
using AcmeStudios.ApiRefactor.Models;

namespace AcemStudios.ApiRefactor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
             {
                 options.AddPolicy("AllowMyOrigin",
                 builder => builder.WithOrigins("http://localhost:4200")
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 );
             });

            services.AddControllers();

            ////Register DB Conneciton
            services.AddDbContext<AcmeStudios.ApiRefactor.Data.DbContext>(options => options.UseSqlServer(
            Configuration.GetConnectionString("StudioConnection")));

            //Initialising DI Objects
            services.AddScoped<IStudioRepository, StudioRepository>();
            services.AddScoped<IStudioService, StudioService>();

            //Initialising Swagger
            services.AddSwaggerGen();

            // Configure AutoMapper
            services.AddAutoMapper(typeof(Startup));
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowMyOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllers().RequireCors("AllowMyOrigin");
            });
        }

    }
}
