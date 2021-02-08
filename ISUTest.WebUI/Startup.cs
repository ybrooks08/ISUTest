using System;
using AutoMapper;
using ISUTest.Data;
using ISUTest.Data.ContactRepository;
using ISUTest.Data.ReservationRepository;
using ISUTest.Interfaces;
using ISUTest.Services;
using ISUTest.Services.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ISUTest.WebUI
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration
        {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            var mappingConfig = new MapperConfiguration( mc =>
            {
                mc.AddProfile( new AppProfile() );
            } );

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton( mapper );

            //Adding context
            services.AddDbContext<ISUDbContext>( item => item.UseSqlServer( Configuration.GetConnectionString( "DefaultConnection" ) ) );

            services.AddControllersWithViews();

            //Configuring Dependency Injection
            services.AddScoped( typeof( IReservationRepository ), typeof( ReservationRepository ) );
            services.AddScoped( typeof( IContactRepository ), typeof( ContactRepository ) );
            services.AddScoped( typeof( IReservationManager ), typeof( ReservationManager ) );
            services.AddScoped( typeof( IContactManager ), typeof( ContactManager ) );

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles( configuration =>
             {
                 ///configuration.RootPath = "ClientApp/build";
                 configuration.RootPath = "ClientApp/dist";
             } );

            services.AddCors( o => o.AddPolicy( "DevelopmentPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }
             ) );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
                app.UseCors( "DevelopmentPolicy" );
            }
            else
            {
                app.UseExceptionHandler( "/Error" );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller}/{action=Index}/{id?}" );
             } );

            app.UseSpa( spa =>
             {
                 spa.Options.SourcePath = "ClientApp";

                 if ( env.IsDevelopment() )
                 {
                     spa.Options.StartupTimeout = TimeSpan.FromSeconds( 300 );
                     spa.UseReactDevelopmentServer( npmScript: "start" );
                 }
             } );
        }
    }
}
