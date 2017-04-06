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
            ///取得條件訂單
            Models.OrderService orderService = new Models.OrderService();
            ViewBag.data = orderService.GetOrderById(arg);

            ///取得員工姓名,ID
            Models.OrderService emp = new Models.OrderService();
            List<Models.Order> empname = emp.GetEmpName();
            List<SelectListItem> EmpData = new List<SelectListItem>();
            EmpData.Add(new SelectListItem());
            foreach (var item in empname)
            {
                EmpData.Add(new SelectListItem()
                            {
                                Text = item.EmpName,
                                Value= item.EmployeeID.ToString()
                            });
            }
            ViewBag.EmpData = EmpData;

            ///取得公司名稱
            Models.OrderService com = new Models.OrderService();
            List<Models.Order> comdata = com.GetComName();
            List<SelectListItem> ComData = new List<SelectListItem>();
            ComData.Add(new SelectListItem());
            foreach (var item in comdata)
            {
                ComData.Add(new SelectListItem()
                {
                    Text=item.ShipName
                });
            }
            ViewBag.ComData = ComData;



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