using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Repositories;
using Cinema_Management_System.Repositories.IRepositories;
using Cinema_Management_System.Service.AccountService;
using Cinema_Management_System.Utilities;
using Cinema_Management_System.Utilities.DbInitializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.CodeAnalysis.Operations;
//using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

namespace Cinema_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var connectionString =
                   builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string"
                       + "'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(connectionString));

           builder.Services.AddIdentity<ApplicationUser,IdentityRole>(option=>
           { 
               //option.SignIn.RequireConfirmedPhoneNumber= true;
               option.SignIn.RequireConfirmedEmail= true;
               option.Password.RequiredLength= 8;
               option.Lockout.AllowedForNewUsers = true;
               option.Lockout.MaxFailedAccessAttempts = 5;

           }).AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IRepository<Cinema>, Repository<Cinema>>();
            builder.Services.AddScoped<IRepository<Movie>, Repository<Movie>>();
            builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
            builder.Services.AddScoped<IRepository<Actor>, Repository<Actor>>();
            builder.Services.AddScoped<IRepository<ApplicationUserOTP>, Repository<ApplicationUserOTP>>();
            builder.Services.AddScoped<IRepositorySubImage, RepositorySubImage>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            //builder.Services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
            //builder.Services.AddScoped<IRepository<IdentityUser>, Repository<IdentityUser>>();

            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider.GetService<IDbInitializer>();
            service.Initialize();

            //builder.Services.AddSession(options =>
            //{
            //    options.Cookie.Name = ".AdventureWorks.Session";
            //    options.IdleTimeout = TimeSpan.FromSeconds(10);
            //    options.Cookie.IsEssential = true;
            //});

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
