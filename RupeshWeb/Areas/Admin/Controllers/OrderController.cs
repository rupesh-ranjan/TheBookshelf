using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Rupesh.DataAccess.Repository.IRepository;
using Rupesh.Models;

namespace RupeshWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			return View();
		}


		#region API Calls
		[HttpGet]
		public IActionResult GetAll()
		{
			List<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
			return Json(new { data = objOrderHeaders });
		}
		#endregion
	}
}
