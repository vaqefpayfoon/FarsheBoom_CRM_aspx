using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

namespace WebApplicationKartable
{
    public partial class FactorsList : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            fill_grid();
        }

        private void CheckLogin()
        {
            if (Request.Cookies["myCookie"] != null)
            {
                if (Request.Cookies["myCookie"]["srl"] != null)
                    ubuzhi.srl = Request.Cookies["myCookie"]["srl"].ToString();
                else
                    Response.Redirect("../login.aspx");
                if (Request.Cookies["myCookie"]["first_name"] != null)
                    ubuzhi.first_name = Request.Cookies["myCookie"]["first_name"].ToString();
                else
                    Response.Redirect("../login.aspx");
                if (Request.Cookies["myCookie"]["last_name"] != null)
                    ubuzhi.last_name = Request.Cookies["myCookie"]["last_name"].ToString();
                else
                    Response.Redirect("../login.aspx");
                if (Request.Cookies["myCookie"]["group_srl"] != null)
                    ubuzhi.group_srl = Request.Cookies["myCookie"]["group_srl"].ToString();
                else
                    Response.Redirect("../login.aspx");
            }
        }


        private void fill_grid()
        {
            Search obj = new Search(strConnString); DataTable dt = new DataTable();
            dt = obj.Get_Data(string.Format("SELECT chele_title, brand_name, size_title, provider_name, color_name, project_name, area, factor_no, u_date_tome,discount, discount_amount, down_payment, final_price FROM dbo.SoldCarpets order by u_date_tome desc"));
            if (dt.Rows.Count > 0)
            {
                gridview.DataSource = dt;
                gridview.DataBind();
            }
        }

        private void BindData()
        {
            DataTable dt = ViewState["dt"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                gridview.DataSource = dt;
                gridview.DataBind();
            }
        }
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            gridview.PageIndex = e.NewPageIndex;
            gridview.DataBind();

        }

        protected void ImageButton_Report_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT srl_f, srl, brand_name, size_title, provider_name, color_name, project_name, area, factor_no, u_date_tome,discount, discount_amount, down_payment, final_price FROM dbo.SoldCarpets";
            if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex == 0 && lst_project.SelectedIndex == 0 && lst_bank.SelectedIndex == 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(query + " where (from_date between '" + txt_from_date.Text + "' and '" + txt_to_date.Text + "') order by u_date_tome desc");

                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex == 0 && lst_bank.SelectedIndex == 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') And provider_srl={2} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_provider.SelectedValue));

                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex == 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex == 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') And header_srl={2} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_project.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex == 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') And (header_srl={2}) And (provider_srl={3}) order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_project.SelectedValue, lst_provider.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex == 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (header_srl={0}) And (provider_srl={1}) order by u_date_tome desc", lst_project.SelectedValue, lst_provider.SelectedValue));

                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex == 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex == 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (header_srl={0}) order by u_date_tome desc", lst_project.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex == 0 && lst_bank.SelectedIndex == 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (provider_srl={0}) order by u_date_tome desc", lst_provider.SelectedValue));

                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
            //start bank
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex == 0 && lst_project.SelectedIndex == 0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') and bank_srl = {2} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_bank.SelectedValue));

                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }




            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex == 0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') And provider_srl={2} and bank_srl={3} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_provider.SelectedValue, lst_bank.SelectedValue));

                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex == 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') And header_srl={2} and bank_srl={3} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_project.SelectedValue, lst_bank.SelectedValue));

                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') And (header_srl={2}) And (provider_srl={3}) and bank_srl={4} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_project.SelectedValue, lst_provider.SelectedValue, lst_bank.SelectedValue));

                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (header_srl={0}) And (provider_srl={1}) and bank_srl={2} order by u_date_tome desc", lst_project.SelectedValue, lst_provider.SelectedValue, lst_bank.SelectedValue));

                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex == 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format(query + " where (header_srl={0}) and bank_srl={1} order by u_date_tome desc", lst_project.SelectedValue, lst_bank.SelectedValue));

                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
                
            }
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex == 0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format( query + " where (provider_srl={0}) and bank_srl = {1} order by u_date_tome desc", lst_provider.SelectedValue, lst_bank.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    gridview.DataSource = dt;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }


            else
                fill_grid();
        }

        protected void ImageButton_print_Click(object sender, ImageClickEventArgs e)
        {
            int count = 0;
            string srl_factor = " Where srl_f In(";
            foreach (GridViewRow gvrow in gridview.Rows)
            {
                CheckBox checkbox = (CheckBox)gvrow.FindControl("chk_delete");

                if (checkbox.Checked)
                {
                    srl_factor += gridview.Rows[count].Cells[0].Text;
                    srl_factor += ",";
                }
                count++;
            }
            srl_factor = srl_factor.Remove(srl_factor.Length - 1);
            srl_factor += ")";
            string query = "SELECT srl_f, srl, brand_name, size_title, provider_name, color_name, project_name, area, factor_no, u_date_tome,discount, discount_amount, down_payment, final_price FROM dbo.SoldCarpets" + srl_factor;

            Search objSearch = new Search(strConnString); DataTable dt = new DataTable();
            dt = objSearch.Get_Data(query);

            Common obj = new Common();

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
                row["porz_title"] = Woak["porz_title"];
                row["chele_title"] = Woak["chele_title"];
                row["plan_desc"] = Woak["plan_title"];
                row["full_name"] = Woak["full_name"];
                row["tel1"] = Woak["tel1"];
                row["cell_phone"] = Woak["cell_phone"];
                row["disc_per"] = Woak["disc_per"];
                row["address1"] = Woak["address1"];
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
                if (!Convert.IsDBNull(Woak["lenght"]) && !Convert.IsDBNull(Woak["widht"]))
                    row["area"] = Math.Round((Convert.ToDouble(Woak["lenght"]) * Convert.ToDouble(Woak["widht"]) / 10000), 2);
                row["logo"] = Server.MapPath("/images//logo.png");

                if (!Convert.IsDBNull(row["sale_price"]) && (!Convert.IsDBNull(row["discount"])))
                {
                    row["disc_per"] = (Convert.ToInt64(Woak["discount"]) * 100) / Convert.ToInt64(Woak["sale_price"]);
                }
                row["state"] = Woak["state"].ToString();
                Temp.Rows.Add(row);
            }
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/FactorList.rdlc");
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
}