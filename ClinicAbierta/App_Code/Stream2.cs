using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using DAL;

namespace DAL
{

    public class Stream2
    {
        private static string _cnnString = ConfigurationManager.ConnectionStrings["SingleBoardConnectionString"].ToString();




        public static void SavePanels(myObject2 StreamList)
        {






        }







        public static void InsertRevisionConnection(int revision, List<object> ConnectionList)
        {
            using (SqlConnection nwConn1 = new SqlConnection(_cnnString))
            {
                foreach (object i in ConnectionList)
                {
                    SqlCommand nwCommand = new SqlCommand();
                    SqlDataAdapter nwDataAdapter = new SqlDataAdapter(nwCommand);
                    nwCommand.CommandText = "insert into Revision_connection (revision_id, connection_id) Values (@revision_id, @connection_id)";
                    //set the command type
                    nwCommand.CommandType = CommandType.Text;
                    nwCommand.Connection = nwConn1;
                    //create and add the parameters
                    nwCommand.Parameters.AddWithValue("@revision_id", revision);
                    nwCommand.Parameters.AddWithValue("@connection_id", Convert.ToString(i));
                    //nwCommand.Parameters.AddWithValue("@connection_id", i));
                    nwConn1.Open();
                    nwCommand.ExecuteNonQuery();

                    nwConn1.Close();


                }
            }
        }


    }

}