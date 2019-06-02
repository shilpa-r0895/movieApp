using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Entity;
using MovieApp.Helpers;
using MovieApp.Model;
using MovieApp.Repository;
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
            services.AddScoped<IMovieAppRepository, MovieAppRepository>();
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
                cfg.CreateMap<Actor, ActorDto>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<Producer, ProducerDto>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<Movie, MovieDto>()
                .ReverseMap()
                .ForMember(dest => dest.YearOfRelease, opt => opt.Condition(src => src.YearOfRelease > 0))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<CompleteMovieDto, MovieDto>()
               .ReverseMap()
               .ForMember(dest => dest.YearOfRelease, opt => opt.Condition(src => src.YearOfRelease > 0))
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<Movie, CompleteMovieDto>()
               .ReverseMap()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<Actor, CompleteActorDto>()
               .ReverseMap()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<Producer, CompleteProducerDto>()
               .ReverseMap()
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
