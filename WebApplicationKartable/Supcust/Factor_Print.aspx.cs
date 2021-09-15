using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;

namespace WebApplicationKartable
{
    public partial class Factor_Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Common obj = new Common();
                Factor prevPage = PreviousPage as Factor;
                DataTable dt = prevPage.dt2;
                DataSet1.Factor_DataTableDataTable Temp = new DataSet1.Factor_DataTableDataTable();
                foreach (DataRow Woak in dt.Rows)
                {
                    DataRow row = Temp.NewRow();
                    row["size_title"] = Woak["size_title"];
                    row["lenght"] = Woak["lenght"];
                    row["widht"] = Woak["widht"];
                    row["city_name"] = Woak["brand_name"];
                    row["code_igd"] = Woak["code_igd"];
                    obj.str = Woak["sale_price"].ToString();
                    row["sale_price"] = obj.str;
                    row["carpet_title"] = Woak["carpet_title"];
                    row["color_name"] = Woak["color_name"].ToString().Trim();
                    row["margin_color"] = color2(Woak["color_srl2"].ToString());
                    row["porz_title"] = Woak["porz_title"];
                    row["chele_title"] = Woak["chele_title"];
                    row["plan_desc"] = Woak["plan_title"];
                    row["full_name"] = Woak["full_name"];
                    row["tel1"] = Woak["tel1"];
                    row["raj_srl"] = Woak["raj_srl"];
                    row["cell_phone"] = Woak["cell_phone"];
                    row["disc_per"] = Woak["disc_per"];                
                    row["address1"] = Woak["address1"];
                    row["email"] = Woak["email"];
                    obj.str = Woak["discount"].ToString();
                    row["discount"] = obj.str;
                    obj.str = Woak["down_payment"].ToString();
                    row["down_payment"] = obj.str;
                    obj.str = Woak["payment"].ToString();
                    row["payment"] = obj.str;
                    row["factor_no"] = Woak["factor_no"];
                    row["factor_date"] = Woak["u_date_tome"];
                    if (!Convert.IsDBNull(Woak["sale_price"]) && !Convert.IsDBNull(Woak["discount"]))
                        obj.str = (Convert.ToInt64(obj.remove_cama(row["sale_price"].ToString())) - Convert.ToInt64(Woak["discount"])).ToString();
                    row["price_after_dis"] = obj.str;
                    if(!Convert.IsDBNull(Woak["lenght"]) && !Convert.IsDBNull(Woak["widht"]))                    
                        row["area"] = Math.Round((Convert.ToDouble(Woak["lenght"]) * Convert.ToDouble(Woak["widht"]) / 10000),2);
                    row["logo"] = Server.MapPath("/images//logo.png");

                    if (!Convert.IsDBNull(row["sale_price"]) && (!Convert.IsDBNull(row["discount"])))
                    {
                        row["disc_per"] = (Convert.ToInt64(Woak["discount"]) * 100) / Convert.ToInt64(Woak["sale_price"]);
                    }
                    row["state"] = Woak["state"].ToString();
                    Temp.Rows.Add(row);
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Factor.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Temp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                ReportViewer1.LocalReport.SetParameters(parameter);

                ReportParameter Hours = new ReportParameter("Hours", DateTime.Now.ToShortTimeString());
                ReportViewer1.LocalReport.SetParameters(Hours);

                ReportViewer1.LocalReport.Refresh();
            }
        }
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private string color2(string str)
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