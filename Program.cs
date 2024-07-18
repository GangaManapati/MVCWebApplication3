using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MVCWebApplication3.Controllers;
using MVCWebApplication3.Models;
using MVCWebApplication3.Repository;

namespace MVCWebApplication3
{
    public class Program
    {
        public static bool UseApi { get; private set; } = true;
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);



            // for DbContext connection 
            builder.Services.AddDbContext<ProDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

             app.Run();
        }

        


    }
}
