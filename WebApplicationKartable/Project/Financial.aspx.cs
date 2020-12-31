using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;

namespace WebApplicationKartable
{
    public partial class Financial : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
                GetReport();
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
        protected void GetReport()
        {
            DataTable dt = new DataTable(); dt = (DataTable)Session["Financial"];
            Session.Remove("Financial");
            if (dt.Rows.Count > 0)
            {
                Common obo = new Common();
                DataSet1.FinancialDataTable Temp = new DataSet1.FinancialDataTable();
                foreach (DataRow Woak in dt.Rows)
                {
                    DataRow row = Temp.NewRow();
                    row["u_date_tome"] = Woak["u_date_tome"];
                    row["code_igd"] = Woak["code_igd"];
                    row["provider_name"] = Woak["provider_name"];
                    row["size_title"] = Woak["size_title"];
                    row["brand_name"] = Woak["brand_name"];
                    row["area"] = Woak["area"];
                    row["factor_no"] = Woak["factor_no"];
                    obo.str = Woak["buy_price"].ToString();
                    row["buy_price"] = obo.str;
                    obo.str = Woak["sale_price"].ToString();
                    row["sale_price"] = obo.str;
                    row["discount"] = Woak["discount"];
                    obo.str = Woak["discount_amount"].ToString();
                    row["discount_amount"] = obo.str;
                    obo.str = Woak["final_sale"].ToString();
                    row["final_sale"] = obo.str;
                    obo.str = Woak["final_discount"].ToString();
                    row["final_discount"] = obo.str;
                    obo.str = Woak["final_price"].ToString();
                    row["final_price"] = obo.str;
                    obo.str = Woak["margin_profit"].ToString();
                    row["margin_profit"] = obo.str;
                    obo.str = Woak["final_profit2"].ToString();
                    row["final_profit"] = obo.str;
                    Temp.Rows.Add(row);
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Financial.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Temp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                ReportParameter parameter_Date = new ReportParameter("Date", new Common().persian_date());
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.SetParameters(parameter_Date);
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}