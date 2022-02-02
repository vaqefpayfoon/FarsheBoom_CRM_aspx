using Cartable;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationKartable
{
    public partial class SupcustReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();

        protected void btn_excel_Click(object sender, EventArgs e)
        {
            DataTable table = ViewState["dt"] as DataTable;
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
            HttpContext.Current.Response.Write("تاریخ");
            HttpContext.Current.Response.Write("</Td>");
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
            HttpContext.Current.Response.Write("وضعیت مراجعه");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("وضعیت خرید");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 1; i < table.Columns.Count; i++)
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
        protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview.PageIndex = e.NewPageIndex;
            fill_grid(lst_status.SelectedIndex);
        }
        private void fill_grid(int state)
        {
            Search obj = new Search(strConnString);
            DataTable dt;
            if (lst_status.SelectedIndex == 0)
            {
                dt = obj.Get_Data("SELECT srl, u_date_time, full_name, tel1, cell_phone, Case When sex = 1 Then 'مراجعه کرده است' Else 'مراجعه نکرده است' End As isRefrence, Case When Age = 1 Then 'خرید کرده است' Else 'خرید نکرده است' End As isBuyer FROM dbo.bas_supcust order by srl desc");

            }
            else if (lst_status.SelectedIndex == 1)
            {
                dt = obj.Get_Data("SELECT srl, u_date_time, full_name, tel1, cell_phone, Case When sex = 1 Then 'مراجعه کرده است' Else 'مراجعه نکرده است' End As isRefrence, Case When Age = 1 Then 'خرید کرده است' Else 'خرید نکرده است' End As isBuyer FROM dbo.bas_supcust where sex=1 order by srl desc");
            }
            else
            {
                dt = obj.Get_Data("SELECT srl, u_date_time, full_name, tel1, cell_phone, Case When sex = 1 Then 'مراجعه کرده است' Else 'مراجعه نکرده است' End As isRefrence, Case When Age = 1 Then 'خرید کرده است' Else 'خرید نکرده است' End As isBuyer FROM dbo.bas_supcust where age=1 order by srl desc");
            }
            ViewState["dt"] = dt;
            //if (dt.Rows.Count > 0)
            {
                gridview.DataSource = dt;
                gridview.DataBind();
            }
        }
        protected void btn_report_Click(object sender, EventArgs e)
        {
            fill_grid(lst_status.SelectedIndex);
        }
    }
}