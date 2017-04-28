using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eSale.Models
{
    public class OrderDetails
    {

        /// <summary>
        /// 訂單編號
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 產品代號
        /// </summary>
        [Required]
        public string ProductId { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        [Required]
        public float UnitPrice { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        [Required]
        public decimal Qty { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public float Discount { get; set; }
    }
}