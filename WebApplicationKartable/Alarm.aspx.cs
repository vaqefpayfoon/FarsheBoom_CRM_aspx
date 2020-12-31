using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;

namespace WebApplicationKartable
{
    public partial class Alarm : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                if(Request.QueryString["srl"] != null)
                {
                    Search obj = new Search(strConnString);
                    DataTable dt = new DataTable();
                    dt = obj.Get_Data("SELECT srl, alarm_subject, date_time, describtion from hpl_alarm Where srl = " + Request.QueryString["srl"]);
                    DataRow row = dt.Rows[0];
                    ViewState["update"] = "1";
                    ViewState["srl"] = row["srl"].ToString();
                    txt_from_date.Text = row["date_time"].ToString();
                    txt_subject.Text = row["alarm_subject"].ToString();
                    txt_desc.Text = row["describtion"].ToString();
                }
                else
                    ViewState["update"] = "0";
                fill_grid(ubuzhi.srl);                
            }
        }
        private void fill_grid(string srl)
        {
            Search obj = new Search(strConnString); DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl, alarm_subject, date_time, describtion from hpl_alarm where hpl_srl = " + srl);
            if (dt.Rows.Count > 0)
            {
                grid.DataSource = dt;
                grid.DataBind();
            }
        }
        protected void btn_save_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["update"].ToString().Equals("0"))
                insert();
            else
                update();
            fill_grid(ubuzhi.srl);
        }
        protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get_data2(ViewState["srl"].ToString(),gridview.SelectedRow.Cells[0].Text.Trim());
            lblError.Text = string.Empty;
            Search obj = new Search(strConnString);
            DataTable dt = new DataTable();
            {
                dt = obj.Get_Data("SELECT srl, alarm_subject, date_time, describtion from hpl_alarm Where srl = " + grid.SelectedRow.Cells[0].Text.Trim());
                DataRow row = dt.Rows[0];
                ViewState["update"] = "1";
                ViewState["srl"] = row["srl"].ToString();
                txt_from_date.Text = row["date_time"].ToString();
                txt_subject.Text = row["alarm_subject"].ToString();
                txt_desc.Text = row["describtion"].ToString();
            }
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
        private int max_srl()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.hpl_alarm";
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
        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            if (ViewState["srl"] == null)
            {
                lblError.Text = "عملیات ناموفق";
                return;
            }
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(lnkRemove.CommandArgument);
            new ManageCommands(param, "del_alarm");
            lblError.Text = "لینک جاری حذف شد";
            fill_grid(ubuzhi.srl);
        }
        private Common o1 = new Common();
        private void insert()
        {
            int srl = max_srl();
            if (lblError.Text == "آلارم جدید ایجاد شد")
                return;
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@hpl_srl", SqlDbType.Int);
            param[1].Value = ubuzhi.srl;
            param[2] = new SqlParameter("@alarm_subject", SqlDbType.VarChar,100);
            param[2].Value = txt_subject.Text;
            param[3] = new SqlParameter("@date_time", SqlDbType.Char, 10);
            param[3].Value = txt_from_date.Text;
            param[4] = new SqlParameter("@describtion", SqlDbType.VarChar);
            param[4].Value = txt_desc.Text;
            new ManageCommands(param, "insert_alarm");
            lblError.Text = "آلارم جدید ایجاد شد";
        }
        private void update()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = ViewState["srl"];
            param[1] = new SqlParameter("@alarm_subject", SqlDbType.VarChar, 100);
            param[1].Value = txt_subject.Text;
            param[2] = new SqlParameter("@date_time", SqlDbType.Char, 10);
            param[2].Value = txt_from_date.Text;
            param[3] = new SqlParameter("@describtion", SqlDbType.VarChar);
            param[3].Value = txt_desc.Text;
            new ManageCommands(param, "update_alarm");
            lblError.Text = "آلارم جدید ویرایش شد";
        }
        protected void btn_new_Click(object sender, ImageClickEventArgs e)
        {
            txt_desc.Text = string.Empty;
            txt_from_date.Text = string.Empty;
            txt_subject.Text = string.Empty;
        }
    }
}