using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models.ViewModels
{
	public class LoginModel
	{
		[System.ComponentModel.DataAnnotations.Required]
		public string Name { get; set; }
		[System.ComponentModel.DataAnnotations.Required]
		[UIHint("password")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; } = "/";
	}
}
