using Microsoft.AspNetCore.Identity;
using static System.Formats.Asn1.AsnWriter;

namespace SportsStore.Models
{
	public class IdentitySeedData
	{
		private const string adminUser = "Admin";
		private const string adminPassword = "Secret123$";
		public static async void EnsurePopulated(IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
				IdentityUser user = await userManager.FindByIdAsync(adminUser);

				if (user == null)
				{
					user = new IdentityUser("Admin");
					await userManager.CreateAsync(user, adminPassword);
				}
			}
		}
	}
}
