using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
	public class ProductController : Controller
	{
		private IProductRepository _repository;
		public int PageSize = 4;
		public ProductController(IProductRepository repository)
		{
			_repository = repository;
		}
		public ViewResult List(int productPage =1) => View(_repository.Products
		.OrderBy(p=> p.ProductID)
		.Skip((productPage -1) * PageSize)
		.Take(PageSize));
		

	}
}
