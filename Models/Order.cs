using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Build.Framework;

namespace SportsStore.Models
{
	public class Order
	{
		[BindNever]
		public int OrderID { get; set; }
		[BindNever]
		public ICollection<CartLine>? Lines { get; set; }
		[BindNever]
		public bool Shipped { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public string Line3 { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string State { get; set; }
		[Required]
		public string Zip { get; set; }
		[Required]
		public string Country { get; set; }
		public bool GiftWrap { get; set; }

	}
}
