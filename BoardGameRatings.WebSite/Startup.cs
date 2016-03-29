using BoardGameRatings.WebSite.Contexts;
using BoardGameRatings.WebSite.Mappers;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Extensions;
using BoardGameRatings.WebSite.Models.Repositories;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BoardGameRatings.WebSite
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddMvc();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoriesContext, CategoriesContext>();
            services.AddScoped<ICategoryContext, CategoryContext>();
            services.AddScoped<ICategoryMapper, CategoryMapper>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGamesContext, GamesContext>();
            services.AddScoped<IGameContext, GameContext>();
            services.AddScoped<IGameMapper, GameMapper>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IPlayersContext, PlayersContext>();
            services.AddScoped<IPlayerContext, PlayerContext>();
            services.AddScoped<IPlayerMapper, PlayerMapper>();
            services.AddScoped<IPlayedDateMapper, PlayedDateMapper>();
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseBrowserLink();
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                    .Database.Migrate();
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                    .EnsureSeedData();
            }
            Configure(app, env, loggerFactory);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                            .Database.Migrate();
                    }
                }
                catch
                {
                }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute("categories", "{controller=Categories}/{action=Index}/{id?}");
                routes.MapRoute("removeCategory", "{controller=Categories}/{action=Remove}/{id?}");
                routes.MapRoute("editCategory", "{controller=Categories}/{action=Edit}/{id?}");
                routes.MapRoute("addCategory", "{controller=Categories}/{action=Add}/{id?}");

                routes.MapRoute("games", "{controller=Games}/{action=Index}/{id?}");
                routes.MapRoute("removeGame", "{controller=Games}/{action=Remove}/{id?}");
                routes.MapRoute("editGame", "{controller=Games}/{action=Edit}/{id?}");
                routes.MapRoute("addGame", "{controller=Games}/{action=Add}/{id?}");

                routes.MapRoute("players", "{controller=Players}/{action=Index}/{id?}");
                routes.MapRoute("removePlayer", "{controller=Players}/{action=Remove}/{id?}");
                routes.MapRoute("editPlayer", "{controller=Players}/{action=Edit}/{id?}");
                routes.MapRoute("addPlayer", "{controller=Players}/{action=Add}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}