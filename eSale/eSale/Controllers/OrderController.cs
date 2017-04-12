using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eSale.Controllers
{
    public class OrderController : Controller
    {
        
        public ActionResult Index(Models.OrderSearchArg arg)
        {
            if (arg.vercode)
            {
               ///取得條件訂單
               Models.OrderService orderService = new Models.OrderService();
               ViewBag.data = orderService.GetOrderById(arg);
            }
            
            ///取得員工姓名,ID
            Models.CodeService emp = new Models.CodeService();
            ViewBag.EmpData = emp.GetEmpName();

            ///取得公司名稱
            Models.CodeService com = new Models.CodeService();
            ViewBag.ComData = com.GetComName();
            return View();
        }




        public ActionResult UpdateOrder()
        {
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

        

    }
}