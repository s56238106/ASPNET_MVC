﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSale.Models
{
    public class OrderSearchArg
    {
        public bool vercode { get; set; }
        public string OrderID { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeID { get; set; }
        public string ShipperID { get; set; }
        public string OrderDate { get; set; }
        public string ShippedDate { get; set; }
        public string RequiredDate { get; set; }
    }
}