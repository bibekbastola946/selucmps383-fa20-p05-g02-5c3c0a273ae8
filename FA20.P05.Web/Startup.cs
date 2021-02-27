using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FA20.P05.Web.Data;
using FA20.P05.Web.Features.Authentication;
using FA20.P05.Web.Features.Schools;
using FA20.P05.Web.Features.SchoolStaffMembers;
using FA20.P05.Web.Features.StaffMembers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FA20.P05.Web
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
            services.AddSpaStaticFiles(configuration => {
              configuration.RootPath = "ClientApp/build";
            });
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DataContext")));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DataContext>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000", "http://localhost:19006")
                                                  .AllowAnyHeader()
                                                  .AllowCredentials()
                                                  .AllowAnyMethod();
                    });
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 403;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // this is a simple way of migrating and seeding on start 
            // this is NOT safe to do in a multi-server environment
            MigrateDb(app);
            SeedData(app);

            // This isn't ideal, but the proper way is significantly more complex and really obscures what is happening
            AddRoles(app).Wait();
            AddUsers(app).Wait();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseSpaStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");

                }
            });


        }

        private static void SeedData(IApplicationBuilder app)
        {
            // make sure to dispose of any dependency injection scopes you make.
            // see: https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
            // and https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement
            // TLDR: using calls "Dispose" on an item once the scope is closed out
            // even if an exception is thrown
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                if (context.Set<School>().Any())
                {
                    return;
                }

                context.AddRange(
                    new School { Name = "Hammond High Magnet School", Active = false, SchoolPopulation = 1434 },
                    new School { Name = "Hammond Westside Montessori School", Active = true, SchoolPopulation = 1127 },
                    new School { Name = "Hammond Eastside Magnet School", Active = true, SchoolPopulation = 1318 });

                context.SaveChanges();
            }
        }

        private static void MigrateDb(IApplicationBuilder app)
        {
            // see https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/using
            // this is a 'new' C#8 short hand for what the seed method above does with it's using
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DataContext>();
            context.Database.Migrate();
        }

        private static async Task AddRoles(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
            if (roleManager.Roles.Any())
            {
                return;
            }

            await roleManager.CreateAsync(new Role { Name = Roles.Admin });
            await roleManager.CreateAsync(new Role { Name = Roles.Principal });
            await roleManager.CreateAsync(new Role { Name = Roles.Staff });
        }

        private static async Task AddUsers(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
            var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
            if (userManager.Users.Any())
            {
                return;
            }

            await CreateUser(dataContext, userManager, "admin", Roles.Admin);
            await CreateUser(dataContext, userManager, "principal", Roles.Principal);
            await CreateUser(dataContext, userManager, "staff", Roles.Staff);
        }

        private static async Task CreateUser(DataContext dataContext, UserManager<User> userManager, string username, string role)
        {
            const string passwordForEveryone = "Password123!";
            var user = new User { UserName = username };
            if (role != Roles.Admin)
            {
                var school = await dataContext.Set<School>().FirstAsync(x => x.Active);
                var staff = dataContext.Set<Staff>().Add(new Staff
                {
                    CreatedUtc = DateTimeOffset.UtcNow,
                    FirstName = "john",
                    LastName = user.UserName,
                    Schools = new List<SchoolStaff>
                    {
                        new SchoolStaff{SchoolId = school.Id}
                    }
                });

                await dataContext.SaveChangesAsync();
                user.StaffId = staff.Entity.Id;
            }
            await userManager.CreateAsync(user, passwordForEveryone);
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
