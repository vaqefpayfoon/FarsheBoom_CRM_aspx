﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


namespace WebApplicationKartable
{
    public partial class Sizes : System.Web.UI.Page
    {
        private LoginInfo ubuzhi = new LoginInfo();
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                BindData();
            }
        }
        private void BindData()
        {
            string strQuery = "SELECT srl, size_title FROM dbo.inv_size";
            SqlCommand cmd = new SqlCommand(strQuery);
            GridView1.DataSource = GetData(cmd);
            GridView1.DataBind();
        }
        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            if (!(dt.Rows.Count > 0))
            {
                DataRow row = dt.NewRow();
                row["srl"] = 0;
                row["size_title"] = "...";
                dt.Rows.Add(row);
            }
            return dt;
        }
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
        protected void AddNewCustomer(object sender, EventArgs e)
        {
            string size_title = ((TextBox)GridView1.FooterRow.FindControl("txt_size_title_add")).Text;
            int srl = max_srl();
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into inv_size(srl,size_title) " +
            "values(@srl,@size_title);" +
             "SELECT srl, size_title FROM dbo.inv_size";
            cmd.Parameters.Add("@srl", SqlDbType.Int).Value = srl;
            cmd.Parameters.Add("@size_title", SqlDbType.VarChar, 30).Value = size_title;
            GridView1.DataSource = GetData(cmd);
            GridView1.DataBind();
            lblError.Text = "";
        }
        protected void DeleteCustomer(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkRemove = (LinkButton)sender;
                SqlConnection con = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from inv_size where " +
                "srl=@srl;" +
                "SELECT srl, size_title FROM dbo.inv_size";
                cmd.Parameters.Add("@srl", SqlDbType.Int).Value = lnkRemove.CommandArgument;
                GridView1.DataSource = GetData(cmd);
                GridView1.DataBind();
            }
            catch
            {
                lblError.Text = "این اطلاعات در بانک استفاده شده است";
            }
        }
        protected void EditCustomer(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindData();
        }
        protected void UpdateCustomer(object sender, GridViewUpdateEventArgs e)
        {
            string srl = ((Label)GridView1.Rows[e.RowIndex].FindControl("lbl_srl")).Text;
            string size_title = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_size_title")).Text;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update inv_size set size_title=@size_title " +
             "where srl=@srl;" +
             "Select srl,size_title From dbo.inv_size";
            cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(srl);
            cmd.Parameters.Add("@size_title", SqlDbType.VarChar, 30).Value = size_title;
            GridView1.EditIndex = -1;
            GridView1.DataSource = GetData(cmd);
            GridView1.DataBind();
            lblError.Text = "";
        }
        private void CheckLogin()
        {
            if (Request.Cookies["myCookie"] != null)
            {
                if (Request.Cookies["myCookie"]["srl"] != null)
                    ubuzhi.srl = Request.Cookies["myCookie"]["srl"];
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
                    ubuzhi.group_srl = Request.Cookies["myCookie"]["group_srl"];
                else
                    Response.Redirect("../login.aspx");
            }
        }
        private int max_srl()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.inv_size";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!Convert.IsDBNull(dr[0]))
                {
                    ViewState["srl"] = dr[0];
                    return Convert.ToInt32(dr[0]);
                }
                else
                    return 1;
            }
            else
            {
                dr.Close(); dr.Dispose();
                return 1;
            }
        }
    }
}