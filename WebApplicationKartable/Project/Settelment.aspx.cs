using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace WebApplicationKartable
{
    public partial class Settelment : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
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
            string p_date = string.Empty, to_date = string.Empty;
            Common obo = new Common();
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            if (lst_provider.SelectedIndex == 0)
            {
                dt = obj.Get_Data(string.Format("SELECT code_igd, provider_code, brand_name, size_title, carpet_title, porz_title, chele_title, lenght, widht, area, color_srl2, plan_title, color_name, buy_price,from_date,to_date,title_igd,down_payment,project_code,bayane,final_price, kaji, badbaf, pakhordegi FROM SoldCarpets Where header_srl={0}", lst_project.SelectedValue));
            }
            else
            {
                dt = obj.Get_Data(string.Format("SELECT code_igd, provider_code, brand_name, size_title, carpet_title, porz_title, chele_title, lenght, widht, area, color_srl2, plan_title, color_name, buy_price,from_date,to_date,title_igd,down_payment,project_code,bayane,final_price, kaji, badbaf, pakhordegi FROM SoldCarpets Where header_srl={0} And provider_srl={1}", lst_project.SelectedValue, lst_provider.SelectedValue));
            }
            double buy = 0;
            foreach (DataRow item in dt.Rows)
            {
                buy += Convert.IsDBNull(item["buy_price"]) ? 0 : Convert.ToDouble(item["buy_price"]);
            }
            DataSet1.CallBackDataTable Temp = new DataSet1.CallBackDataTable();
            if (dt.Rows.Count > 0)
            {
                string irad = "";
                ViewState["lstOutstandingOrders"] = dt;
                foreach (DataRow Woak in dt.Rows)
                {
                    DataRow row = Temp.NewRow();
                    row["code_igd"] = Woak["code_igd"];
                    p_date = Woak["from_date"].ToString();
                    to_date = Woak["to_date"].ToString();
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
                    row["couple"] = Convert.IsDBNull(Woak["kaji"]) ? "تک" : Convert.ToBoolean(Woak["kaji"]) == false ? "تک" : "جفت";
                    row["neet"] = Convert.IsDBNull(Woak["badbaf"]) ? "جدید بافت" : Convert.ToBoolean(Woak["badbaf"]) == false ? "جدید بافت" : "قدیم بافت";
                    row["feet"] = Convert.IsDBNull(Woak["pakhordegi"]) ? "نو" : Convert.ToBoolean(Woak["pakhordegi"]) == false ? "نو" : "پاخورده";
                    obo.str = Woak["buy_price"].ToString();
                    row["buy_price"] = obo.str;
                    Temp.Rows.Add(row);
                }
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable dt2 = new DataTable();
            dt2 = obj.Get_Data(string.Format("SELECT COUNT(dbo.bas_project_goods.igd_srl) FROM dbo.bas_project_goods INNER JOIN dbo.inv_goods ON dbo.bas_project_goods.igd_srl = dbo.inv_goods.srl Where dbo.bas_project_goods.header_srl={0} AND dbo.inv_goods.provider_srl={1}", lst_project.SelectedValue, lst_provider.SelectedValue));
            sb.Append("از تعداد");
            sb.Append(" ............ ");
            //if(dt2.Rows.Count > 0)
            //    sb.Append(dt2.Rows[0][0]);            
            sb.Append("  ");
            sb.Append("تخته فرشی که");
            sb.Append("  ");
            sb.Append(lst_provider.SelectedItem.Text);
            sb.Append("  ");
            sb.Append("در تاریخ");
            sb.Append("  ");
            sb.Append(obo.persian_date2(p_date));
            sb.Append("  ");
            sb.Append("به فرش بوم تحویل داده بودند، تعداد");
            sb.Append("  ");
            sb.Append("<b>");
            if (dt.Rows.Count > 0)
                sb.Append(dt.Rows.Count);
            sb.Append("</b>");
            sb.Append("  ");
            sb.Append("تخته به فروش رفته و مابقی به تعداد");
            sb.Append("  ");
            //if (dt2.Rows.Count > 0 && dt.Rows.Count > 0)
            //    sb.Append((Convert.ToInt32(dt2.Rows[0][0]) - dt.Rows.Count));
            sb.Append(" ..................... ");
            sb.Append("تخته به طور صحیح و سالم ،در تاریخ");
            sb.Append("  ");
            sb.Append(obo.persian_date2(to_date));
            sb.Append("  ");
            sb.Append("به ایشان عودت داده شد. جمع مبلغ فرشهای فروش رفته معادل");
            sb.Append("  ");
            double totalprice = 0;
            double totaldownpayment = 0;
            double totalpayment = 0;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    totalprice = Convert.ToDouble(dt.Compute("SUM(buy_price)", ""));
                }
                catch { }
                obo.str = totalprice.ToString();
                sb.Append(obo.str);
            }
            sb.Append(" ریال است ");
            sb.Append("که تعداد ");
            sb.Append(" ");
            int cnt = 0;
            try
            {
                cnt = Convert.ToInt32(dt.Compute("Count(buy_price)", "bayane=1"));
            }
            catch { }
            sb.Append(cnt.ToString());
            sb.Append(" تخته از آنها به ارزش ");
            sb.Append("  ");
            if (dt.Rows.Count > 0)
            {
                try
                {
                    //totaldownpayment = Convert.ToDouble(dt.Compute("SUM(down_payment)", "bayane=1"));
                    //totalpayment = Convert.ToDouble(dt.Compute("SUM(payment)", "bayane=1"));
                    totaldownpayment = Convert.ToInt32(dt.Compute("Sum(buy_price)", "bayane=1"));
                }
                catch { }
                //obo.str = (totaldownpayment - totalpayment).ToString();
                obo.str = totaldownpayment.ToString();
                sb.Append(obo.str);
            }

            sb.Append(" ريال ");
            sb.Append(" به صورت بیعانه ای به فروش رفته که در صورت تسویه مشتریان، تا تاریخ... تسویه و در غیر این صورت عودت می گردد.بنابراین مبلغ قابل پرداخت ");
            try
            {
                totalprice = 0;
                totalprice = Convert.ToDouble(dt.Compute("SUM(buy_price)", "bayane=1"));
            }
            catch { }
            
            obo.str = (buy - totalprice).ToString();
            sb.Append("  ");
            sb.Append(obo.str);
            sb.Append(" ریال است که طی سند ذیل پرداخت شده و در نتیجه کلیه حسابهای فیمابین نمایشگاه ");
            if(dt.Rows.Count > 0)
                sb.Append(dt.Rows[0]["project_code"]);
            sb.Append(" ");
            sb.Append("به طور کامل تسویه حساب شده و طرفین هیچ حقی نسبت به هم ندارند.");
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Settelment.rdlc");
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Temp);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;
            ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
            ReportParameter parameter_Date = new ReportParameter("Date", new Common().persian_date());
            ReportParameter ProviderName = new ReportParameter("ProviderName", lst_provider.SelectedItem.Text);
            ReportParameter ProjectName = new ReportParameter("ProjectName", lst_project.SelectedItem.Text);
            ReportParameter Passage = new ReportParameter("Passage", sb.ToString());
            ReportViewer1.LocalReport.SetParameters(parameter);
            ReportViewer1.LocalReport.SetParameters(parameter_Date);
            ReportViewer1.LocalReport.SetParameters(ProviderName);
            ReportViewer1.LocalReport.SetParameters(ProjectName);
            ReportViewer1.LocalReport.SetParameters(Passage);
            ReportViewer1.LocalReport.Refresh();
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
        protected void btn_print_pic_Click(object sender, EventArgs e)
        {
            if (ViewState["lstOutstandingOrders"] == null) return;
            Session["ThrowList"] = ViewState["lstOutstandingOrders"];
            Response.Redirect("SettelmentsPic.aspx");
        }
    }
}