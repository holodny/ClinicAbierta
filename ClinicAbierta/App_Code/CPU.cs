using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{

    public class CPU
    {

        private static string _cnnString = ConfigurationManager.ConnectionStrings["SingleBoardConnectionString"].ToString();

        public static List<CPUDT> GetListOfCPUs()
        {
            List<CPUDT> CPUList = new List<CPUDT>();

            SqlDataAdapter adp = new SqlDataAdapter("SELECT cpu_id, model FROM CPU", _cnnString);
            adp.SelectCommand.Connection.Open();
            SqlDataReader dr = null;
            dr = adp.SelectCommand.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CPUDT ds = new CPUDT();
                    ds.cpu_id = Convert.ToInt32(dr["cpu_id"]);
                    ds.model = dr["model"].ToString();
                    //Add the Customer to the list
                    CPUList.Add(ds);
                }
                //Close the connections
                dr.Close();
                adp.SelectCommand.Connection.Close();
            }

            return CPUList;
        }
    }

}