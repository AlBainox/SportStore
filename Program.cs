using SportsStore.Models;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using Microsoft.AspNetCore.Routing.Template;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetValue<string>("Data:ConnectionStrings:SportStoreProducts")));
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductRepository, EFProductRepository>();	
builder.Services.AddMvc();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapGet("/hi", () => "Hello!");
app.MapDefaultControllerRoute();
app.MapRazorPages();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	name: "pagination",
	pattern: "Produkty/Strona{productPage}",
	defaults: new { controller = "Product", action = "List" });
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller = Product}/{action=List}/{id?}");
});
SeedData.EnsurePopulated(app);
app.Run();
