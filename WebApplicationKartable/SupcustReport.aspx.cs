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

        protected void btn_excel_buyers_Click(object sender, EventArgs e)
        {
            Search obj = new Search(strConnString);
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
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
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
            HttpContext.Current.Response.Write("آخرین فاکتور");
            HttpContext.Current.Response.Write("</Td>");

            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("قیمت فروش");
            HttpContext.Current.Response.Write("</Td>");

            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تخفیف مدیریتی");
            HttpContext.Current.Response.Write("</Td>");

            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تخفیف نمایشگاهی");
            HttpContext.Current.Response.Write("</Td>");

            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تخفیف");
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
    }
}