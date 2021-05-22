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
    public partial class Provider_Managment : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl, provider_name, related_person FROM dbo.bas_provider");
                grid.DataSource = dt; grid.DataBind();

                BindData();
            }
        }
        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, provider_name, related_person FROM dbo.bas_provider");
            grid.DataSource = dt; grid.DataBind();
            mp1.Show();
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT provider_name FROM dbo.bas_provider where provider_name like '%'+ @SearchText + '%'", prefixText, count);
        }
        protected void CustomersGridView_SelectedIndexChanging(Object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = grid.Rows[e.NewSelectedIndex];
            txtContactsSearch.Text = row.Cells[1].Text.ToString();
            txt_code.Text = row.Cells[0].Text.ToString();
        }
        protected void add_new_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Provider.aspx?srl=-1");
        }
        protected void edit_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.bas_provider where provider_name='" + txtContactsSearch.Text + "'");
            if (dt.Rows.Count > 0)
                Response.Redirect("Provider.aspx?srl=" + dt.Rows[0][0]);
        }
        protected void delete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl FROM dbo.bas_provider where provider_name='" + txtContactsSearch.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@srl", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(dt.Rows[0]["srl"]);
                    new ManageCommands(param, "del_provider");
                    lblError.Text = "تامین کننده جاری حذف گردید";
                }
            }
            catch { lblError.Text = "عملیات ناموفق"; }
        }
        protected void report_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Archives/AllReports.aspx?snd=provider_list");
        }
        private void BindData()
        {
            string strQuery = "SELECT srl, provider_name, provider_code, tel1, fax1, cell_phone FROM dbo.bas_provider";
            SqlCommand cmd = new SqlCommand(strQuery);
            GridView1.DataSource = GetData(cmd);
            GridView1.DataBind();
        }
        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            if (!(dt.Rows.Count > 0))
            {
                DataRow row = dt.NewRow();
                row["srl"] = 0;
                row["provider_name"] = "";
                row["provider_code"] = "";
                row["tel1"] = "";
                row["fax1"] = "";
                row["cell_phone"] = "";
                dt.Rows.Add(row);
            }
            return dt;
        }
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
        protected void AddNewCustomer(object sender, EventArgs e)
        {
            int srl = max_srl();
            string provider_name = ((TextBox)GridView1.FooterRow.FindControl("txt_provider_name_add")).Text;
            string provider_code = ((TextBox)GridView1.FooterRow.FindControl("txt_provider_code_add")).Text;
            string tel1 = ((TextBox)GridView1.FooterRow.FindControl("txt_tel1_add")).Text;
            string fax1 = ((TextBox)GridView1.FooterRow.FindControl("txt_fax1_add")).Text;
            string cell_phone = ((TextBox)GridView1.FooterRow.FindControl("txt_cell_phone_add")).Text;
            if (duplicate_provider_name(provider_name))
            {
                lblError.Text = "نام تامین کننده تکراری است";
                return;
            }
            if (duplicate_provider_tel1(tel1))
            {
                lblError.Text = "تلفن تامین کننده تکراری است";
                return;
            }
            if (duplicate_provider_cell_phone(cell_phone))
            {
                lblError.Text = "تلفن همراه تامین کننده تکراری است";
                return;
            }

            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into bas_provider(srl, provider_name, provider_code, tel1, fax1, cell_phone) " +
            "values(@srl, @provider_name, @provider_code, @tel1, @fax1, @cell_phone);" +
             "SELECT srl, provider_name, provider_code, tel1, fax1, cell_phone FROM dbo.bas_provider";
            cmd.Parameters.Add("@srl", SqlDbType.Int).Value = srl;
            cmd.Parameters.Add("@provider_name", SqlDbType.VarChar, 50).Value = provider_name;
            cmd.Parameters.Add("@provider_code", SqlDbType.VarChar, 50).Value = provider_code;
            cmd.Parameters.Add("@tel1", SqlDbType.VarChar, 12).Value = tel1;
            cmd.Parameters.Add("@fax1", SqlDbType.VarChar, 12).Value = fax1;
            cmd.Parameters.Add("@cell_phone", SqlDbType.VarChar, 12).Value = cell_phone;
            GridView1.DataSource = GetData(cmd);
            GridView1.DataBind();
        }
        protected void DeleteCustomer(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkRemove = (LinkButton)sender;
                SqlConnection con = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from  bas_provider where " +
                "srl=@srl;" +
                "SELECT srl, provider_name, provider_code, tel1, fax1, cell_phone FROM dbo.bas_provider";
                cmd.Parameters.Add("@srl", SqlDbType.Int).Value = lnkRemove.CommandArgument;
                GridView1.DataSource = GetData(cmd);
                GridView1.DataBind();
            }
            catch { }
        }
        protected void EditCustomer(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindData();
        }
        protected void UpdateCustomer(object sender, GridViewUpdateEventArgs e)
        {
            string srl = ((Label)GridView1.Rows[e.RowIndex].FindControl("lbl_srl")).Text;
            string provider_name = ((TextBox)GridView1.FooterRow.FindControl("txt_provider_name_add")).Text;
            string provider_code = ((TextBox)GridView1.FooterRow.FindControl("txt_provider_code_add")).Text;
            string tel1 = ((TextBox)GridView1.FooterRow.FindControl("txt_tel1_add")).Text;
            string fax1 = ((TextBox)GridView1.FooterRow.FindControl("txt_fax1_add")).Text;
            string cell_phone = ((TextBox)GridView1.FooterRow.FindControl("txt_cell_phone_add")).Text;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update bas_provider set provider_name=@provider_name, provider_code=@provider_code, tel1=@tel1, fax1=@fax1, cell_phone=@cell_phone " +
             "where srl=@srl;" +
             "SELECT srl, provider_name, provider_code, tel1, fax1, cell_phone FROM dbo.bas_provider";
            cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(srl);
            cmd.Parameters.Add("@provider_name", SqlDbType.VarChar, 50).Value = provider_name;
            cmd.Parameters.Add("@provider_code", SqlDbType.VarChar, 50).Value = provider_code;
            cmd.Parameters.Add("@tel1", SqlDbType.VarChar, 12).Value = tel1;
            cmd.Parameters.Add("@fax1", SqlDbType.VarChar, 12).Value = fax1;
            cmd.Parameters.Add("@cell_phone", SqlDbType.VarChar, 12).Value = cell_phone;
            GridView1.EditIndex = -1;
            GridView1.DataSource = GetData(cmd);
            GridView1.DataBind();
        }
        private void CheckLogin()
        {
            if (Request.Cookies["myCookie"] != null)
            {
                if (Request.Cookies["myCookie"]["srl"] != null)
                    ubuzhi.srl = Request.Cookies["myCookie"]["srl"];
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
                    ubuzhi.group_srl = Request.Cookies["myCookie"]["group_srl"];
                else
                    Response.Redirect("../login.aspx");
            }
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
        private bool duplicate_provider_name(string txt_provider_name)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_name('" + txt_provider_name + "')";
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
        private bool duplicate_provider_tel1(string txt_tel1)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_tel1('" + txt_tel1 + "')";
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
        private bool duplicate_provider_cell_phone(string txt_cell_phone)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_cellphone('" + txt_cell_phone + "')";
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

    }
}