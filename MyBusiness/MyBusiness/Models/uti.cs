using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Collections;

namespace MyBusiness.Models
{
    public static class uti
    {
        public static DataTable GetInfo(string folder)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection cnn = new MySqlConnection(ConnectionStrings.MySqlConnectionString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM mybusiness_images WHERE folder=@folder";
                    cmd.Parameters.AddWithValue("folder", folder);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}