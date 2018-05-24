using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Data.SqlClient;
//Access to the database using ADO.Net
using System.Configuration;

namespace DAL
{

    public class Manufacturer
    {

        //Set the connection string from the web.config available to methods/functions within this class
        private static string _cnnString = ConfigurationManager.ConnectionStrings["SingleBoardConnectionString"].ToString();

        public static void InsertManufacturer(string manufacturerName, string manufacturerWebsite)
        {
            using (SqlConnection nwConn1 = new SqlConnection(_cnnString))
            {
                SqlCommand nwCommand = new SqlCommand();
                SqlDataAdapter nwDataAdapter = new SqlDataAdapter(nwCommand);
                nwCommand.CommandText = "insert into Manufacturer (name, website) Values (@name, @website)";
                //set the command type
                nwCommand.CommandType = CommandType.Text;
                nwCommand.Connection = nwConn1;
                //create and add the parameters
                nwCommand.Parameters.AddWithValue("@name", manufacturerName);
                nwCommand.Parameters.AddWithValue("@website", manufacturerWebsite);
                nwConn1.Open();
                nwCommand.ExecuteNonQuery();
            }
        }


    }

}