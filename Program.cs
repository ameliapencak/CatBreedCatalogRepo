using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CatBreedCatalog.Models;
using CatBreedCatalog.Data;


namespace CatBreedCatalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();


			builder.Services.AddDbContext<CatBreedContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("CatBreedContext")));//
			
            builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

            builder.Services.AddAuthentication("Cookies")
            .AddCookie("Cookies", options =>
            {
                options.LoginPath = "/User/Login";
            });

            builder.Services.AddAuthorization();

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var context = services.GetRequiredService<CatBreedContext>();
				context.Database.EnsureCreated(); // Ensure the database is created without migrations
			}
			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}