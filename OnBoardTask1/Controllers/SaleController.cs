using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using OnBoardTask1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnBoardTask1.Controllers
{
    public class SaleController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select SaleID,ProductID,CustomerID,StoreID,convert(varchar(10),DateSold,120) as DateSold from dbo.Sales";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyTable"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Sale sl)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"insert into dbo.Sales (ProductID,CustomerID,StoreID,DateSold) values
                                         (
                                          '" + sl.ProductID + @"'
                                          ,'" + sl.CustomerID + @"'
                                           ,'" + sl.StoreID + @"'
                                            ,'" + sl.DateSold + @"'   
                                          )
                                         ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyTable"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successfully";
            }
            catch (Exception)
            {
                return "failed to Add";
            }

        }

        public string Put(Sale sl)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                update dbo.Sales set 
                                ProductID = '" + sl.ProductID + @"'
                                ,CustomerID = '" + sl.CustomerID + @"'
                                ,StoreID = '" + sl.StoreID + @"'
                                ,DateSold = '" + sl.DateSold + @"'
                                where SaleID = " + sl.SaleID + @"
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyTable"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "failed to Update";
            }
        }

        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                delete from  dbo.Sales where SaleID =" + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyTable"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "failed to Delete";
            }
        }
    }

    
}
