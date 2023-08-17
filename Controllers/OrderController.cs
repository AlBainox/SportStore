using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
	public class OrderController : Controller
	{
		private IOrderRepository _orderRepository;
		private Cart cart;
		public OrderController(IOrderRepository orderRepository, Cart cart)
		{
			_orderRepository = orderRepository;
			this.cart = cart;
		}
		[Authorize]
		public ViewResult List() => View(_orderRepository.Orders.Where(o => !o.Shipped));
		
		[HttpPost]
		[Authorize]
		public IActionResult MarkShipped(int orderID)
		{ 
			Order? order = _orderRepository.Orders.FirstOrDefault(o=>o.OrderID == orderID);
			if (order is not null)
			{
				order.Shipped= true;
				_orderRepository.SaveOrder(order);
			}
			return RedirectToAction(nameof(List));
		}
		public ViewResult Checkout() => View(new Order());
		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			if (cart.Lines.Count() == 0)
			{
				ModelState.AddModelError("", "Koszyk jest pusty!");
			}
			if (ModelState.IsValid)
			{
				order.Lines = cart.Lines.ToArray();
				_orderRepository.SaveOrder(order);
				return RedirectToAction(nameof(Completed));
			}
			else
			{
				return View(order);
			}
		}
		public ViewResult Completed()
		{
			cart.Clear();
			return View();
		}

	}
}
