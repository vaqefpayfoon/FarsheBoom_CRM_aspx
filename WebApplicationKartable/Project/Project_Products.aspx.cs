using Cartable;
using System;
using System.Configuration;
using System.Data;

namespace WebApplicationKartable
{
    public partial class Project_Products : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
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
        protected void btn_report_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data(string.Format("SELECT dbo.inv_goods.code_igd FROM dbo.bas_project_goods INNER JOIN                         dbo.inv_goods ON dbo.bas_project_goods.igd_srl = dbo.inv_goods.srl WHERE dbo.inv_goods.provider_srl={0} AND dbo.bas_project_goods.header_srl={1} order by code_igd", lst_provider.SelectedValue, lst_project.SelectedValue));
            grid.DataSource = dt; grid.DataBind();
            lbl.Text = "تعداد کل " + dt.Rows.Count.ToString();
        }
    }
}