using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Helpers;
using MovieApp.Helpers.Interfaces;
using MovieApp.Repository;
using MovieApp.Repository.Interfaces;
using System;

namespace MovieApp
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<MovieAppDbContext>();
            services.AddCors();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IMovieHelper, MovieHelper>();
            services.AddScoped<IPersonHelper, PersonHelper>();
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
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entity.Person, Model.ClientModel.Person>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<Entity.Person, Model.RequestModel.Person>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<Entity.Person, Model.RequestModel.AddPerson>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

                cfg.CreateMap<Entity.Movie, Model.ClientModel.Movie>()
                .ReverseMap()
                .ForMember(dest => dest.YearOfRelease, opt => opt.Condition(src => src.YearOfRelease > 0))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<Entity.Movie, Model.RequestModel.Movie>()
                .ReverseMap()
                .ForMember(dest => dest.YearOfRelease, opt => opt.Condition(src => src.YearOfRelease > 0))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<Entity.Movie, Model.RequestModel.AddMovie>()
                .ReverseMap()
                .ForMember(dest => dest.YearOfRelease, opt => opt.Condition(src => src.YearOfRelease > 0))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            });
            app.UseCors(options =>
                options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod().SetPreflightMaxAge(new TimeSpan(3, 1, 1))
            );
            app.UseMvc();
        }
    }
}
