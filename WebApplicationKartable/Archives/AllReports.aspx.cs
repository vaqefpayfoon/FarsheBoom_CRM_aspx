using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Configuration;

namespace WebApplicationKartable
{
    public partial class AllReports : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            Report rpt = new Report();
            Search obj = new Search(strConnString);
            switch(Request.QueryString["snd"])
            {
                case "personels_list":
                    {
                        lbl_header.Text = "گزارش پرسنل ها";
                        DataTable dt = obj.Get_Data("SELECT  first_name + ' ' + last_name AS full_name, no_hpl, mobile_no, tel_no, address_hpl FROM dbo.hpl_personal");
                        literal_report.Text = rpt.personels_list(dt);
                    }
                    break;
                case "provider_list":
                    {
                        lbl_header.Text = "گزارش تامین کننده ها";
                        DataTable dt = obj.Get_Data("SELECT srl, provider_name, related_person, tel1, cell_phone FROM dbo.bas_provider");
                        literal_report.Text = rpt.provider_list(dt);
                    }
                    break;
                case "supcust_list":
                    {
                        lbl_header.Text = "گزارش مشتری";
                        DataTable dt = obj.Get_Data("SELECT dbo.bas_supcust.srl, dbo.bas_supcust.full_name, dbo.bas_supcust.u_date_time, dbo.bas_supcust.cell_phone, dbo.bas_sale_clue.meet_title, dbo.bas_supcust.describtion FROM dbo.bas_sale_clue INNER JOIN dbo.bas_supcust ON dbo.bas_sale_clue.srl = dbo.bas_supcust.clue_srl");
                        literal_report.Text = rpt.supcust_list(dt);
                    }
                    break;
                case "project_list":
                    {
                        lbl_header.Text = "فرش های نمایشگاه جاری";
                        DataTable dt = obj.Get_Data("SELECT code_igd, size_title, city_name, carpet_title, color_name ,sale_price FROM dbo.Project_Goods_View Where header_srl=" + Request.QueryString["srl"]);
                        literal_report.Text = rpt.project_list(dt);
                    }
                    break;
                case "AllProviderGoods":
                    {
                        lbl_header.Text = "لیست تمام فرش ها";
                        DataTable dt = obj.Get_Data("SELECT srl, code_igd,provider_name, brand_name, size_title, provider_code, plan_title, porz_title, chele_title, color_name, build_state FROM AllProviderGoods");
                        literal_report.Text = rpt.AllProviderGoods(dt);
                    }
                    break;
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