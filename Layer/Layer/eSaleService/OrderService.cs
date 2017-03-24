using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSaleService
{
    public class OrderService
    {
        /// <summary>
        /// 依訂單編號取的訂單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public eSaleModel.Order GetOrderById(string id)
        {
            eSaleDao.OrderDao orderDao=new eSaleDao.OrderDao();
            return orderDao.GetOrderById(id);
        }
    }
}
