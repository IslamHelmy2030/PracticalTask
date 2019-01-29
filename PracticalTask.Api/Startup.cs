using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PracticalTask.Business;
using PracticalTask.Business.Dto;
using PracticalTask.Business.Dto.Parameter;
using PracticalTask.Core.APIUtilities;
using PracticalTask.Data.PracticalDataModel;
using PracticalTask.Data.PracticalDbContext;
using PracticalTask.Repositories.Repository;
using PracticalTask.Repositories.UnitOfWork;
using Swashbuckle.AspNetCore.Swagger;

namespace PracticalTask.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            RegisterServices(services);

            services.AddCors(options => options.AddPolicy("Cors", builder => {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(cfg => cfg.SwaggerDoc("v1", new Info { Title = "Evolvice Practical Task API", Version = "v1" }));


        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddDbContext<PracticalContext>(cfg =>
                cfg.UseSqlServer(_configuration.GetConnectionString("PracticalTaskConnection")));

            services.AddAutoMapper();
            services.AddScoped<ILogger, Logger<User>>();
            services.AddScoped<DbContext, PracticalContext>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IUnitOfWork<User>, UnitOfWork<User>>();
            services.AddScoped<IUserBusiness, UserBusiness>();

            services.AddTransient<IActionResultResponseHandler, ActionResultResponseHandler>();
            services.AddTransient<IRepositoryActionResult, RepositoryActionResult>();
            services.AddTransient<IRepositoryResult, RepositoryResult>();
            services.AddTransient<IUserDto, UserDto>();
            services.AddTransient<UserParameterDto>();
            services.AddTransient<UsernameParameterDto>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("Cors");
            app.UseMvc();


            app.UseSwagger();
            app.UseSwaggerUI(cfg => cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "Evolvice Practical Task API V1"));
        }
    }
}
