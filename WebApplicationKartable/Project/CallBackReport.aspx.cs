using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace WebApplicationKartable
{
    public partial class CallBackReport : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl,provider_name FROM dbo.bas_provider");
                if (dt.Rows.Count > 0)
                {
                    lst_provider.Items.Add(new ListItem("---", "0"));
                    foreach (DataRow Woak in dt.Rows)
                    {
                        lst_provider.Items.Add(new ListItem(Woak["provider_name"].ToString(), Woak["srl"].ToString()));
                    }
                }
            }
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
            StringBuilder irad = new StringBuilder();
            Common obobo = new Common(); DataTable dt2 = new DataTable();
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            if (lst_provider.SelectedIndex == 0)
            {
                dt = obj.Get_Data(string.Format("SELECT code_igd, provider_code, brand_name, size_title, carpet_title, porz_title, chele_title, lenght, widht, ROUND(lenght * widht / 10000, 2) AS area, color_srl2, plan_title, color_name,dorangi,rofo, kaji, badbaf, pakhordegi, tear FROM dbo.Project_Goods_View Where header_srl={0} order by {1}", lst_project.SelectedValue, lst_sort.SelectedValue));
                dt2 = obj.Get_Data(string.Format("Select brand_name,size_title, carpet_title, count(brand_name)cnt,dorangi,rofo, kaji, badbaf, pakhordegi, tear from dbo.Project_Goods_View Where header_srl={0} group by brand_name, size_title, carpet_title,dorangi,rofo, kaji, badbaf, pakhordegi, tear", lst_project.SelectedValue));
            }
            else
            {
                dt = obj.Get_Data(string.Format("SELECT code_igd, provider_code, brand_name, size_title, carpet_title, porz_title, chele_title, lenght, widht, ROUND(lenght * widht / 10000, 2) AS area, color_srl2, plan_title, color_name,dorangi,rofo, kaji, badbaf, pakhordegi, tear FROM dbo.Project_Goods_View Where header_srl={0} And provider_srl={1} order by {2}", lst_project.SelectedValue, lst_provider.SelectedValue, lst_sort.SelectedValue));                
                dt2 = obj.Get_Data(string.Format("Select brand_name,size_title, carpet_title, count(brand_name)cnt,dorangi,rofo, kaji, badbaf, pakhordegi, tear from dbo.Project_Goods_View Where header_srl={0} And provider_srl={1} group by brand_name, size_title, carpet_title,dorangi,rofo, kaji, badbaf, pakhordegi, tear", lst_project.SelectedValue, lst_provider.SelectedValue));
            }
            if (dt.Rows.Count > 0)
            {
                DataSet1.CallBackDataTable Temp = new DataSet1.CallBackDataTable();
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

                    if((!Convert.IsDBNull(Woak["dorangi"])) ||
                       (!Convert.IsDBNull(Woak["rofo"])) ||
                       (!Convert.IsDBNull(Woak["badbaf"])) ||
                       (!Convert.IsDBNull(Woak["pakhordegi"])) ||
                       (!Convert.IsDBNull(Woak["tear"])))
                    {
                        try
                        {
                            if ((Convert.ToBoolean(Woak["dorangi"])) ||
                                (Convert.ToBoolean(Woak["rofo"])) ||
                                (Convert.ToBoolean(Woak["badbaf"])) ||
                                (Convert.ToBoolean(Woak["pakhordegi"])) ||
                                (Convert.ToBoolean(Woak["tear"])))
                            {
                                irad.Append("فرش");
                                irad.Append(" ");
                                irad.Append(Woak["brand_name"]);
                                irad.Append(" ");
                                irad.Append(Woak["size_title"]);
                                irad.Append(" ");
                                irad.Append("با ایرادات ");
                                irad.Append(!Convert.ToBoolean(Woak["dorangi"]) ? "" : "دو رنگی");
                                irad.Append(" ");
                                irad.Append(!Convert.ToBoolean(Woak["rofo"]) ? "" : "رفو");
                                irad.Append(" ");
                                irad.Append(!Convert.ToBoolean(Woak["kaji"]) ? "" : "کجی");
                                irad.Append(" ");
                                irad.Append(!Convert.ToBoolean(Woak["badbaf"]) ? "" : "بدبافت");
                                irad.Append(" ");
                                irad.Append(!Convert.ToBoolean(Woak["pakhordegi"]) ? "" : "پاخوردگی");
                                irad.Append(" ");
                                irad.Append(!Convert.ToBoolean(Woak["tear"]) ? "" : "پارگی");
                                irad.Append("تحویل گرفته شد");
                            }
                        }
                        catch { }
                    }
                    Temp.Rows.Add(row);
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    //sb.Append(" تعداد ");
                    //sb.Append("  ");
                    //sb.Append(" ............ ");
                    //sb.Append("تخته فرشی مطابق لیست فوق در تاریخ");
                    //sb.Append("  ");
                    //sb.Append(obobo.persian_date2());
                    //sb.Append("  ");
                    //sb.Append("از طرف جناب آقای");
                    //sb.Append("  ");
                    //sb.Append(lst_provider.SelectedItem.Text);
                    //sb.Append("  ");
                    //sb.Append("تحویل فرش بوم شد.");
                    sb.Append(" گواهی می کنم که شرایط فرش های فروش رفته را که در لیست تحویل نمایشگاه ");
                    sb.Append("  ");
                    sb.Append(lst_project.SelectedItem.Text);
                    sb.Append("اعلام داشته اند کاملا صحیح است در غیر این صورت موظف هستم  فرش را پس گرفته و مبلغ آن را عودت دهم");
                }
                DataSet1.groupingDataTable Tmp = new DataSet1.groupingDataTable();
                if (dt2.Rows.Count > 0)
                {                    
                    foreach (DataRow Woak in dt2.Rows)
                    {
                        DataRow row = Tmp.NewRow();
                        row["brand_name"] = Woak["brand_name"];
                        row["size_title"] =  Woak["size_title"].ToString();
                        row["carpet_title"] = Woak["carpet_title"].ToString();
                        row["cnt"] = Convert.ToInt32(Woak["cnt"]);
                        Tmp.Rows.Add(row);
                    }
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CallBackReport.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Temp);
                ReportDataSource datasource2 = new ReportDataSource("DataSet2", (DataTable)Tmp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.DataSources.Add(datasource2);
                string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                ReportParameter parameter_Date = new ReportParameter("Date", new Common().persian_date());
                ReportParameter ProviderName = new ReportParameter("ProviderName", lst_provider.SelectedItem.Text);
                ReportParameter ProjectName = new ReportParameter("ProjectName", lst_project.SelectedItem.Text);
                ReportParameter Passage = new ReportParameter("Passage", sb.ToString());
                ReportParameter Problems = new ReportParameter("Problems", irad.ToString());

                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.SetParameters(parameter_Date);
                ReportViewer1.LocalReport.SetParameters(ProviderName);
                ReportViewer1.LocalReport.SetParameters(ProjectName);
                ReportViewer1.LocalReport.SetParameters(Passage);
                ReportViewer1.LocalReport.SetParameters(Problems);
                ReportViewer1.LocalReport.Refresh();
            }
            else
            {
                DataSet1.CallBackDataTable Temp = new DataSet1.CallBackDataTable();
                DataSet1.groupingDataTable Tmp = new DataSet1.groupingDataTable();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CallBackReport.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Temp);
                ReportDataSource datasource2 = new ReportDataSource("DataSet2", (DataTable)Tmp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.DataSources.Add(datasource2);
                string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                ReportParameter parameter_Date = new ReportParameter("Date", new Common().persian_date());
                ReportParameter ProviderName = new ReportParameter("ProviderName", lst_provider.SelectedItem.Text);
                ReportParameter ProjectName = new ReportParameter("ProjectName", lst_project.SelectedItem.Text);
                ReportParameter Passage = new ReportParameter("Passage", string.Empty);
                ReportParameter Problems = new ReportParameter("Problems", irad.ToString());

                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.SetParameters(parameter_Date);
                ReportViewer1.LocalReport.SetParameters(ProviderName);
                ReportViewer1.LocalReport.SetParameters(ProjectName);
                ReportViewer1.LocalReport.SetParameters(Passage);
                ReportViewer1.LocalReport.SetParameters(Problems);
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