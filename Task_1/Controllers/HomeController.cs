using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_1.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Task_1.ViewModels;

namespace Task_1.Controllers
{
    public class HomeController : Controller
    {
        NORTHWNDContext context = new NORTHWNDContext();
        public IActionResult Index()
        {

            //List<OrderDetailViewModel> discount = (from od in context.OrderDetails
            //                                              join o in context.Orders on od.OrderId equals o.OrderId
            //                                              select new OrderDetailViewModel()
            //                                              {
            //                                                  OrderId = o.OrderId,
            //                                                  Quantity = od.Quantity,
            //                                                  Salary = od.UnitPrice,
            //                                                  Discount = od.UnitPrice * (1 - (decimal)od.Discount),
            //                                                  TotalPrice = od.Quantity*(od.UnitPrice * (1 - (decimal)od.Discount))
            //                                              }).ToList();

            List<OrderViewModel> orderList = (from o in context.Orders
                                                   join c in context.Customers on o.CustomerId equals c.CustomerId
                                                   join e in context.Employees on o.EmployeeId equals e.EmployeeId
                                                   
                                                   select new OrderViewModel()
                                                   {
                                                       OrderId = o.OrderId,
                                                       SiparisTarihi = o.OrderDate,
                                                       MusteriAdi = c.CompanyName,
                                                       CalisanAdi = e.FirstName + " " + e.LastName,
                                                       

                                                   }).ToList();

            //var query = orderList.GroupBy(c => c.OrderId).Select(a => new { SiparisToplamı = a.Count() });

            return View(orderList);
        }

        public IActionResult Details(int id)
        {
            List<OrderDetailViewModel> orderDetailList = (from od in context.OrderDetails
                                                          join p in context.Products on od.ProductId equals p.ProductId
                                                          join o in context.Orders on od.OrderId equals o.OrderId
                                                          select new OrderDetailViewModel()
                                                          {
                                                              OrderId = o.OrderId,
                                                              ProductName = p.ProductName,
                                                              Quantity = od.Quantity,
                                                              Salary = od.UnitPrice,
                                                              Discount= od.UnitPrice * (1 - (decimal)od.Discount),
                                                              TotalPrice = od.Quantity * (od.UnitPrice * (1 - (decimal)od.Discount))
                                                          }).ToList();

           List<OrderDetailViewModel> orderss = orderDetailList.Where(o => o.OrderId == id).ToList();

            return View(orderss);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreateShipper()
        {
            Shipper shipper = new Shipper();
            shipper.CompanyName = Request.Form["CompanyName"];
            shipper.Phone = Request.Form["Phone"];
            context.Shippers.Update(shipper);
            context.SaveChanges();
            return View();
        }
    }
}