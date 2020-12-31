using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cartable;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
namespace WebApplicationKartable
{
    public partial class ReadData : System.Web.UI.Page
    {
        private Search obj1 = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\MyFiles\\FB.accdb";
            string cnn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\data.accdb";
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Code, BuyingPrice, SellingPrice, Status FROM Carpet2", cnn);
            DataTable dts = new DataTable();
            da.Fill(dts); Common obj = new Common();

            DataTable dt = new DataTable();
            dt = obj1.Get_Data("SELECT code_igd from inv_goods");

            if (!(dts.Rows.Count > 0)) return;
            {
                foreach (DataRow Woak in dts.Rows)
                {
                    string code = Woak["Code"].ToString();
                    if (dt.Rows.Count > 0)
                    {
                        SqlConnection con = new SqlConnection(strConnString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update inv_goods set build_state=@build_state,buy_price=@buy_price,sale_price=@sale_price where code_igd=@code_igd;";
                        cmd.Parameters.Add("@code_igd", SqlDbType.VarChar).Value = ID;
                        string status = Woak["Status"].ToString();
                        if (status.Equals("1"))
                            cmd.Parameters.Add("@build_state", SqlDbType.VarChar).Value = "0";
                        else if (status.Equals("2"))
                            cmd.Parameters.Add("@build_state", SqlDbType.VarChar).Value = "4";
                        else if (status.Equals("3"))
                            cmd.Parameters.Add("@build_state", SqlDbType.VarChar).Value = "1";
                        else if (status.Equals("4"))
                            cmd.Parameters.Add("@build_state", SqlDbType.VarChar).Value = "2";
                        else
                            cmd.Parameters.Add("@build_state", SqlDbType.VarChar).Value = "3";

                        if (!string.IsNullOrEmpty(Woak["BuyingPrice"].ToString()))
                            cmd.Parameters.Add("@buy_price", SqlDbType.BigInt).Value = Convert.ToInt64(Woak["BuyingPrice"]);
                        else
                            cmd.Parameters.Add("@buy_price", SqlDbType.BigInt).Value = DBNull.Value;

                        if (!string.IsNullOrEmpty(Woak["SellingPrice"].ToString()))
                            cmd.Parameters.Add("@sale_price", SqlDbType.BigInt).Value = Convert.ToInt64(Woak["SellingPrice"]);
                        else
                            cmd.Parameters.Add("@sale_price", SqlDbType.BigInt).Value = DBNull.Value;
                        if (cmd.Connection.State != ConnectionState.Open)
                            con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private void temp()
        {
            //code = row.ToString();
            //SqlConnection con = new SqlConnection(strConnString);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "update inv_goods set build_state=@build_state where code_igd=@code_igd;";
            //cmd.Parameters.Add("@code_igd", SqlDbType.Int).Value = ID;
            //cmd.Parameters.Add("@build_state", SqlDbType.Int).Value = "1";
            //if (cmd.Connection.State != ConnectionState.Open)
            //    con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
        }
    }
}