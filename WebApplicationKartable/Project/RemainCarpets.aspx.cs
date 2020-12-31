using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplicationKartable
{
    public partial class RemainCarpets : System.Web.UI.Page
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
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data(string.Format("SELECT dbo.inv_goods.code_igd, dbo.inv_brand.brand_name, dbo.inv_carpet.carpet_title, dbo.inv_porz.porz_title, dbo.inv_plan.plan_title, dbo.inv_chele.chele_title, dbo.inv_size.size_title, dbo.inv_goods.title_igd FROM dbo.inv_goods LEFT OUTER JOIN dbo.inv_plan ON dbo.inv_goods.city_srl = dbo.inv_plan.srl LEFT OUTER JOIN dbo.inv_porz ON dbo.inv_goods.porz_type = dbo.inv_porz.srl LEFT OUTER JOIN dbo.inv_chele ON dbo.inv_goods.chele_type = dbo.inv_chele.srl LEFT OUTER JOIN dbo.inv_carpet ON dbo.inv_goods.carpet_type = dbo.inv_carpet.srl LEFT OUTER JOIN dbo.inv_brand ON dbo.inv_goods.ibt_srl = dbo.inv_brand.srl LEFT OUTER JOIN dbo.inv_size ON dbo.inv_goods.size_srl = dbo.inv_size.srl WHERE(provider_srl = {1}) AND(build_state = 0) AND (code_igd NOT IN (SELECT inv_goods_1.code_igd FROM dbo.bas_project LEFT OUTER JOIN dbo.bas_project_goods ON dbo.bas_project.srl = dbo.bas_project_goods.header_srl LEFT OUTER JOIN dbo.inv_goods AS inv_goods_1 ON dbo.bas_project_goods.igd_srl = inv_goods_1.srl WHERE(dbo.bas_project.srl = {0}) AND(inv_goods_1.provider_srl = {1}))) order by code_igd", lst_project.SelectedValue, lst_provider.SelectedValue));

            gridview.DataSource = dt;gridview.DataBind();
            txt_count.Text = dt.Rows.Count.ToString();
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

        protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["srl_good"] = get_goods_srl(gridview.SelectedRow.Cells[0].Text.Trim());
        }
        protected void btn_assign_Click(object sender, EventArgs e)
        {
            if (ViewState["srl_good"] == null)
            {
                lblError.Text = "فرشی بارگذاری نشده است";
                return;
            }
            int srl = max_srl_details();
            if (!Duplicate_GoodsInproject(ViewState["srl_good"].ToString(), lst_project.SelectedValue))
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@srl", SqlDbType.Int);
                param[0].Value = srl;
                param[1] = new SqlParameter("@header_srl", SqlDbType.Int);
                param[1].Value = Convert.ToInt32(lst_project.SelectedValue);
                param[2] = new SqlParameter("@igd_srl", SqlDbType.Int);
                param[2].Value = Convert.ToInt32(ViewState["srl_good"]);
                new ManageCommands(param, "insert_project_goods");
                SqlConnection con = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update inv_goods set selection=@selection where srl=@srl;";
                cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(ViewState["srl_good"]);
                cmd.Parameters.Add("@selection", SqlDbType.Bit).Value = true;
                cmd.ExecuteNonQuery();
                con.Close();
                lblError.Text = "تخصیص انجام شد";
            }
            else
            {
                lblError.Text = "فرش انتخاب شده در همین نمایشگاه موجود می باشد";
                return;
            }
        }
        protected void btn_delete_assign_Click(object sender, EventArgs e)
        {
            if (ViewState["srl_good"] == null)
            {
                lblError.Text = "فرشی بارگذاری نشده است";
                return;
            }
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@header_srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(lst_project.SelectedValue);
            param[1] = new SqlParameter("@igd_srl", SqlDbType.Int);
            param[1].Value = Convert.ToInt32(ViewState["srl_good"]);
            new ManageCommands(param, "del_project_goods");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update inv_goods set selection=@selection where srl=@srl;";
            cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(ViewState["srl_good"]);
            cmd.Parameters.Add("@selection", SqlDbType.Bit).Value = false;
            cmd.ExecuteNonQuery();
            con.Close();
            lblError.Text = "تخصیص حذف شد";
        }
        private int max_srl_details()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.bas_project_goods";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!Convert.IsDBNull(dr[0]))
                {
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
        private bool Duplicate_GoodsInproject(string igd, string header_srl)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_GoodsInproject(" + igd + "," + header_srl + ")";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0)
                {
                    dr.Close(); dr.Dispose();
                    return true;
                }
                else
                {
                    dr.Close(); dr.Dispose();
                    return false;
                }
            }
            else
            {
                dr.Close(); dr.Dispose();
                return false;
            }
        }
        private string get_goods_srl(string code)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data(string.Format("SELECT srl FROM inv_goods where code_igd={0}", code));
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return string.Empty;
        }
    }
}