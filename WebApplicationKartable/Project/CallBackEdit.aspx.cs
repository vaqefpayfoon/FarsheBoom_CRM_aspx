using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationKartable
{
    public partial class CallBackEdit : System.Web.UI.Page
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
            fill_grid();
        }
        private void fill_grid()
        {
            Common obo = new Common();
            DataTable dt = new DataTable(); Search obj = new Search
                (strConnString);
            if (chk_all.Checked)
            {
                dt = obj.Get_Data(string.Format("SELECT igd_srl,code_igd, provider_code, brand_name, size_title, carpet_title, porz_title, chele_title, lenght, widht, ROUND(lenght * widht / 10000, 2) AS area, color_srl2, plan_title, color_name, buy_price, ROUND(buy_price / NULLIF(ROUND(lenght * widht / 10000, 1), 0),0) AS u_buy,title_igd FROM dbo.Project_Goods_View Where  header_srl={0} order by {1}", lst_project.SelectedValue, lst_sort.SelectedValue));
            }
            else
            {
                dt = obj.Get_Data(string.Format("SELECT igd_srl,code_igd, provider_code, brand_name, size_title, carpet_title, porz_title, chele_title, lenght, widht, ROUND(lenght * widht / 10000, 2) AS area, color_srl2, plan_title, color_name, buy_price, ROUND(buy_price / NULLIF(ROUND(lenght * widht / 10000, 1), 0),0) AS u_buy,title_igd FROM dbo.Project_Goods_View Where  header_srl={0} And provider_srl={1} order by {2}", lst_project.SelectedValue, lst_provider.SelectedValue, lst_sort.SelectedValue));
            }
            if (dt.Rows.Count > 0)
            {
                txt_count.Text = dt.Rows.Count.ToString();
                gridview.DataSource = dt;
                gridview.DataBind();

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
        protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview.PageIndex = e.NewPageIndex;
            fill_grid();
        }
        protected void ImageButton_delete_Click(object sender, ImageClickEventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridview.Rows)
            {
                string srl = gridview.Rows[count].Cells[0].Text;
                CheckBox check = (CheckBox)row.FindControl("chk_delete");
                if (check.Checked)
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@header_srl", SqlDbType.Int);
                    param[0].Value = Convert.ToInt32(lst_project.SelectedValue);
                    param[1] = new SqlParameter("@igd_srl", SqlDbType.Int);
                    param[1].Value = Convert.ToInt32(srl);
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
                count++;
            }
            fill_grid();
        }
        
    }
}