using GepardOOD.Data.Models;
using GepardOOD.Services.Data.Interfaces;
using GepardOOD.Web.Data;
using GepardOOD.Web.Infrastructure.Extensions;
using GepardOOD.Web.Infrastructure.ModelBinders;
using static GepardOOD.Common.GeneralApplicationConstants;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<GepardOODDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 4;
                })
	            .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<GepardOODDbContext>();

            builder.Services.AddApplicationServices(typeof(IBeerService));

            builder.Services.AddControllersWithViews()
	            .AddMvcOptions(opt =>
	            {
		            opt.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
	            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.SeedAdministrator(DevelopmentAdminEmail);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}