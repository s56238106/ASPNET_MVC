using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Layer.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            eSaleService.OrderService orderService = new eSaleService.OrderService();
            var data=orderService.GetOrderById("1");
            ViewBag.data = data.CustId + " . " + data.CustName + " . " + data.OrderId;
            return View();


        }
    }
}