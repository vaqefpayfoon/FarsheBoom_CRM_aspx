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
                // fill_grid();
                security();
            }
        }
        private void security()
        {
            switch(ubuzhi.group_srl)
            {
                case "1":
                    {
                        lnk_firm.Enabled = true;
                        box_supcust.Enabled = true;
                        box_provider.Enabled = true;
                        box_alarm.Enabled = true;
                        lnk_alarm.Enabled = true;
                        lnk_supcust_excel.Enabled = true;                        
                    }
                    break;
                case "2":
                    {
                        box_supcust.Enabled = true;
                        box_alarm.Enabled = true;
                        box_opportunity.Enabled = true;
                        box_provider.Enabled = true;
                        lnk_brands.Enabled = true;
                        lnk_carpet.Enabled = true;
                        lnk_chele.Enabled = true;
                        lnk_colors.Enabled = true;
                        lnk_bank.Enabled = true;
                        lnk_factor.Enabled = true;
                        lnk_firm.Enabled = true;
                        lnk_opportunity_sale.Enabled = true;
                        lnk_porz.Enabled = true;
                        lnk_project.Enabled = true;
                        lnk_reject.Enabled = true;                        
                        lnk_Sale_price.Enabled = true;
                        lnk_sizes.Enabled = true;
                        lnk_supcust.Enabled = true;
                        lnk_supcust_report.Enabled = true;
                        lnk_alarm.Enabled = true;
                        lnk_plan.Enabled = true;
                        lnk_supcust_excel.Enabled = true;
                        lnk_all_goods.Enabled = true;
                        lnk_label.Enabled = true;
                        lnk_main_callback.Enabled = true;
                        lnk_complete.Enabled = true;
                        lnk_settelment.Enabled = true;
                        lnk_remain_carpet.Enabled = true;
                        lnk_factorreport.Enabled = true;
                        lnk_customers.Enabled = true;
                        lnk_barcode_report.Enabled = true;
                        lnk_Survey.Enabled = true;
                    }
                    break;
                case "3":
                    {
                        box_supcust.Enabled = true;
                        box_alarm.Enabled = true;
                        box_opportunity.Enabled = true;
                        box_provider.Enabled = true;                        
                        lnk_brands.Enabled = true;
                        lnk_carpet.Enabled = true;
                        lnk_chele.Enabled = true;
                        lnk_colors.Enabled = true;
                        lnk_bank.Enabled = true;
                        lnk_factor.Enabled = true;
                        lnk_firm.Enabled = true;
                        lnk_opportunity_sale.Enabled = true;
                        lnk_personal.Enabled = true;
                        lnk_porz.Enabled = true;
                        lnk_project.Enabled = true;
                        lnk_reject.Enabled = true;
                        lnk_sale.Enabled = true;
                        lnk_Sale_price.Enabled = true;
                        lnk_sizes.Enabled = true;
                        lnk_supcust.Enabled = true;
                        lnk_supcust_report.Enabled = true;
                        lnk_alarm.Enabled = true;
                        lnk_plan.Enabled = true;
                        lnk_supcust_excel.Enabled = true;
                        lnk_all_goods.Enabled = true;
                        lnk_label.Enabled = true;
                        lnk_main_callback.Enabled = true;
                        lnk_complete.Enabled = true;
                        lnk_settelment.Enabled = true;
                        lnk_remain_carpet.Enabled = true;
                        lnk_factorreport.Enabled = true;
                        lnk_customers.Enabled = true;
                        lnk_barcode_report.Enabled = true;
                        lnk_Survey.Enabled = true;
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
        private string one_day_earlier()
        {
            DateTime dt = pc.AddDays(DateTime.Now, -1);
            string year = pc.GetYear(dt).ToString();
            string month = pc.GetMonth(dt).ToString("d2");
            string day = pc.GetDayOfMonth(dt).ToString("d2");
            return string.Concat(year, "/", month, "/", day);
        }
        private string one_day_later()
        {
            DateTime dt = pc.AddDays(DateTime.Now, 1);
            string year = pc.GetYear(dt).ToString();
            string month = pc.GetMonth(dt).ToString("d2");
            string day = pc.GetDayOfMonth(dt).ToString("d2");
            return string.Concat(year, "/", month, "/", day);
        }
        private string persian_date()
        {
            DateTime dt2 = DateTime.Now;
            string year1 = pc.GetYear(dt2).ToString();
            string month1 = pc.GetMonth(dt2).ToString("d2");
            string day1 = pc.GetDayOfMonth(dt2).ToString("d2");
            return string.Concat(year1, "/", month1, "/", day1);
        }
        private void check_requests()
        {
            if (Request.QueryString["snd"] != null)
            {
                SqlConnection con = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = "update ltr_transfer_details set read_l=@read_l where srl=@srl";
                cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["snd"]);
                cmd.Parameters.Add("@read_l", SqlDbType.Bit).Value = true;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else if (Request.QueryString["snd2"] != null)
            {
                SqlConnection con = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = "update ltr_transfer_details set archive=@archive where srl=@srl";
                cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["snd2"]);
                cmd.Parameters.Add("@archive", SqlDbType.Bit).Value = true;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
       
        private int provider_srl(string name)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.bas_provider where provider_name='" + name + "' ");
            int srl = 0;
            if (dt.Rows.Count > 0)
                srl = Convert.ToInt32(dt.Rows[0][0]);
            return srl;
        }
        private int goods_srl(string name)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.inv_goods where code_igd='" + name + "' ");
            int srl = 0;
            if (dt.Rows.Count > 0)
                srl = Convert.ToInt32(dt.Rows[0][0]);
            return srl;
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

            dt = obj.Get_Data(@"SELECT dbo.bas_supcust.full_name, dbo.bas_supcust.tel1, dbo.bas_supcust.cell_phone, COUNT(dbo.acc_factor.srl) AS carpetCount, MAX(dbo.acc_factor.u_date_tome) AS u_date_tome, SUM(dbo.acc_factor.payment) AS payment FROM dbo.bas_supcust INNER JOIN dbo.acc_factor ON dbo.bas_supcust.srl = dbo.acc_factor.bassc_srl WHERE(dbo.acc_factor.u_date_tome <> '') GROUP BY dbo.bas_supcust.full_name, dbo.acc_factor.u_date_tome, dbo.bas_supcust.tel1, dbo.bas_supcust.cell_phone, dbo.acc_factor.payment)");
            ExporttoExcelSupcust(dt);
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
        private void ExporttoExcelSupcust(DataTable table)
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
            HttpContext.Current.Response.Write("تلفن");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("موبایل");
            HttpContext.Current.Response.Write("</Td>");

            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تعداد فرش");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("آخرین فاکتور");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("جمع مبلغ فروش");
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