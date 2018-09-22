using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniversityAPI.Models;
using UniversityAPI.Dto;
using Microsoft.EntityFrameworkCore;
using UniversityAPI.Services;
using UniversityAPI.Services.Interfaces;
using UniversityAPI.Dto.CreationDto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UniversityAPI.Services.Exceptions;
using UniversityAPI.Dto.UpdateDto;

namespace UniversityAPI
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    
                    ValidIssuer = "http://localhost:57324/",
                    ValidAudience = "http://localhost:57324/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("wiElKi_595_CLM_$!_52()=?"))
                };
            });

            services.AddMvc();

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });

            string connectionString = @"Data Source=DESKTOP-LR8PJSS;Initial Catalog=EduData;Integrated Security=True";
            services.AddDbContext<EduDataContext>(o => o.UseSqlServer(connectionString));

            DependencyInjections(services);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            AutoMapperConfiguration();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseAuthentication();

            app.UseCors("EnableCORS");

            app.UseMvc();            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        private void DependencyInjections(IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddTransient<ITypeCheckerHelper, TypeCheckerHelper>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        /// <summary>
        /// 
        /// </summary>
        private void AutoMapperConfiguration()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Teachers, TeacherDto>();
                cfg.CreateMap<Students, StudentDto>();
                cfg.CreateMap<Classes, ClassDto>();
                cfg.CreateMap<Classes, ClassSimpleDto>();
                cfg.CreateMap<Teachers, TeacherDto>();
                cfg.CreateMap<StudentCreationDto, Students>();

                cfg.CreateMap<ClassCreationDto, Classes>();
                cfg.CreateMap<Classes, ClassCreationDto>();

                cfg.CreateMap<UserCreationDto, Users>();
                cfg.CreateMap<UserCreationDto, Students>();
                cfg.CreateMap<UserCreationDto, Teachers>();
                cfg.CreateMap<UserCreationDto, Parents>();
                cfg.CreateMap<Users, UserCreationDto>();

                cfg.CreateMap<StudentUpdateDto, Students>();
                cfg.CreateMap<ClassUpdateDto, Classes>();
            });
        }
    }
}
