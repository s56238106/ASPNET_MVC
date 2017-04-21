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
            Models.OrderService orderService = new Models.OrderService();
            Models.CodeService codeservice = new Models.CodeService();
            
            ///修改_取得ID條件訂單
            Models.Order data = orderService.GetOrderById(Id);
            ///取得List客戶名稱,ID
            ViewBag.CustData = codeservice.GetCustomer();
            ///取得List員工姓名,ID
            ViewBag.EmpData = codeservice.GetEmpName();
            ///取得公司名稱,ID
            ViewBag.ComData = codeservice.GetComName();
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
        public ActionResult InsertOrder(Models.Order order)
        {
            Models.CodeService codeservice = new Models.CodeService();
            ///取得List客戶名稱,ID
            ViewBag.CustData = codeservice.GetCustomer();
            ///取得List員工姓名,ID
            ViewBag.EmpData = codeservice.GetEmpName();
            ///取得公司名稱,ID
            ViewBag.ComData = codeservice.GetComName();

            if (order.vercode)
            {
                Models.OrderService orderService = new Models.OrderService();
                string a;
                a=Convert.ToString(orderService.InsertOrder(order));
                order.OrderID = a;
                return View("Index");
            }
            else { 
                return View();
            }
        }

        

        /// <summary>
        /// 刪除訂單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteOrder(Models.Order order)
        {
            try { 
                 Models.OrderService orderservice = new Models.OrderService();
                 orderservice.DeleteOrderById(order);
                 return this.Json(true);
            }
            catch
            {
                return this.Json(false);
            }

        }

    }
}