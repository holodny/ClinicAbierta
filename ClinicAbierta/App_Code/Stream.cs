using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;


namespace DAL
{

    public class Stream
    {   
       // private static string _cnnString = ConfigurationManager.ConnectionStrings["OPENSIMConnectionString1"].ToString();

       // private static string _cnnString = ConfigurationManager.ConnectionStrings["OPENSIMConnectionString"].ToString();

       // SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["nVLC_Demo_WinForms.Properties.Settings.OPENSIMConnectionString"].ConnectionString);
        
        public static DataTable FindCameras()
        {

            //SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["nVLC_Demo_WinForms.Properties.Settings.OPENSIMConnectionString"].ConnectionString);
            //SqlConnection con = new SqlConnection("Server='.\\SQLEXPRESS';Database=OPENSIM;Trusted_Connection=true;");
           
            //SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\OPENSIM.mdf;Integrated Security=True;User Instance=True");
            
            SqlConnection con = new SqlConnection("Data Source=user1-PC\\SQLEXPRESS;Initial Catalog=OPENSIM;Integrated Security=True");
            

            con.Open();

            //SqlDataAdapter adp = new SqlDataAdapter("select distinct s.stream_id, c.camera_id, c.stream, s.panel from dbo.Stream s, dbo.Camera c where c.camera_id = s.camera_id order by panel", con);
            SqlDataAdapter adp = new SqlDataAdapter("select distinct c.camera_id, c.stream, c.panel, c.parameters, c.delay, c.cam_url, c.cam_name from dbo.Camera c where c.panel is not null order by panel", con);

            //SqlDataAdapter adp = new SqlDataAdapter("select distinct stream_id, camera_id, mic_id, room_id, stream, panel from OPENSIM.dbo.Stream", con);


                DataSet ds1 = new DataSet("Streams");
                adp.Fill(ds1, "Streams");
                //con.Close();
                return ds1.Tables["Streams"];

        }
        




    }  

    
}
