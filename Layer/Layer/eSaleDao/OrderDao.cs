using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSaleDao
{
    public class OrderDao
    {
        /// <summary>
        /// 依訂單編號取的訂單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public eSaleModel.Order GetOrderById(string id)
        {
            return new eSaleModel.Order() {CustId="GSS",CustName="叡揚資訊",OrderId="1" };
        }
    }
}
