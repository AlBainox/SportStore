using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using SportsStore.Extensions;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Controllers
{
	public class CartController : Controller
	{
		private IProductRepository _productRepository;
		public CartController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public ViewResult Index(string retunUrl) {
			return View(new CartIndexViewModel
			{
				Cart = GetCart(),
				ReturnUrl = retunUrl
			});
		}
		public RedirectToActionResult AddToCart(int productId, string returnUrl)
		{
			Product product = _productRepository.Products.FirstOrDefault(p => p.ProductID== productId);
			if (product is not null)
			{
				Cart cart = GetCart();
				cart.AddItem(product, 1);
				SaveCart(cart);
			}

			return RedirectToAction("Index", new { returnUrl });
		}
		public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
		{
			Product product = _productRepository.Products.FirstOrDefault(p => p.ProductID == productId);

			if (product is not null)
			{
				Cart cart = GetCart();
				cart.RemoveLine(product);
				SaveCart(cart);
			}
			return RedirectToAction("Index", new {returnUrl});	
		}

		private Cart GetCart()
		{
			Cart cart= HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
			return cart;
		}
		private void SaveCart(Cart cart) {
			HttpContext.Session.Set("Cart", cart);
		}
	}
}
