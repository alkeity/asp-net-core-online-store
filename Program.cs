using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Services;
using OnlineStore.Services.Implementations;

namespace OnlineStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>
                (
                options =>
                {
                    string? connectionString = builder.Configuration.GetConnectionString("Default");
                    if (connectionString == null) throw new MissingFieldException("Failed to get connection string.");

                    options.UseSqlServer(connectionString);
                }
                );

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

            app.Run();
        }
    }
}
