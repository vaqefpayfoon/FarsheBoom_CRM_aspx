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
                fill_grid();
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
                        lnk_cities.Enabled = true;
                        lnk_clue.Enabled = true;
                        lnk_colors.Enabled = true;
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
                        lnk_locate.Enabled = true;
                        lnk_supcust_excel.Enabled = true;
                        lnk_all_goods.Enabled = true;
                        lnk_label.Enabled = true;
                        lnk_main_callback.Enabled = true;
                        lnk_raj.Enabled = true;
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
                        lnk_cities.Enabled = true;
                        lnk_clue.Enabled = true;
                        lnk_colors.Enabled = true;
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
                        lnk_locate.Enabled = true;
                        lnk_supcust_excel.Enabled = true;
                        lnk_all_goods.Enabled = true;
                        lnk_label.Enabled = true;
                        lnk_main_callback.Enabled = true;
                        lnk_raj.Enabled = true;
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
        protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("supcust/factor.aspx?srl=" + gridview.SelectedRow.Cells[0].Text.Trim());
        }
        protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview.PageIndex = e.NewPageIndex;
            fill_grid();
        }
        private void fill_grid()
        {
            CheckLogin();
            StringBuilder sb_personal = new StringBuilder();
            StringBuilder sb_online = new StringBuilder();
            StringBuilder sb_dropdown_menu = new StringBuilder();
            Search obj = new Search(strConnString); DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl, u_date_time, meet_title,full_name, header_srl, igd_srl, code_igd, provider_name, size_title,project_srl FROM  dbo.Project_Details_View order by project_srl desc");
            if (dt.Rows.Count > 0)
            {
                gridview.DataSource = dt;
                gridview.DataBind();
            }
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
                alarms();
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
        private void alarms()
        {
            StringBuilder sb_alarm = new StringBuilder();
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl,date_time, alarm_subject FROM dbo.hpl_alarm WHERE hpl_srl=" + ubuzhi.srl + " AND (date_time between '" + one_day_earlier() + "' AND '" + one_day_later() + "')");
            if (dt.Rows.Count > 0)
            {
                object sumObject;
                sumObject = dt.Compute("count(date_time)", "date_time IS NOT NULL");
                if (sumObject != null)
                    literal_alarm_count.Text = sumObject.ToString();

                foreach (DataRow Woak in dt.Rows)
                {
                    if (!Convert.IsDBNull(Woak["date_time"]))
                    {
                        sb_alarm.Append("<li><a href='../Alarm.aspx?srl=" + Woak["srl"] + "'>");
                        sb_alarm.Append(Woak["date_time"]);
                        sb_alarm.Append(" _ ");
                        sb_alarm.Append(Woak["alarm_subject"]);
                        sb_alarm.Append("</a></li>");
                    }
                }
                literal_alarm.Text = sb_alarm.ToString();
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
        protected void btn_filter_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            if (!string.IsNullOrEmpty(txtContactsSearch.Text))
            {
                int provider = provider_srl(txtContactsSearch.Text);
                dt = obj.Get_Data("SELECT srl, u_date_time, meet_title,full_name, header_srl, igd_srl, code_igd, provider_name, size_title,project_srl FROM  dbo.Project_Details_View Where (sold is null or sold = 'False') AND  ([confirm] is null or (confirm='False')) and  provider_srl=" + provider);
                gridview.DataSource = dt; gridview.DataBind();
            }
            else if (!string.IsNullOrEmpty(txt_product.Text))
            {
                int good = goods_srl(txt_product.Text);
                dt = obj.Get_Data("SELECT srl, u_date_time, meet_title,full_name, header_srl, igd_srl, code_igd, provider_name, size_title,project_srl FROM  dbo.Project_Details_View Where  (sold is null or sold = 'False') AND ([confirm] is null or (confirm='False')) and  igd_srl=" + good);
                gridview.DataSource = dt; gridview.DataBind();
            }
            else if (lst_city2.SelectedIndex > 0)
            {
                dt = obj.Get_Data("SELECT srl, u_date_time, meet_title,full_name, header_srl, igd_srl, code_igd, provider_name, size_title,project_srl FROM  dbo.Project_Details_View Where (sold is null or sold = 'False') AND ([confirm] is null or (confirm='False')) and ibt_srl=" + lst_city2.SelectedValue);
                gridview.DataSource = dt; gridview.DataBind();
            }
            else if (lst_size.SelectedIndex > 0)
            {
                dt = obj.Get_Data("SELECT srl, u_date_time, meet_title,full_name, header_srl, igd_srl, code_igd, provider_name, size_title,project_srl FROM  dbo.Project_Details_View Where (sold is null or sold = 'False') AND ([confirm] is null or (confirm='False')) and size_srl=" + lst_size.SelectedValue);
                gridview.DataSource = dt; gridview.DataBind();
            }
            else
            {
                dt = obj.Get_Data("SELECT srl, u_date_time, meet_title,full_name, header_srl, igd_srl, code_igd, provider_name, size_title,project_srl FROM  dbo.Project_Details_View Where (sold is null or sold = 'False') AND ([confirm] is null or (confirm='False'))");
                gridview.DataSource = dt; gridview.DataBind();
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
    }
}