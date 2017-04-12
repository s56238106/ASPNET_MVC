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
        public void InsertOrder(Models.Order order)
        {

        }
        /// <summary>
        /// 刪除訂單
        /// </summary>
        public void DeleteOrderById()
        {

        }
        /// <summary>
        /// 更新訂單
        /// </summary>
        public void UpdateOrder()
        {

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
                    (FirstName+' '+LastName) as EmpName,
					CONVERT(varchar(10),A.OrderDate,111) as OrderDate,
                    CONVERT(varchar(10),A.RequiredDate,111) as RequiredDate,
                    CONVERT(varchar(10),A.ShippedDate,111) as ShippedDate,
                    D.CompanyName,A.Freight,A.ShipCountry,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipAddress,A.ShipName

					From Sales.Orders As A 
					INNER JOIN Sales.Customers As B ON A.CustomerID=B.CustomerID
					INNER JOIN HR.Employees As C On A.EmployeeID=C.EmployeeID
					INNER JOIN Sales.Shippers As D ON A.ShipperID=D.ShipperID

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

            foreach (DataRow item in dt.Rows)
            {
                result.OrderID = item["OrderID"].ToString();
                result.CustName = item["CustomerName"].ToString();
                result.EmpName = item["EmpName"].ToString();
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
                result.ShipName = item["ShipName"].ToString();

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

                    Where (A.OrderID=@OrderID Or @OrderID='') And 
						  (B.CompanyName Like @CustomerName Or @CustomerName='') And
                          (C.EmployeeID=@EmployeeID Or @EmployeeID='') And
                          (D.CompanyName=@CompanyName Or @CompanyName='') And
                          (A.OrderDate=@OrderDate Or @OrderDate='') And
                          (A.ShippedDate=@ShippedDate Or @ShippedDate='') And
                          (A.RequiredDate=@RequiredDate Or @RequiredDate='') ";



			using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.Add(new SqlParameter("@OrderID",arg.OrderID==null?string.Empty:arg.OrderID));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", arg.CustomerName == null ? string.Empty : '%'+arg.CustomerName+'%'));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", arg.EmployeeID == null ? string.Empty : arg.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", arg.ShipperID == null ? string.Empty : arg.ShipperID));
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
                 ///   CustomerID = (int)row["CustomerID"],
                    CustName = row["CustomerName"].ToString(),
                 ///   EmployeeID = (int)row["EmployeeID"],
                 ///   EmpName = row["EmpName"].ToString(),
                    OrderDate = row["OrderDate"].ToString(),
                 ///RequiredDate = row["RequiredDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["RequiredDate"],
                    ShippedDate = row["ShippedDate"].ToString(),
                    CompanyName=row["CompanyName"].ToString()
                    ///   ShipperID = (int)row["ShipperID"],
                    ///   ShipperName = row["ShipperName"].ToString(),
                    ///   Freight = (decimal)row["Freight"],
                    ///   ShipName = row["ShipName"].ToString(),
                    ///   ShipAddress = row["ShipAddress"].ToString(),
                    ///   ShipCity = row["ShipCity"].ToString(),
                    ///   ShipRegion = row["ShipRegion"].ToString(),
                    ///   ShipPostalCode = row["ShipPostalCode"].ToString(),
                    ///   ShipCountry = row["ShipCountry"].ToString()
                });
            }
            return result;
        }
    }
}