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
    public class ProductController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select ProductID,ProductName,ProductPrice from dbo.Products";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyTable"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Product prdt)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"insert into dbo.Products (ProductName,ProductPrice) values
                                         (
                                          '" + prdt.ProductName + @"'
                                          ,'" + prdt.ProductPrice + @"'
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

        public string Put(Product prdt)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                update dbo.Products set 
                                ProductName = '" + prdt.ProductName + @"'
                                ,ProductPrice = '" + prdt.ProductPrice+ @"'
                                where ProductID = " + prdt.ProductID + @"
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
                                delete from  dbo.Products where ProductID =" + id;

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
