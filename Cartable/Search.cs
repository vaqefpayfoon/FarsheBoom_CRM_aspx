using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Cartable
{
    public class Search
    {
        private string ConnectionString;
        public Search(string str)
        {
            ConnectionString = str;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]

        public List<string> FilterSearch(string query, string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> mylist = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            mylist.Add(sdr[0].ToString());
                        }
                    }
                    conn.Close();
                    return mylist;
                }
            }
        }
        public DataTable Get_Data(string query)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, ConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public string name_hpl(int srl,string cnn)
        {
            SqlConnection connection = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("select first_name+' '+last_name As full_name from hpl_personal where srl=" + srl, connection);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return dr[0].ToString();
            }
            connection.Close();
            return string.Empty;
        }
    }
}
