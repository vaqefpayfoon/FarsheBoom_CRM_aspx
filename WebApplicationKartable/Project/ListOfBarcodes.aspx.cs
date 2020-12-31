using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace WebApplicationKartable
{
    public partial class ListOfBarcodes : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_add_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string txt = txt_enter_codes.Text.Replace("\r\n", "");
            IEnumerable<string> here = Enumerable.Range(0, txt.Length / 6).Select(i => txt.Substring(i * 6, 6));
            var ss = string.Join(",", here.ToArray());
            Common obo = new Common();
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data(string.Format("SELECT srl,code_igd, provider_code, brand_name, size_title, carpet_title, porz_title, chele_title, lenght, widht, ROUND(lenght * widht / 10000, 2) AS area, color_srl2, plan_title, color_name, buy_price, ROUND(buy_price / NULLIF(ROUND(lenght * widht / 10000, 1), 0),0) AS u_buy,title_igd FROM dbo.Project_Goods_View Where header_srl={0} And code_igd Not In ({2}) order by {1}", lst_project.SelectedValue, lst_sort.SelectedValue, ss));
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
                    pc.provider_name = "";
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
                ReportParameter parameter_Title = new ReportParameter("Title", "فرش های غایب");
                string provider_name = string.Empty;
                provider_name = "تمام تامین کننده ها";
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

        protected void btn_present_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string txt = txt_enter_codes.Text.Replace("\r\n", "");
            IEnumerable<string> here = Enumerable.Range(0, txt.Length / 6).Select(i => txt.Substring(i * 6, 6));
            var ss = string.Join(",", here.ToArray());
            Common obo = new Common();
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data(string.Format("SELECT srl,code_igd, provider_code, brand_name, size_title, carpet_title, porz_title, chele_title, lenght, widht, ROUND(lenght * widht / 10000, 2) AS area, color_srl2, plan_title, color_name, buy_price, ROUND(buy_price / NULLIF(ROUND(lenght * widht / 10000, 1), 0),0) AS u_buy,title_igd FROM dbo.Project_Goods_View Where header_srl={0} And code_igd In ({2}) order by {1}", lst_project.SelectedValue, lst_sort.SelectedValue, ss));
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
                    pc.provider_name = "";
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
                ReportParameter parameter_Title = new ReportParameter("Title", "فرش های حاضر");
                string provider_name = string.Empty;
                provider_name = "تمام تامین کننده ها";
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
    }
}