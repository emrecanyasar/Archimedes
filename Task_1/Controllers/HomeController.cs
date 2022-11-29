using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_1.Models;
using System.Linq;
using Task_1.DTO;
using Microsoft.EntityFrameworkCore;

namespace Task_1.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            NORTHWNDContext context = new NORTHWNDContext();
            List<Order> ordersList = context.Orders.ToList();
            return View(ordersList);
        }
    }
}