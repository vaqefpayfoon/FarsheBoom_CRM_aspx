﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;

namespace WebApplicationKartable
{
    public partial class Opportunity_Managment : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl, project_name,from_date FROM dbo.bas_project where (confirm is null) or (confirm='False') ");
                grid.DataSource = dt; grid.DataBind();
            }
        }
        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, project_name,from_date FROM dbo.bas_project where (confirm is null) or (confirm='False')");
            grid.DataSource = dt; grid.DataBind();
            mp1.Show();
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT project_name FROM dbo.bas_project where project_name like '%'+ @SearchText + '%'", prefixText, count);
        }
        protected void CustomersGridView_SelectedIndexChanging(Object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = grid.Rows[e.NewSelectedIndex];
            txtContactsSearch.Text = row.Cells[1].Text.ToString();
            txt_code.Text = row.Cells[0].Text.ToString();
        }
        protected void edit_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.bas_project where project_name='" + txtContactsSearch.Text + "'");
            if (dt.Rows.Count > 0)
                Response.Redirect("Opportunity.aspx?srl=" + dt.Rows[0][0]);
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
    }
}