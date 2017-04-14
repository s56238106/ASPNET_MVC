using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eSale.Controllers
{
    public class OrderController : Controller
    {
        /// <summary>
        /// 首頁
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public ActionResult Index(Models.OrderSearchArg arg)
        {
            if (arg.vercode)
            {
               ///取得條件訂單
               Models.OrderService orderService = new Models.OrderService();
               ViewBag.data = orderService.GetOrderByCondition(arg);
            }
            
            ///取得員工姓名,ID
            Models.CodeService emp = new Models.CodeService();
            ViewBag.EmpData = emp.GetEmpName();

            ///取得公司名稱
            Models.CodeService com = new Models.CodeService();
            ViewBag.ComData = com.GetComName();
            return View();
        }



        /// <summary>
        /// 修改畫面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult UpdateOrder(string Id)
        {
            ///修改_取得ID條件訂單
            Models.OrderService orderService = new Models.OrderService();
            Models.Order data = orderService.GetOrderById(Id);
            ///取得List客戶名稱,ID
            Models.CodeService cus = new Models.CodeService();
            ViewBag.CustData = cus.GetCustomer();
            ///取得List員工姓名,ID
            Models.CodeService emp = new Models.CodeService();
            ViewBag.EmpData = emp.GetEmpName();
            ///取得公司名稱
            Models.CodeService com = new Models.CodeService();
            ViewBag.ComData = com.GetComName();
            return View(data);
        }

        /// <summary>
        /// 修改訂單
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ActionResult UpdateOrderAction(Models.Order order)
        {
            Models.OrderService up = new Models.OrderService();
            up.UpdateOrder(order);
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