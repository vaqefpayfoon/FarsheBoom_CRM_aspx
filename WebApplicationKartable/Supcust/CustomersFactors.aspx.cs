using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;


namespace WebApplicationKartable
{
    public partial class CustomersFactors : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT full_name FROM dbo.bas_supcust where full_name like '%'+ @SearchText + '%'", prefixText, count);
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

        protected void btn_cell_phone_finder_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.bas_supcust where cell_phone='" + txt_cell_phone.Text + "'");
            if (dt.Rows.Count > 0)
                fill_grid(dt.Rows[0][0].ToString());
            else
                lblError.Text = "تلفن همراه وارده موجود نیست";
        }
        protected void report_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.bas_supcust where full_name='" + txtContactsSearch.Text + "'");
            if (dt.Rows.Count > 0)
                fill_grid(dt.Rows[0][0].ToString());
        }
        private void fill_grid(string srl)
        {
            Search obj = new Search(strConnString); DataTable dt = new DataTable();
            dt = obj.Get_Data(string.Format("Select srl_f, factor_no, u_date_tome, code_igd, provider_name,size_title,brand_name, area, buy_price, sale_price, discount, discount_amount, final_sale, final_discount, final_price,margin_profit,final_profit2,title_igd FROM SoldCarpets Where bassc_srl={0} order by u_date_tome desc", srl));
            if (dt.Rows.Count > 0)
            {
                gridview.DataSource = dt;
                gridview.DataBind();
            }
        }
    }
}