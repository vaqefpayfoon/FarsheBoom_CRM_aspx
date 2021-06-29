using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;
using System.IO;

namespace WebApplicationKartable
{
    public partial class Provider : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                if (Request.QueryString["srl"] != "-1")
                    set_boxes(Request.QueryString["srl"].ToString());
            }            
        }
        private void set_boxes(string srl)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, u_date_time, provider_name, related_person, tel1, tel2, fax1, cell_phone, address1, type_srl, city_srl, describtion, provider_code FROM dbo.bas_provider WHERE srl=" + srl);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ViewState["srl"] = row["srl"].ToString();
                txt_provider_name.Text = row["provider_name"].ToString();
                ViewState["name"] = row["provider_name"].ToString();
                txt_related_person.Text = row["related_person"].ToString();
                txt_tel1.Text = row["tel1"].ToString();
                ViewState["tel1"] = row["tel1"].ToString();
                txt_tel2.Text = row["tel2"].ToString();
                txt_fax1.Text = row["fax1"].ToString();
                txt_cell_phone.Text = row["cell_phone"].ToString();
                ViewState["cell_phone"] = row["cell_phone"].ToString();
                txt_address.Text = row["address1"].ToString();
                lst_type.SelectedValue = row["type_srl"].ToString();
                txt_provider_code.Text = row["provider_code"].ToString();
                txt_desc.Text = row["describtion"].ToString();
            }
        }
        protected void btn_save_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.QueryString["srl"] == "-1")
            {
                insert();
            }
            else
                update();
        }
        private void insert()
        {
            if (duplicate_provider_name())
            {
                lblError.Text = "نام تامین کننده تکراری است";
                return;
            }
            if (txt_tel1.Text != "" && duplicate_provider_tel1())
            {
                lblError.Text = "تلفن تامین کننده تکراری است";
                return;
            }
            if (txt_cell_phone.Text != "" && duplicate_provider_cell_phone())
            {
                lblError.Text = "تلفن همراه تامین کننده تکراری است";
                return;
            }
            int srl = max_srl();
            if (lblError.Text == "تامین کننده جدید ایجاد شد")
                return;
            ViewState["srl"] = srl;
            ViewState["tel1"] = txt_tel1.Text;
            ViewState["cell_phone"] = txt_cell_phone.Text;
            Common per = new Common();
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@u_date_time", SqlDbType.Char,10);
            param[1].Value = per.persian_date();
            param[2] = new SqlParameter("@provider_name", SqlDbType.VarChar, 100);
            param[2].Value = txt_provider_name.Text;
            param[3] = new SqlParameter("@related_person", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_related_person.Text))
                param[3].Value = txt_related_person.Text;
            else
                param[3].Value = DBNull.Value;
            param[4] = new SqlParameter("@tel1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel1.Text))
                param[4].Value = txt_tel1.Text;
            else
                param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@tel2", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel2.Text))
                param[5].Value = txt_tel2.Text;
            else
                param[5].Value = DBNull.Value;
            param[6] = new SqlParameter("@fax1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_fax1.Text))
                param[6].Value = txt_fax1.Text;
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@cell_phone", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_cell_phone.Text))
                param[7].Value = txt_cell_phone.Text;
            else
                param[7].Value = DBNull.Value;
            param[8] = new SqlParameter("@address1", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_address.Text))
                param[8].Value = txt_address.Text;
            else
                param[8].Value = DBNull.Value;
            param[9] = new SqlParameter("@type_srl", SqlDbType.TinyInt);
            param[9].Value = Convert.ToInt32(lst_type.SelectedValue);
            param[10] = new SqlParameter("@city_srl", SqlDbType.TinyInt);
            param[10].Value = 1; 
            param[11] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_desc.Text))
                param[11].Value = txt_desc.Text;
            else
                param[11].Value = DBNull.Value;
            param[12] = new SqlParameter("@provider_code", SqlDbType.VarChar,30);
            param[12].Value = txt_provider_code.Text;
            new ManageCommands(param, "insert_provider");
            lblError.Text = "تامین کننده جدید ایجاد شد";
        }
        private void update()
        {
            if (duplicate_provider_name_update())
            {
                lblError.Text = "نام تامین کننده تکراری است";
                return;
            }
            if (txt_tel1.Text != "" && duplicate_provider_tel1_update())
            {
                lblError.Text = "تلفن تامین کننده تکراری است";
                return;
            }
            if (txt_cell_phone.Text != "" && duplicate_provider_cell_phone_update())
            {
                lblError.Text = "تلفن همراه تامین کننده تکراری است";
                return;
            }
            ViewState["tel1"] = txt_tel1.Text;
            ViewState["cell_phone"] = txt_cell_phone.Text;
            Common per = new Common();
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ViewState["srl"]);
            param[1] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[1].Value = per.persian_date();
            param[2] = new SqlParameter("@provider_name", SqlDbType.VarChar, 100);
            param[2].Value = txt_provider_name.Text;
            param[3] = new SqlParameter("@related_person", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_related_person.Text))
                param[3].Value = txt_related_person.Text;
            else
                param[3].Value = DBNull.Value;
            param[4] = new SqlParameter("@tel1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel1.Text))
                param[4].Value = txt_tel1.Text;
            else
                param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@tel2", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel2.Text))
                param[5].Value = txt_tel2.Text;
            else
                param[5].Value = DBNull.Value;
            param[6] = new SqlParameter("@fax1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_fax1.Text))
                param[6].Value = txt_fax1.Text;
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@cell_phone", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_cell_phone.Text))
                param[7].Value = txt_cell_phone.Text;
            else
                param[7].Value = DBNull.Value;
            param[8] = new SqlParameter("@address1", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_address.Text))
                param[8].Value = txt_address.Text;
            else
                param[8].Value = DBNull.Value;
            param[9] = new SqlParameter("@type_srl", SqlDbType.TinyInt);
            param[9].Value = Convert.ToInt32(lst_type.SelectedValue);
            param[10] = new SqlParameter("@city_srl", SqlDbType.TinyInt);
            param[10].Value = 1;
            param[11] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_desc.Text))
                param[11].Value = txt_desc.Text;
            else
                param[11].Value = DBNull.Value;
            param[12] = new SqlParameter("@provider_code", SqlDbType.VarChar, 30);
            param[12].Value = txt_provider_code.Text;
            new ManageCommands(param, "update_provider");
            lblError.Text = "تامین کننده جاری ویرایش شد";
        }
        private int max_srl()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.bas_provider";
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
        private bool duplicate_provider_name()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_name('" + txt_provider_name.Text + "')";
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
        private bool duplicate_provider_name_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_name('" + txt_provider_name.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_provider_name.Text != ViewState["name"].ToString())
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
        private bool duplicate_provider_tel1()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_tel1('" + txt_tel1.Text + "')";
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
        private bool duplicate_provider_tel1_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_tel1('" + txt_tel1.Text + "')";
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
        private bool duplicate_provider_cell_phone()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_cellphone('" + txt_cell_phone.Text + "')";
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
        private bool duplicate_provider_cell_phone_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_cellphone('" + txt_cell_phone.Text + "')";
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
    }
}