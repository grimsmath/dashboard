using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dashboard
{
    public class Databases 
    {        
        public static SqlDataReader QueryResults(string connectionName, 
            string sqlCommand, 
            Dictionary<string, object> sqlParams)
        {
            ArrayList list = new ArrayList();

            SqlConnection   con = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);
            SqlCommand      cmd = new SqlCommand();
            
            cmd.Connection  = con;
            cmd.CommandText = sqlCommand;
            cmd.CommandType = CommandType.Text;

            if (sqlParams != null)
            {
                foreach (KeyValuePair<string, object> entry in sqlParams)
                {
                    cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }
            }

            try
            {
                con.Open();

                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}