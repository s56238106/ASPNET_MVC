using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace eSale.Models
{
    /// <summary>
    /// 訂單服務
    /// </summary>
    public class OrderService
    {
        /// <summary>
		/// 取得DB連線字串
		/// </summary>
		/// <returns></returns>
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString.ToString();
        }


        /// <summary>
        /// 新增訂單
        /// </summary>
        public int InsertOrder(Models.Order order)
        {
            string sql1 = @" Insert INTO Sales.Orders
						 (
							CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipperID,Freight,
							ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry
						)
						VALUES
						(
							@custid,@empid,@orderdate,@requireddate,@shippeddate,@shipperid,@freight,
							@shipname,@shipaddress,@shipcity,@shipregion,@shippostalcode,@shipcountry
						)
						Select SCOPE_IDENTITY()
						";
            string sql2 = @"INSERT INTO Sales.OrderDetails(OrderID,ProductID,UnitPrice,Qty,Discount)
                        VALUES(@OrderID,@ProductID,@UnitPrice,@Qty,@Discount)";
            int id;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql1, conn);
                cmd.Parameters.Add(new SqlParameter("@custid", order.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@empid", order.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@orderdate", order.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@requireddate", order.RequiredDate));
                cmd.Parameters.Add(new SqlParameter("@shippeddate", order.ShippedDate == null ? string.Empty : order.ShippedDate));
                cmd.Parameters.Add(new SqlParameter("@shipperid", order.ShipperID));
                cmd.Parameters.Add(new SqlParameter("@freight", order.Freight == null ? string.Empty : order.Freight));
                cmd.Parameters.Add(new SqlParameter("@shipname", order.ShipName == null ? string.Empty : order.ShipName));
                cmd.Parameters.Add(new SqlParameter("@shipaddress", order.ShipAddress == null ? string.Empty : order.ShipAddress));
                cmd.Parameters.Add(new SqlParameter("@shipcity", order.ShipCity == null ? string.Empty : order.ShipCity));
                cmd.Parameters.Add(new SqlParameter("@shipregion", order.ShipRegion == null ? string.Empty : order.ShipRegion));
                cmd.Parameters.Add(new SqlParameter("@shippostalcode", order.ShipPostalCode == null ? string.Empty : order.ShipPostalCode));
                cmd.Parameters.Add(new SqlParameter("@shipcountry", order.ShipCountry == null ? string.Empty : order.ShipCountry));
                
                id = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new SqlCommand(sql2,conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", id));
                for (int i = 0; i < order.OrderDetails.Count; i++)
                {
                    
                    cmd.Parameters.Add(new SqlParameter("@ProductID", order.OrderDetails[i].ProductId));
                    cmd.Parameters.Add(new SqlParameter("@UnitPrice", order.OrderDetails[i].UnitPrice));
                    cmd.Parameters.Add(new SqlParameter("@Qty", order.OrderDetails[i].Qty));
                    cmd.Parameters.Add(new SqlParameter("@Discount", order.OrderDetails[i].Discount));
                    cmd.ExecuteScalar();
                }



                conn.Close();
            }
            return id;

        }
        /// <summary>
        /// 刪除訂單
        /// </summary>
        public void DeleteOrderById(Models.Order order)
        {
            try
            {
                string sql = "Delete FROM Sales.OrderDetails Where orderid=@orderid";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@orderid", order.OrderID));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                sql = "Delete FROM Sales.Orders Where orderid=@orderid";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@orderid", order.OrderID));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
		/// 修改訂單
		/// </summary>
		/// <param name="order"></param>
		public void UpdateOrder(Models.Order order)
        {
            string sql = @"Update Sales.Orders SET 
							CustomerID=@custid,
                            EmployeeID=@empid,
							OrderDate=@orderdate,
                            RequiredDate=@requireddate,
							ShippedDate=@shippeddate,
							Freight=@freight,
                            ShipperID=@shipperid,
							ShipAddress=@shipaddress,
                            ShipCity=@shipcity,
							ShipRegion=@shipregion,
                            ShipPostalCode=@shippostalcode,
							ShipCountry=@shipcountry,
                            ShipName=@shipname
							WHERE OrderID=@orderid";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@custid", order.CustomerID==null ? string.Empty: order.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@empid", order.EmployeeID == null ? string.Empty : order.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@orderdate", order.OrderDate == null ? string.Empty : order.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@requireddate", order.RequiredDate == null ? string.Empty : order.RequiredDate));
                cmd.Parameters.Add(new SqlParameter("@shippeddate", order.ShippedDate == null ? string.Empty : order.ShippedDate));
                cmd.Parameters.Add(new SqlParameter("@shipperid", order.ShipperID == null ? string.Empty : order.ShipperID));
                cmd.Parameters.Add(new SqlParameter("@freight", order.Freight == null ? string.Empty : order.Freight));
                cmd.Parameters.Add(new SqlParameter("@shipaddress", order.ShipAddress == null ? string.Empty : order.ShipAddress));
                cmd.Parameters.Add(new SqlParameter("@shipcity", order.ShipCity == null ? string.Empty : order.ShipCity));
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", order.RequiredDate == null ? string.Empty : order.RequiredDate));
                cmd.Parameters.Add(new SqlParameter("@shippostalcode", order.ShipPostalCode == null ? string.Empty : order.ShipPostalCode));
                cmd.Parameters.Add(new SqlParameter("@shipcountry", order.ShipCountry == null ? string.Empty : order.ShipCountry));
                cmd.Parameters.Add(new SqlParameter("@orderid", order.OrderID == null ? string.Empty : order.OrderID));
                cmd.Parameters.Add(new SqlParameter("@shipname", order.ShipName == null ? string.Empty : order.ShipName));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }




        /// <summary>
        /// 修改_取得ID條件訂單
        /// </summary>
        /// <returns></returns>
        public Models.Order GetOrderById(string Id)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT 
					A.OrderID,
                    B.CompanyName As CustomerName,
                    A.CustomerID,
                    (FirstName+' '+LastName) as EmpName,
                    C.EmployeeID,
					CONVERT(varchar(10),A.OrderDate,120) as OrderDate,
                    CONVERT(varchar(10),A.RequiredDate,120) as RequiredDate,
                    CONVERT(varchar(10),A.ShippedDate,120) as ShippedDate,
                    D.CompanyName,A.Freight,A.ShipCountry,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipAddress,A.ShipperID,A.ShipName,
                    E.ProductID,E.UnitPrice,E.Qty,E.Discount

					From Sales.Orders As A 
					INNER JOIN Sales.Customers As B ON A.CustomerID=B.CustomerID
					INNER JOIN HR.Employees As C On A.EmployeeID=C.EmployeeID
					INNER JOIN Sales.Shippers As D ON A.ShipperID=D.ShipperID
                    INNER JOIN Sales.OrderDetails As E ON A.OrderID=E.OrderID
                    Where (A.OrderID=@OrderID)";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID",Id));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            Models.Order result = new Models.Order();


            int c = 0;
            foreach (DataRow item in dt.Rows)
            {
                result.OrderID = item["OrderID"].ToString();
                result.CustName = item["CustomerName"].ToString();
                result.CustomerID = item["CustomerID"].ToString();
                result.EmpName = item["EmpName"].ToString();
                result.EmployeeID = item["EmployeeID"].ToString();
                result.OrderDate = item["OrderDate"].ToString();
                result.RequiredDate = item["RequiredDate"].ToString();
                result.ShippedDate = item["ShippedDate"].ToString();
                result.CompanyName = item["CompanyName"].ToString();
                result.Freight = item["Freight"].ToString();
                result.ShipCountry = item["ShipCountry"].ToString();
                result.ShipCity = item["ShipCity"].ToString();
                result.ShipRegion = item["ShipRegion"].ToString();
                result.ShipPostalCode = item["ShipPostalCode"].ToString();
                result.ShipAddress = item["ShipAddress"].ToString();
                result.ShipperID = item["ShipperID"].ToString();
                result.ShipName = item["ShipName"].ToString();

                Models.OrderDetails detail = new Models.OrderDetails();
                detail.ProductId= item["ProductId"].ToString();
                detail.UnitPrice = float.Parse(item["UnitPrice"].ToString());
                detail.Qty = decimal.Parse(item["Qty"].ToString());
                detail.Discount = float.Parse(item["Discount"].ToString());
                result.OrderDetails.Add(detail);

                //result.OrderDetails[c].ProductId = item["ProductId"].ToString();
                //result.OrderDetails[c].UnitPrice = int.Parse(item["UnitPrice"].ToString());
                //result.OrderDetails[c].Qty = decimal.Parse(item["Qty"].ToString());
                //result.OrderDetails[c].Discount = float.Parse(item["Discount"].ToString());
                c++;
            }
            return result;

        }




        /// <summary>
        /// 取得條件訂單
        /// </summary>
        /// <returns></returns>
        public List<Models.Order> GetOrderByCondition(Models.OrderSearchArg arg)
        {
            DataTable dt = new DataTable();
			string sql = @"SELECT 
					A.OrderID,B.CompanyName As CustomerName,
					CONVERT(varchar(10),A.OrderDate,111) as OrderDate,CONVERT(varchar(10),A.ShippedDate,111) as ShippedDate,D.CompanyName

					From Sales.Orders As A 
					INNER JOIN Sales.Customers As B ON A.CustomerID=B.CustomerID
					INNER JOIN HR.Employees As C On A.EmployeeID=C.EmployeeID
					INNER JOIN Sales.Shippers As D ON A.ShipperID=D.ShipperID

                    Where (A.OrderID Like @OrderID Or @OrderID='') And 
						  (B.CompanyName Like @CustomerName Or @CustomerName='') And
                          (C.EmployeeID=@EmployeeID Or @EmployeeID='') And
                          (D.ShipperID=@ShipperID Or @ShipperID='') And
                          (A.OrderDate=@OrderDate Or @OrderDate='') And
                          (A.ShippedDate=@ShippedDate Or @ShippedDate='') And
                          (A.RequiredDate=@RequiredDate Or @RequiredDate='') ";



			using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.Add(new SqlParameter("@OrderID",arg.OrderID==null?string.Empty:'%'+arg.OrderID+'%'));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", arg.CustomerName == null ? string.Empty : '%'+arg.CustomerName+'%'));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", arg.EmployeeID == null ? string.Empty : arg.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@ShipperID", arg.ShipperID == null ? string.Empty : arg.ShipperID));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", arg.OrderDate == null ? string.Empty : arg.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", arg.ShippedDate == null ? string.Empty : arg.ShippedDate));
                cmd.Parameters.Add(new SqlParameter("@RequiredDate", arg.RequiredDate == null ? string.Empty : arg.RequiredDate));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
				sqlAdapter.Fill(dt);
				conn.Close();
			}
			return this.MapOrderDataToList(dt);

        }

        /// <summary>
        /// SQL資料整理
        /// </summary>
        /// <param name="orderData"></param>
        /// <returns></returns>
        private List<Models.Order> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Order> result = new List<Order>();
            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Order()
                {
                    OrderID = row["OrderID"].ToString(),
                    CustName = row["CustomerName"].ToString(),
                    OrderDate = row["OrderDate"].ToString(),
                    ShippedDate = row["ShippedDate"].ToString(),
                    CompanyName=row["CompanyName"].ToString()
                   
                });
            }
            return result;
        }
    }
}