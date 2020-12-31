using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;

namespace WebApplicationKartable
{
    public partial class LabelGenerator : System.Web.UI.Page
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
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            if (lst_sort.SelectedValue == "0")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where header_srl=" + lst_project.SelectedValue);
            else if (lst_sort.SelectedValue == "1")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where discount = 5 and header_srl=" + lst_project.SelectedValue);
            else if(lst_sort.SelectedValue == "2")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where discount > 5 and header_srl=" + lst_project.SelectedValue);
            else if (lst_sort.SelectedValue == "3")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where (discount < 5 Or discount is null) and header_srl=" + lst_project.SelectedValue);
            else if (lst_sort.SelectedValue == "brand_name,size_title")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where header_srl=" + lst_project.SelectedValue + " Order By brand_name,size_title");
            else if (lst_sort.SelectedValue == "size_title,brand_name")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where header_srl=" + lst_project.SelectedValue + " Order By size_title,brand_name");
            if (dt.Rows.Count > 0)
            {
                Common obj1 = new Common();
                DataSet1.LableDataTable Temp = new DataSet1.LableDataTable();
                foreach (DataRow Woak in dt.Rows)
                {
                    DataRow row = Temp.NewRow();
                    row["code_igd"] = Woak["code_igd"];
                    row["provider_code"] = Woak["provider_code"];
                    obj1.str = Woak["sale_price"].ToString();
                    obj1.str = Woak["sale_price"].ToString();
                    if (Woak["discount"].ToString().Equals("0"))
                        row["discount"] = string.Empty;
                    else
                        row["discount"] = Woak["discount"] + " % ";
                    row["brand_name"] = Woak["brand_name"];
                    row["payment"] = obj1.str;
                    if (!Convert.IsDBNull(Woak["describtion"]) && Woak["describtion"].ToString().StartsWith(".."))
                    {
                        string describtion = new Uri(Server.MapPath(Woak["describtion"].ToString().Replace("..", "~"))).AbsoluteUri;
                        row["describtion"] = describtion;
                    }
                    if (!Convert.IsDBNull(Woak["lenght"]) && !Convert.IsDBNull(Woak["widht"]))
                        row["area"] = Woak["lenght"] + " * " + Woak["widht"] + " = " + Woak["area"];
                    row["size"] = Woak["size_title"];
                    Temp.Rows.Add(row);
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Label.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Temp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                //string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;
                //ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                //ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.Refresh();
            }
        }

        protected void btn_filter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string txt = txt_enter_codes.Text;
            string[] lst = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int count = 0;
            foreach (string Woak in lst)
            {
                count++;
                if(lst.Length != count)
                    sb.Append(Woak + ",");
                else
                    sb.Append(Woak);
            }
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where code_igd in(" + sb.ToString() + ") Group By code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title");
            if (dt.Rows.Count > 0)
            {
                Common obj1 = new Common();
                DataSet1.LableDataTable Temp = new DataSet1.LableDataTable();
                foreach (DataRow Woak in dt.Rows)
                {
                    DataRow row = Temp.NewRow();
                    row["code_igd"] = Woak["code_igd"];
                    row["provider_code"] = Woak["provider_code"];
                    obj1.str = Woak["sale_price"].ToString();
                    obj1.str = Woak["sale_price"].ToString();
                    if (Woak["discount"].ToString().Equals("0"))
                        row["discount"] = string.Empty;
                    else
                        row["discount"] = Woak["discount"] + " % ";
                    row["brand_name"] = Woak["brand_name"];
                    row["payment"] = obj1.str;
                    if (!Convert.IsDBNull(Woak["describtion"]) && Woak["describtion"].ToString().StartsWith(".."))
                    {
                        string describtion = new Uri(Server.MapPath(Woak["describtion"].ToString().Replace("..", "~"))).AbsoluteUri;
                        row["describtion"] = describtion;
                    }
                    if (!Convert.IsDBNull(Woak["lenght"]) && !Convert.IsDBNull(Woak["widht"]))
                        row["area"] = Woak["lenght"] + " * " + Woak["widht"] + " = " + Woak["area"];
                    row["size"] = Woak["size_title"];
                    Temp.Rows.Add(row);
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Label.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Temp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                //string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;
                //ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                //ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.Refresh();
            }
        }

        protected void btn_report_provider_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            if (lst_sort.SelectedValue == "0")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where provider_srl=" + lst_provider.SelectedValue + " And header_srl=" + lst_project.SelectedValue);
            else if (lst_sort.SelectedValue == "1")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where discount = 5 and provider_srl=" + lst_provider.SelectedValue + " And header_srl=" + lst_project.SelectedValue);
            else if(lst_sort.SelectedValue == "2")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where discount > 5 and provider_srl=" + lst_provider.SelectedValue + " And header_srl=" + lst_project.SelectedValue);
            else if (lst_sort.SelectedValue == "3")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where (discount < 5 Or discount is null) and provider_srl=" + lst_provider.SelectedValue + " And header_srl=" + lst_project.SelectedValue);
            else if (lst_sort.SelectedValue == "brand_name,size_title")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where provider_srl=" + lst_provider.SelectedValue + " And header_srl=" + lst_project.SelectedValue + " Order By brand_name,size_title");
            else if (lst_sort.SelectedValue == "size_title,brand_name")
                dt = obj.Get_Data("SELECT code_igd, lenght, widht, payment, brand_name,discount,sale_price, area, provider_code, describtion, size_title FROM LabelView Where provider_srl=" + lst_provider.SelectedValue + " And header_srl=" + lst_project.SelectedValue + " Order By size_title,brand_name");
            if (dt.Rows.Count > 0)
            {
                Common obj1 = new Common();
                DataSet1.LableDataTable Temp = new DataSet1.LableDataTable();
                foreach (DataRow Woak in dt.Rows)
                {
                    DataRow row = Temp.NewRow();
                    row["code_igd"] = Woak["code_igd"];
                    row["provider_code"] = Woak["provider_code"];
                    obj1.str = Woak["sale_price"].ToString();
                    if (Woak["discount"].ToString().Equals("0"))
                        row["discount"] = string.Empty;
                    else
                        row["discount"] = Woak["discount"] + " % ";
                    row["brand_name"] = Woak["brand_name"];
                    row["payment"] = obj1.str;
                    if (!Convert.IsDBNull(Woak["lenght"]) && !Convert.IsDBNull(Woak["widht"]))
                        row["area"] = Woak["lenght"] + " * " + Woak["widht"] + " = " + Woak["area"];
                    if (!Convert.IsDBNull(Woak["describtion"]) && Woak["describtion"].ToString().StartsWith(".."))
                    {
                        string describtion = new Uri(Server.MapPath(Woak["describtion"].ToString().Replace("..","~"))).AbsoluteUri;
                        row["describtion"] = describtion;
                    }
                    row["size"] = Woak["size_title"];
                    Temp.Rows.Add(row);
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Label.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Temp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                //string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;
                //ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                //ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}