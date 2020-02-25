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
    public class CustomerController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select CustomerID,CustomerName,CustomerAddress from dbo.Customers";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyTable"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Customer cust)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"insert into dbo.Customers (CustomerName,CustomerAddress) values
                                         (
                                          '" + cust.CustomerName + @"'
                                          ,'" + cust.CustomerAddress + @"'
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
        public string Put(Customer cust)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                update dbo.Customers set 
                                CustomerName = '" + cust.CustomerName + @"'
                                ,CustomerAddress = '" + cust.CustomerAddress + @"'
                                where CustomerID = " + cust.CustomerID + @"
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
                                delete from  dbo.Customers where CustomerID =" + id;

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
