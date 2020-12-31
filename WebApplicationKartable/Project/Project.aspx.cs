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
    public partial class Project : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();private Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                if (Request.QueryString["srl"] != "-1")
                    set_boxes(Request.QueryString["srl"].ToString());
                fill_grid();
            }
        }
        private void set_boxes(string srl)
        {
            DataTable dt = new DataTable(); 
            dt = obj.Get_Data("SELECT srl, from_date,to_date,project_name,describtion,confirm,locate_srl,project_code FROM dbo.bas_project WHERE srl=" + srl);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ViewState["srl"] = row["srl"].ToString();
                txt_project_name.Text = row["project_name"].ToString();
                ViewState["project_name"] = row["project_name"].ToString();
                txt_project_code.Text= row["project_code"].ToString();
                txt_from_date.Text = row["from_date"].ToString();
                txt_to_date.Text = row["to_date"].ToString();
                txt_desc.Text = row["describtion"].ToString();
                if (!Convert.IsDBNull(row["confirm"]))
                    chk_confirm.Checked = Convert.ToBoolean(row["confirm"]);
                if (!Convert.IsDBNull(row["locate_srl"]))
                    lst_locate.SelectedValue = row["locate_srl"].ToString();
                else
                    lst_locate.SelectedIndex = 0;
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
            int srl = max_srl();
            if (lblError.Text == "نمایشگاه جدید ایجاد شد")
                return;
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@from_date", SqlDbType.Char,10);
            param[1].Value = txt_from_date.Text;
            param[2] = new SqlParameter("@to_date", SqlDbType.Char, 10);
            param[2].Value = txt_to_date.Text;
            param[3] = new SqlParameter("@project_name", SqlDbType.VarChar, 100);
            param[3].Value = txt_project_name.Text;
            param[4] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if(!string.IsNullOrEmpty(txt_desc.Text))
                param[4].Value = txt_desc.Text;
            else
                param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@confirm", SqlDbType.Bit);
            param[5].Value = chk_confirm.Checked;
            param[6] = new SqlParameter("@locate_srl", SqlDbType.TinyInt);
            if (lst_locate.SelectedIndex > 0)
                param[6].Value = Convert.ToInt32(lst_locate.SelectedValue);
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@project_code", SqlDbType.VarChar, 20);
            param[7].Value = txt_project_code.Text;
            new ManageCommands(param, "insert_project");
            lblError.Text = "نمایشگاه جدید ایجاد شد";
        }
        private void update()
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ViewState["srl"]);
            param[1] = new SqlParameter("@from_date", SqlDbType.Char, 10);
            param[1].Value = txt_from_date.Text;
            param[2] = new SqlParameter("@to_date", SqlDbType.Char, 10);
            param[2].Value = txt_to_date.Text;
            param[3] = new SqlParameter("@project_name", SqlDbType.VarChar, 100);
            param[3].Value = txt_project_name.Text;
            param[4] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_desc.Text))
                param[4].Value = txt_desc.Text;
            else
                param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@confirm", SqlDbType.Bit);
            param[5].Value = chk_confirm.Checked;
            param[6] = new SqlParameter("@locate_srl", SqlDbType.TinyInt);
            if (lst_locate.SelectedIndex > 0)
                param[6].Value = Convert.ToInt32(lst_locate.SelectedValue);
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@project_code", SqlDbType.VarChar, 20);
            param[7].Value = txt_project_code.Text;
            new ManageCommands(param, "update_project");
            lblError.Text = "نمایشگاه جاری ویرایش شد";
        }
        private int max_srl()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.bas_project";
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
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT provider_name FROM dbo.bas_provider where provider_name like '%'+ @SearchText + '%'", prefixText, count);
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch2(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT code_igd FROM dbo.inv_goods where((sold is null) or (sold='False')) AND code_igd like '%'+ @SearchText + '%'", prefixText, count);
        }
        private void fill_grid()
        {
            DataTable dt = new DataTable(); 
            dt = obj.Get_Data("SELECT provider_srl, provider_name, code_igd FROM dbo.Provider_Goods where (selection is null) or (selection = 'False') order by srl desc");
            grid.DataSource = dt; grid.DataBind();
            ViewState["selection"] = true;
            btn_save_assign.Text = "تخصیص فرش به نمایشگاه";
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            Panel_grid.Visible = true;
            fill_grid();
        }
        private void WoakOnTable()
        {
            int count = 0;
            foreach (GridViewRow row in grid.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("confirm");                
                int igd_srl = goods_srl(grid.Rows[count].Cells[2].Text.Trim());
                int srl = max_srl_details();
                if (check.Checked && igd_srl > 0)
                {
                    if (!Duplicate_GoodsInproject(igd_srl.ToString() , ViewState["srl"].ToString()))
                    {
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@srl", SqlDbType.Int);
                        param[0].Value = srl;
                        param[1] = new SqlParameter("@header_srl", SqlDbType.Int);
                        param[1].Value = Convert.ToInt32(ViewState["srl"]);
                        param[2] = new SqlParameter("@igd_srl", SqlDbType.Int);
                        param[2].Value = Convert.ToInt32(igd_srl);
                        new ManageCommands(param, "insert_project_goods");
                        SqlConnection con = new SqlConnection(strConnString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update inv_goods set selection=@selection where srl=@srl;";
                        cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(igd_srl);
                        cmd.Parameters.Add("@selection", SqlDbType.Bit).Value = true;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                count++;
            }
        }
        protected void btn_filter_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(txtContactsSearch.Text))
            {
                dt = obj.Get_Data("SELECT provider_srl, provider_name, code_igd FROM dbo.Provider_Goods where ((selection is null) or (selection = 'False')) and  provider_name='" + txtContactsSearch.Text + "' order by srl desc");
                grid.DataSource = dt; grid.DataBind();
            }
            else if (!string.IsNullOrEmpty(txt_product.Text))
            {
                dt = obj.Get_Data("SELECT provider_srl, provider_name, code_igd FROM dbo.Provider_Goods where ((selection is null) or (selection = 'False')) and  code_igd=" + txt_product.Text + " order by srl desc");
                grid.DataSource = dt; grid.DataBind();
            }
            else if(lst_city2.SelectedIndex > 0)
            {
                dt = obj.Get_Data("SELECT provider_srl, provider_name, code_igd FROM dbo.Provider_Goods where ((selection is null) or (selection = 'False')) and ibt_srl=" + lst_city2.SelectedValue + " order by srl desc");
                grid.DataSource = dt; grid.DataBind();
            }
            else if(lst_size.SelectedIndex > 0)
            {
                dt = obj.Get_Data("SELECT provider_srl, provider_name, code_igd FROM dbo.Provider_Goods where ((selection is null) or (selection = 'False')) and size_srl=" + lst_size.SelectedValue + " order by srl desc");
                grid.DataSource = dt; grid.DataBind();
            }
            else
            {
                dt = obj.Get_Data("SELECT provider_srl, provider_name, code_igd FROM dbo.Provider_Goods where (selection is null) or (selection = 'False') order by srl desc");
                grid.DataSource = dt; grid.DataBind();
            }
        }
        private int provider_srl(string name)
        {
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl FROM dbo.bas_provider where provider_name='" + name + "' ");
            int srl = 0;
            if (dt.Rows.Count > 0)
                srl = Convert.ToInt32(dt.Rows[0][0]);
            return srl;
        }
        private int goods_srl(string name)
        {
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl FROM dbo.inv_goods where code_igd='" + name + "' ");
            int srl = 0;
            if(dt.Rows.Count > 0)            
                srl = Convert.ToInt32(dt.Rows[0][0]);            
            return srl;
        }
        protected void btn_save_assign_Click(object sender, EventArgs e)
        {
            if(ViewState["selection"].ToString().Equals("True"))
                WoakOnTable();
            else
                WoakOnGrid2();
            fill_grid();
        }
        protected void btn_view_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../Archives/AllReports.aspx?snd=project_list&srl=" + ViewState["srl"]);
            fill_grid2();
            ViewState["selection"] = false;
            btn_save_assign.Text = "حذف فرش از نمایشگاه";
        }
        private void WoakOnGrid2()
        {
            int count = 0;
            foreach (GridViewRow row in grid.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("confirm");
                int igd_srl = goods_srl(grid.Rows[count].Cells[2].Text.Trim());
                if (check.Checked)
                {
                    try
                    {
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@header_srl", SqlDbType.Int);
                        param[0].Value = Convert.ToInt32(ViewState["srl"]);
                        param[1] = new SqlParameter("@igd_srl", SqlDbType.Int);
                        param[1].Value = Convert.ToInt32(igd_srl);
                        new ManageCommands(param, "del_project_goods");
                        SqlConnection con = new SqlConnection(strConnString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update inv_goods set selection=@selection where srl=@srl;";
                        cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(igd_srl);
                        cmd.Parameters.Add("@selection", SqlDbType.Bit).Value = false;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch {lblError.Text="عملیات ناموفق ، برای این فرش فاکتور صادر شده است"; continue; }
                }
                count++;
            }
        }
        private bool Duplicate_bas_project()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_bas_project('" + txt_project_name.Text + "')";
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
        private bool Duplicate_GoodsInproject(string igd , string header_srl)
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
        private bool Duplicate_bas_project_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_bas_project('" + txt_project_name.Text+ "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_project_name.Text != ViewState["project_name"].ToString())
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
        private void fill_grid2()
        {
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT provider_srl, provider_name, code_igd FROM dbo.Provider_Goods where (selection = 'True') order by srl desc");
            grid.DataSource = dt; grid.DataBind();
        }
        protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = grid.Rows[e.NewSelectedIndex];
            string srl = row.Cells[2].Text.ToString();
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT title_igd FROM dbo.inv_goods where code_igd=" + srl);
            if(dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["title_igd"] != DBNull.Value)
                {
                    image1.ImageUrl = dt.Rows[0]["title_igd"].ToString();
                }
                else
                {
                    image1.ImageUrl = null;
                }
            }
        }
        private void deleteAllAssigns()
        {
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT provider_srl, provider_name, code_igd FROM dbo.Provider_Goods where (selection = 'True') order by srl desc");
            foreach (DataRow row in dt.Rows)
            {
                if (true)
                {
                    try
                    {
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@header_srl", SqlDbType.Int);
                        param[0].Value = Convert.ToInt32(ViewState["srl"]);
                        param[1] = new SqlParameter("@igd_srl", SqlDbType.Int);
                        param[1].Value = Convert.ToInt32(row["code_igd"]);
                        new ManageCommands(param, "del_project_goods");
                        SqlConnection con = new SqlConnection(strConnString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update inv_goods set selection=@selection where code_igd=@code_igd;";
                        cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(row["code_igd"]);
                        cmd.Parameters.Add("@selection", SqlDbType.Bit).Value = false;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch { lblError.Text = "عملیات ناموفق ، برای این فرش فاکتور صادر شده است"; continue; }
                }
            }
        }
        protected void btn_delete_allassign_Click(object sender, EventArgs e)
        {
            deleteAllAssigns();
        }
    }
}