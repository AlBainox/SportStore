using SportsStore.Models;

namespace SportStore.Models.ViewModels
{
	public class ProductListViewModel
	{
		public IEnumerable<Product>	Products { get; set; }
		public PagingInfo PagingInfo { get; set; }
	}
}
