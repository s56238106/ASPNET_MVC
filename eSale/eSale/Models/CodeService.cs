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
                           EmployeeID,(FirstName+' '+LastName) as name
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
                    Text = row["name"].ToString(),
                    Value = row["EmployeeID"].ToString()
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
            string sql = @"SELECT CompanyName,ShipperID
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
                    Value = row["CompanyName"].ToString()
                });
            }
            return result;
        }


    }
}