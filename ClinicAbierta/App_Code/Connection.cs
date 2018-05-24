using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{

    public class Connection
    {

        private static string _cnnString = ConfigurationManager.ConnectionStrings["SingleBoardConnectionString"].ToString();

        public static List<ConnectionDT> GetListOfConnections()
        {
            List<ConnectionDT> ConnectionList = new List<ConnectionDT>();

            SqlDataAdapter adp = new SqlDataAdapter("SELECT connection_id, connection FROM Connection", _cnnString);
            adp.SelectCommand.Connection.Open();
            SqlDataReader dr = null;
            dr = adp.SelectCommand.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ConnectionDT ds = new ConnectionDT();
                    ds.connection_id = Convert.ToInt32(dr["connection_id"]);
                    ds.connection = dr["connection"].ToString();
                    //Add the Customer to the list
                    ConnectionList.Add(ds);
                }
                //Close the connections
                dr.Close();
                adp.SelectCommand.Connection.Close();
            }

            return ConnectionList;
        }


    }

}