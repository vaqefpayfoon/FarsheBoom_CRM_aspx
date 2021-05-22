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
                        DataTable dt = obj.Get_Data("SELECT srl, provider_name, related_person, tel1, cell_phone FROM dbo.bas_provider order by srl desc");
                        literal_report.Text = rpt.provider_list(dt);
                    }
                    break;
                case "supcust_list":
                    {
                        lbl_header.Text = "گزارش مشتری";
                        DataTable dt = obj.Get_Data("SELECT srl, u_date_time, full_name, tel1, cell_phone FROM dbo.bas_supcust order by srl desc");
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
                        DataTable dt = obj.Get_Data("SELECT srl, code_igd,provider_name, brand_name, size_title, provider_code, plan_title, porz_title, chele_title, color_name, build_state FROM AllProviderGoods Where build_state In (0, 3) order by srl desc");
                        literal_report.Text = rpt.AllProviderGoods(dt);
                    }
                    break;
                case "Buyers":
                    {
                        lbl_header.Text = "لیست مشتریان";
                        DataTable dt = obj.Get_Data("SELECT dbo.bas_supcust.srl, dbo.bas_supcust.full_name, dbo.bas_supcust.tel1, dbo.bas_supcust.cell_phone, COUNT(dbo.acc_factor.srl) AS carpetCount, MAX(dbo.acc_factor.u_date_tome) AS u_date_tome, SUM(dbo.acc_factor.payment) AS payment FROM dbo.bas_supcust INNER JOIN                          dbo.acc_factor ON dbo.bas_supcust.srl = dbo.acc_factor.bassc_srl WHERE(dbo.acc_factor.u_date_tome <> '') GROUP BY dbo.bas_supcust.full_name, dbo.acc_factor.u_date_tome, dbo.bas_supcust.tel1, dbo.bas_supcust.cell_phone, dbo.acc_factor.payment)");
                        literal_report.Text = rpt.buyers_list(dt);
                    }
                    break;
                case "Audience":
                    {
                        lbl_header.Text = "لیست مخاطبین";
                        DataTable dt = obj.Get_Data("SELECT srl, u_date_time, full_name, tel1, cell_phone FROM dbo.bas_supcust where srl not in (SELECT bassc_srl as srl FROM dbo.acc_factor)");
                        literal_report.Text = rpt.supcust_list(dt);
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