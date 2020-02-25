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
    public class StoreController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select StoreID,StoreName,StoreAddress from dbo.Stores";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyTable"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Store str)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"insert into dbo.Stores (StoreName,StoreAddress) values
                                         (
                                          '" + str.StoreName + @"'
                                          ,'" + str.StoreAddress + @"'
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

        public string Put(Store str)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                update dbo.Stores set 
                               StoreName = '" + str.StoreName + @"'
                                ,StoreAddress = '" + str.StoreAddress + @"'
                                 where StoreID = " + str.StoreID + @"
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
                                delete from  dbo.Stores where StoreID =" + id;

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
