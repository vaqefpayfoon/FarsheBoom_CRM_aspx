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
    public partial class FactorsManagment : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl, factor_no,u_date_tome FROM dbo.acc_factor order by factor_no desc");
                grid.DataSource = dt; grid.DataBind();
            }
        }
        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, factor_no,u_date_tome FROM dbo.acc_factor order by factor_no desc");
            grid.DataSource = dt; grid.DataBind();
            mp1.Show();
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT factor_no FROM dbo.acc_factor where factor_no like '%'+ @SearchText + '%' AND ((reject is null) or reject = 'false')", prefixText, count);
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch2(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT full_name FROM dbo.bas_supcust where full_name like '%'+ @SearchText + '%'", prefixText, count);
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch3(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT cell_phone FROM dbo.bas_supcust where cell_phone like '%'+ @SearchText + '%' AND ((reject is null) or reject = 'false')", prefixText, count);
        }
        protected void CustomersGridView_SelectedIndexChanging(Object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = grid.Rows[e.NewSelectedIndex];
            txtContactsSearch.Text = row.Cells[1].Text.ToString();
            txt_code.Text = row.Cells[0].Text.ToString();
        }
        protected void add_new_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Factor.aspx?snd=-1");
        }
        protected void edit_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            if (!string.IsNullOrEmpty(txtContactsSearch.Text))
            {
                dt = obj.Get_Data("SELECT srl FROM dbo.acc_factor where factor_no='" + txtContactsSearch.Text + "'");
                if (dt.Rows.Count > 0)
                    Response.Redirect("Factor.aspx?snd=" + dt.Rows[0][0]);
            }
            else if (!string.IsNullOrEmpty(txtContactsSearch2.Text))
            {
                dt = obj.Get_Data("SELECT srl FROM dbo.bas_supcust where full_name='" + txtContactsSearch2.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    DataTable dt2 = new DataTable();
                    dt2 = obj.Get_Data("SELECT srl FROM dbo.acc_factor where bassc_srl='" + dt.Rows[0][0] + "'");
                    if (dt2.Rows.Count > 0)
                        Response.Redirect("Factor.aspx?snd=" + dt2.Rows[0][0]);
                }
            }
            else if (!string.IsNullOrEmpty(txt_cell_phone.Text))
            {
                dt = obj.Get_Data("SELECT srl FROM dbo.bas_supcust where cell_phone='" + txt_cell_phone.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    DataTable dt2 = new DataTable();
                    dt2 = obj.Get_Data("SELECT srl FROM dbo.acc_factor where bassc_srl='" + dt.Rows[0][0] + "'");
                    if (dt2.Rows.Count > 0)
                        Response.Redirect("Factor.aspx?snd=" + dt2.Rows[0][0]);
                }
            }
        }
        private void CheckLogin()
        {
            if (Request.Cookies["myCookie"] != null)
            {
                if (Request.Cookies["myCookie"]["srl"] != null)
                    ubuzhi.srl = Request.Cookies["myCookie"]["srl"].ToString();
                else
                    Response.Redirect("../login.aspx");
                if (Request.Cookies["myCookie"]["first_name"] != null)
                    ubuzhi.first_name = Request.Cookies["myCookie"]["first_name"].ToString();
                else
                    Response.Redirect("../login.aspx");
                if (Request.Cookies["myCookie"]["last_name"] != null)
                    ubuzhi.last_name = Request.Cookies["myCookie"]["last_name"].ToString();
                else
                    Response.Redirect("../login.aspx");
                if (Request.Cookies["myCookie"]["group_srl"] != null)
                    ubuzhi.group_srl = Request.Cookies["myCookie"]["group_srl"].ToString();
                else
                    Response.Redirect("../login.aspx");
            }
        }
        protected void btn_cell_phone_finder_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.Factor_View where code_igd='" + txt_code_igd.Text + "'");
            if (dt.Rows.Count > 0)
                Response.Redirect("Factor.aspx?snd=" + dt.Rows[0][0]);
        }

        protected void btn_return_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl,igd_srl FROM dbo.acc_factor where factor_no='" + txtContactsSearch.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    //SqlParameter[] param = new SqlParameter[1];
                    //param[0] = new SqlParameter("@srl", SqlDbType.Int);
                    //param[0].Value = Convert.ToInt32(dt.Rows[0]["srl"]);
                    //new ManageCommands(param, "del_factor");
                    //lblError.Text = "فاکتور جاری حذف گردید";
                    SqlConnection con = new SqlConnection(strConnString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update inv_goods set sold=@sold,build_state=@build_state where srl=@srl;";
                    cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[0]["igd_srl"]); ;
                    cmd.Parameters.Add("@sold", SqlDbType.Bit).Value = false;
                    cmd.Parameters.Add("@build_state", SqlDbType.VarChar).Value = "5";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update acc_factor set reject=@reject where srl=@srl;";
                    cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[0]["srl"]); ;
                    cmd.Parameters.Add("@reject", SqlDbType.Bit).Value = true;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    lblError.Text = "عملیات موفق";
                }
            }
            catch { lblError.Text = "عملیات ناموفق"; }
        }

        protected void btn_delete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl,igd_srl FROM dbo.acc_factor where factor_no='" + txtContactsSearch.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@srl", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(dt.Rows[0]["srl"]);
                    new ManageCommands(param, "del_factor");
                    lblError.Text = "فاکتور جاری حذف گردید";
                }
            }
            catch { lblError.Text = "عملیات ناموفق"; }
        }

        protected void btn_cancelreturn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl,igd_srl FROM dbo.acc_factor where factor_no='" + txtContactsSearch.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    //SqlParameter[] param = new SqlParameter[1];
                    //param[0] = new SqlParameter("@srl", SqlDbType.Int);
                    //param[0].Value = Convert.ToInt32(dt.Rows[0]["srl"]);
                    //new ManageCommands(param, "del_factor");
                    //lblError.Text = "فاکتور جاری حذف گردید";
                    SqlConnection con = new SqlConnection(strConnString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update inv_goods set sold=@sold,build_state=@build_state where srl=@srl;";
                    cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[0]["igd_srl"]); ;
                    cmd.Parameters.Add("@sold", SqlDbType.Bit).Value = true;
                    cmd.Parameters.Add("@build_state", SqlDbType.VarChar).Value = "4";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update acc_factor set reject=@reject where srl=@srl;";
                    cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(dt.Rows[0]["srl"]); ;
                    cmd.Parameters.Add("@reject", SqlDbType.Bit).Value = false;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    lblError.Text = "عملیات موفق";
                }                
            }
            catch { lblError.Text = "عملیات ناموفق"; }
        }

        protected void btn_cancel_report_Click(object sender, EventArgs e)
        {

        }
    }
}