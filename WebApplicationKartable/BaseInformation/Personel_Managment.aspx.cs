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
    public partial class Personel_Managment : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl, first_name, last_name FROM dbo.hpl_personal");
                grid.DataSource = dt; grid.DataBind();
            }
        }
        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, first_name, last_name FROM dbo.hpl_personal");
            grid.DataSource = dt; grid.DataBind();
            mp1.Show();
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT srl FROM dbo.hpl_personal where srl like '%'+ @SearchText + '%'", prefixText, count);
        }
        protected void CustomersGridView_SelectedIndexChanging(Object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = grid.Rows[e.NewSelectedIndex];
            txtContactsSearch.Text = row.Cells[2].Text.ToString();
            txt_code.Text = row.Cells[0].Text.ToString();
        }
        protected void add_new_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Personel_Definition.aspx?srl=-1");
        }
        protected void edit_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.hpl_personal where srl='" + txt_code.Text + "'");
            if (dt.Rows.Count > 0)
                Response.Redirect("Personel_Definition.aspx?srl=" + dt.Rows[0][0]);
        }
        protected void delete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl FROM dbo.hpl_personal where srl='" + txt_code.Text.Trim() + "'");
                if (dt.Rows.Count > 0)
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@srl", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(dt.Rows[0]["srl"]);
                    new ManageCommands(param, "del_personal");
                    lblError.Text = "پرسنل جاری حذف گردید";
                }
            }
            catch { lblError.Text = "عملیات ناموفق"; }
        }
        protected void report_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Archives/AllReports.aspx?snd=personels_list");
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
    }
}