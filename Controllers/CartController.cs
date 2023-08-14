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
		private Cart cart;
		public CartController(IProductRepository productRepository, Cart cartService)
		{
			_productRepository = productRepository;
			this.cart = cartService;			
		}

		public ViewResult Index(string returnUrl) {
			return View(new CartIndexViewModel
			{
				Cart = cart,
				ReturnUrl = returnUrl
			});
		}
		public RedirectToActionResult AddToCart(int productId, string returnUrl)
		{
			Product product = _productRepository.Products.FirstOrDefault(p => p.ProductID== productId);
			if (product is not null)
			{				
				cart.AddItem(product, 1);				
			}

			return RedirectToAction("Index", new { returnUrl });
		}
		public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
		{
			Product product = _productRepository.Products.FirstOrDefault(p => p.ProductID == productId);

			if (product is not null)
			{			
				cart.RemoveLine(product);				
			}
			return RedirectToAction("Index", new {returnUrl});	
		}
	}
}
