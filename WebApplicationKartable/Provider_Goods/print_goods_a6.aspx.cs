using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplicationKartable
{
    public partial class print_goods_a6 : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dataset = new DataSet();
            SqlConnection cnn = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "A6_Print_Proc";
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            cmd.Parameters.Add("@srl", SqlDbType.Int, 6).Value = Convert.ToInt32(Request.QueryString["snd"]);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dataset, "A6_Print");
            cnn.Close();
        }
    }
}