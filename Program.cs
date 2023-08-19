using SportsStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetValue<string>("Data:ConnectionStrings:SportStoreProducts")));
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppIdentityDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetValue<string>("Data:ConnectionStrings:SportStoreIdentity")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddTransient<IProductRepository, EFProductRepository>();
builder.Services.AddScoped(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IOrderRepository, EFOrderRepository>();
builder.Services.AddMvc();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.MapGet("/hi", () => "Hello!");
app.MapDefaultControllerRoute();
app.MapRazorPages();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{	
	endpoints.MapControllerRoute(
		name: null,
		pattern: "{category}/Strona{productPage:int}",
		defaults: new { controller = "Product", action = "List"});
	endpoints.MapControllerRoute(
		name: null,
		pattern: "Strona{productPage:int}",
		defaults: new { controller = "Product", action = "List", productPage = 1});
	endpoints.MapControllerRoute(
		name: null,
		pattern: "{category}",
		defaults: new { controller = "Product", action = "List", productPage = 1 });
	endpoints.MapControllerRoute(
		name: null,
		pattern: "",
		defaults: new { controller = "Product", action = "List", productPage = 1 });
	endpoints.MapControllerRoute(
		name: null,
		pattern: "{controller}/{action}/{id?}");
});
SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);
app.Run();
