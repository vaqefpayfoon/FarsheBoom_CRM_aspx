﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Web;

namespace WebApplicationKartable
{
    public partial class FactorsList : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                fill_grid();
                fill_combo();
            }
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch2(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT full_name FROM dbo.bas_supcust where full_name like '%'+ @SearchText + '%'", prefixText, count);
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch3(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT cell_phone FROM dbo.bas_supcust where cell_phone like '%'+ @SearchText + '%'", prefixText, count);
        }
        private void fill_grid()
        {
            DataTable dt = new DataTable();
            Search obj = new Search(strConnString);  
            dt = obj.Get_Data(string.Format("SELECT srl_f, srl, code_igd, brand_name, size_title, provider_name, color_name, project_name, area, factor_no, u_date_tome,discount, discount_amount, down_payment, final_price, sale_price, cell_phone, full_name FROM dbo.SoldCarpets order by u_date_tome desc"));
            if (dt.Rows.Count > 0)
            {
                gridview.DataSource = dt;
                gridview.DataBind();
            }
            ViewState.Add("table", dt);
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
        protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview.PageIndex = e.NewPageIndex;
            fill_grid();
        }
        protected void ImageButton_Report_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            string query = "SELECT srl_f, srl, code_igd, brand_name, size_title, provider_name, color_name, project_name, area, factor_no, u_date_tome,discount, discount_amount, down_payment, final_price, sale_price, cell_phone, full_name FROM dbo.SoldCarpets";
            if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex == 0 && lst_project.SelectedIndex == 0 && lst_bank.SelectedIndex == 0 && txt_discount.Text == "")
            {
                Search obj = new Search(strConnString);
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
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex ==0 && lst_bank.SelectedIndex ==0)
            {
                Search obj = new Search(strConnString);
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
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex ==0)
            {
                Search obj = new Search(strConnString);
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
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex ==0)
            {
                Search obj = new Search(strConnString);
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
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex ==0)
            {
                Search obj = new Search(strConnString);
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
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex ==0)
            {
                Search obj = new Search(strConnString);
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
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex ==0 && lst_bank.SelectedIndex ==0)
            {
                Search obj = new Search(strConnString);
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
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex ==0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString);
                dt = obj.Get_Data(string.Format(query + " where bank_srl = {1} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_bank.SelectedValue));

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
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex ==0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString);
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
                Search obj = new Search(strConnString);  
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
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString);  
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
                Search obj = new Search(strConnString);  
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
                Search obj = new Search(strConnString);  
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
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString);  
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
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex ==0 && lst_bank.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString);  
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
            //start discount
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex ==0 && lst_bank.SelectedIndex ==0 && txt_discount.Text != "")
            {
                Search obj = new Search(strConnString);
                dt = obj.Get_Data(string.Format(query + " where discount={0} order by u_date_tome desc", txt_discount.Text));

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
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex ==0 && lst_bank.SelectedIndex > 0 && txt_discount.Text != "")
            {
                Search obj = new Search(strConnString);
                dt = obj.Get_Data(string.Format(query + " where discount={0} and bank_srl = {1} order by u_date_tome desc", txt_discount.Text, lst_bank.SelectedValue));

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
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex ==0 && lst_bank.SelectedIndex ==0 && txt_discount.Text != "")
            {
                Search obj = new Search(strConnString);
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') and discount={2} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, txt_discount.Text));

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
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex ==0 && lst_bank.SelectedIndex > 0 && txt_discount.Text != "")
            {
                Search obj = new Search(strConnString);
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') and bank_srl = {2} and discount={3} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_bank.SelectedValue, txt_discount.Text));

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
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex == 0 && lst_bank.SelectedIndex > 0 && txt_discount.Text != "")
            {
                Search obj = new Search(strConnString);
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') And provider_srl={2} and bank_srl={3} and discount = {4} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_provider.SelectedValue, lst_bank.SelectedValue, txt_discount.Text));

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
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex > 0 && txt_discount.Text != "")
            {
                Search obj = new Search(strConnString);
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') And header_srl={2} and bank_srl={3} and discount={4} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_project.SelectedValue, lst_bank.SelectedValue, txt_discount.Text));

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
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex > 0 && txt_discount.Text != "")
            {
                Search obj = new Search(strConnString);
                dt = obj.Get_Data(string.Format(query + " where (from_date between '{0}' and '{1}') And (header_srl={2}) And (provider_srl={3}) and bank_srl={4} and discount={5} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_project.SelectedValue, lst_provider.SelectedValue, lst_bank.SelectedValue, txt_discount.Text));

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
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex > 0 && txt_discount.Text != "")
            {
                Search obj = new Search(strConnString);
                dt = obj.Get_Data(string.Format(query + " where (header_srl={0}) And (provider_srl={1}) and bank_srl={2} and discount={3} order by u_date_tome desc", lst_project.SelectedValue, lst_provider.SelectedValue, lst_bank.SelectedValue, txt_discount.Text));

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
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex ==0 && lst_project.SelectedIndex > 0 && lst_bank.SelectedIndex > 0 && txt_discount.Text != "")
            {
                Search obj = new Search(strConnString);
                dt = obj.Get_Data(string.Format(query + " where (header_srl={0}) and bank_srl={1} and discount={2} order by u_date_tome desc", lst_project.SelectedValue, lst_bank.SelectedValue, txt_discount.Text));

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
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex ==0 && lst_bank.SelectedIndex > 0 && txt_discount.Text != "")
            {
                Search obj = new Search(strConnString);
                dt = obj.Get_Data(string.Format(query + " where (provider_srl={0}) and bank_srl = {1} and discount={2} order by u_date_tome desc", lst_provider.SelectedValue, lst_bank.SelectedValue, txt_discount.Text));
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
            ViewState.Add("table", dt);
        }
        public void fill_combo()
        {
            Search obj = new Search(strConnString); DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl,project_code FROM dbo.bas_project order by srl desc");
            if (dt.Rows.Count > 0)
            {
                lst_project.Items.Add(new ListItem("---", "0"));
                foreach (DataRow Woak in dt.Rows)
                {
                    lst_project.Items.Add(new ListItem(Woak["project_code"].ToString(), Woak["srl"].ToString()));
                }
            }
            dt = new DataTable();
            dt = obj.Get_Data("SELECT srl,provider_name FROM dbo.bas_provider");
            if (dt.Rows.Count > 0)
            {
                lst_provider.Items.Add(new ListItem("---", "0"));
                foreach (DataRow Woak in dt.Rows)
                {
                    lst_provider.Items.Add(new ListItem(Woak["provider_name"].ToString(), Woak["srl"].ToString()));
                }
            }

            dt = new DataTable();
            dt = obj.Get_Data("SELECT srl, bank_name FROM dbo.inv_bank");
            if (dt.Rows.Count > 0)
            {
                lst_bank.Items.Add(new ListItem("---", "0"));
                foreach (DataRow Woak in dt.Rows)
                {
                    lst_bank.Items.Add(new ListItem(Woak["bank_name"].ToString(), Woak["srl"].ToString()));
                }
            }
        }
        protected void ImageButton_print_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            int count = 0;
            bool chk = false;
            string srl_factor = " Where code_igd In(";
            foreach (GridViewRow gvrow in gridview.Rows)
            {
                //CheckBox checkbox = (CheckBox)gvrow.FindControl("CheckBox1");
                var checkbox = gvrow.FindControl("CheckBox1") as CheckBox;
                var items = gridview.Rows[count].Cells;

                if (checkbox.Checked)
                {
                    chk = true;
                    srl_factor += gridview.Rows[count].Cells[3].Text;
                    srl_factor += ",";
                }
                count++;
            }
            if (!chk) return;
            srl_factor = srl_factor.Remove(srl_factor.Length - 1);
            srl_factor += ")";
            string query = "SELECT srl_f, srl, brand_name, size_title, provider_name, color_name, project_name, area, factor_no, u_date_tome,discount, discount_amount, down_payment, final_price, code_igd, sale_price, full_name, address1, final_price, discount, cell_phone, email FROM dbo.SoldCarpets" + srl_factor;

            Search objSearch = new Search(strConnString);  
            dt = objSearch.Get_Data(query);

            Common obj = new Common();

            DataSet1.FactorListDataTable Temp = new DataSet1.FactorListDataTable();
            int countRow = 1;

            string str_down_payment = "";
            object down_payment;
            down_payment = dt.Compute("SUM(down_payment)", "");
            if (!Convert.IsDBNull(down_payment))
            {
                obj.str = down_payment.ToString();
                str_down_payment = obj.str;
            }
            else
            {
                down_payment = 0;
            }


            string str_discount_amount = "";
            object discount_amount;
            discount_amount = dt.Compute("SUM(discount_amount)", "");
            if (!Convert.IsDBNull(discount_amount))
            {
                obj.str = discount_amount.ToString();
                str_discount_amount = obj.str;
            }
            else
            {
                discount_amount = 0;
            }

            string str_final_price = "";
            object final_price;
            final_price = dt.Compute("SUM(final_price)", "");
            if (!Convert.IsDBNull(final_price))
            {
                final_price = Convert.ToDouble(final_price);// - Convert.ToDouble(down_payment) - Convert.ToDouble(discount_amount);
                obj.str = final_price.ToString();
                str_final_price = obj.str;
            }

            foreach (DataRow Woak in dt.Rows)
            {
                DataRow row = Temp.NewRow();
                row["sum_downpayment"] = str_down_payment;
                row["sum_remaining"] = str_final_price;
                row["row"] = countRow;
                row["size_title"] = Woak["size_title"];
                row["brand_name"] = Woak["brand_name"];
                row["code_igd"] = Woak["code_igd"];
                obj.str = Woak["sale_price"].ToString();
                row["sale_price"] = obj.str;
                row["fullname"] = Woak["full_name"];
                //row["tel1"] = Woak["tel1"];
                row["cell_phone"] = Woak["cell_phone"];
                row["address1"] = Woak["address1"];
                row["email"] = Woak["email"];
                obj.str = Woak["discount_amount"].ToString();
                row["discount_amount"] = obj.str;
                //obj.str = Woak["down_payment"].ToString();
                //row["down_payment"] = obj.str;
                obj.str = Woak["final_price"].ToString();
                row["final_price"] = obj.str;
                row["factor_no"] = Woak["factor_no"];
                row["factor_date"] = Woak["u_date_tome"];
                if (!Convert.IsDBNull(Woak["sale_price"]) && !Convert.IsDBNull(Woak["discount"]))
                    obj.str = (Convert.ToInt64(obj.remove_cama(row["sale_price"].ToString())) - Convert.ToInt64(Woak["discount_amount"])).ToString();
                row["price_after_dis"] = obj.str;
                //if (!Convert.IsDBNull(Woak["lenght"]) && !Convert.IsDBNull(Woak["widht"]))
                //    row["area"] = Math.Round((Convert.ToDouble(Woak["lenght"]) * Convert.ToDouble(Woak["widht"]) / 10000), 2);
                // row["logo"] = Server.MapPath("/images//logo.png");
                // row["logo"] = Server.MapPath("/images//logo.png");

                //if (!Convert.IsDBNull(row["sale_price"]) && (!Convert.IsDBNull(row["discount"])))
                //{
                //    row["disc_per"] = (Convert.ToInt64(Woak["discount"]) * 100) / Convert.ToInt64(Woak["sale_price"]);
                //}
                Temp.Rows.Add(row);
                countRow++;
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

        protected void excel_export_Click(object sender, ImageClickEventArgs e)
        {
            // code_igd, brand_name, size_title, provider_name, color_name, project_name, area, factor_no, u_date_tome,discount, discount_amount, down_payment, final_price, sale_price
            DataTable table = ViewState["table"] as DataTable;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Write("<font style='font-size:14.0pt; font-family:B Nazanin;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:14.0pt; font-family:B Nazanin; background:white;'> <TR>");
            int columnscount = table.Columns.Count;
            HttpContext.Current.Response.Write("</TR>");
            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("کد فرش بوم");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("گونه");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("اندازه");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تامین کننده");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("رنگ");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("نمایشگاه");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("ابعاد");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("شماره فاکتور");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تاریخ فاکتور");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("تخفیف");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("مبلغ تخفیف");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("پیش پرداخت");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("پرداختی");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("قیمت فروش");
            HttpContext.Current.Response.Write("</Td>");
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 2; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        protected void btn_supcust_name_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Search obj = new Search(strConnString);
            dt = obj.Get_Data(string.Format("SELECT srl_f, srl, code_igd, brand_name, size_title, provider_name, color_name, project_name, area, factor_no, u_date_tome,discount, discount_amount, down_payment, final_price, sale_price, cell_phone, full_name FROM dbo.SoldCarpets where full_name='{0}' order by u_date_tome desc", txtContactsSearch2.Text));
            if (dt.Rows.Count > 0)
            {
                gridview.DataSource = dt;
                gridview.DataBind();
            }
            ViewState.Add("table", dt);
        }
        protected void btn_cell_phone_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Search obj = new Search(strConnString);
            dt = obj.Get_Data(string.Format("SELECT srl_f, srl, code_igd, brand_name, size_title, provider_name, color_name, project_name, area, factor_no, u_date_tome,discount, discount_amount, down_payment, final_price, sale_price, cell_phone, full_name FROM dbo.SoldCarpets Where cell_phone='{0}' order by u_date_tome desc", txt_cell_phone.Text));
            if (dt.Rows.Count > 0)
            {
                gridview.DataSource = dt;
                gridview.DataBind();
            }
            ViewState.Add("table", dt);
        }
    }
}