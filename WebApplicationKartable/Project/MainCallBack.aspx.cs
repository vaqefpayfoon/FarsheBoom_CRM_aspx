using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace WebApplicationKartable
{
    public partial class MainCallBack : System.Web.UI.Page
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
            Common obo = new Common();
            DataTable dt = new DataTable(); Search obj = new Search
                (strConnString);
            if (chk_all.Checked)
            {
                dt = obj.Get_Data(string.Format("SELECT srl,code_igd, provider_code, brand_name, size_title, carpet_title, porz_title, chele_title, lenght, widht, ROUND(lenght * widht / 10000, 2) AS area, color_srl2, plan_title, color_name, buy_price, ROUND(buy_price / NULLIF(ROUND(lenght * widht / 10000, 1), 0),0) AS u_buy,title_igd FROM dbo.Project_Goods_View Where  header_srl={0} order by {1}", lst_project.SelectedValue, lst_sort.SelectedValue));
            }
            else
            {
                dt = obj.Get_Data(string.Format("SELECT srl,code_igd, provider_code, brand_name, size_title, carpet_title, porz_title, chele_title, lenght, widht, ROUND(lenght * widht / 10000, 2) AS area, color_srl2, plan_title, color_name, buy_price, ROUND(buy_price / NULLIF(ROUND(lenght * widht / 10000, 1), 0),0) AS u_buy,title_igd FROM dbo.Project_Goods_View Where  header_srl={0} And provider_srl={1} order by {2}", lst_project.SelectedValue, lst_provider.SelectedValue, lst_sort.SelectedValue));
            }
            if (dt.Rows.Count > 0)
            {
                txt_count.Text = dt.Rows.Count.ToString();
                DataSet1.CallBackDataTable Temp = new DataSet1.CallBackDataTable();
                List<GoodsClass> lstGoodsClass = new List<GoodsClass>();
                foreach (DataRow Woak in dt.Rows)
                {
                    DataRow row = Temp.NewRow();
                    row["code_igd"] = Woak["code_igd"];
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
                    row["provider_code"] = Woak["provider_code"];
                    obo.str = Woak["buy_price"].ToString();
                    row["buy_price"] = obo.str;
                    obo.str = Woak["u_buy"].ToString();                    
                    row["u_buy"] = obo.str;
                    Temp.Rows.Add(row);

                    GoodsClass pc = new GoodsClass();
                    pc.srl = Woak["srl"].ToString();
                    pc.code_igd = Woak["code_igd"].ToString();
                    pc.brand_name = Woak["brand_name"].ToString();
                    pc.size_title = Woak["size_title"].ToString();
                    pc.carpet_title = Woak["carpet_title"].ToString();
                    pc.color_name = Woak["color_name"].ToString();
                    pc.porz_title = Woak["porz_title"].ToString();
                    pc.chele_title = Woak["chele_title"].ToString();
                    pc.plan_title = Woak["plan_title"].ToString();
                    pc.widht = Woak["widht"].ToString();
                    pc.lenght = Woak["lenght"].ToString();
                    pc.provider_code = Woak["provider_code"].ToString();
                    pc.provider_name = lst_provider.SelectedItem.Text;
                    pc.selection = string.Empty;
                    pc.sold = string.Empty;
                    pc.buy_price = Woak["buy_price"].ToString();
                    pc.margin_color = color(Woak["color_srl2"].ToString());
                    pc.area = Woak["area"].ToString();
                    pc.title_igd = Woak["title_igd"].ToString();
                    lstGoodsClass.Add(pc);
                }
                Session["ThrowList"] = lstGoodsClass;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/MainCallBack.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Temp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                ReportParameter parameter_Date = new ReportParameter("Date", new Common().persian_date());
                ReportParameter parameter_Title = new ReportParameter("Title", "لیست فراخوان");
                string provider_name = string.Empty;
                if (chk_all.Checked)
                    provider_name = "تمام تامین کننده ها";
                else
                    provider_name = lst_provider.SelectedItem.Text;
                ReportParameter ProviderName = new ReportParameter("ProviderName", provider_name);
                ReportParameter ProjectName = new ReportParameter("ProjectName", lst_project.SelectedItem.Text);
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.SetParameters(parameter_Date);
                ReportViewer1.LocalReport.SetParameters(ProviderName);
                ReportViewer1.LocalReport.SetParameters(ProjectName);
                ReportViewer1.LocalReport.SetParameters(parameter_Title);
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
        protected void btn_print_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if(Session["ThrowList"] != null)
                Response.Redirect("../Provider_Goods/CompleteWithPic.aspx");            
        }
    }
}