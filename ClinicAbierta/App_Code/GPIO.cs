using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class GPIO
    {

        private static string _cnnString = ConfigurationManager.ConnectionStrings["SingleBoardConnectionString"].ToString();

        /*
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
        }  */


        //this has to wait until Revision table is filled.
        public static void InsertGPIO(int revision_id, string digital, string analog)
        {
            using (SqlConnection nwConn1 = new SqlConnection(_cnnString))
            {
                SqlCommand nwCommand = new SqlCommand();
                SqlDataAdapter nwDataAdapter = new SqlDataAdapter(nwCommand);
                nwCommand.CommandText = "insert into GPIO (revision_id,digital, analog) Values (@revision_id,@digital,@analog)";
                //set the command type
                nwCommand.CommandType = CommandType.Text;
                nwCommand.Connection = nwConn1;
                //create and add the parameters
                nwCommand.Parameters.AddWithValue("@revision_id", Convert.ToInt32(revision_id));
                nwCommand.Parameters.AddWithValue("@digital", Convert.ToInt32(digital));
                nwCommand.Parameters.AddWithValue("@analog", Convert.ToInt32(analog));
                nwConn1.Open();
                nwCommand.ExecuteNonQuery();
            }
        }


    }

}