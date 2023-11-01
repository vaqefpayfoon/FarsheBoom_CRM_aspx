using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.IO;

namespace WebApplicationKartable
{
    public partial class HomePage : System.Web.UI.Page
    {
        public string str;
        private LoginInfo ubuzhi = new LoginInfo();
        private Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private PersianCalendar pc = new PersianCalendar();
        protected void Page_Load(object sender, EventArgs e)
        {           
            if(!IsPostBack)
            {
                fill_grid();
                security();
            }
        }
        private void security()
        {
            if(ubuzhi.group_srl == null)
            {

            }
            switch(ubuzhi.group_srl)
            {
                case "1":
                    {
                        lnk_firm.Enabled = true;
                        lnk_supcustReport.Enabled = true;
                        lnk_supcust.Enabled = true;
                    }
                    break;
                case "2":
                    {
                        lnk_brands.Enabled = true;
                        lnk_carpet.Enabled = true;
                        lnk_chele.Enabled = true;
                        lnk_colors.Enabled = true;
                        lnk_bank.Enabled = true;
                        lnk_factor.Enabled = true;
                        lnk_firm.Enabled = true;
                        //lnk_opportunity_sale.Enabled = true;
                        lnk_porz.Enabled = true;
                        lnk_project.Enabled = true;
                        lnk_reject.Enabled = true;                        
                        lnk_Sale_price.Enabled = true;
                        lnk_sizes.Enabled = true;
                        lnk_supcust.Enabled = true;
                        //lnk_supcust_report.Enabled = true;
                        lnk_plan.Enabled = true;
                        lnk_supcustReport.Enabled = true;
                        lnk_all_goods.Enabled = true;
                        //lnk_label.Enabled = true;
                        lnk_main_callback.Enabled = true;
                        lnk_complete.Enabled = true;
                        lnk_settelment.Enabled = true;
                        lnk_remain_carpet.Enabled = true;
                        lnk_customers.Enabled = true;
                        lnk_Survey.Enabled = true;

                        lnk_supcust_buyers.Enabled = true;
                        //lnk_supcust_audience.Enabled = true;
                        //lnk_audience_excel.Enabled = true;
                    }
                    break;
                case "3":
                    {                      
                        lnk_brands.Enabled = true;
                        lnk_carpet.Enabled = true;
                        lnk_chele.Enabled = true;
                        lnk_colors.Enabled = true;
                        lnk_bank.Enabled = true;
                        lnk_factor.Enabled = true;
                        lnk_firm.Enabled = true;
                        //lnk_opportunity_sale.Enabled = true;
                        lnk_personal.Enabled = true;
                        lnk_porz.Enabled = true;
                        lnk_project.Enabled = true;
                        lnk_reject.Enabled = true;
                        lnk_sale.Enabled = true;
                        lnk_Sale_price.Enabled = true;
                        lnk_sizes.Enabled = true;
                        lnk_supcust.Enabled = true;
                        //lnk_supcust_report.Enabled = true;
                        lnk_plan.Enabled = true;
                        lnk_supcustReport.Enabled = true;
                        lnk_all_goods.Enabled = true;
                        //lnk_label.Enabled = true;
                        lnk_main_callback.Enabled = true;
                        lnk_complete.Enabled = true;
                        lnk_settelment.Enabled = true;
                        lnk_remain_carpet.Enabled = true;
                        lnk_customers.Enabled = true;
                        lnk_Survey.Enabled = true;

                        lnk_supcust_buyers.Enabled = true;
                        //lnk_supcust_audience.Enabled = true;
                        //lnk_audience_excel.Enabled = true;
                    }
                    break;
                default:
                    {
                        box_provider.Visible = false;
                        box_opportunity.Visible = false;
                        box_alarm.Visible = false;
                        box_supcust.Visible = false;
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
        private void fill_grid()
        {
            CheckLogin();
            StringBuilder sb_personal = new StringBuilder();
            StringBuilder sb_online = new StringBuilder();
            StringBuilder sb_dropdown_menu = new StringBuilder();
            Search obj = new Search(strConnString);
            DataTable dt_personal = new DataTable();
            dt_personal = obj.Get_Data("SELECT first_name + ' ' + last_name AS full_name, image_hpl FROM dbo.hpl_personal WHERE srl=" + ubuzhi.srl);
            if (dt_personal.Rows.Count > 0)
            {
                DataRow row = dt_personal.Rows[0];
                if (!Convert.IsDBNull(row["image_hpl"]))
                {
                    sb_personal.Append("<a href='#' class='dropdown-toggle' data-toggle='dropdown'><i class='glyphicon glyphicon-user'></i><span>" + row["full_name"] + "<i class='caret'></i></span></a><ul class='dropdown-menu'><li class='user-header bg-light-blue'><img src='" + row["image_hpl"] + "' class='img-circle' /><p>" + row["full_name"] + "</p></li><li class='user-body'><div class='col-xs-6 text-center'><a href='#' name='logout' runat='server' >ارزیابی</a></div></li><li class='user-footer'><div class='pull-left'><a href='BaseInformation/Edit_Profile.aspx' class='btn btn-default btn-flat'>مشخصات فردی</a></div><div class='pull-right'><a href='../login.aspx?snd=-1' class='btn btn-default btn-flat' name='logout'>خروج</a></div></li></ul>");
                    sb_online.Append("<div class='pull-left image'><img src=" + row["image_hpl"] + " class='img-circle' /></div><div class='pull-left info'>");
                }
                else
                {
                    sb_dropdown_menu.Append("<img src='img/person.png' class='img-circle'/>");
                }
                if (row["full_name"] != null)
                {
                    sb_online.Append("<p>" + row["full_name"] + "</p><i class='fa fa-circle text-success'></i> Online</div>");
                }
                literal_personal.Text = sb_personal.ToString();
                literal_online.Text = sb_online.ToString();
            }
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT provider_name FROM dbo.bas_provider where provider_name like '%'+ @SearchText + '%'", prefixText, count);
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch2(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT code_igd FROM dbo.inv_goods where ((sold is null) or (sold='False')) AND selection='True' AND code_igd like '%'+ @SearchText + '%'", prefixText, count);
        }

        protected void lnkExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            
                dt = obj.Get_Data(@"SELECT [code_igd]
                                  ,[provider_code]
                                  ,[sale_price]
                                  ,[size_title]
                                  ,[discount]
                                  ,[lenght]
                                  ,[widht]
                                  ,[area]
                              FROM[94_farsheboom].[94_vaq].[excel]");
            ExporttoExcel2(dt);
        }
        protected void lnkExcel_supcust_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);

            //dt = obj.Get_Data(@"SELECT srl, full_name, cell_phone, carpetCount, u_date_tome, sale_price, manager_discount, event_discount, discount, payment FROM [94_vaq].Buyers");
            ExporttoExcelSupcust();
        }
        protected void lnkExcel_audience_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);

            dt = obj.Get_Data(@"SELECT full_name, u_date_time, tel1, cell_phone FROM dbo.bas_supcust where srl not in (SELECT bassc_srl as srl FROM dbo.acc_factor)");
            ExporttoExcelAudience(dt);
        }
        private void ExporttoExcel2(DataTable table)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Write("<font style='font-size:14.0pt; font-family:B Nazanin;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:14.0pt; font-family:B Nazanin; background:white;'> <TR>");
            int columnscount = table.Columns.Count;
            HttpContext.Current.Response.Write("</TR>");
            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("کد تامین کننده");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("قیمت فروش");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("اندازه");
            HttpContext.Current.Response.Write("</Td>");

            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تخفیف");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("طول");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("عرض");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("مساحت");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        private void ExporttoExcelSupcust()
        {
            DataTable dt = obj.Get_Data(@"SELECT dbo.bas_supcust.srl, dbo.bas_supcust.full_name, dbo.bas_supcust.cell_phone, COUNT(dbo.acc_factor.srl) AS carpetCount, MAX(dbo.acc_factor.u_date_tome)
                          AS u_date_tome, SUM(dbo.inv_goods.sale_price) AS sale_price, SUM(CASE WHEN (dbo.acc_factor.discount - ISNULL(dbo.inv_goods.discount, 0) * ISNULL(dbo.inv_goods.sale_price, 0) / 100) < 0 THEN 0 ELSE (dbo.acc_factor.discount - ISNULL(dbo.inv_goods.discount, 0) 
                         * ISNULL(dbo.inv_goods.sale_price, 0) / 100) END) AS manager_discount, SUM(ISNULL(dbo.inv_goods.discount, 0) * ISNULL(dbo.inv_goods.sale_price, 0) / 100) AS event_discount, SUM(dbo.acc_factor.discount) AS discount, 
                         SUM(dbo.inv_goods.sale_price - dbo.acc_factor.discount) AS payment
FROM dbo.bas_supcust INNER JOIN
                         dbo.acc_factor ON dbo.bas_supcust.srl = dbo.acc_factor.bassc_srl INNER JOIN
                         dbo.inv_goods ON dbo.inv_goods.srl = dbo.acc_factor.igd_srl
WHERE        (dbo.acc_factor.u_date_tome <> '')
GROUP BY dbo.bas_supcust.srl, dbo.bas_supcust.full_name, dbo.bas_supcust.cell_phone
ORDER BY u_date_tome DESC");
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
            //HttpContext.Current.Response.Charset = "UTF-8";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1256");
            HttpContext.Current.Response.Charset = "windows-1256";
            HttpContext.Current.Response.Write("<font style='font-size:14.0pt; font-family:B Nazanin;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:14.0pt; font-family:B Nazanin; background:white;'> <TR>");
            int columnscount = dt.Columns.Count;
            HttpContext.Current.Response.Write("</TR>");
            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("نام");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("همراه");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تعداد فرش");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("آخرين فاکتور");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("قيمت فروش");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تخفيف مديريت");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تخفيف نمايـشگاه");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تخفـيف");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("قابل پرداخت");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in dt.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        private void ExporttoExcelAudience(DataTable table)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Write("<font style='font-size:14.0pt; font-family:B Nazanin;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:14.0pt; font-family:B Nazanin; background:white;'> <TR>");
            int columnscount = table.Columns.Count;
            HttpContext.Current.Response.Write("</TR>");
            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("نام و نام خانوادگی");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تاریخ ورود به سیستم");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تلفن");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("موبایل");
            HttpContext.Current.Response.Write("</Td>");   
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}