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
    public partial class RejectFactor : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btn_report_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl,factor_no,u_date_tome,code_igd,payment,chele_title,carpet_title FROM dbo.Factor_View where reject='true' order by factor_no desc");
            gridview.DataSource = dt; gridview.DataBind();
        }

        protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}