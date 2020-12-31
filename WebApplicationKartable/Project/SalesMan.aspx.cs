using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;

namespace WebApplicationKartable
{
    public partial class SalesMan : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
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
        protected void btn_report_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);Common obo = new WebApplicationKartable.Common();
            dt = obj.Get_Data(string.Format("SELECT code_igd, provider_code, brand_name, size_title, carpet_title, porz_title, chele_title, lenght, widht, ROUND(lenght * widht / 10000, 2) AS area, color_srl2, plan_title, color_name,discount, discount_amount, sale_price FROM dbo.Project_Goods_View Where header_srl={0}", lst_project.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                DataSet1.CallBackDataTable Temp = new DataSet1.CallBackDataTable();
                foreach (DataRow Woak in dt.Rows)
                {
                    DataRow row = Temp.NewRow();
                    row["code_igd"] = Woak["code_igd"];
                    row["provider_code"] = Woak["provider_code"];
                    row["brand_name"] = Woak["brand_name"];
                    row["size_title"] = Woak["size_title"];
                    row["carpet_title"] = Woak["carpet_title"];
                    row["porz_title"] = Woak["porz_title"];
                    row["chele_title"] = Woak["chele_title"];
                    row["lenght"] = Woak["lenght"];
                    row["widht"] = Woak["widht"];
                    row["area"] = Woak["area"];
                    row["margin_color"] = color(Woak["color_srl2"].ToString());
                    row["plan_title"] = Woak["plan_title"];
                    row["color_name"] = Woak["color_name"];
                    row["discount"] = Woak["discount"];
                    obo.str = Woak["discount_amount"].ToString();
                    row["discount_amount"] = obo.str;
                    obo.str = Woak["sale_price"].ToString();
                    row["sale_price"] = obo.str;
                    if(!Convert.IsDBNull(row["sale_price"]) && !Convert.IsDBNull(row["discount_amount"]))
                    {
                        double final_price = Convert.ToDouble(row["sale_price"]) - Convert.ToDouble(row["discount_amount"]);
                        obo.str = final_price.ToString();
                        row["final_price"] = obo.str;
                    }
                    Temp.Rows.Add(row);
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SalesMan.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Temp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                ReportParameter parameter_Date = new ReportParameter("Date", new Common().persian_date());
                ReportParameter ProjectName = new ReportParameter("ProjectName", lst_project.SelectedItem.Text);
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.SetParameters(parameter_Date);
                ReportViewer1.LocalReport.SetParameters(ProjectName);
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