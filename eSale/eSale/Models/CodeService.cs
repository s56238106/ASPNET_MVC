﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace eSale.Models
{
    public class CodeService
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
        /// 取得所有員工姓名ID
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetEmpName()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT 
                           EmployeeId,(FirstName+' '+LastName) as Name
                           FROM HR.Employees";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem());
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["Name"].ToString(),
                    Value = row["EmployeeId"].ToString()
                });
            }
            return result;
        }



        /// <summary>
        /// 取得公司名稱ID
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetComName()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT CompanyName,ShipperId
                           FROM Sales.Shippers";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem());
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CompanyName"].ToString(),
                    Value = row["ShipperId"].ToString()
                });
            }
            return result;
        }


        /// <summary>
        /// 取得客戶資料
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCustomer()
        {
            DataTable dt = new DataTable();
            string sql = @"Select CustomerId,CompanyName As CustName FROM Sales.Customers";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem());
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CustName"].ToString(),
                    Value = row["CustomerId"].ToString()
                });
            }
            return result;
        }



        /// <summary>
        /// 取得產品
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetProduct()
        {
            DataTable dt = new DataTable();
            string sql = @"Select ProductId,ProductName From Production.Products";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem());
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["ProductName"].ToString(),
                    Value = row["ProductId"].ToString()
                });
            }
            return result;
        }

    }
}