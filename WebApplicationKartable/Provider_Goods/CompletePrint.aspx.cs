using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace WebApplicationKartable
{
    public partial class CompletePrint : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                bring();
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
        protected void bring()
        {
            
            Common obo = new Common();
            List<GoodsClass> lstPricingClass = new List<GoodsClass>();
            if (Session["ThrowList"] == null) return;
            lstPricingClass = Session["ThrowList"] as List<GoodsClass>;
            Session.Remove("ThrowList");
            string provider_name = string.Empty;
            if (lstPricingClass.Count > 0)
            {
                DataSet1.CallBackDataTable Temp = new DataSet1.CallBackDataTable();
                foreach (GoodsClass Woak in lstPricingClass)
                {
                    DataRow row = Temp.NewRow();
                    row["code_igd"] = Woak.code_igd;
                    row["brand_name"] = Woak.brand_name;
                    row["size_title"] = Woak.size_title;
                    row["carpet_title"] = Woak.carpet_title;
                    row["porz_title"] = Woak.porz_title;
                    row["chele_title"] = Woak.chele_title;
                    row["lenght"] = Woak.lenght;
                    row["widht"] = Woak.widht;
                    row["area"] = Woak.area;
                    row["margin_color"] = Woak.margin_color;
                    row["plan_title"] = Woak.plan_title;
                    row["color_name"] = Woak.color_name;
                    row["provider_code"] = Woak.provider_code;
                    obo.str = Woak.buy_price;
                    row["buy_price"] = obo.str;
                    provider_name = Woak.provider_name;
                    row["has_pic"] = Woak.has_pic;
                    Temp.Rows.Add(row);
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Complete.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Temp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                ReportParameter parameter_Date = new ReportParameter("Date", new Common().persian_date());
                ReportParameter ProviderName = new ReportParameter("ProviderName", provider_name);
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.SetParameters(parameter_Date);
                ReportViewer1.LocalReport.SetParameters(ProviderName);
                ReportViewer1.LocalReport.Refresh();
            }
        }
        private string color(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data(string.Format("SELECT color_name FROM inv_color where srl={0}", str));
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return string.Empty;
        }
    }
}