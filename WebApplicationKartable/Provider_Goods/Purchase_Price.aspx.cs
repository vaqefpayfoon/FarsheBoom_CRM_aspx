using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;

namespace WebApplicationKartable
{
    public partial class Purchase_Price : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if(!IsPostBack)
            {
                if (ubuzhi.group_srl == "3")
                {
                    fill_grid(Request.QueryString["snd"]);
                    ViewState["update"] = "0";
                    ViewState["igd_srl"] = Request.QueryString["snd"];
                    search_panel.Enabled = true;
                }
            }
        }
        private void fill_grid(string srl)
        {
            Search obj = new Search(strConnString); DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl, u_date_time, price, active from inv_purchase_price where igd_srl = " + srl);
            if (dt.Rows.Count > 0)
            {
                grid.DataSource = dt;
                grid.DataBind();
            }
        }
        private bool woak_for_active()
        {
            bool bln = false;
            Search obj = new Search(strConnString); DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl, active from inv_purchase_price where igd_srl = " + ViewState["igd_srl"]);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                    if (!Convert.IsDBNull(row["active"]))
                    {
                        if (Convert.ToBoolean(row["active"]))
                        {
                            if (ViewState["update"].ToString().Equals("0"))
                            {
                                bln = true;
                                return bln;
                            }
                            else
                            {
                                if (ViewState["srl"].ToString().Equals(row["srl"]))
                                    return false;
                                else
                                {
                                    bln = true;
                                    return bln;
                                }
                            }
                        }
                    }
                    else
                        return bln;
            }            
                return bln;
        }
        protected void btn_save_Click(object sender, ImageClickEventArgs e)
        {
            if(chk_active.Checked)
            {
                if(woak_for_active())
                {
                    lblError.Text = "یک قیمت فعال وجود دارد لطفا آن را غیر فعال کنید";
                    return;
                }
            }
            if (ViewState["update"].ToString().Equals("0"))
                insert();
            else
                update();
            fill_grid(Request.QueryString["snd"]);
        }
        protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get_data2(ViewState["srl"].ToString(),gridview.SelectedRow.Cells[0].Text.Trim());
            lblError.Text = string.Empty;
            Search obj = new Search(strConnString);
            DataTable dt = new DataTable();
            {
                dt = obj.Get_Data("SELECT srl, u_date_time, price, active from inv_purchase_price  Where srl = " + grid.SelectedRow.Cells[0].Text.Trim());
                DataRow row = dt.Rows[0];
                ViewState["update"] = "1";
                ViewState["srl"] = row["srl"].ToString();
                txt_from_date.Text = row["u_date_time"].ToString();
                txt_price.Text = row["price"].ToString();
                if (!Convert.IsDBNull(row["active"]))
                    chk_active.Checked = Convert.ToBoolean(row["active"]);
                else
                    chk_active.Checked = false;
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
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.inv_purchase_price";
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
            try
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
                new ManageCommands(param, "del_purchase_price");
                lblError.Text = "لینک جاری حذف شد";
                fill_grid(Request.QueryString["snd"]);
            }
            catch { lblError.Text = "عملیات ناموفق"; }
        }
        private Common o1 = new Common();
        private void insert()
        {
            int srl = max_srl();
            if (lblError.Text == "نمایشگاه جدید ایجاد شد")
                return;
            string price = o1.remove_cama(txt_price.Text);
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@igd_srl", SqlDbType.Int);
            param[1].Value = Convert.ToInt32(Request.QueryString["snd"]);
            param[2] = new SqlParameter("@u_date_time", SqlDbType.Char,10);
            param[2].Value = txt_from_date.Text;
            param[3] = new SqlParameter("@price", SqlDbType.BigInt);
            param[3].Value = Convert.ToInt64(price);
            param[4] = new SqlParameter("@active", SqlDbType.Bit);
            param[4].Value = chk_active.Checked;
            new ManageCommands(param, "insert_purchase_price");
            lblError.Text = "مبلغ جدید ایجاد شد";
        }
        private void update()
        {
            string price = o1.remove_cama(txt_price.Text);
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ViewState["srl"]);
            param[1] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[1].Value = txt_from_date.Text;
            param[2] = new SqlParameter("@price", SqlDbType.BigInt);
            param[2].Value = Convert.ToInt64(price);
            param[3] = new SqlParameter("@active", SqlDbType.Bit);
            param[3].Value = chk_active.Checked;
            new ManageCommands(param, "update_purchase_price");
            lblError.Text = "مبلغ جاری ویرایش شد";
        }
        protected void ImageButton_addnew_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["update"] = 0;
            txt_from_date.Text = string.Empty;
            txt_price.Text = string.Empty;
            chk_active.Checked = false;
        }
        protected void btn_return_Click(object sender, ImageClickEventArgs e)
        {
            string srl = Session["purchase"].ToString();
            Session.Remove("purchase");
            Response.Redirect("Provider.aspx?srl=" + srl);
        }
    }
}