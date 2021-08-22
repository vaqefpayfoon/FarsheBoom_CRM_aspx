using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebApplicationKartable
{
    public partial class Supcust : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        Common obj = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                if (Request.QueryString["srl"] != "-1")
                    set_boxes(Request.QueryString["srl"].ToString());
                else
                    txt_u_date_time.Text = obj.persian_date();
                ViewState["update"] = "0"; ViewState["update2"] = "0";
            }
        }
        private void set_boxes(string srl)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, u_date_time, full_name, tel1, cell_phone, instagram, address1, describtion FROM dbo.bas_supcust WHERE srl=" + srl);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ViewState["srl"] = row["srl"].ToString();
                txt_u_date_time.Text = row["u_date_time"].ToString();
                txt_full_name.Text = row["full_name"].ToString();
                ViewState["full_name"] = row["full_name"].ToString();
                txt_tel1.Text = row["tel1"].ToString();
                ViewState["tel1"] = row["tel1"].ToString();
                txt_cell_phone.Text = row["cell_phone"].ToString();
                ViewState["cell_phone"] = row["cell_phone"].ToString();
                txt_address.Text = row["address1"].ToString();
                txt_desc.Text = row["describtion"].ToString();
            }
        }
        protected void btn_save_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.QueryString["srl"] == "-1")
                insert();
            else
                update();
        }
        private void insert()
        {
            if (duplicate_supcust_tel1())
            {
                lblError.Text = "تلفن مشتری تکراری است";
                return;
            }
            if (duplicate_supcust_cell_phone())
            {
                //lblError.Text = "تلفن همراه مشتری تکراری است";
                //return;
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl FROM dbo.bas_supcust where cell_phone='" + txt_cell_phone.Text + "'");
                if (dt.Rows.Count > 0)
                    Response.Redirect("Supcust.aspx?srl=" + dt.Rows[0][0]);
            }
            int srl = max_srl();
            if (lblError.Text == "تامین کننده جدید ایجاد شد")
                return;
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[1].Value = obj.persian_date();
            param[2] = new SqlParameter("@full_name", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_full_name.Text))
                param[2].Value = txt_full_name.Text;
            else
                param[2].Value = DBNull.Value;
            param[3] = new SqlParameter("@city_srl", SqlDbType.Int);
            param[3].Value = DBNull.Value;
            param[4] = new SqlParameter("@meet_srl", SqlDbType.Int);
            param[4].Value = 1;
            param[5] = new SqlParameter("@meet_locate", SqlDbType.VarChar, 50);
            param[5].Value = DBNull.Value;
            param[6] = new SqlParameter("@tel1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel1.Text))
                param[6].Value = txt_tel1.Text;
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@cell_phone", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_cell_phone.Text))
                param[7].Value = txt_cell_phone.Text;
            else
                param[7].Value = DBNull.Value;
            param[8] = new SqlParameter("@instagram", SqlDbType.VarChar, 50);
            param[8].Value = DBNull.Value;
            param[9] = new SqlParameter("@sex", SqlDbType.Char, 1);
            param[9].Value = '1';
            param[10] = new SqlParameter("@age", SqlDbType.Char, 3);
            param[10].Value = '1';
            param[11] = new SqlParameter("@user_name", SqlDbType.VarChar, 10);
            param[11].Value = DBNull.Value;
            param[12] = new SqlParameter("@pass", SqlDbType.VarChar, 10);
            param[12].Value = DBNull.Value;
            param[13] = new SqlParameter("@address1", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_address.Text))
                param[13].Value = txt_address.Text;
            else
                param[13].Value = DBNull.Value;
            param[14] = new SqlParameter("@email", SqlDbType.VarChar, 100);
            param[14].Value = DBNull.Value;
            param[15] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_desc.Text))
                param[15].Value = txt_desc.Text;
            else
                param[15].Value = DBNull.Value;
            param[16] = new SqlParameter("@clue_srl", SqlDbType.Int);
            param[16].Value = 1;
            new ManageCommands(param, "insert_supcust");
            lblError.Text = "مشتری جدید ایجاد شد";
            emptyboxex();
        }
        private void update()
        {
            if (duplicate_supcust_tel1_update())
            {
                lblError.Text = "تلفن مشتری تکراری است";
                return;
            }
            if (duplicate_supcust_cell_phone_update())
            {
                lblError.Text = "تلفن همراه مشتری تکراری است";
                return;
            }
            Common per = new Common();
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ViewState["srl"]);
            param[1] = new SqlParameter("@full_name", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_full_name.Text))
                param[1].Value = txt_full_name.Text;
            else
                param[1].Value = DBNull.Value;
            param[2] = new SqlParameter("@city_srl", SqlDbType.Int);
            param[2].Value = DBNull.Value;
            param[3] = new SqlParameter("@meet_srl", SqlDbType.Int);
            param[3].Value = 1;
            param[4] = new SqlParameter("@meet_locate", SqlDbType.VarChar, 50);
            param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@tel1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel1.Text))
                param[5].Value = txt_tel1.Text;
            else
                param[5].Value = DBNull.Value;
            param[6] = new SqlParameter("@cell_phone", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_cell_phone.Text))
                param[6].Value = txt_cell_phone.Text;
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@instagram", SqlDbType.VarChar, 50);
            param[7].Value = DBNull.Value;
            param[8] = new SqlParameter("@sex", SqlDbType.Char, 1);
            param[8].Value = '1';
            param[9] = new SqlParameter("@age", SqlDbType.Char, 3);
            param[9].Value = '1';
            param[10] = new SqlParameter("@user_name", SqlDbType.VarChar, 10);
            param[10].Value = DBNull.Value;
            param[11] = new SqlParameter("@pass", SqlDbType.VarChar, 10);
            param[11].Value = DBNull.Value;
            param[12] = new SqlParameter("@address1", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_address.Text))
                param[12].Value = txt_address.Text;
            else
                param[12].Value = DBNull.Value;
            param[13] = new SqlParameter("@email", SqlDbType.VarChar, 100);
            param[13].Value = DBNull.Value;
            param[14] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_desc.Text))
                param[14].Value = txt_desc.Text;
            else
                param[14].Value = DBNull.Value;
            param[15] = new SqlParameter("@clue_srl", SqlDbType.Int);
            param[15].Value = 1;
            new ManageCommands(param, "update_supcust");
            lblError.Text = "مشتری جاری ویرایش شد";
        }
        private int max_srl()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.bas_supcust";
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
        private void CheckLogin()
        {
            if (Request.Cookies["myCookie"] != null)
            {
                if (Request.Cookies["myCookie"]["srl"] != null)
                    ubuzhi.srl = Request.Cookies["myCookie"]["srl"].ToString();
                else
                    Response.Redirect("login.aspx");
                if (Request.Cookies["myCookie"]["first_name"] != null)
                    ubuzhi.first_name = Request.Cookies["myCookie"]["first_name"].ToString();
                else
                    Response.Redirect("login.aspx");
                if (Request.Cookies["myCookie"]["last_name"] != null)
                    ubuzhi.last_name = Request.Cookies["myCookie"]["last_name"].ToString();
                else
                    Response.Redirect("login.aspx");
                if (Request.Cookies["myCookie"]["group_srl"] != null)
                    ubuzhi.group_srl = Request.Cookies["myCookie"]["group_srl"].ToString();
                else
                    Response.Redirect("login.aspx");
            }
        }
        private void emptyboxex()
        {
            txt_full_name.Text = string.Empty;
            txt_tel1.Text = string.Empty;
            txt_cell_phone.Text = string.Empty;
            txt_address.Text = string.Empty;
            txt_desc.Text = string.Empty;
        }

        private bool duplicate_supcust_name()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_name('" + txt_full_name.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0)
                {
                    dr.Close(); dr.Dispose();
                    return true;
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
        private bool duplicate_supcust_name_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_name('" + txt_full_name.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_full_name.Text != ViewState["full_name"].ToString())
                {
                    dr.Close(); dr.Dispose();
                    return true;
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
        private bool duplicate_supcust_tel1()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_tel1('" + txt_tel1.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0)
                {
                    dr.Close(); dr.Dispose();
                    return true;
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
        private bool duplicate_supcust_tel1_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_tel1('" + txt_tel1.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_tel1.Text != ViewState["tel1"].ToString())
                {
                    dr.Close(); dr.Dispose();
                    return true;
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
        private bool duplicate_supcust_cell_phone()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_cellphone('" + txt_cell_phone.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0)
                {
                    dr.Close(); dr.Dispose();
                    return true;
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
        private bool duplicate_supcust_cell_phone_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_cellphone('" + txt_cell_phone.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_cell_phone.Text != ViewState["cell_phone"].ToString())
                {
                    dr.Close(); dr.Dispose();
                    return true;
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
    }
}