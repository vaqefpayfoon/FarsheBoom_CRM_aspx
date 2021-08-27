using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebApplicationKartable
{
    public partial class Factor : System.Web.UI.Page
    {
        private LoginInfo ubuzhi = new LoginInfo();
        private Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        public DataTable dt2 = new DataTable();
        private Common obb = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                if (Request.QueryString["srl"] == null)
                {
                    if (Request.QueryString["snd"] == "-1")
                        Panel_grid.Visible = true;
                    else
                        setboxes2(Request.QueryString["snd"]);
                }
                else
                {
                    setboxes();
                }
            }
        }
        private void setboxes()
        {
            image1.ImageUrl = "..\\img\\person.png";
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT header_srl,bassc_srl FROM dbo.Project_Details_View Where [confirm] is null or (confirm='False') and srl=" + Request.QueryString["srl"]);
            if(dt.Rows.Count > 0)
            {
                ViewState["bassc_srl"] = dt.Rows[0][1];
                string header_srl = dt.Rows[0][0].ToString();
                dt2 = obj.Get_Data("SELECT srl, brand_name, size_title, provider_srl, code_igd, provider_name, color_name, porz_title, chele_title, plan_title, carpet_title, sale_price, widht, lenght, buy_price, discount,color_srl2,provider_code, ' ' As full_name, ' ' As tel1, ' ' cell_phone, ' ', As email, ' ' As address1, 0 AS down_payment, 0 AS payment,' ' factor_no , ' ' AS u_date_tome, dorangi, rofo, kaji, badbaf, pakhordegi, tear, ' ' As state  FROM dbo.Project_Goods_View where srl =" + header_srl);
                if(dt2.Rows.Count > 0)
                {
                    Common obb = new Common();
                    DataRow Find = dt2.Rows[0];
                    string state = string.Empty;
                    if (!Convert.IsDBNull(Find["dorangi"]))
                    {
                        bool dorangi = Convert.ToBoolean(Find["dorangi"]);
                        if (dorangi)
                            state += "دورنگی ،";
                    }
                    if (!Convert.IsDBNull(Find["rofo"]))
                    {
                        bool dorangi = Convert.ToBoolean(Find["rofo"]);
                        if (dorangi)
                            state += "رفو ،";
                    }
                    if (!Convert.IsDBNull(Find["kaji"]))
                    {
                        bool dorangi = Convert.ToBoolean(Find["kaji"]);
                        if (dorangi)
                            state += "کجی ،";
                    }
                    if (!Convert.IsDBNull(Find["badbaf"]))
                    {
                        bool dorangi = Convert.ToBoolean(Find["badbaf"]);
                        if (dorangi)
                            state += "بدبافت ،";
                    }
                    if (!Convert.IsDBNull(Find["pakhordegi"]))
                    {
                        bool dorangi = Convert.ToBoolean(Find["pakhordegi"]);
                        if (dorangi)
                            state += "پاخوردگی ،";
                    }
                    if (!Convert.IsDBNull(Find["tear"]))
                    {
                        bool dorangi = Convert.ToBoolean(Find["tear"]);
                        if (dorangi)
                            state += "پارگی ،";
                    }
                    if (state.Length > 0)
                        state = string.Format("{0}در فرش با کد {1} می باشد", state, Find["code_igd"].ToString().Trim());
                    dt2.Rows[0]["state"] = state;
                    ViewState["igd_srl"] = Find["srl"];
                    txt_code.Text = Find["code_igd"].ToString().Trim();
                    txt_carpet_type.Text = Find["carpet_title"].ToString().Trim();
                    if (Find["title_igd"] != DBNull.Value)
                    {
                        image1.ImageUrl = Find["title_igd"].ToString();
                        ViewState["title_igd"] = Find["title_igd"].ToString();
                    }
                    else
                    {
                        image1.ImageUrl = null;
                        ViewState["title_igd"] = null;
                    }
                    //
                    int widht = 1;
                    int lenght = 1;
                    long sale = 0;
                    long buy = 0;
                    long dis = 0;
                    if (!Convert.IsDBNull(Find["widht"]))
                        widht = Convert.ToInt32(Find["widht"]);
                    if (!Convert.IsDBNull(Find["lenght"]))
                        lenght = Convert.ToInt32(Find["lenght"]);
                    if (!Convert.IsDBNull(Find["sale_price"]))
                        sale = Convert.ToInt64(Find["sale_price"]);
                    if (!Convert.IsDBNull(Find["buy_price"]))
                        buy = Convert.ToInt64(Find["buy_price"]);
                    if (!Convert.IsDBNull(Find["discount"]))
                        dis = Convert.ToInt64(Find["discount"]);
                    double temp = Math.Round(Convert.ToDouble((lenght * widht) / 10000), 1);
                    if (temp == 0) temp = 1;
                    obb.str = (Math.Round(sale / temp)).ToString();
                    txt_u_sale.Text = obb.str;
                    obb.str = (Math.Round(buy / temp)).ToString();
                    txt_u_buy.Text = obb.str;
                    if (dis > 0 && sale > 0)
                    {
                        obb.str = (dis * sale / 100).ToString();
                        txt_first_discount.Text = obb.str;
                    }
                    obb.str = buy.ToString();
                    txt_buy.Text = obb.str;
                    obb.str = sale.ToString();
                    txt_sale.Text = obb.str;
                    //
                    txt_brand_name.Text = Find["brand_name"].ToString().Trim();
                    txt_color.Text = Find["color_name"].ToString().Trim();
                    txt_porz.Text = Find["porz_title"].ToString().Trim();
                    txt_chele.Text = Find["chele_title"].ToString().Trim();                    
                    txt_size.Text = Find["lenght"].ToString().Trim() + " * " + Find["widht"].ToString().Trim() + ":" + Find["size_title"].ToString().Trim();
                    txt_margin.Text = color(Find["color_srl2"].ToString());
                    txt_plan.Text = Find["plan_title"].ToString().Trim();
                    string[] str_supcust = find_supcust(ViewState["bassc_srl"].ToString());
                    txt_full_name.Text = str_supcust[0];
                    txt_tel1.Text = str_supcust[1] + " ," + str_supcust[2];                    
                    txt_address.Text = str_supcust[3];
                    dt2.Rows[0]["full_name"] = str_supcust[0];
                    dt2.Rows[0]["tel1"] = str_supcust[1];
                    dt2.Rows[0]["cell_phone"] = str_supcust[2];
                    dt2.Rows[0]["address1"] = str_supcust[3];
                    dt2.Rows[0]["email"] = str_supcust[4];
                    DataTable dt3 = new DataTable();
                    dt3 = obj.Get_Data("SELECT  dbo.bas_project.project_code FROM dbo.bas_project_goods INNER JOIN dbo.bas_project ON dbo.bas_project_goods.header_srl = dbo.bas_project.srl where dbo.bas_project_goods.igd_srl=" + Find["srl"].ToString());
                    if (dt3.Rows.Count > 0)
                    {
                        string str = dt3.Rows[0]["project_code"].ToString();
                        preparecode(str);
                    }
                }
                ViewState["table"] = dt2;
            }
        }
        private void setboxes2(string str_srl)
        {
            image1.ImageUrl = "..\\img\\person.png";
            dt2 = obj.Get_Data("SELECT srl, brand_name, size_title, code_igd, color_name, porz_title, chele_title, plan_title, carpet_title, sale_price, widht, lenght, buy_price,color_srl2,provider_code, ' ' As full_name , ' ' AS tel1, ' ' cell_phone , ' ' AS address1,' ' As email,igd_srl,bassc_srl,factor_no,u_date_tome,disc_per,discount,down_payment, payment, title_igd,bayane, dorangi, rofo, kaji, badbaf, pakhordegi, tear, ' ' As state, bank_srl, project_srl FROM dbo.Factor_View Where srl=" + str_srl);
            if (dt2.Rows.Count > 0)
            {                
                DataRow Find = dt2.Rows[0];
                lst_project.SelectedValue = Find["project_srl"].ToString();
                ViewState["srl"] = Find["srl"];
                ViewState["igd_srl"] = Find["igd_srl"];
                ViewState["bassc_srl"] = Find["bassc_srl"];
                ViewState["factor_no"] = Find["factor_no"];
                txt_code.Text = Find["code_igd"].ToString().Trim();
                txt_brand_name.Text = Find["brand_name"].ToString().Trim();
                txt_carpet_type.Text = Find["carpet_title"].ToString().Trim();
                obb.str = Find["sale_price"].ToString().Trim();
                if (Find["title_igd"] != DBNull.Value)
                {
                    image1.ImageUrl = Find["title_igd"].ToString();
                    ViewState["title_igd"] = Find["title_igd"].ToString();
                }
                else
                {
                    image1.ImageUrl = null;
                    ViewState["title_igd"] = null;
                }
                string state = "";
                if (!Convert.IsDBNull(Find["dorangi"]))
                {
                    bool dorangi = Convert.ToBoolean(Find["dorangi"]);
                    if (dorangi)
                        state += "دورنگی ،";
                }
                if (!Convert.IsDBNull(Find["rofo"]))
                {
                    bool dorangi = Convert.ToBoolean(Find["rofo"]);
                    if (dorangi)
                        state += "رفو ،";
                }
                if (!Convert.IsDBNull(Find["kaji"]))
                {
                    bool dorangi = Convert.ToBoolean(Find["kaji"]);
                    if (dorangi)
                        state += "کجی ،";
                }
                if (!Convert.IsDBNull(Find["badbaf"]))
                {
                    bool dorangi = Convert.ToBoolean(Find["badbaf"]);
                    if (dorangi)
                        state += "بدبافت ،";
                }
                if (!Convert.IsDBNull(Find["pakhordegi"]))
                {
                    bool dorangi = Convert.ToBoolean(Find["pakhordegi"]);
                    if (dorangi)
                        state += "پاخوردگی ،";
                }
                if (!Convert.IsDBNull(Find["tear"]))
                {
                    bool dorangi = Convert.ToBoolean(Find["tear"]);
                    if (dorangi)
                        state += "پارگی ،";
                }
                if (state.Length > 0)
                    state = string.Format("{0}در این فرش می باشد", state);
                dt2.Rows[0]["state"] = state;
                //
                //
                int widht = 1;
                int lenght = 1;
                long sale = 0;
                long buy = 0;
                long dis = 0;
                if (!Convert.IsDBNull(Find["widht"]))
                    widht = Convert.ToInt32(Find["widht"]);
                if (!Convert.IsDBNull(Find["lenght"]))
                    lenght = Convert.ToInt32(Find["lenght"]);
                if (!Convert.IsDBNull(Find["sale_price"]))
                    sale = Convert.ToInt64(Find["sale_price"]);
                if (!Convert.IsDBNull(Find["buy_price"]))
                    buy = Convert.ToInt64(Find["buy_price"]);
                if (!Convert.IsDBNull(Find["disc_per"]))
                    dis = Convert.ToInt64(Find["disc_per"]);
                double temp = Math.Round(Convert.ToDouble((lenght * widht) / 10000), 1);
                if (temp == 0) temp = 1;
                obb.str = (Math.Round(sale / temp)).ToString();
                txt_u_sale.Text = obb.str;
                obb.str = (Math.Round(buy / temp)).ToString();
                txt_u_buy.Text = obb.str;
                if (dis > 0 && sale > 0)
                {
                    obb.str = (dis * sale / 100).ToString();
                    txt_first_discount.Text = obb.str;
                }
                obb.str = buy.ToString();
                txt_buy.Text = obb.str;
                obb.str = sale.ToString();
                txt_sale.Text = obb.str;
                //
                txt_porz.Text = Find["porz_title"].ToString().Trim();
                txt_chele.Text = Find["chele_title"].ToString().Trim();
                txt_size.Text = Find["lenght"].ToString().Trim() + " * " + Find["widht"].ToString().Trim() + ":" + Find["size_title"].ToString().Trim();
                txt_color.Text = Find["color_name"].ToString().Trim();
                txt_margin.Text = color(Find["color_srl2"].ToString());
                txt_plan.Text = Find["plan_title"].ToString().Trim();
                string[] str_supcust = find_supcust(ViewState["bassc_srl"].ToString());
                txt_full_name.Text = str_supcust[0];
                txt_tel1.Text = str_supcust[1] + " ," + str_supcust[2];
                txt_address.Text = str_supcust[3];
                dt2.Rows[0]["full_name"] = str_supcust[0];
                dt2.Rows[0]["tel1"] = str_supcust[1];
                dt2.Rows[0]["cell_phone"] = str_supcust[2];
                dt2.Rows[0]["address1"] = str_supcust[3];
                dt2.Rows[0]["email"] = str_supcust[4];
                txt_factor_date.Text = Find["u_date_tome"].ToString();
                txt_factor_no.Text = Find["factor_no"].ToString();
                obb.str = Find["discount"].ToString();
                txt_discount.Text = obb.str;
                obb.str = Find["down_payment"].ToString();
                txt_down_payment.Text = obb.str;
                obb.str = Find["payment"].ToString();
                txt_payment.Text = obb.str;
                if (!Convert.IsDBNull(Find["bayane"]))
                {
                    chk_bayane.Checked = Convert.ToBoolean(Find["bayane"]);
                }
                if (!Convert.IsDBNull(Find["bank_srl"]))
                {
                    lst_bank.SelectedValue = Find["bank_srl"].ToString();
                }
            }
            ViewState["table"] = dt2;
        }
        private string[] find_supcust(string str)
        {
            string[] collection = new string[5];
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT full_name, tel1, cell_phone, address1, email from bas_supcust where srl=" + str);
            if(dt.Rows.Count > 0)
            {
                collection[0] = dt.Rows[0][0].ToString();
                collection[1] = dt.Rows[0]["tel1"].ToString();
                collection[2] = dt.Rows[0]["cell_phone"].ToString();
                collection[3] = dt.Rows[0]["address1"].ToString();
                collection[4] = dt.Rows[0]["email"].ToString();
                return collection;
            }
            else
            {
                return collection;
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
        protected void ImageButton_save_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_down_payment.Text))
                txt_down_payment.Text = "0";
            if (string.IsNullOrEmpty(txt_discount.Text))
                txt_discount.Text = "0";
            if (string.IsNullOrEmpty(txt_payment.Text))
            {
                lblError.Text = "مبلغ فاکتور نامعتبر است";
                return;
            }
            if (string.IsNullOrEmpty(txt_factor_no.Text) && string.IsNullOrEmpty(txt_factor_date.Text))
            {
                lblError.Text = "شماره فاکتور یا تاریخ فاکتور نامعتبر است";
                return;
            }
            if (Request.QueryString["srl"] == null)
            {
                if (Request.QueryString["snd"] == "-1")
                {
                    insert();
                    setboxes2(ViewState["srl"].ToString());
                }
                else
                {
                    if (ViewState["bassc_srl"] != null && ViewState["igd_srl"] != null)
                        update(Convert.ToInt32(ViewState["igd_srl"]), Convert.ToInt32(ViewState["bassc_srl"]));
                }
            }
            else
            {
                if (ViewState["bassc_srl"] != null && ViewState["igd_srl"] != null)
                    insert(Convert.ToInt32(ViewState["igd_srl"]), Convert.ToInt32(ViewState["bassc_srl"]));
            }
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update bas_supcust set clue_srl=@clue_srl " +
             "where srl=@srl";
            cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(ViewState["bassc_srl"]);
            cmd.Parameters.Add("@clue_srl", SqlDbType.Int).Value = 2;
            if(con.State == ConnectionState.Closed)
                con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        protected void ImageButton_print_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_first_discount.Text)) txt_first_discount.Text = "0";
            dt2 = (DataTable)ViewState["table"];
            Common obj = new Common();
            if(string.IsNullOrEmpty(txt_discount.Text))
                dt2.Rows[0]["discount"] = 0;
            else
                dt2.Rows[0]["discount"] = Convert.ToInt64(obj.remove_cama(txt_discount.Text)) + Convert.ToInt64(obj.remove_cama(txt_first_discount.Text));
            if (string.IsNullOrEmpty(txt_payment.Text))
                dt2.Rows[0]["payment"] = 0;
            else
                dt2.Rows[0]["payment"] = Convert.ToInt64(obj.remove_cama(txt_payment.Text));
            if (string.IsNullOrEmpty(txt_down_payment.Text))
                dt2.Rows[0]["down_payment"] = 0;
            else
                dt2.Rows[0]["down_payment"] = Convert.ToInt64(obj.remove_cama(txt_down_payment.Text));
            
            dt2.Rows[0]["factor_no"] = txt_factor_no.Text;
            dt2.Rows[0]["u_date_tome"] = txt_factor_date.Text;
            Server.Transfer("Factor_Print.aspx");
        }
        private void insert(int igd_srl,int bassc_srl)
        {
            if (Duplicate_factor())
            {
                lblError.Text = "شماره فاکتور تکراری است";
                return;
            }
            int srl = max_srl();
            if (lblError.Text == "فاکتور جدید ایجاد شد")
                return;
            Common obb = new Common();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@factor_no", SqlDbType.Char, 10);
            param[1].Value = txt_factor_no.Text;
            param[2] = new SqlParameter("@u_date_tome", SqlDbType.Char, 10);
            param[2].Value = txt_factor_date.Text;
            param[3] = new SqlParameter("@bassc_srl", SqlDbType.VarChar, 100);
            param[3].Value = bassc_srl;
            param[4] = new SqlParameter("@igd_srl", SqlDbType.VarChar);
            param[4].Value = igd_srl;
            param[5] = new SqlParameter("@down_payment", SqlDbType.BigInt);
            param[5].Value = Convert.ToInt64(obb.remove_cama(txt_down_payment.Text));
            param[6] = new SqlParameter("@discount", SqlDbType.BigInt);
            param[6].Value = Convert.ToInt64(obb.remove_cama(txt_discount.Text));
            param[7] = new SqlParameter("@payment", SqlDbType.BigInt);
            param[7].Value = Convert.ToInt64(obb.remove_cama(txt_payment.Text));
            param[8] = new SqlParameter("@project_srl", SqlDbType.Int);
            param[8].Value = Convert.ToInt32(lst_project.SelectedValue);
            param[9] = new SqlParameter("@bayane", SqlDbType.Bit);
            param[9].Value = chk_bayane.Checked;
            param[10] = new SqlParameter("@bank_srl", SqlDbType.Int);
            param[10].Value = lst_bank.SelectedValue;
            new ManageCommands(param, "insert_factor");           

            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update inv_goods set sold=@sold,build_state=@build_state where srl=@srl;";
            cmd.Parameters.Add("@srl", SqlDbType.Int).Value = igd_srl;
            cmd.Parameters.Add("@sold", SqlDbType.Bit).Value = true;
            cmd.Parameters.Add("@build_state", SqlDbType.VarChar).Value = "4";
            cmd.ExecuteNonQuery();
            con.Close();
            lblError.Text = "فاکتور جدید ایجاد شد";
        }
        private void insert()
        {
            if((string.IsNullOrEmpty(txtContactsSearch.Text) && (string.IsNullOrEmpty(txt_cellphone.Text)) || string.IsNullOrEmpty(txt_product.Text)))
            {
                lblError.Text = "فرش و مشتری حتما باید انتخاب شده باشند";
                return;
            }
            int bassc_srl = 0;
            if (!string.IsNullOrEmpty(txtContactsSearch.Text))
                 bassc_srl = supcust_srl(txtContactsSearch.Text);
            if (!string.IsNullOrEmpty(txt_cellphone.Text))
                bassc_srl = supcust_srl2(txt_cellphone.Text);
            if(bassc_srl == 0)
            {
                lblError.Text = "مشخصات مشتری نامعتبر است";
                return;
            }
            int igd_srl = goods_srl(txt_product.Text);
            if (Duplicate_factor())
            {
                lblError.Text = "شماره فاکتور تکراری است";
                return;
            }
            int srl = max_srl();
            if (lblError.Text == "فاکتور جدید ایجاد شد")
                return;
            Common obb = new Common();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@factor_no", SqlDbType.Char, 10);
            param[1].Value = txt_factor_no.Text;
            param[2] = new SqlParameter("@u_date_tome", SqlDbType.Char, 10);
            param[2].Value = txt_factor_date.Text;
            param[3] = new SqlParameter("@bassc_srl", SqlDbType.VarChar, 100);
            param[3].Value = bassc_srl;
            param[4] = new SqlParameter("@igd_srl", SqlDbType.VarChar);
            param[4].Value = igd_srl;
            param[5] = new SqlParameter("@down_payment", SqlDbType.BigInt);
            param[5].Value = Convert.ToInt64(obb.remove_cama(txt_down_payment.Text));
            param[6] = new SqlParameter("@discount", SqlDbType.BigInt);
            param[6].Value = Convert.ToInt64(obb.remove_cama(txt_discount.Text));
            param[7] = new SqlParameter("@payment", SqlDbType.BigInt);
            param[7].Value = Convert.ToInt64(obb.remove_cama(txt_payment.Text));
            param[8] = new SqlParameter("@project_srl", SqlDbType.Int);
            param[8].Value = Convert.ToInt32(lst_project.SelectedValue);
            param[9] = new SqlParameter("@bayane", SqlDbType.Bit);
            param[9].Value = chk_bayane.Checked;
            param[10] = new SqlParameter("@bank_srl", SqlDbType.Int);
            param[10].Value = lst_bank.SelectedValue;
            new ManageCommands(param, "insert_factor");            

            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update inv_goods set sold=@sold,build_state=@build_state where srl=@srl;";
            cmd.Parameters.Add("@srl", SqlDbType.Int).Value = igd_srl;
            cmd.Parameters.Add("@sold", SqlDbType.Bit).Value = true;
            cmd.Parameters.Add("@build_state", SqlDbType.VarChar).Value = "4";
            cmd.ExecuteNonQuery();
            con.Close();

            lblError.Text = "فاکتور جدید ایجاد شد";
        }
        private void update(int igd_srl, int bassc_srl)
        {
            if (Duplicate_factor_update())
            {
                lblError.Text = "شماره فاکتور تکراری است";
                return;
            }
            Common obb = new Common();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ViewState["srl"]);
            param[1] = new SqlParameter("@factor_no", SqlDbType.Char, 10);
            param[1].Value = txt_factor_no.Text;
            param[2] = new SqlParameter("@u_date_tome", SqlDbType.Char, 10);
            param[2].Value = txt_factor_date.Text;
            param[3] = new SqlParameter("@bassc_srl", SqlDbType.VarChar, 100);
            param[3].Value = bassc_srl;
            param[4] = new SqlParameter("@igd_srl", SqlDbType.VarChar);
            param[4].Value = igd_srl;
            param[5] = new SqlParameter("@down_payment", SqlDbType.BigInt);
            param[5].Value = Convert.ToInt64(obb.remove_cama(txt_down_payment.Text));
            param[6] = new SqlParameter("@discount", SqlDbType.BigInt);
            param[6].Value = Convert.ToInt64(obb.remove_cama(txt_discount.Text));
            param[7] = new SqlParameter("@payment", SqlDbType.BigInt);
            param[7].Value = Convert.ToInt64(obb.remove_cama(txt_payment.Text));
            param[8] = new SqlParameter("@project_srl", SqlDbType.Int);
            param[8].Value = Convert.ToInt32(lst_project.SelectedValue);
            param[9] = new SqlParameter("@bayane", SqlDbType.Bit);
            param[9].Value = chk_bayane.Checked;
            param[10] = new SqlParameter("@bank_srl", SqlDbType.Int);
            param[10].Value = lst_bank.SelectedValue;
            new ManageCommands(param, "update_factor");
            lblError.Text = "فاکتور جدید ویرایش شد";

            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update inv_goods set sold=@sold,build_state=@build_state where srl=@srl;";
            cmd.Parameters.Add("@srl", SqlDbType.Int).Value = igd_srl;
            cmd.Parameters.Add("@sold", SqlDbType.Bit).Value = true;
            cmd.Parameters.Add("@build_state", SqlDbType.VarChar).Value = "4";
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private int max_srl()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.acc_factor";
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
        private bool Duplicate_factor()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_factor('" + txt_factor_no.Text + "')";
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
        private bool Duplicate_factor_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_factor('" + txt_factor_no.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_factor_no.Text != ViewState["factor_no"].ToString())
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
        public static List<string> FilterSearch(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT full_name FROM dbo.bas_supcust where full_name like '%'+ @SearchText + '%'", prefixText, count);
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch2(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT code_igd FROM dbo.inv_goods where ((sold is null) or (sold='False')) AND (selection='True')AND (dbo.inv_goods.build_state = '0') AND code_igd like '%'+ @SearchText + '%'", prefixText, count);
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> FilterSearch3(string prefixText, int count)
        {
            Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            return obj.FilterSearch("SELECT cell_phone FROM dbo.bas_supcust where cell_phone like '%'+ @SearchText + '%'", prefixText, count);
        }
        private int supcust_srl(string name)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.bas_supcust where full_name='" + name + "' ");
            int srl = 0;
            if (dt.Rows.Count > 0)
                srl = Convert.ToInt32(dt.Rows[0][0]);
            return srl;
        }
        private int supcust_srl2(string name)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.bas_supcust where cell_phone='" + name + "' ");
            int srl = 0;
            if (dt.Rows.Count > 0)
                srl = Convert.ToInt32(dt.Rows[0][0]);
            return srl;
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
       private void emptyboxes()
        {
            txtContactsSearch.Text = string.Empty;
            txt_address.Text = string.Empty;
            txt_brand_name.Text = string.Empty;
            txt_buy.Text = string.Empty;
            txt_carpet_type.Text = string.Empty;
            txt_cellphone.Text = string.Empty;
            txt_chele.Text = string.Empty;
            txt_code.Text = string.Empty;
            txt_color.Text = string.Empty;
            txt_discount.Text = string.Empty;
            txt_down_payment.Text = string.Empty;
            txt_factor_date.Text = string.Empty;
            txt_factor_no.Text = string.Empty;
            txt_first_discount.Text = string.Empty;
            txt_full_name.Text = string.Empty;
            txt_margin.Text = string.Empty;
            txt_payment.Text = string.Empty;
            txt_plan.Text = string.Empty;
            txt_porz.Text = string.Empty;
            txt_product.Text = string.Empty;
            txt_sale.Text = string.Empty;
            txt_size.Text = string.Empty;
            txt_tel1.Text = string.Empty;
            txt_u_buy.Text = string.Empty;
            txt_u_sale.Text = string.Empty;           
        }
        protected void btn_list_Click(object sender, EventArgs e)
        {
            bool bln = false;
            if(!string.IsNullOrEmpty(txt_cellphone.Text) && !string.IsNullOrEmpty(txt_product.Text) && Request.QueryString["snd"] == "-1")
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT full_name, tel1, cell_phone, address1 FROM dbo.bas_supcust where cell_phone='" + txt_cellphone.Text + "' ");
                if(dt.Rows.Count > 0)
                {                    
                    txt_full_name.Text = dt.Rows[0][0].ToString();
                    txt_tel1.Text = dt.Rows[0][1].ToString() + " " + dt.Rows[0][2].ToString();
                    txt_address.Text = dt.Rows[0][3].ToString();
                    bln = true;
                }
                else
                {
                    lblError.Text = "مشخصات مشتری نامعتبر است";
                    emptyboxes();
                    return;
                }
            }
            else if(!string.IsNullOrEmpty(txtContactsSearch.Text) && !string.IsNullOrEmpty(txt_product.Text) && Request.QueryString["snd"] == "-1")
            {
                string[] str_supcust = find_supcust(supcust_srl(txtContactsSearch.Text).ToString());
                txt_full_name.Text = str_supcust[0];
                txt_tel1.Text = str_supcust[1] + " " + str_supcust[2];
                txt_address.Text = str_supcust[3];
                bln = true;
            }
            else
            {
                lblError.Text = "مشخصات مشتری نامعتبر است";
                emptyboxes();
                return;
            }
            if (bln)
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT srl, brand_name, size_title, provider_srl, code_igd, provider_name, color_name, porz_title, chele_title, plan_title, carpet_title, sale_price, widht, lenght, buy_price, discount,color_srl2,provider_code FROM dbo.Provider_Goods where code_igd='" + txt_product.Text + "' ");
                if (dt.Rows.Count > 0)
                {
                    DataRow Find = dt.Rows[0];
                    txt_carpet_type.Text = Find["carpet_title"].ToString().Trim();
                    txt_brand_name.Text = Find["brand_name"].ToString().Trim();
                    txt_color.Text = Find["color_name"].ToString().Trim();
                    if (!Convert.IsDBNull(Find["color_srl2"]))
                        txt_margin.Text = color(Find["color_srl2"].ToString());
                    txt_porz.Text = Find["porz_title"].ToString().Trim();
                    txt_chele.Text = Find["chele_title"].ToString().Trim();
                    txt_size.Text = Find["lenght"].ToString().Trim() + " * " + Find["widht"].ToString().Trim() + ":" + Find["size_title"].ToString().Trim();
                    //
                    int widht = 1;
                    int lenght = 1;
                    long sale = 0;
                    long buy = 0;
                    long dis = 0;
                    if (!Convert.IsDBNull(Find["widht"]))
                        widht = Convert.ToInt32(Find["widht"]);
                    if (!Convert.IsDBNull(Find["lenght"]))
                        lenght = Convert.ToInt32(Find["lenght"]);
                    if (!Convert.IsDBNull(Find["sale_price"]))
                        sale = Convert.ToInt64(Find["sale_price"]);
                    if (!Convert.IsDBNull(Find["buy_price"]))
                        buy = Convert.ToInt64(Find["buy_price"]);
                    if (!Convert.IsDBNull(Find["discount"]))
                        dis = Convert.ToInt64(Find["discount"]);
                    double temp = Math.Round(Convert.ToDouble((lenght * widht) / 10000), 1);
                    if (temp == 0) temp = 1;
                    obb.str = (Math.Round(sale / temp)).ToString();
                    txt_u_sale.Text = obb.str;
                    obb.str = (Math.Round(buy / temp)).ToString();
                    txt_u_buy.Text = obb.str;
                    if (dis > 0 && sale > 0)
                    {
                        obb.str = (dis * sale / 100).ToString();
                        txt_first_discount.Text = obb.str;
                    }
                    obb.str = buy.ToString();
                    txt_buy.Text = obb.str;
                    obb.str = sale.ToString();
                    txt_sale.Text = obb.str;
                    txt_down_payment.Text = "0";
                    txt_discount.Text = "0";
                    txt_payment.Text = "0";
                    //
                    txt_plan.Text = Find["plan_title"].ToString().Trim();
                    txt_code.Text = Find["provider_code"].ToString().Trim();
                    DataTable dt2 = new DataTable();
                    dt2 = obj.Get_Data("SELECT  dbo.bas_project.project_code FROM dbo.bas_project_goods INNER JOIN dbo.bas_project ON dbo.bas_project_goods.header_srl = dbo.bas_project.srl where dbo.bas_project_goods.igd_srl=" + Find["srl"].ToString());
                    if (dt2.Rows.Count > 0)
                    {
                        string str = dt2.Rows[0]["project_code"].ToString();
                        preparecode(str);
                    }
                }
            }
        }
        private void preparecode(string str)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data(string.Format("SELECT Convert(int,factor_no) As code FROM acc_factor where factor_no like '%{0}%' ", str));
            if (dt.Rows.Count > 0)
            {
                object max_code;
                max_code = dt.Compute("MAX(code)", "");
                txt_factor_no.Text = (Convert.ToInt64(max_code) + 1).ToString();
            }
            else
            {
                txt_factor_no.Text = str + "001";
            }
            txt_factor_date.Text = obb.persian_date().ToString();
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
        protected void btnShow_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            long first_dis = 0;
            long dis = 0;
            long sale = 0;
            long down = 0;
            if (!string.IsNullOrEmpty(txt_first_discount.Text))
                first_dis = Convert.ToInt64(obb.remove_cama(txt_first_discount.Text));
            if (!string.IsNullOrEmpty(txt_discount.Text))
                dis = Convert.ToInt64(obb.remove_cama(txt_discount.Text));
            if (!string.IsNullOrEmpty(txt_sale.Text))
                sale = Convert.ToInt64(obb.remove_cama(txt_sale.Text));
            if (!string.IsNullOrEmpty(txt_down_payment.Text))
                down = Convert.ToInt64(obb.remove_cama(txt_down_payment.Text));
            obb.str = (sale - (first_dis + dis + down)).ToString();
            txt_payment.Text = obb.str;
        }

        protected void lst_project_SelectedIndexChanged(object sender, EventArgs e)
        {
            string project = lst_project.SelectedItem.Text;
            string defaultStr = "000";
            DataTable dt = obj.Get_Data("SELECT count(srl) FROM dbo.acc_factor Where project_srl=" + lst_project.SelectedValue);
            if(dt.Rows.Count > 0)
            {
                string count = dt.Rows[0][0].ToString();
                if(count == "0")
                {
                    string newString = defaultStr.PadLeft(3, '0') + 1;
                    txt_factor_no.Text = project + newString;
                } 
                else
                {
                    string newString = (count.Length == 1 ? "000" : count.Length == 2 ? "0" : "").PadLeft(count.Length - 1, '0') + count;
                    txt_factor_no.Text = project + newString;
                }
            }
            else
            {
                string newString =  defaultStr.PadLeft(3, '0') + 1;
                txt_factor_no.Text = project + newString;
            }
        }
    }
}