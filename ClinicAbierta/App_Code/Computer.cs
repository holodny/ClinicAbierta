using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class Computer
    {

        private static string _cnnString = ConfigurationManager.ConnectionStrings["SingleBoardConnectionString"].ToString();


        public static DataTable GetComputer(string companyName)
        {
            DataSet ds = new DataSet("Customers");
            if (companyName != null)
            {
                SqlDataAdapter adp = new SqlDataAdapter("SELECT CustomerID,CompanyName,Phone FROM customers where companyname like @CompanyName", _cnnString);
                //the SQL % sign is added for the like condition to return all companynames that match the first part of the search
                adp.SelectCommand.Parameters.AddWithValue("@CompanyName", companyName + "%");
                adp.Fill(ds, "Customers");
            }
            return ds.Tables["Customers"];
        }


        public static void InsertComputer(string manufacturerName, string model)
        {
            using (SqlConnection nwConn1 = new SqlConnection(_cnnString))
            {
                SqlCommand nwCommand = new SqlCommand();
                SqlDataAdapter nwDataAdapter = new SqlDataAdapter(nwCommand);
                nwCommand.CommandText = "insert into Computer (manufacturer_id, model) Values ((select manufacturer_id from Manufacturer where name = @name), @model)";
                //set the command type
                nwCommand.CommandType = CommandType.Text;
                nwCommand.Connection = nwConn1;
                //create and add the parameters
                nwCommand.Parameters.AddWithValue("@name", manufacturerName);
                nwCommand.Parameters.AddWithValue("@model", model);
                nwConn1.Open();
                nwCommand.ExecuteNonQuery();
            }
        }

    }

}