using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		private IProductRepository _productRepository;
		public AdminController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public ViewResult Index() => View(_productRepository.Products);
		public ViewResult Edit(int productId) => View(_productRepository.Products.FirstOrDefault(p => p.ProductID == productId));
		[HttpPost]
		public IActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				_productRepository.SaveProduct(product);
				TempData["message"] = $"Zapisano {product.Name}.";
				return RedirectToAction("Index");
			}
			else
			{
				return View(product);
			}
		}
		public ViewResult Create() => View("Edit", new Product());
		[HttpPost]
		public IActionResult Delete(int productId)
		{
			Product deletedProduct = _productRepository.DeleteProduct(productId);
			if (deletedProduct is not null)
			{
				TempData["message"] = $"Usunięto {deletedProduct.Name}.";
			}
			return RedirectToAction("Index");
		}
	}
}
