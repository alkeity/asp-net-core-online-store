using OnlineStore.Data.Repositories;
using OnlineStore.Data.Repositories.Implementations;
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

            builder.Services.AddScoped<BaseRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

            app.Run();
        }
    }
}
