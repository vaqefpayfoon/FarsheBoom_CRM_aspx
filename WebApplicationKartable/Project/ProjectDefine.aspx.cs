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
    public partial class ProjectDefine : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo(); private Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                if (Request.QueryString["srl"] != "-1")
                    set_boxes(Request.QueryString["srl"].ToString());
                fill_grid();
            }
        }
        private void set_boxes(string srl)
        {
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl, from_date,to_date,project_name,describtion,confirm,locate_srl,project_code FROM dbo.bas_project WHERE srl=" + srl);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ViewState["srl"] = row["srl"].ToString();
                txt_project_name.Text = row["project_name"].ToString();
                ViewState["project_name"] = row["project_name"].ToString();
                txt_project_code.Text = row["project_code"].ToString();
                txt_from_date.Text = row["from_date"].ToString();
                txt_to_date.Text = row["to_date"].ToString();
                txt_desc.Text = row["describtion"].ToString();
                if (!Convert.IsDBNull(row["confirm"]))
                    chk_confirm.Checked = Convert.ToBoolean(row["confirm"]);
                //if (!Convert.IsDBNull(row["locate_srl"]))
                //    lst_locate.SelectedValue = row["locate_srl"].ToString();
                //else
                //    lst_locate.SelectedIndex = 0;
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
            int srl = max_srl();
            if (lblError.Text == "نمایشگاه جدید ایجاد شد")
                return;
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@from_date", SqlDbType.Char, 10);
            param[1].Value = txt_from_date.Text;
            param[2] = new SqlParameter("@to_date", SqlDbType.Char, 10);
            param[2].Value = txt_to_date.Text;
            param[3] = new SqlParameter("@project_name", SqlDbType.VarChar, 100);
            param[3].Value = txt_project_name.Text;
            param[4] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_desc.Text))
                param[4].Value = txt_desc.Text;
            else
                param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@confirm", SqlDbType.Bit);
            param[5].Value = chk_confirm.Checked;
            param[6] = new SqlParameter("@locate_srl", SqlDbType.TinyInt);
            //if (lst_locate.SelectedIndex > 0)
            //    param[6].Value = Convert.ToInt32(lst_locate.SelectedValue);
            //else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@project_code", SqlDbType.VarChar, 20);
            param[7].Value = txt_project_code.Text;
            new ManageCommands(param, "insert_project");
            lblError.Text = "نمایشگاه جدید ایجاد شد";
        }
        private void update()
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ViewState["srl"]);
            param[1] = new SqlParameter("@from_date", SqlDbType.Char, 10);
            param[1].Value = txt_from_date.Text;
            param[2] = new SqlParameter("@to_date", SqlDbType.Char, 10);
            param[2].Value = txt_to_date.Text;
            param[3] = new SqlParameter("@project_name", SqlDbType.VarChar, 100);
            param[3].Value = txt_project_name.Text;
            param[4] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_desc.Text))
                param[4].Value = txt_desc.Text;
            else
                param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@confirm", SqlDbType.Bit);
            param[5].Value = chk_confirm.Checked;
            param[6] = new SqlParameter("@locate_srl", SqlDbType.TinyInt);
            //if (lst_locate.SelectedIndex > 0)
            //    param[6].Value = Convert.ToInt32(lst_locate.SelectedValue);
            //else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@project_code", SqlDbType.VarChar, 20);
            param[7].Value = txt_project_code.Text;
            new ManageCommands(param, "update_project");
            lblError.Text = "نمایشگاه جاری ویرایش شد";
        }
        private int max_srl()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.bas_project";
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
        private int max_srl_details()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.bas_project_goods";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!Convert.IsDBNull(dr[0]))
                {
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
        private void fill_grid()
        {
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT provider_srl, provider_name, code_igd FROM dbo.Provider_Goods where (selection is null) or (selection = 'False') order by srl desc");
        }
        private int provider_srl(string name)
        {
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl FROM dbo.bas_provider where provider_name='" + name + "' ");
            int srl = 0;
            if (dt.Rows.Count > 0)
                srl = Convert.ToInt32(dt.Rows[0][0]);
            return srl;
        }
        private int goods_srl(string name)
        {
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl FROM dbo.Provider_Goods where code_igd='" + name + "' ");
            int srl = 0;
            if (dt.Rows.Count > 0)
                srl = Convert.ToInt32(dt.Rows[0][0]);
            return srl;
        }
        private int goods_srl2(string name)
        {
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl FROM dbo.Provider_Goods where code_igd='" + name + "' ");
            int srl = 0;
            if (dt.Rows.Count > 0)
                srl = Convert.ToInt32(dt.Rows[0][0]);
            return srl;
        }
        private bool Duplicate_bas_project()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_bas_project('" + txt_project_name.Text + "')";
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
        private bool Duplicate_GoodsInproject(string igd, string header_srl)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_GoodsInproject(" + igd + "," + header_srl + ")";
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
        private bool Duplicate_bas_project_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_bas_project('" + txt_project_name.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_project_name.Text != ViewState["project_name"].ToString())
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
        private void deleteAllAssigns()
        {
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT dbo.bas_project_goods.igd_srl FROM dbo.bas_project_goods INNER JOIN dbo.inv_goods ON dbo.bas_project_goods.igd_srl = dbo.inv_goods.srl Where (selection='True') And ((sold is null) or (sold='False')) And (dbo.bas_project_goods.header_srl =" + ViewState["srl"] + ")");
            foreach (DataRow row in dt.Rows)
            {
                if (true)
                {
                    try
                    {
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@header_srl", SqlDbType.Int);
                        param[0].Value = Convert.ToInt32(ViewState["srl"]);
                        param[1] = new SqlParameter("@igd_srl", SqlDbType.Int);
                        param[1].Value = Convert.ToInt32(row["code_igd"]);
                        new ManageCommands(param, "del_project_goods");
                        SqlConnection con = new SqlConnection(strConnString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update inv_goods set selection=@selection where code_igd=@code_igd;";
                        cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(row["code_igd"]);
                        cmd.Parameters.Add("@selection", SqlDbType.Bit).Value = false;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch { lblError.Text = "عملیات ناموفق ، برای این فرش فاکتور صادر شده است"; continue; }
                }
            }
        }
        protected void btn_add_Click(object sender, EventArgs e)
        {
            lblAdd.Text = string.Empty;
            bool bln = false;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string txt = txt_enter_codes.Text;
            string[] lst = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string Woak in lst)
            {
                int igd_srl = goods_srl(Woak);
                int srl = max_srl_details();
                if (igd_srl > 0)
                {
                    if (!Duplicate_GoodsInproject(igd_srl.ToString(), ViewState["srl"].ToString()))
                    {
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@srl", SqlDbType.Int);
                        param[0].Value = srl;
                        param[1] = new SqlParameter("@header_srl", SqlDbType.Int);
                        param[1].Value = Convert.ToInt32(ViewState["srl"]);
                        param[2] = new SqlParameter("@igd_srl", SqlDbType.Int);
                        param[2].Value = Convert.ToInt32(igd_srl);
                        new ManageCommands(param, "insert_project_goods");
                        SqlConnection con = new SqlConnection(strConnString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update inv_goods set selection=@selection where srl=@srl;";
                        cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(igd_srl);
                        cmd.Parameters.Add("@selection", SqlDbType.Bit).Value = true;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        lblError.Text = " کد فرش " + igd_srl + " در همین نمایشگاه موجود است ";
                    }
                }
                else
                {
                    sb.Append(Woak);
                    sb.Append(" , ");
                    bln = true;
                }
            }
            if(bln)
            {
                sb.Append(" تخصیص نیافتند ");
                lblAdd.Text = sb.ToString();
            }
        }
        protected void btn_remove_Click(object sender, EventArgs e)
        {
            lblAdd.Text = string.Empty;
            bool bln = false;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string txt = txt_enter_codes.Text;
            string[] lst = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string Woak in lst)
            {
                int igd_srl = goods_srl2(Woak);
                int srl = max_srl_details();
                if (igd_srl > 0)
                {
                    //if (!Duplicate_GoodsInproject(igd_srl.ToString(), ViewState["srl"].ToString()))
                    {
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@header_srl", SqlDbType.Int);
                        param[0].Value = Convert.ToInt32(ViewState["srl"]);
                        param[1] = new SqlParameter("@igd_srl", SqlDbType.Int);
                        param[1].Value = Convert.ToInt32(igd_srl);
                        new ManageCommands(param, "del_project_goods");
                        SqlConnection con = new SqlConnection(strConnString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update inv_goods set selection=@selection where srl=@srl;";
                        cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(igd_srl);
                        cmd.Parameters.Add("@selection", SqlDbType.Bit).Value = false;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else
                {
                    sb.Append(Woak);
                    sb.Append(" , ");
                    bln = true;
                }
            }
            if (bln)
            {
                sb.Append(" حذف نشدند ");
                lblAdd.Text = sb.ToString();
            }
        }
        protected void btn_remove_all_Click(object sender, EventArgs e)
        {
            deleteAllAssigns();
        }
    }
}