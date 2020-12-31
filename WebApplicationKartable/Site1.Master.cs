using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Configuration;
using System.Globalization;

namespace WebApplicationKartable
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public string str;
        private LoginInfo ubuzhi = new LoginInfo();
        private PersianCalendar pc = new PersianCalendar();
        private Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fill_grid();
                security();
            }
        }
        private void fill_grid()
        {
            CheckLogin();
            StringBuilder sb_personal = new StringBuilder();
            StringBuilder sb_online = new StringBuilder();
            StringBuilder sb_dropdown_menu = new StringBuilder();
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
        private void security()
        {
            switch (ubuzhi.group_srl)
            {
                case "1":
                    {
                        lnk_firm.Enabled = true;
                        lnk_alarm.Enabled = true;
                        lnk_supcust_excel.Enabled = true;
                    }
                    break;
                case "2":
                    {
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
    }
}