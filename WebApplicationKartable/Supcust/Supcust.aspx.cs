using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebApplicationKartable
{
    public partial class Supcust : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                if (Request.QueryString["srl"] != "-1")
                    set_boxes(Request.QueryString["srl"].ToString());
                ViewState["update"] = "0"; ViewState["update2"] = "0";
            }
        }
        private void set_boxes(string srl)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, u_date_time, full_name, city_srl, meet_srl, meet_locate, tel1, cell_phone, instagram, sex, age, address1, email, describtion, clue_srl FROM dbo.bas_supcust WHERE srl=" + srl);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ViewState["srl"] = row["srl"].ToString();
                txt_u_date_time.Text = row["u_date_time"].ToString();
                txt_full_name.Text = row["full_name"].ToString();
                ViewState["full_name"] = row["full_name"].ToString();
                if(!Convert.IsDBNull(row["city_srl"]))
                    lst_city.SelectedValue = row["city_srl"].ToString();
                lst_meet.SelectedValue = row["meet_srl"].ToString();
                txt_tel1.Text = row["tel1"].ToString();
                ViewState["tel1"] = row["tel1"].ToString();
                txt_cell_phone.Text = row["cell_phone"].ToString();
                ViewState["cell_phone"] = row["cell_phone"].ToString();
                lst_sex.SelectedValue = row["sex"].ToString();
                lst_age.SelectedValue = row["age"].ToString();
                txt_address.Text = row["address1"].ToString();
                txt_desc.Text = row["describtion"].ToString();
                lst_clue.SelectedValue = row["clue_srl"].ToString();
                fill_grid(ViewState["srl"].ToString()); fill_grid2(ViewState["srl"].ToString());
            }
        }
        protected void btn_save_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.QueryString["srl"] == "-1")
                insert();
            else
                update();
        }
        private void insert()
        {
            //if (duplicate_supcust_name())
            //{
            //    lblError.Text = "نام مرکز تکراری است";
            //    return;
            //}
            if (duplicate_supcust_tel1())
            {
                lblError.Text = "تلفن مشتری تکراری است";
                return;
            }
            if (duplicate_supcust_cell_phone())
            {
                lblError.Text = "تلفن همراه مشتری تکراری است";
                return;
            }
            int srl = max_srl();
            if (lblError.Text == "تامین کننده جدید ایجاد شد")
                return;
            Common per = new Common();
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[1].Value = per.persian_date();
            param[2] = new SqlParameter("@full_name", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_full_name.Text))
                param[2].Value = txt_full_name.Text;
            else
                param[2].Value = DBNull.Value;
            param[3] = new SqlParameter("@city_srl", SqlDbType.Int);
            if (lst_city.SelectedIndex == 0)
                param[3].Value = DBNull.Value;
            else
                param[3].Value = lst_city.SelectedValue;
            param[4] = new SqlParameter("@meet_srl", SqlDbType.Int);
            param[4].Value = lst_meet.SelectedValue;
            param[5] = new SqlParameter("@meet_locate", SqlDbType.VarChar, 50);
            param[5].Value = DBNull.Value;
            param[6] = new SqlParameter("@tel1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel1.Text))
                param[6].Value = txt_tel1.Text;
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@cell_phone", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_cell_phone.Text))
                param[7].Value = txt_cell_phone.Text;
            else
                param[7].Value = DBNull.Value;
            param[8] = new SqlParameter("@instagram", SqlDbType.VarChar, 50);
            param[8].Value = DBNull.Value;
            param[9] = new SqlParameter("@sex", SqlDbType.Char, 1);
            param[9].Value = lst_sex.SelectedValue;
            param[10] = new SqlParameter("@age", SqlDbType.Char, 3);
            param[10].Value = lst_age.SelectedValue;
            param[11] = new SqlParameter("@user_name", SqlDbType.VarChar, 10);
            param[11].Value = DBNull.Value;
            param[12] = new SqlParameter("@pass", SqlDbType.VarChar, 10);
            param[12].Value = DBNull.Value;
            param[13] = new SqlParameter("@address1", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_address.Text))
                param[13].Value = txt_address.Text;
            else
                param[13].Value = DBNull.Value;
            param[14] = new SqlParameter("@email", SqlDbType.VarChar, 100);
            param[14].Value = DBNull.Value;
            param[15] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_desc.Text))
                param[15].Value = txt_desc.Text;
            else
                param[15].Value = DBNull.Value;
            param[16] = new SqlParameter("@clue_srl", SqlDbType.Int);
            param[16].Value = lst_clue.SelectedValue;
            new ManageCommands(param, "insert_supcust");
            lblError.Text = "مشتری جدید ایجاد شد";
            emptyboxex();
        }
        private void update()
        {
            //if (duplicate_supcust_name_update())
            //{
            //    lblError.Text = "نام مرکز تکراری است";
            //    return;
            //}
            if (duplicate_supcust_tel1_update())
            {
                lblError.Text = "تلفن مشتری تکراری است";
                return;
            }
            if (duplicate_supcust_cell_phone_update())
            {
                lblError.Text = "تلفن همراه مشتری تکراری است";
                return;
            }
            Common per = new Common();
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ViewState["srl"]);
            param[1] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[1].Value = per.persian_date();
            param[2] = new SqlParameter("@full_name", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_full_name.Text))
                param[2].Value = txt_full_name.Text;
            else
                param[2].Value = DBNull.Value;
            param[3] = new SqlParameter("@city_srl", SqlDbType.Int);
            if (lst_city.SelectedIndex == 0)
                param[3].Value = DBNull.Value;
            else
                param[3].Value = lst_city.SelectedValue;
            param[4] = new SqlParameter("@meet_srl", SqlDbType.Int);
            param[4].Value = lst_meet.SelectedValue;
            param[5] = new SqlParameter("@meet_locate", SqlDbType.VarChar, 50);
            param[5].Value = DBNull.Value;
            param[6] = new SqlParameter("@tel1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel1.Text))
                param[6].Value = txt_tel1.Text;
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@cell_phone", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_cell_phone.Text))
                param[7].Value = txt_cell_phone.Text;
            else
                param[7].Value = DBNull.Value;
            param[8] = new SqlParameter("@instagram", SqlDbType.VarChar, 50);
            param[8].Value = DBNull.Value;
            param[9] = new SqlParameter("@sex", SqlDbType.Char, 1);
            param[9].Value = lst_sex.SelectedValue;
            param[10] = new SqlParameter("@age", SqlDbType.Char, 3);
            param[10].Value = lst_age.SelectedValue;
            param[11] = new SqlParameter("@user_name", SqlDbType.VarChar, 10);
            param[11].Value = DBNull.Value;
            param[12] = new SqlParameter("@pass", SqlDbType.VarChar, 10);
            param[12].Value = DBNull.Value;
            param[13] = new SqlParameter("@address1", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_address.Text))
                param[13].Value = txt_address.Text;
            else
                param[13].Value = DBNull.Value;
            param[14] = new SqlParameter("@email", SqlDbType.VarChar, 100);
            param[14].Value = DBNull.Value;
            param[15] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_desc.Text))
                param[15].Value = txt_desc.Text;
            else
                param[15].Value = DBNull.Value;
            param[16] = new SqlParameter("@clue_srl", SqlDbType.Int);
            param[16].Value = lst_clue.SelectedValue;
            new ManageCommands(param, "update_supcust");
            lblError.Text = "مشتری جاری ویرایش شد";
        }
        private int max_srl()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.bas_supcust";
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
        private void emptyboxex()
        {
            txt_full_name.Text = string.Empty;
            lst_city.SelectedIndex = 0;
            lst_meet.SelectedIndex = 0;
            txt_tel1.Text = string.Empty;
            txt_cell_phone.Text = string.Empty;
            lst_sex.SelectedIndex = 0;
            lst_age.SelectedIndex = 0;
            txt_address.Text = string.Empty;
            txt_desc.Text = string.Empty;
            lst_clue.SelectedIndex = 0;
        }
        protected void ImageButton_save_Click(object sender, ImageClickEventArgs e)
        {
            if(lst_color.SelectedIndex == 0 || lst_size.SelectedIndex == 0)
            {
                lblError.Text = "رنگ متن و اندازه فرش اجباریست";
                return;
            }
            if (ViewState["update"].ToString().Equals("0"))
                insert_goods();
            else
                update_goods();
            fill_grid(ViewState["srl"].ToString());
        }
        protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search obj = new Search(strConnString);
            DataTable dt = new DataTable();
            {
                dt = obj.Get_Data("SELECT srl, bassc_srl, city_srl, size_srl, color_srl, u_date_time, carpet_type, from_price, to_price, describtion, opportunity_srl FROM dbo.bas_supcust_order Where srl = " + gridview.SelectedRow.Cells[0].Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    ViewState["update"] = "1";
                    ViewState["srl_details"] = gridview.SelectedRow.Cells[0].Text.Trim();
                    if (!Convert.IsDBNull(row["city_srl"]))
                        lst_city2.SelectedValue = row["city_srl"].ToString();
                    else
                        lst_city2.SelectedIndex = 0;
                    if (!Convert.IsDBNull(row["size_srl"]))
                        lst_size.SelectedValue = row["size_srl"].ToString();
                    else
                        lst_size.SelectedIndex = 0;
                    if (!Convert.IsDBNull(row["color_srl"]))
                        lst_color.SelectedValue = row["color_srl"].ToString();
                    else
                        lst_color.SelectedIndex = 0;
                    if (!Convert.IsDBNull(row["carpet_type"]))
                        lst_carpet.SelectedValue = row["carpet_type"].ToString();
                    else
                        lst_carpet.SelectedIndex = 0;
                    txt_from_price.Text = row["from_price"].ToString();
                    txt_to_price.Text = row["to_price"].ToString();
                    txt_describtion.Text = row["describtion"].ToString();
                }
            }
        }
        private void fill_grid(string srl)
        {
            Search obj = new Search(strConnString); DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT  srl, bassc_srl, u_date_time, brand_name, size_title, color_name, carpet_title FROM  dbo.Supcust_Order_View Where bassc_srl = " + srl);
            if (dt.Rows.Count > 0)
            {
                gridview.DataSource = dt;
                gridview.DataBind();
            }
        }
        private void insert_goods()
        {
            if (ViewState["srl"] == null)
            {
                lblError.Text = "تامین کننده ای یافت نشد ، لطفا تامین کننده را ذخیره کنید";
                return;
            }
            if((!string.IsNullOrEmpty(txt_from_price.Text) && string.IsNullOrEmpty(txt_to_price.Text)) || (string.IsNullOrEmpty(txt_from_price.Text) && !string.IsNullOrEmpty(txt_to_price.Text)))
            {
                lblError.Text = "مبلغ ها نامعتبر است";
                return;
            }
            double dblfrom_price = 0;
            double dblto_price = 0;
            if (!string.IsNullOrEmpty(txt_from_price.Text))
                dblfrom_price = Convert.ToDouble(txt_from_price.Text);
            if (!string.IsNullOrEmpty(txt_to_price.Text))
                dblto_price = Convert.ToDouble(txt_to_price.Text);
            if(dblfrom_price > dblto_price)
            {
                lblError.Text = "مبلغ ها نامعتبر است";
                return;
            }
            Common obj = new Common();
            string from_price = obj.remove_cama(txt_from_price.Text);
            string to_price = obj.remove_cama(txt_to_price.Text);
            int srl = max_srl_supcust_order();
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@bassc_srl", SqlDbType.Int);
            param[1].Value = Convert.ToInt32(ViewState["srl"]);
            param[2] = new SqlParameter("@city_srl", SqlDbType.Int);
            if (lst_city2.SelectedIndex == 0)
                param[2].Value = DBNull.Value;
            else
                param[2].Value = Convert.ToInt32(lst_city2.SelectedValue);
            param[3] = new SqlParameter("@size_srl", SqlDbType.Int);
            if (lst_size.SelectedIndex == 0)
                param[3].Value = DBNull.Value;
            else
                param[3].Value = Convert.ToInt32(lst_size.SelectedValue);
            param[4] = new SqlParameter("@color_srl", SqlDbType.Int);
            if (lst_color.SelectedIndex == 0)
                param[4].Value = DBNull.Value;
            else
                param[4].Value = Convert.ToInt32(lst_color.SelectedValue);
            param[5] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[5].Value = obj.persian_date();
            param[6] = new SqlParameter("@carpet_type", SqlDbType.TinyInt);
            if (lst_carpet.SelectedIndex == 0)
                param[6].Value = DBNull.Value;
            else
                param[6].Value = Convert.ToInt32(lst_carpet.SelectedValue);
            param[7] = new SqlParameter("@from_price", SqlDbType.BigInt);
            if (!string.IsNullOrEmpty(from_price))
                param[7].Value = Convert.ToInt64(from_price);
            else
                param[7].Value = 0;
            param[8] = new SqlParameter("@to_price", SqlDbType.BigInt);
            if (!string.IsNullOrEmpty(to_price))
                param[8].Value = Convert.ToInt64(to_price);
            else
                param[8].Value = 0;
            param[9] = new SqlParameter("@describtion", SqlDbType.VarChar,100);
            if (!string.IsNullOrEmpty(txt_describtion.Text))
                param[9].Value = txt_describtion.Text;
            else
                param[9].Value = DBNull.Value;
            param[10] = new SqlParameter("@opportunity_srl", SqlDbType.Int);
            param[10].Value = DBNull.Value;
            param[11] = new SqlParameter("@plan_srl", SqlDbType.Int);
            if (lst_plan.SelectedIndex == 0)
                param[11].Value = DBNull.Value;
            else
                param[11].Value = Convert.ToInt32(lst_plan.SelectedValue);
            new ManageCommands(param, "insert_supcust_order");
            lblError.Text = "سفارش جدید ایجاد شد";
        }
        private void update_goods()
        {
            if ((!string.IsNullOrEmpty(txt_from_price.Text) && string.IsNullOrEmpty(txt_to_price.Text)) || (string.IsNullOrEmpty(txt_from_price.Text) && !string.IsNullOrEmpty(txt_to_price.Text)))
            {
                lblError.Text = "مبلغ ها نامعتبر است";
                return;
            }
            double dblfrom_price = 0;
            double dblto_price = 0;
            if (!string.IsNullOrEmpty(txt_from_price.Text))
                dblfrom_price = Convert.ToDouble(txt_from_price.Text);
            if (!string.IsNullOrEmpty(txt_to_price.Text))
                dblto_price = Convert.ToDouble(txt_to_price.Text);
            if (dblfrom_price > dblto_price)
            {
                lblError.Text = "مبلغ ها نامعتبر است";
                return;
            }
            Common obj = new Common();
            string from_price = obj.remove_cama(txt_from_price.Text);
            string to_price = obj.remove_cama(txt_to_price.Text);
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = ViewState["srl_details"];
            param[1] = new SqlParameter("@bassc_srl", SqlDbType.Int);
            param[1].Value = Convert.ToInt32(ViewState["srl"]);
            param[2] = new SqlParameter("@city_srl", SqlDbType.Int);
            if (lst_city2.SelectedIndex == 0)
                param[2].Value = DBNull.Value;
            else
                param[2].Value = Convert.ToInt32(lst_city2.SelectedValue);
            param[3] = new SqlParameter("@size_srl", SqlDbType.Int);
            if (lst_size.SelectedIndex == 0)
                param[3].Value = DBNull.Value;
            else
                param[3].Value = Convert.ToInt32(lst_size.SelectedValue);
            param[4] = new SqlParameter("@color_srl", SqlDbType.Int);
            if (lst_color.SelectedIndex == 0)
                param[4].Value = DBNull.Value;
            else
                param[4].Value = Convert.ToInt32(lst_color.SelectedValue);
            param[5] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[5].Value = obj.persian_date();
            param[6] = new SqlParameter("@carpet_type", SqlDbType.TinyInt);
            if (lst_carpet.SelectedIndex == 0)
                param[6].Value = DBNull.Value;
            else
                param[6].Value = Convert.ToInt32(lst_carpet.SelectedValue);
            param[7] = new SqlParameter("@from_price", SqlDbType.BigInt);
            if (!string.IsNullOrEmpty(from_price))
                param[7].Value = Convert.ToInt64(from_price);
            else
                param[7].Value = 0;
            param[8] = new SqlParameter("@to_price", SqlDbType.BigInt);
            if (!string.IsNullOrEmpty(to_price))
                param[8].Value = Convert.ToInt64(to_price);
            else
                param[8].Value = 0;
            param[9] = new SqlParameter("@describtion", SqlDbType.VarChar, 100);
            if(!string.IsNullOrEmpty(txt_describtion.Text))
                param[9].Value = txt_describtion.Text;
            else
                param[9].Value = DBNull.Value;
            param[10] = new SqlParameter("@opportunity_srl", SqlDbType.Int);
            param[10].Value = DBNull.Value;
            param[11] = new SqlParameter("@plan_srl", SqlDbType.Int);
            if (lst_plan.SelectedIndex == 0)
                param[11].Value = DBNull.Value;
            else
                param[11].Value = Convert.ToInt32(lst_plan.SelectedValue);
            new ManageCommands(param, "update_supcust_order");
            lblError.Text = "سفارش جاری ویرایش شد";
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
                new ManageCommands(param, "del_supcust_order");
                lblError.Text = "لینک جاری حذف شد";
                fill_grid(ViewState["srl"].ToString());
            }
            catch { lblError.Text = "عملیات ناموفق"; }
        }
        private int max_srl_supcust_order()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.bas_supcust_order";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!Convert.IsDBNull(dr[0]))
                {
                    ViewState["srl_details"] = dr[0];
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
        protected void ImageButton_addnew_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["update"] = "0";
            lst_city2.SelectedIndex = 0;
            lst_size.SelectedIndex = 0;
            lst_color.SelectedIndex = 0;
            lst_carpet.SelectedIndex = 0;
            txt_from_price.Text = string.Empty;
            txt_to_price.Text = string.Empty;
            txt_describtion.Text = string.Empty;
        }
        private bool duplicate_supcust_name()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_name('" + txt_full_name.Text + "')";
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
        private bool duplicate_supcust_name_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_name('" + txt_full_name.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_full_name.Text != ViewState["full_name"].ToString())
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
        private bool duplicate_supcust_tel1()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_tel1('" + txt_tel1.Text + "')";
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
        private bool duplicate_supcust_tel1_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_tel1('" + txt_tel1.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_tel1.Text != ViewState["tel1"].ToString())
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
        private bool duplicate_supcust_cell_phone()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_cellphone('" + txt_cell_phone.Text + "')";
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
        private bool duplicate_supcust_cell_phone_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_supcust_cellphone('" + txt_cell_phone.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_cell_phone.Text != ViewState["cell_phone"].ToString())
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
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch2(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT code_igd FROM dbo.inv_goods where code_igd like '%'+ @SearchText + '%'", prefixText, count);
        }
        private void emptyboxex2()
        {
            txt_register_date.Text = string.Empty;
            txt_product.Text = string.Empty;
        }
        protected void ImageButton2_save_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["update2"].ToString().Equals("0"))
                insert_goods2();
            else
                update_goods2();
            fill_grid2(ViewState["srl"].ToString());
        }
        protected void gridview2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search obj = new Search(strConnString);
            DataTable dt = new DataTable();
            {
                dt = obj.Get_Data("SELECT srl, igd_srl, opportunity_srl, u_date_time FROM dbo.bas_supcust_goods Where srl = " + gridview2.SelectedRow.Cells[0].Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    ViewState["update2"] = "1";
                    ViewState["srl_details2"] = gridview2.SelectedRow.Cells[0].Text.Trim();
                    txt_register_date.Text = row["u_date_time"].ToString();
                    txt_product.Text = goods_name(row["igd_srl"].ToString());
                }
            }
        }
        private void fill_grid2(string srl)
        {
            Search obj = new Search(strConnString); DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT  srl, u_date_time, code_igd, code_igd FROM  dbo.Supcust_Goods Where bassc_srl = " + srl);
            if (dt.Rows.Count > 0)
            {
                gridview2.DataSource = dt;
                gridview2.DataBind();
            }
        }
        private void insert_goods2()
        {
            if (ViewState["srl"] == null)
            {
                lblError.Text = "تامین کننده ای یافت نشد ، لطفا تامین کننده را ذخیره کنید";
                return;
            }
            if (string.IsNullOrEmpty(txt_product.Text))
            {
                lblError.Text = "لطفا فرش را انتخاب نمائید";
                return;
            }
            int igd_srl = goods_srl(txt_product.Text);
            int srl = max_srl_supcust_goods();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@bassc_srl", SqlDbType.Int);
            param[1].Value = Convert.ToInt32(ViewState["srl"]);
            param[2] = new SqlParameter("@igd_srl", SqlDbType.Int);
            param[2].Value = igd_srl;
            param[3] = new SqlParameter("@opportunity_srl", SqlDbType.Int);
            param[3].Value = 1;
            param[4] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[4].Value = txt_register_date.Text;
            new ManageCommands(param, "insert_supcust_goods");
            lblError.Text = "کالای جدید به این مشتری ایجاد شد";
        }
        private void update_goods2()
        {
            if (ViewState["srl"] == null)
            {
                lblError.Text = "تامین کننده ای یافت نشد ، لطفا تامین کننده را ذخیره کنید";
                return;
            }
            if (string.IsNullOrEmpty(txt_product.Text))
            {
                lblError.Text = "لطفا فرش را انتخاب نمائید";
                return;
            }
            int igd_srl = goods_srl(txt_product.Text);
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ViewState["srl_details2"]);
            param[1] = new SqlParameter("@bassc_srl", SqlDbType.Int);
            param[1].Value = Convert.ToInt32(ViewState["srl"]);
            param[2] = new SqlParameter("@igd_srl", SqlDbType.Int);
            param[2].Value = igd_srl;
            param[3] = new SqlParameter("@opportunity_srl", SqlDbType.Int);
            param[3].Value = 1;
            param[4] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[4].Value = txt_register_date.Text;
            new ManageCommands(param, "update_supcust_goods");
            lblError.Text = "کالای این مشتری ویرایش شد";
        }
        protected void lnkRemove2_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkRemove2 = (LinkButton)sender;
                if (ViewState["srl"] == null)
                {
                    lblError.Text = "عملیات ناموفق";
                    return;
                }
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@srl", SqlDbType.Int);
                param[0].Value = Convert.ToInt32(lnkRemove2.CommandArgument);
                new ManageCommands(param, "del_supcust_goods");
                lblError.Text = "لینک جاری حذف شد";
                fill_grid2(ViewState["srl"].ToString());
            }
            catch { lblError.Text = "عملیات ناموفق"; }
        }
        private int max_srl_supcust_goods()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.bas_supcust_goods";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!Convert.IsDBNull(dr[0]))
                {
                    ViewState["srl_details2"] = dr[0];
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
        protected void ImageButton2_addnew_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["update2"] = "0";
            emptyboxex2();
        }
        private int goods_srl(string name)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.inv_goods where code_igd='" + name + "' ");
            int srl = 0;
            if (dt.Rows.Count > 0)
                srl = Convert.ToInt32(dt.Rows[0][0]);
            return srl;
        }
        private string goods_name(string srl)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT code_igd FROM dbo.inv_goods where srl=" + srl );
            string name = string.Empty; ;
            if (dt.Rows.Count > 0)
                name = dt.Rows[0][0].ToString();
            return name;
        }
    }
}