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
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IDatabaseService, JsonDBService>();
            builder.Services.AddScoped<IReviewService, JsonReviewService>();

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

            app.Run();
        }
    }
}
