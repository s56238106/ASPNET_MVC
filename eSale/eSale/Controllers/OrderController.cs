using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eSale.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            Models.OrderService orderService = new Models.OrderService();
            ViewBag.data = orderService.GetOrders();

            List<SelectListItem> custData = new List<SelectListItem>();
            custData.Add(new SelectListItem()
            {
                Text = "叡揚資訊",
                Value = "1"
            });
            custData.Add(new SelectListItem()
            {
                Text = "高應資訊",
                Value = "2"
            });

            ViewBag.custData = custData;
            return View();
        }

        /// <summary>
        /// 新增訂單畫面
        /// </summary>
        /// <returns></returns>


        public ActionResult InsertOrder()
        {
            return View();
        }

        /// <summary>
        /// 新增訂單存檔的Action
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertOrder(Models.Order order)
        {
           /// Models.OrderService orderService = new Models.OrderService();
           /// orderService.InsertOrder(order);

            ViewBag.Desc1 = "我是ViewBag";
            ViewData["Desc2"] = "我是ViewData";
            TempData["Desc3"] = "我是TempData";
            return RedirectToAction("index");

           /// return View("index");
        }

        [HttpGet]
        public JsonResult TestJson()
        {

            ///var result = new Models.Order();
            ///result.CustId = "GSS";
            ///result.CustName = "叡揚資訊";

            var result = new Models.Order(){CustId = "Gss",CustName = "叡揚資訊"};

            return this.Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TestJson2()
        {
            var redult = new Models.Order() {CustId="0000",CustName="5566" };
            return this.Json(redult, JsonRequestBehavior.AllowGet);
        }

    }
}