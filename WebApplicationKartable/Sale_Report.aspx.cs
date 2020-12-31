﻿using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WebApplicationKartable
{
    public partial class Sale_Report : System.Web.UI.Page
    {
        private LoginInfo ubuzhi = new LoginInfo();
        private Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                fill_grid();
                fill_combo();
                //lst_project.Items.Insert(0, new ListItem("---", "0"));
                //lst_provider.Items.Insert(0, new ListItem("---", "0"));
            }
        }
        protected void btn_filter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex == 0 && lst_project.SelectedIndex == 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data("Select srl_f, factor_no,u_date_tome, code_igd, provider_name,size_title,brand_name, area, buy_price, sale_price, discount, discount_amount, final_sale, final_discount, final_price,margin_profit,final_profit2,title_igd FROM SoldCarpets where (from_date between '" + txt_from_date.Text + "' and '" + txt_to_date.Text + "') order by u_date_tome desc");
                Session["Financial"] = dt;
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
                calcute(dt);
            }
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex == 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format("Select srl_f, factor_no, u_date_tome, code_igd, provider_name,size_title,brand_name, area, buy_price, sale_price, discount, discount_amount, final_sale, final_discount, final_price,margin_profit,final_profit2,title_igd FROM SoldCarpets where (from_date between '{0}' and '{1}') And provider_srl={2} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_provider.SelectedValue));
                Session["Financial"] = dt;
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
                calcute(dt);
            }
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex == 0 && lst_project.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format("Select srl_f, factor_no, u_date_tome, code_igd, provider_name,size_title,brand_name, area, buy_price, sale_price, discount, discount_amount, final_sale, final_discount, final_price,margin_profit,final_profit2,title_igd FROM SoldCarpets where (from_date between '{0}' and '{1}') And header_srl={2} order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_project.SelectedValue));
                Session["Financial"] = dt;
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
                calcute(dt);
            }
            else if (!string.IsNullOrEmpty(txt_from_date.Text) && !string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format("Select srl_f, factor_no, u_date_tome, code_igd, provider_name,size_title,brand_name, area, buy_price, sale_price, discount, discount_amount, final_sale, final_discount, final_price,margin_profit,final_profit2,title_igd FROM SoldCarpets where (from_date between '{0}' and '{1}') And (header_srl={2}) And (provider_srl={3}) order by u_date_tome desc", txt_from_date.Text, txt_to_date.Text, lst_project.SelectedValue, lst_provider.SelectedValue));
                Session["Financial"] = dt;
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
                calcute(dt);
            }
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format("Select srl_f, factor_no, u_date_tome, code_igd, provider_name,size_title,brand_name, area, buy_price, sale_price, discount, discount_amount, final_sale, final_discount, final_price,margin_profit,final_profit2,title_igd FROM SoldCarpets where (header_srl={0}) And (provider_srl={1}) order by u_date_tome desc", lst_project.SelectedValue, lst_provider.SelectedValue));
                Session["Financial"] = dt;
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
                calcute(dt);
            }
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex == 0 && lst_project.SelectedIndex > 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format("Select srl_f, factor_no, u_date_tome, code_igd, provider_name,size_title,brand_name, area, buy_price, sale_price, discount, discount_amount, final_sale, final_discount, final_price,margin_profit,final_profit2,title_igd FROM SoldCarpets where (header_srl={0}) order by u_date_tome desc", lst_project.SelectedValue));
                Session["Financial"] = dt;
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
                calcute(dt);
            }
            else if (string.IsNullOrEmpty(txt_from_date.Text) && string.IsNullOrEmpty(txt_to_date.Text) && lst_provider.SelectedIndex > 0 && lst_project.SelectedIndex == 0)
            {
                Search obj = new Search(strConnString); DataTable dt = new DataTable();
                dt = obj.Get_Data(string.Format("Select srl_f, factor_no, u_date_tome, code_igd, provider_name,size_title,brand_name, area, buy_price, sale_price, discount, discount_amount, final_sale, final_discount, final_price,margin_profit,final_profit2,title_igd FROM SoldCarpets where (provider_srl={0}) order by u_date_tome desc", lst_provider.SelectedValue));
                Session["Financial"] = dt;
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
                calcute(dt);
            }
            else
                fill_grid();
        }
        private void fill_grid()
        {
            Search obj = new Search(strConnString); DataTable dt = new DataTable();
            dt = obj.Get_Data("Select srl_f, factor_no, u_date_tome, code_igd, provider_name,size_title,brand_name, area, buy_price, sale_price, discount, discount_amount, final_sale, final_discount, final_price,margin_profit,final_profit2,title_igd FROM SoldCarpets order by u_date_tome desc");
            Session["Financial"] = dt;
            if (dt.Rows.Count > 0)
            {
                gridview.DataSource = dt;
                gridview.DataBind();
                calcute(dt);
            }
        }
        private void CheckLogin()
        {
            if (Request.Cookies["myCookie"] != null)
            {
                if (Request.Cookies["myCookie"]["group_srl"].ToString() != "3") Response.Redirect("~/login.aspx");
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
        private void calcute(DataTable dt)
        {
            Common obj = new Common();
            if (dt.Rows.Count > 0)
            {
                txt_sum.Text = dt.Rows.Count.ToString();
                object purchase;
                purchase = dt.Compute("SUM(buy_price)", "");
                if (!Convert.IsDBNull(purchase))
                {
                    obj.str = purchase.ToString();
                    txt_buy.Text = obj.str;
                }
                object payment;
                payment = dt.Compute("SUM(final_price)", "");
                if (!Convert.IsDBNull(payment))
                {
                    obj.str = payment.ToString();
                    txt_final_price.Text = obj.str;
                }
                object discount_amount;
                discount_amount = dt.Compute("SUM(discount_amount)", "");
                object final_discount;
                final_discount = dt.Compute("SUM(final_discount)", "");
                if (!Convert.IsDBNull(discount_amount) && !Convert.IsDBNull(final_discount))
                {
                    obj.str = (Convert.ToInt64(discount_amount) + Convert.ToInt64(final_discount)).ToString();
                    txt_total_discount.Text = obj.str;
                }
                object margin;
                margin = dt.Compute("SUM(margin_profit)", "");
                if (!Convert.IsDBNull(margin))
                {
                    obj.str = margin.ToString();
                    txt_margin.Text = obj.str;
                }
                object profit;
                profit = dt.Compute("SUM(final_profit2)", "");
                if (!Convert.IsDBNull(profit))
                {
                    obj.str = profit.ToString();
                    txt_profit.Text = obj.str;
                }
            }
            else
            {
                txt_sum.Text = string.Empty;
                txt_buy.Text = string.Empty;
                txt_final_price.Text = string.Empty;
                txt_total_discount.Text = string.Empty;
                txt_margin.Text = string.Empty;
                txt_profit.Text = string.Empty;
            }
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
        }
        protected void imaged_product_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("Project/SaleReportPics.aspx");
        }
    }
}