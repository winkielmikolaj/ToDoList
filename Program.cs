using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Models.Domains;
using ToDoList.Persistance;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Domy�lna trasa zaktualizowana na now� nazw� akcji
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Task}/{action=ListTasks}/{id?}");

            // Mo�esz doda� dodatkowe trasy, je�li potrzebujesz specyficznych �cie�ek
            app.MapControllerRoute(
                name: "editTask",
                pattern: "tasks/edit/{id?}",
                defaults: new { controller = "Task", action = "EditTask" });

            app.MapRazorPages();

            app.Run();
        }
    }
}
