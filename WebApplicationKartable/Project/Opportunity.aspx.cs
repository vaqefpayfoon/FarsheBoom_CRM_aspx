using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;


namespace WebApplicationKartable
{
    public partial class Opportunity1 : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btn_transfer_Click(object sender, EventArgs e)
        {
            Search obj = new Search(strConnString);

            DataTable dt_supcusts_orders = obj.Get_Data("SELECT srl, bassc_srl, city_srl, size_srl, color_srl, u_date_time, carpet_type, from_price, to_price, describtion, opportunity_srl FROM dbo.bas_supcust_order");
            DataTable dt_supcusts_goods = obj.Get_Data("SELECT srl, bassc_srl, igd_srl, opportunity_srl, u_date_time FROM dbo.bas_supcust_goods");
            DataTable dt_projects_goods = obj.Get_Data("SELECT srl, header_srl, igd_srl, ibt_srl, size_srl, color_srl, carpet_type, sale_price FROM dbo.Project_Goods_View Where header_srl=" + Request.QueryString["srl"]);
            string jquery = "$('#content').removeClass('fullwidth').delay(2).queue(function(next){$(this).addClass('fullwidth');next();});";
            foreach (DataRow Woak in dt_supcusts_orders.Rows)
            {
                int city_srl = 0;
                if (!Convert.IsDBNull(Woak["city_srl"]))
                     city_srl = Convert.ToInt32(Woak["city_srl"]);
                int size_srl = 0;
                if (!Convert.IsDBNull(Woak["size_srl"]))
                    size_srl = Convert.ToInt32(Woak["size_srl"]);
                int color_srl = 0;
                if (!Convert.IsDBNull(Woak["color_srl"]))
                    color_srl = Convert.ToInt32(Woak["color_srl"]);
                int carpet_type = 0;
                if (!Convert.IsDBNull(Woak["carpet_type"]))
                    carpet_type = Convert.ToInt32(Woak["carpet_type"]);
                long from_price = 0;
                if (!Convert.IsDBNull(Woak["from_price"]))
                    from_price = Convert.ToInt64(Woak["from_price"]);
                long to_price = 0;
                if (!Convert.IsDBNull(Woak["to_price"]))
                    to_price = Convert.ToInt64(Woak["to_price"]);
                int i = dt_projects_goods.Rows.Count;
                foreach (DataRow Look in dt_projects_goods.Rows)
                {
                    if (Look["igd_srl"].ToString().Equals("4"))
                        ubuzhi.first_name = "shix";
                    int city_srl2 = 0;
                    if (!Convert.IsDBNull(Look["ibt_srl"]))
                        city_srl2 = Convert.ToInt32(Look["ibt_srl"]);
                    int size_srl2 = 0;
                    if (!Convert.IsDBNull(Look["size_srl"]))
                        size_srl2 = Convert.ToInt32(Look["size_srl"]);
                    int color_srl2 = 0;
                    if (!Convert.IsDBNull(Look["color_srl"]))
                        color_srl2 = Convert.ToInt32(Look["color_srl"]);
                    int carpet_type2 = 0;
                    if (!Convert.IsDBNull(Look["carpet_type"]))
                        carpet_type2 = Convert.ToInt32(Look["carpet_type"]);
                    long sale_price = 0;
                    if (!Convert.IsDBNull(Look["sale_price"]))
                        sale_price = Convert.ToInt64(Look["sale_price"]);

                    foreach(DataRow Pick in dt_supcusts_goods.Rows)
                    {
                        if (Convert.ToInt32(Look["igd_srl"]) == Convert.ToInt32(Pick["igd_srl"]))
                        {
                            insert2(Convert.ToInt32(Pick["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Pick["srl"]));
                        }
                    }

                    if ((city_srl > 0) && (size_srl > 0) && (color_srl > 0) && (carpet_type > 0) && (from_price > 0))
                    {
                        if((city_srl == city_srl2) && (size_srl == size_srl2) && (color_srl == color_srl2) && (carpet_type == carpet_type2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl > 0) && (color_srl > 0) && (carpet_type > 0) && (from_price == 0))
                    {
                        if ((city_srl == city_srl2) && (size_srl == size_srl2) && (color_srl == color_srl2) && (carpet_type == carpet_type2))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl > 0) && (color_srl > 0) && (carpet_type == 0) && (from_price > 0))
                    {
                        if ((city_srl == city_srl2) && (size_srl == size_srl2) && (color_srl == color_srl2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl > 0) && (color_srl == 0) && (carpet_type > 0) && (from_price > 0))
                    {
                        if ((city_srl == city_srl2) && (size_srl == size_srl2) && (carpet_type == carpet_type2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1,  Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl == 0) && (color_srl > 0) && (carpet_type > 0) && (from_price > 0))
                    {
                        if ((city_srl == city_srl2) && (color_srl == color_srl2) && (carpet_type == carpet_type2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl > 0) && (color_srl > 0) && (carpet_type > 0) && (from_price > 0))
                    {
                        if ((size_srl == size_srl2) && (color_srl == color_srl2) && (carpet_type == carpet_type2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    //3
                    if ((city_srl > 0) && (size_srl > 0) && (color_srl > 0) && (carpet_type == 0) && (from_price == 0))
                    {
                        if ((city_srl == city_srl2) && (size_srl == size_srl2) && (color_srl == color_srl2))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl > 0) && (color_srl == 0) && (carpet_type == 0) && (from_price > 0))
                    {
                        if ((city_srl == city_srl2) && (size_srl == size_srl2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl == 0) && (color_srl == 0) && (carpet_type > 0) && (from_price > 0))
                    {
                        if ((city_srl == city_srl2) && (carpet_type == carpet_type2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl == 0) && (color_srl > 0) && (carpet_type > 0) && (from_price > 0))
                    {
                        if ((color_srl == color_srl2) && (carpet_type == carpet_type2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl > 0) && (color_srl == 0) && (carpet_type == 0) && (from_price == 0))
                    {
                        if ((city_srl == city_srl2) && (size_srl == size_srl2))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl == 0) && (color_srl > 0) && (carpet_type == 0) && (from_price == 0))
                    {
                        if ((city_srl == city_srl2) && (color_srl == color_srl2))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl == 0) && (color_srl == 0) && (carpet_type > 0) && (from_price == 0))
                    {
                        if ((city_srl == city_srl2) && (carpet_type == carpet_type2))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl == 0) && (color_srl == 0) && (carpet_type == 0) && (from_price > 0))
                    {
                        if ((city_srl == city_srl2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl > 0) && (color_srl > 0) && (carpet_type > 0) && (from_price == 0))
                    {
                        if ((size_srl == size_srl2) && (color_srl == color_srl2) && (carpet_type == carpet_type2))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl > 0) && (color_srl > 0) && (carpet_type == 0) && (from_price > 0))
                    {
                        if ((size_srl == size_srl2) && (color_srl == color_srl2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl > 0) && (color_srl == 0) && (carpet_type > 0) && (from_price > 0))
                    {
                        if ((size_srl == size_srl2)  && (carpet_type == carpet_type2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl == 0) && (color_srl > 0) && (carpet_type > 0) && (from_price == 0))
                    {
                        if ((size_srl == size_srl2) && (color_srl == color_srl2) && (carpet_type == carpet_type2))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl > 0) && (color_srl == 0) && (carpet_type == 0) && (from_price > 0))
                    {
                        if ((city_srl == city_srl2) && (size_srl == size_srl2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl > 0) && (color_srl > 0) && (carpet_type == 0) && (from_price == 0))
                    {
                        if ((size_srl == size_srl2) && (color_srl == color_srl2))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    else if ((city_srl == 0) && (size_srl > 0) && (color_srl == 0) && (carpet_type > 0) && (from_price == 0))
                    {
                        if ((size_srl == size_srl2) && (carpet_type == carpet_type2))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl > 0) && (color_srl == 0) && (carpet_type == 0) && (from_price > 0))
                    {
                        if ((size_srl == size_srl2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl == 0) && (color_srl > 0) && (carpet_type > 0) && (from_price == 0))
                    {
                        if ((color_srl == color_srl2) && (carpet_type == carpet_type2))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl == 0) && (color_srl > 0) && (carpet_type == 0) && (from_price > 0))
                    {
                        if ((color_srl == color_srl2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl == 0) && (color_srl == 0) && (carpet_type > 0) && (from_price > 0))
                    {
                        if ((carpet_type == carpet_type2) && ((from_price <= sale_price) && (to_price >= sale_price)))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl > 0) && (size_srl == 0) && (color_srl == 0) && (carpet_type == 0) && (from_price == 0))
                    {
                        if (city_srl == city_srl2)
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl > 0) && (color_srl == 0) && (carpet_type == 0) && (from_price == 0))
                    {
                        if (size_srl == size_srl2)
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl == 0) && (color_srl > 0) && (carpet_type == 0) && (from_price == 0))
                    {
                        if (color_srl == color_srl2)
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl == 0) && (color_srl == 0) && (carpet_type > 0) && (from_price == 0))
                    {
                        if (carpet_type == carpet_type2)
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                    if ((city_srl == 0) && (size_srl == 0) && (color_srl == 0) && (carpet_type == 0) && (from_price > 0))
                    {
                        if ((from_price <= sale_price) && (to_price >= sale_price))
                        {
                            insert(Convert.ToInt32(Woak["bassc_srl"]), Convert.ToInt32(Look["srl"]), 1, Convert.ToInt32(Look["header_srl"]), Convert.ToInt32(Woak["srl"]));
                            continue;
                        }
                    }
                }
            }
            ClientScript.RegisterStartupScript(typeof(Page), "a key", "<script type=\"text/javascript\">" + jquery + "</script>");
        }
        private void insert(int bassc_srl,int srl_h,int opportunity_srl,int header_srl,int srl_order)
        {
            if (!Duplicate_bas_project(header_srl,srl_order))
            {
                Common ubobo = new Common();
                int srl = max_srl();
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@srl", SqlDbType.Int);
                param[0].Value = srl;
                param[1] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
                param[1].Value = ubobo.persian_date();
                param[2] = new SqlParameter("@header_srl", SqlDbType.Int);
                param[2].Value = srl_h;
                param[3] = new SqlParameter("@bassc_srl", SqlDbType.Int);
                param[3].Value = bassc_srl;
                param[4] = new SqlParameter("@opportunity_srl", SqlDbType.Int);
                param[4].Value = opportunity_srl;
                new ManageCommands(param, "insert_project_details");

                SqlConnection con = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update bas_supcust_order set project_srl=@project_srl where srl=@srl;";
                cmd.Parameters.Add("@srl", SqlDbType.Int).Value = srl_order;
                cmd.Parameters.Add("@project_srl", SqlDbType.Int).Value = header_srl;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private void insert2(int bassc_srl, int srl_h, int opportunity_srl, int header_srl, int srl_order)
        {
            if (!Duplicate_bas_project2(header_srl, srl_order))
            {
                Common ubobo = new Common();
                int srl = max_srl();
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@srl", SqlDbType.Int);
                param[0].Value = srl;
                param[1] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
                param[1].Value = ubobo.persian_date();
                param[2] = new SqlParameter("@header_srl", SqlDbType.Int);
                param[2].Value = srl_h;
                param[3] = new SqlParameter("@bassc_srl", SqlDbType.Int);
                param[3].Value = bassc_srl;
                param[4] = new SqlParameter("@opportunity_srl", SqlDbType.Int);
                param[4].Value = opportunity_srl;
                new ManageCommands(param, "insert_project_details");

                SqlConnection con = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update bas_supcust_goods set project_srl=@project_srl where srl=@srl;";
                cmd.Parameters.Add("@srl", SqlDbType.Int).Value = srl_order;
                cmd.Parameters.Add("@project_srl", SqlDbType.Int).Value = header_srl;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private bool Duplicate_bas_project(int header_srl, int srl_order)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select project_srl From bas_supcust_order where srl=" + srl_order;
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!Convert.IsDBNull(dr[0]))
                {
                    if(Convert.ToInt32(dr[0]) == header_srl)
                    {
                        dr.Close(); dr.Dispose();
                        return true;
                    }
                    dr.Close(); dr.Dispose();
                    return false;
                }
                else
                {
                    dr.Close(); dr.Dispose();
                    return false;
                }
            }
            else
            {
                dr.Close(); dr.Dispose();
                return false;
            }
        }
        private bool Duplicate_bas_project2(int header_srl, int srl_order)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select project_srl From bas_supcust_goods where srl=" + srl_order;
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!Convert.IsDBNull(dr[0]))
                {
                    if (Convert.ToInt32(dr[0]) == header_srl)
                    {
                        dr.Close(); dr.Dispose();
                        return true;
                    }
                    dr.Close(); dr.Dispose();
                    return false;
                }
                else
                {
                    dr.Close(); dr.Dispose();
                    return false;
                }
            }
            else
            {
                dr.Close(); dr.Dispose();
                return false;
            }
        }
        private int max_srl()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.bas_project_details";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!Convert.IsDBNull(dr[0]))
                {
                    ViewState["srl"] = dr[0];
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