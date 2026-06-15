using Cinema_Management_System.Models;
using Cinema_Management_System.Repositories;
using Cinema_Management_System.Repositories.IRepositories;
using Cinema_Management_System.Service.AccountService;
using Cinema_Management_System.Utilities;
using Cinema_Management_System.Utilities.DbInitializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Cinema_Management_System
{
    public static class DependencyInjection
    {
        public static void Configure(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IRepository<Cinema>, Repository<Cinema>>();
            services.AddScoped<IRepository<Cart>, Repository<Cart>>();
            services.AddScoped<IRepository<Movie>, Repository<Movie>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Actor>, Repository<Actor>>();
            services.AddScoped<IRepository<ApplicationUserOTP>, Repository<ApplicationUserOTP>>();
            services.AddScoped<IRepositorySubImage, RepositorySubImage>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDbInitializer, DbInitializer>();
        }
    }
}
