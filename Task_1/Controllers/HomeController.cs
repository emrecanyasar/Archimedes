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
            List<OrderViewModel> orderList = (from o in context.Orders
                                              join c in context.Customers on o.CustomerId equals c.CustomerId
                                              join e in context.Employees on o.EmployeeId equals e.EmployeeId
                                              join od in context.OrderDetails on o.OrderId equals od.OrderId
                                              group o by o.OrderId into g
                                              orderby g.Key 
                                              select new OrderViewModel()
                                              {
                                                  OrderId = g.First().OrderId,
                                                  SiparisTarihi = g.First().OrderDate,
                                                  MusteriAdi = g.First().Customer.ContactName,
                                                  CalisanAdi = g.First().Employee.FirstName + " " + g.First().Employee.LastName,
                                                  TotalPrice =g.First().OrderDetails.Sum(x=>x.UnitPrice*x.Quantity*(1-(decimal)x.Discount))
                                              }).ToList();
                           
            return View(orderList);
        } 

        public IActionResult Details(int id)
        {
            List<OrderDetailViewModel> orderDetailList = (from od in context.OrderDetails
                                                          join p in context.Products on od.ProductId equals p.ProductId
                                                          join o in context.Orders on od.OrderId equals o.OrderId
                                                          where od.OrderId == id
                                                          select new OrderDetailViewModel()
                                                          {
                                                              OrderId = o.OrderId,
                                                              ProductName = p.ProductName,
                                                              Quantity = od.Quantity,
                                                              Salary = od.UnitPrice,
                                                              Price = od.UnitPrice * (1 - (decimal)od.Discount),
                                                          }).ToList();

            return View(orderDetailList);
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