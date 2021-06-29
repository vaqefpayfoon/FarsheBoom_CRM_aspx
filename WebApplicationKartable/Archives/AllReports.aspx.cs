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
                        string str = @"SELECT        dbo.inv_goods.srl, dbo.inv_goods.title_igd, dbo.inv_brand.brand_name, dbo.inv_size.size_title, dbo.inv_goods.u_date_time, dbo.inv_goods.provider_srl, dbo.inv_goods.code_igd, dbo.bas_provider.provider_name, 
                         dbo.inv_goods.selection, dbo.inv_goods.sold, dbo.inv_goods.ibt_srl, dbo.inv_goods.size_srl, dbo.inv_color.color_name, dbo.inv_porz.porz_title, dbo.inv_chele.chele_title, dbo.inv_plan.plan_title, dbo.inv_carpet.carpet_title, 
                         dbo.inv_goods.sale_price, dbo.inv_goods.widht, dbo.inv_goods.lenght, dbo.inv_goods.buy_price, dbo.inv_goods.discount, dbo.inv_goods.color_srl2, dbo.inv_goods.provider_code, dbo.inv_goods.build_state, 
                         CASE WHEN build_state = 0 THEN 'فرش های موجود' WHEN build_state = 1 THEN N'فروش توسط تامین کننده' WHEN build_state = 2 THEN 'غیر فعال' WHEN build_state = 3 THEN 'آماده نیست' WHEN build_state = 5 THEN 'فرش مرجوعی' WHEN
                          sold = 'True' THEN 'فروش توسط فرش بوم' ELSE 'موجود نزد تامین کننده' END AS build_state_exp
FROM            dbo.inv_goods LEFT OUTER JOIN
                         dbo.bas_provider ON dbo.inv_goods.provider_srl = dbo.bas_provider.srl LEFT OUTER JOIN
                         dbo.inv_carpet ON dbo.inv_goods.carpet_type = dbo.inv_carpet.srl LEFT OUTER JOIN
                         dbo.inv_chele ON dbo.inv_goods.chele_type = dbo.inv_chele.srl LEFT OUTER JOIN
                         dbo.inv_porz ON dbo.inv_goods.porz_type = dbo.inv_porz.srl LEFT OUTER JOIN
                         dbo.inv_plan ON dbo.inv_goods.city_srl = dbo.inv_plan.srl LEFT OUTER JOIN
                         dbo.inv_color ON dbo.inv_goods.color_srl = dbo.inv_color.srl LEFT OUTER JOIN
                         dbo.inv_size ON dbo.inv_goods.size_srl = dbo.inv_size.srl LEFT OUTER JOIN
                         dbo.inv_brand ON dbo.inv_goods.ibt_srl = dbo.inv_brand.srl
Where build_state In (0, 3) order by srl desc";
                        lbl_header.Text = "لیست تمام فرش ها";
                        DataTable dt = obj.Get_Data(str);
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