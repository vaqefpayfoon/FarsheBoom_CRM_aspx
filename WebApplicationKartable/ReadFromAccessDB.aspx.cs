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
    public partial class ReadFromAccessDB : System.Web.UI.Page
    {
        private Search obj1 = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\MyFiles\\FB.accdb";
            string cnn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Data.accdb";
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Code, Supplier, SupplierCode, [Size], Origin, Type, FirstColor, SecondColor, Design, Lenght, Width, WarpMaterial, PileMaterial, DateofEntry, BuyingPrice, SellingPrice, LabelDiscount, FinalDiscount, Status FROM Carpet1", cnn);
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
                        DataRow[] Find = dt.Select(string.Format("code_igd ='{0}'", code));
                        if(!(Find.Count() > 0))
                        {
                            long sale_price = 0; long buy_price = 0; long discount = 0;
                            int provider = 0;int porzid = 0;int cheleid = 0;int carpet_type = 0;int build_state = 0;int ibt_srl = 0; int city_srl = 0; int size_srl = 0; int color_srl = 0; int color_srl2 = 0; float lenght = 0; float widht = 0;string code_igd = "0";string provider_code = "0";
                            if (!Convert.IsDBNull(Woak["Supplier"]))
                                provider = Convert.ToInt32(Woak["Supplier"]);
                            if (!Convert.IsDBNull(Woak["PileMaterial"]))
                                porzid = Convert.ToInt32(Woak["PileMaterial"]);
                            if (!Convert.IsDBNull(Woak["WarpMaterial"]))
                                cheleid = Convert.ToInt32(Woak["WarpMaterial"]);
                            if (!Convert.IsDBNull(Woak["Type"]))
                                carpet_type = Convert.ToInt32(Woak["Type"]);
                            if (!Convert.IsDBNull(Woak["Status"]))
                            {
                                if (Convert.ToInt32(Woak["Status"]) == 2)
                                    build_state = 1;
                            }
                            if (!Convert.IsDBNull(Woak["Origin"]))
                                ibt_srl = Convert.ToInt32(Woak["Origin"]);
                            if (!Convert.IsDBNull(Woak["Design"]))
                                city_srl = Convert.ToInt32(Woak["Design"]);
                            if (!Convert.IsDBNull(Woak["Size"]))
                                size_srl = Convert.ToInt32(Woak["Size"]);
                            if (!Convert.IsDBNull(Woak["FirstColor"]))
                                color_srl = Convert.ToInt32(Woak["FirstColor"]);
                            if (!Convert.IsDBNull(Woak["SecondColor"]))
                                color_srl2 = Convert.ToInt32(Woak["SecondColor"]);
                            if (!Convert.IsDBNull(Woak["Lenght"]))
                                lenght = Convert.ToInt32(Woak["Lenght"]);
                            if (!Convert.IsDBNull(Woak["Width"]))
                                widht = Convert.ToInt32(Woak["Width"]);
                            if (!Convert.IsDBNull(Woak["Code"]))
                                code_igd = Woak["Code"].ToString();
                            if (!Convert.IsDBNull(Woak["SupplierCode"]))
                                provider_code = Woak["SupplierCode"].ToString();
                            if (!Convert.IsDBNull(Woak["SellingPrice"]))
                                sale_price = Convert.ToInt64(Woak["SellingPrice"]);
                            if (!Convert.IsDBNull(Woak["BuyingPrice"]))
                                buy_price = Convert.ToInt64(Woak["BuyingPrice"]);
                            if (!Convert.IsDBNull(Woak["LabelDiscount"]))
                                discount = Convert.ToInt64(Woak["LabelDiscount"]);
                            insert_goods(provider,porzid,cheleid,carpet_type,build_state,ibt_srl,city_srl,size_srl,color_srl,lenght,widht,code_igd,provider_code,color_srl2,sale_price,buy_price,discount);
                        }
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
        private void insert_goods(int provider,int porzid ,int cheleid, int carpet_type,int build_state,int ibt_srl, int city_srl,int size_srl,int color_srl,float lenght,float widht,string code_igd,string provider_code,int color_srl2,long sale_price,long buy_price,long discount)
        {
            if (provider == 0 || code_igd == "0") return;
            Common obj = new Common();
            int srl = max_srl_goods();
            int remain = (srl / 100) + 1;
            SqlParameter[] param = new SqlParameter[33];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[1].Value = obj.persian_date();
            param[2] = new SqlParameter("@title_igd", SqlDbType.VarChar, 100);
            param[2].Value = DBNull.Value;
            param[3] = new SqlParameter("@provider_srl", SqlDbType.Int);
            param[3].Value = provider;
            param[4] = new SqlParameter("@porz_type", SqlDbType.TinyInt);
            if (porzid != 0)
                param[4].Value = porzid;
            else
                param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@chele_type", SqlDbType.TinyInt);
            if (cheleid != 0)
                param[5].Value = cheleid;
            else
                param[5].Value = DBNull.Value;
            param[6] = new SqlParameter("@carpet_type", SqlDbType.TinyInt);
            if(carpet_type != 0)
                param[6].Value = carpet_type;
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@build_state", SqlDbType.TinyInt);
            param[7].Value = build_state;
            param[8] = new SqlParameter("@ibt_srl", SqlDbType.TinyInt);
            if(ibt_srl != 0)
                param[8].Value = ibt_srl;
            else
                param[8].Value = DBNull.Value;
            param[9] = new SqlParameter("@city_srl", SqlDbType.TinyInt);
            if (city_srl != 0)
                param[9].Value = city_srl;
            else
                param[9].Value = DBNull.Value;
            param[10] = new SqlParameter("@size_srl", SqlDbType.TinyInt);
            if(size_srl != 0)
                param[10].Value = size_srl;
            else
                param[10].Value = DBNull.Value;
            param[11] = new SqlParameter("@color_srl", SqlDbType.TinyInt);
            if(color_srl != 0)
                param[11].Value = color_srl;
            else
                param[11].Value = DBNull.Value;
            param[12] = new SqlParameter("@barcode", SqlDbType.Char, 30);
            param[12].Value = DBNull.Value;
            param[13] = new SqlParameter("@sold", SqlDbType.Bit);
            param[13].Value = DBNull.Value;
            param[14] = new SqlParameter("@describtion", SqlDbType.VarChar);
            param[14].Value = DBNull.Value;
            param[15] = new SqlParameter("@lenght", SqlDbType.Float, 20);
            if (lenght != 0)
                param[15].Value = lenght;
            else
                param[15].Value = DBNull.Value;
            param[16] = new SqlParameter("@widht", SqlDbType.Float, 20);
            if(widht != 0)
                param[16].Value = widht;
            else
                param[16].Value = DBNull.Value;
            param[17] = new SqlParameter("@kind", SqlDbType.VarChar, 30);
            param[17].Value = DBNull.Value;
            param[18] = new SqlParameter("@margin_color", SqlDbType.VarChar, 30);
            param[18].Value = DBNull.Value;
            param[19] = new SqlParameter("@dorangi", SqlDbType.Bit);
            param[19].Value = DBNull.Value;
            param[20] = new SqlParameter("@rofo", SqlDbType.Bit);
            param[20].Value = DBNull.Value;
            param[21] = new SqlParameter("@kaji", SqlDbType.Bit);
            param[21].Value = DBNull.Value;
            param[22] = new SqlParameter("@badbaf", SqlDbType.Bit);
            param[22].Value = DBNull.Value;
            param[23] = new SqlParameter("@pakhordegi", SqlDbType.Bit);
            param[23].Value = DBNull.Value;
            param[24] = new SqlParameter("@tear", SqlDbType.Bit);
            param[24].Value = DBNull.Value;
            param[25] = new SqlParameter("@code_igd", SqlDbType.VarChar, 30);
            param[25].Value = code_igd;
            param[26] = new SqlParameter("@plan_desc", SqlDbType.VarChar, 100);
            param[26].Value = DBNull.Value;
            param[27] = new SqlParameter("@provider_code", SqlDbType.VarChar, 30);
            param[27].Value = provider_code;
            param[28] = new SqlParameter("@good_value", SqlDbType.TinyInt);
            param[28].Value = 1;
            param[29] = new SqlParameter("@color_srl2", SqlDbType.TinyInt);
            if(color_srl2 != 0)
                param[29].Value = color_srl2;
            else
                param[29].Value = DBNull.Value;
            param[30] = new SqlParameter("@sale_price", SqlDbType.BigInt);
            if (sale_price != 0)
                param[30].Value = sale_price;
            else
                param[30].Value = DBNull.Value;
            param[31] = new SqlParameter("@buy_price", SqlDbType.BigInt);
            if (buy_price != 0)
                param[31].Value = buy_price;
            else
                param[31].Value = DBNull.Value;
            param[32] = new SqlParameter("@discount", SqlDbType.BigInt);
            if (discount != 0)
                param[32].Value = discount;
            else
                param[32].Value = DBNull.Value;
            new ManageCommands(param, "insert_goods1");
        }
        private void update_goods(int provider, int porzid, int cheleid, int carpet_type, int build_state, int ibt_srl, int city_srl, int size_srl, int color_srl, float lenght, float widht, string code_igd, string provider_code, int lst_color2)
        {

            Common per = new Common();
            SqlParameter[] param = new SqlParameter[30];
            param[0] = new SqlParameter("@porz_type", SqlDbType.TinyInt);
            param[0].Value = Convert.ToInt32(porzid);
            param[1] = new SqlParameter("@chele_type", SqlDbType.TinyInt);
            param[1].Value = cheleid;
            param[2] = new SqlParameter("@carpet_type", SqlDbType.TinyInt);
            param[2].Value = carpet_type;
            param[3] = new SqlParameter("@build_state", SqlDbType.TinyInt);
            param[3].Value = build_state;
            param[4] = new SqlParameter("@ibt_srl", SqlDbType.TinyInt);
            param[4].Value = ibt_srl;
            param[5] = new SqlParameter("@city_srl", SqlDbType.TinyInt);
            param[5].Value = city_srl;
            param[6] = new SqlParameter("@size_srl", SqlDbType.TinyInt);
            param[6].Value = size_srl;
            param[7] = new SqlParameter("@color_srl", SqlDbType.TinyInt);
            param[7].Value = color_srl;
            param[8] = new SqlParameter("@lenght", SqlDbType.Float, 20);
            param[8].Value = lenght;
            param[9] = new SqlParameter("@widht", SqlDbType.Float, 20);
            param[9].Value = widht;
            param[10] = new SqlParameter("@code_igd", SqlDbType.VarChar, 30);
            param[10].Value = code_igd;
            param[11] = new SqlParameter("@provider_code", SqlDbType.VarChar, 30);
            param[11].Value = provider_code;
            param[12] = new SqlParameter("@color_srl2", SqlDbType.TinyInt);
            param[12].Value = lst_color2;
            new ManageCommands(param, "update_goods1");
        }
        private int max_srl_goods()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.inv_goods";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!Convert.IsDBNull(dr[0]))
                {
                    ViewState["srl_details"] = dr[0];
                    return Convert.ToInt32(dr[0]);
                }
                else
                    return 1;
            }
            else
            {
                dr.Close(); dr.Dispose();
                return 1;
            }
        }
    }
    
}