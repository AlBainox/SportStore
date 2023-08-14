using Newtonsoft.Json;
using SportsStore.Extensions;

namespace SportsStore.Models
{
	public class SessionCart : Cart
	{
		public static Cart GetCart(IServiceProvider serviceProvider)
		{
			ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			SessionCart cart = session?.Get<SessionCart>("Cart") ?? new SessionCart();
			cart.Session = session;
			return cart;
		}
		public override void AddItem(Product product, int quantity)
		{
			base.AddItem(product, quantity);
			Session.Set("Cart", this);
		}
		[JsonIgnore]
		public ISession Session { get; set; }
		public override void RemoveLine(Product product)
		{
			base.RemoveLine(product);
			Session.Set("Cart", this);
		}
		public override void Clear()
		{
			base.Clear();
			Session.Remove("Cart");
		}
	}

}
