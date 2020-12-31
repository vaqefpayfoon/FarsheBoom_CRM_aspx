using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;
using System.IO;

namespace WebApplicationKartable
{
    public partial class Provider : System.Web.UI.Page
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
                ViewState["update"] = "0"; image1.ImageUrl = "..\\img\\person.png";
            }            
        }
        private void set_boxes(string srl)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, u_date_time, provider_name, related_person, tel1, tel2, fax1, cell_phone, address1, type_srl, city_srl, describtion, provider_code FROM dbo.bas_provider WHERE srl=" + srl);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ViewState["srl"] = row["srl"].ToString();
                txt_provider_name.Text = row["provider_name"].ToString();
                ViewState["name"] = row["provider_name"].ToString();
                txt_related_person.Text = row["related_person"].ToString();
                txt_tel1.Text = row["tel1"].ToString();
                ViewState["tel1"] = row["tel1"].ToString();
                txt_tel2.Text = row["tel2"].ToString();
                txt_fax1.Text = row["fax1"].ToString();
                txt_cell_phone.Text = row["cell_phone"].ToString();
                ViewState["cell_phone"] = row["cell_phone"].ToString();
                txt_address.Text = row["address1"].ToString();
                lst_type.SelectedValue = row["type_srl"].ToString();
                txt_provider_code.Text = row["provider_code"].ToString();
                if (!Convert.IsDBNull(row["city_srl"]))
                    lst_city.SelectedValue = row["city_srl"].ToString();
                txt_desc.Text = row["describtion"].ToString();
                fill_grid(ViewState["srl"].ToString());
            }
        }
        protected void btn_save_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.QueryString["srl"] == "-1")
            {
                insert();
                preparecode();
            }
            else
                update();
        }
        private void insert()
        {
            if (duplicate_provider_name())
            {
                lblError.Text = "نام تامین کننده تکراری است";
                return;
            }
            if (duplicate_provider_tel1())
            {
                lblError.Text = "تلفن تامین کننده تکراری است";
                return;
            }
            if (duplicate_provider_cell_phone())
            {
                lblError.Text = "تلفن همراه تامین کننده تکراری است";
                return;
            }
            int srl = max_srl();
            if (lblError.Text == "تامین کننده جدید ایجاد شد")
                return;
            ViewState["srl"] = srl;
            Common per = new Common();
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@u_date_time", SqlDbType.Char,10);
            param[1].Value = per.persian_date();
            param[2] = new SqlParameter("@provider_name", SqlDbType.VarChar, 100);
            param[2].Value = txt_provider_name.Text;
            param[3] = new SqlParameter("@related_person", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_related_person.Text))
                param[3].Value = txt_related_person.Text;
            else
                param[3].Value = DBNull.Value;
            param[4] = new SqlParameter("@tel1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel1.Text))
                param[4].Value = txt_tel1.Text;
            else
                param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@tel2", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel2.Text))
                param[5].Value = txt_tel2.Text;
            else
                param[5].Value = DBNull.Value;
            param[6] = new SqlParameter("@fax1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_fax1.Text))
                param[6].Value = txt_fax1.Text;
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@cell_phone", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_cell_phone.Text))
                param[7].Value = txt_cell_phone.Text;
            else
                param[7].Value = DBNull.Value;
            param[8] = new SqlParameter("@address1", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_address.Text))
                param[8].Value = txt_address.Text;
            else
                param[8].Value = DBNull.Value;
            param[9] = new SqlParameter("@type_srl", SqlDbType.TinyInt);
            param[9].Value = Convert.ToInt32(lst_type.SelectedValue);
            param[10] = new SqlParameter("@city_srl", SqlDbType.TinyInt);
            if (lst_city.SelectedIndex == 0)
                param[10].Value = DBNull.Value;
            else
                param[10].Value = Convert.ToInt32(lst_city.SelectedValue); 
            param[11] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_desc.Text))
                param[11].Value = txt_desc.Text;
            else
                param[11].Value = DBNull.Value;
            param[12] = new SqlParameter("@provider_code", SqlDbType.VarChar,30);
            param[12].Value = txt_provider_code.Text;
            new ManageCommands(param, "insert_provider");
            lblError.Text = "تامین کننده جدید ایجاد شد";
        }
        private void update()
        {
            if (duplicate_provider_name_update())
            {
                lblError.Text = "نام تامین کننده تکراری است";
                return;
            }
            if (duplicate_provider_tel1_update())
            {
                lblError.Text = "تلفن تامین کننده تکراری است";
                return;
            }
            if (duplicate_provider_cell_phone_update())
            {
                lblError.Text = "تلفن همراه تامین کننده تکراری است";
                return;
            }
            Common per = new Common();
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ViewState["srl"]);
            param[1] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[1].Value = per.persian_date();
            param[2] = new SqlParameter("@provider_name", SqlDbType.VarChar, 100);
            param[2].Value = txt_provider_name.Text;
            param[3] = new SqlParameter("@related_person", SqlDbType.VarChar, 100);
            if (!string.IsNullOrEmpty(txt_related_person.Text))
                param[3].Value = txt_related_person.Text;
            else
                param[3].Value = DBNull.Value;
            param[4] = new SqlParameter("@tel1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel1.Text))
                param[4].Value = txt_tel1.Text;
            else
                param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@tel2", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_tel2.Text))
                param[5].Value = txt_tel2.Text;
            else
                param[5].Value = DBNull.Value;
            param[6] = new SqlParameter("@fax1", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_fax1.Text))
                param[6].Value = txt_fax1.Text;
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@cell_phone", SqlDbType.Char, 11);
            if (!string.IsNullOrEmpty(txt_cell_phone.Text))
                param[7].Value = txt_cell_phone.Text;
            else
                param[7].Value = DBNull.Value;
            param[8] = new SqlParameter("@address1", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_address.Text))
                param[8].Value = txt_address.Text;
            else
                param[8].Value = DBNull.Value;
            param[9] = new SqlParameter("@type_srl", SqlDbType.TinyInt);
            param[9].Value = Convert.ToInt32(lst_type.SelectedValue);
            param[10] = new SqlParameter("@city_srl", SqlDbType.TinyInt);
            if (lst_city.SelectedIndex == 0)
                param[10].Value = DBNull.Value;
            else
                param[10].Value = Convert.ToInt32(lst_city.SelectedValue);
            param[11] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_desc.Text))
                param[11].Value = txt_desc.Text;
            else
                param[11].Value = DBNull.Value;
            param[12] = new SqlParameter("@provider_code", SqlDbType.VarChar, 30);
            param[12].Value = txt_provider_code.Text;
            new ManageCommands(param, "update_provider");
            lblError.Text = "تامین کننده جاری ویرایش شد";
        }
        private int max_srl()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.bas_provider";
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
        protected void ImageButton_save_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_code.Text.StartsWith(txt_provider_code.Text.Substring(0, 2)))
            {
                if (ViewState["update"].ToString().Equals("0"))
                    insert_goods();
                else
                    update_goods();
                fill_grid(ViewState["srl"].ToString());
            }
            else
                lblError.Text = "کد فرش بوم نامعتبر است";         
        }
        protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get_data2(ViewState["srl"].ToString(),gridview.SelectedRow.Cells[0].Text.Trim());
            Search obj = new Search(strConnString);
            DataTable dt = new DataTable();
            {
                dt = obj.Get_Data("SELECT srl, u_date_time, title_igd, provider_srl, porz_type, chele_type, carpet_type, build_state, ibt_srl, city_srl, size_srl, color_srl, describtion, lenght, widht, margin_color, dorangi, rofo, kaji, badbaf, pakhordegi, tear, code_igd, provider_code, good_value, color_srl2,raj_srl FROM dbo.inv_goods Where srl = " + gridview.SelectedRow.Cells[0].Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    ViewState["update"] = "1";
                    ViewState["srl_details"] = gridview.SelectedRow.Cells[0].Text.Trim();
                    if (row["title_igd"] != DBNull.Value)
                    {
                        image1.ImageUrl = row["title_igd"].ToString();
                        ViewState["title_igd"] = row["title_igd"].ToString();
                    }
                    else
                    {
                        image1.ImageUrl = null;
                        ViewState["title_igd"] = null;
                    }
                    ViewState["title_igd"] = row["title_igd"].ToString();
                    if (!Convert.IsDBNull(row["porz_type"]))
                        lst_porz.SelectedValue = row["porz_type"].ToString();
                    else
                        lst_porz.SelectedIndex = 0;
                    if (!Convert.IsDBNull(row["city_srl"]))
                        lst_plan.SelectedValue = row["city_srl"].ToString();
                    else
                        lst_plan.SelectedIndex = 0;
                    if (!Convert.IsDBNull(row["chele_type"]))
                        lst_chele.SelectedValue = row["chele_type"].ToString();
                    else
                        lst_chele.SelectedIndex = 0;
                    if (!Convert.IsDBNull(row["carpet_type"]))
                        lst_carpet.SelectedValue = row["carpet_type"].ToString();
                    else
                        lst_carpet.SelectedIndex = 0;
                    if (!string.IsNullOrEmpty(row["build_state"].ToString()))
                        lst_status.SelectedValue = row["build_state"].ToString();
                    else
                        lst_status.SelectedIndex = 0;
                    if (!Convert.IsDBNull(row["ibt_srl"]))
                        lst_brand.SelectedValue = row["ibt_srl"].ToString();
                    else
                        lst_brand.SelectedIndex = 0;
                    if (!Convert.IsDBNull(row["size_srl"]))
                        lst_size.SelectedValue = row["size_srl"].ToString();
                    else
                        lst_size.SelectedIndex = 0;
                    if (!Convert.IsDBNull(row["color_srl"]))
                        lst_color.SelectedValue = row["color_srl"].ToString();
                    else
                        lst_color.SelectedIndex = 0;
                    if (!Convert.IsDBNull(row["color_srl2"]))
                        lst_color2.SelectedValue = row["color_srl2"].ToString();
                    else
                        lst_color2.SelectedIndex = 0;
                    if (!Convert.IsDBNull(row["raj_srl"]))
                        lst_raj.SelectedValue = row["raj_srl"].ToString();
                    else
                        lst_raj.SelectedIndex = 0;
                    txt_describtion.Text = row["describtion"].ToString();
                    txt_lenght.Text = row["lenght"].ToString();
                    txt_weight.Text = row["widht"].ToString();
                    //txt_color.Text = row["margin_color"].ToString();
                    //if (!Convert.IsDBNull(row["dorangi"]))
                    //    chk_dorangi.Checked = Convert.ToBoolean(row["dorangi"]);
                    //else
                    //    chk_dorangi.Checked = false;
                    //if (!Convert.IsDBNull(row["rofo"]))
                    //    chk_rofo.Checked = Convert.ToBoolean(row["rofo"]);
                    //else
                    //    chk_rofo.Checked = false;
                    //if (!Convert.IsDBNull(row["kaji"]))
                    //    chk_kaji.Checked = Convert.ToBoolean(row["kaji"]);
                    //else
                    //    chk_kaji.Checked = false;
                    //if (!Convert.IsDBNull(row["badbaf"]))
                    //    chk_badbaf.Checked = Convert.ToBoolean(row["badbaf"]);
                    //else
                    //    chk_badbaf.Checked = false;
                    //if (!Convert.IsDBNull(row["pakhordegi"]))
                    //    chk_pakhordegi.Checked = Convert.ToBoolean(row["pakhordegi"]);
                    //else
                    //    chk_pakhordegi.Checked = false;
                    //if (!Convert.IsDBNull(row["tear"]))
                    //    chk_tear.Checked = Convert.ToBoolean(row["tear"]);
                    //else
                    //    chk_tear.Checked = false;
                    txt_code.Text = row["code_igd"].ToString();
                    ViewState["code_igd"] = row["code_igd"].ToString();
                    txt_pcode.Text = row["provider_code"].ToString();
                    if (!Convert.IsDBNull(row["good_value"]))
                        lst_value.SelectedValue = row["good_value"].ToString();
                    else
                        lst_value.SelectedIndex = 0;
                }
            }
        }
        private void fill_grid(string srl)
        {
            Search obj = new Search(strConnString);DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl, code_igd, brand_name, size_title, color_name FROM Provider_Goods Where provider_srl = " + srl + " order by srl desc");
            if(dt.Rows.Count > 0)
            {
                gridview.DataSource = dt;
                gridview.DataBind();
            }
        }
        private void insert_goods()
        {
            if(ViewState["srl"] == null)
            {
                lblError.Text = "تامین کننده ای یافت نشد ، لطفا تامین کننده را ذخیره کنید";
                return;
            }
            if (duplicate_goods_code())
            {
                lblError.Text = "کد فرش بوم تکراری است";
                return;
            }
            Common obj = new Common();
            int srl = max_srl_goods();
            int remain = (srl / 100) + 1;
            string path = Server.MapPath("/product_images");
            string save_image = "../product_images/" + remain;
            if (!Directory.Exists(path + "/" + remain))
                Directory.CreateDirectory(path + "/" + remain);
            path = Server.MapPath("/product_images/" + remain);
            if (upload.HasFile)
            {
                string file_type = Path.GetExtension(upload.FileName);
                path = path + "\\" + srl + file_type;
                save_image = save_image + "\\" + srl + file_type;
                upload.SaveAs(path);
                image1.ImageUrl = save_image;
            }
            else
            {
                if (ViewState["title_igd"] != null)
                    save_image = ViewState["title_igd"].ToString();
                else
                    save_image = "..\\img\\person.png";
            }
            SqlParameter[] param = new SqlParameter[31];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[1].Value = obj.persian_date();
            param[2] = new SqlParameter("@title_igd", SqlDbType.VarChar, 100);
            param[2].Value = save_image;
            param[3] = new SqlParameter("@provider_srl", SqlDbType.Int);
            param[3].Value = Convert.ToInt32(ViewState["srl"]);
            param[4] = new SqlParameter("@porz_type", SqlDbType.TinyInt);
            if (lst_porz.SelectedIndex == 0)
                param[4].Value = DBNull.Value;
            else
                param[4].Value = Convert.ToInt32(lst_porz.SelectedValue);
            param[5] = new SqlParameter("@chele_type", SqlDbType.TinyInt);
            if (lst_chele.SelectedIndex == 0)
                param[5].Value = DBNull.Value;
            else
                param[5].Value = Convert.ToInt32(lst_chele.SelectedValue);
            param[6] = new SqlParameter("@carpet_type", SqlDbType.TinyInt);
            if (lst_carpet.SelectedIndex == 0)
                param[6].Value = DBNull.Value;
            else
                param[6].Value = Convert.ToInt32(lst_carpet.SelectedValue);
            param[7] = new SqlParameter("@build_state", SqlDbType.TinyInt);
            param[7].Value = lst_status.SelectedValue;
            param[8] = new SqlParameter("@ibt_srl", SqlDbType.TinyInt);
            if (lst_brand.SelectedIndex == 0)
                param[8].Value = DBNull.Value;
            else
                param[8].Value = Convert.ToInt32(lst_brand.SelectedValue);
            param[9] = new SqlParameter("@city_srl", SqlDbType.TinyInt);
            if (lst_plan.SelectedIndex == 0)
                param[9].Value = DBNull.Value;
            else
                param[9].Value = Convert.ToInt32(lst_plan.SelectedValue);
            param[10] = new SqlParameter("@size_srl", SqlDbType.TinyInt);
            if (lst_size.SelectedIndex == 0)
                param[10].Value = DBNull.Value;
            else
                param[10].Value = Convert.ToInt32(lst_size.SelectedValue);
            param[11] = new SqlParameter("@color_srl", SqlDbType.TinyInt);
            if (lst_color.SelectedIndex == 0)
                param[11].Value = DBNull.Value;
            else
                param[11].Value = Convert.ToInt32(lst_color.SelectedValue);
            param[12] = new SqlParameter("@barcode", SqlDbType.Char, 30);
            param[12].Value = DBNull.Value;
            param[13] = new SqlParameter("@sold", SqlDbType.Bit);
            param[13].Value = DBNull.Value;
            param[14] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_describtion.Text))
                param[14].Value = txt_describtion.Text;
            else
                param[14].Value = DBNull.Value;
            param[15] = new SqlParameter("@lenght", SqlDbType.Char, 20);
            if (!string.IsNullOrEmpty(txt_lenght.Text))
                param[15].Value = Convert.ToDecimal(txt_lenght.Text);
            else
                param[15].Value = DBNull.Value;
            param[16] = new SqlParameter("@widht", SqlDbType.Char, 20);
            if (!string.IsNullOrEmpty(txt_weight.Text))
                param[16].Value = Convert.ToDecimal(txt_weight.Text);
            else
                param[16].Value = DBNull.Value;
            param[17] = new SqlParameter("@kind", SqlDbType.VarChar, 30);
            param[17].Value = DBNull.Value;
            param[18] = new SqlParameter("@margin_color", SqlDbType.VarChar, 30);
            param[18].Value = DBNull.Value;
            param[19] = new SqlParameter("@dorangi", SqlDbType.Bit);
            param[19].Value = DBNull.Value;
            param[20] = new SqlParameter("@rofo", SqlDbType.Bit);
            param[20].Value = DBNull.Value;
            param[21] = new SqlParameter("@kaji", SqlDbType.Bit);
            param[21].Value = DBNull.Value;
            param[22] = new SqlParameter("@badbaf", SqlDbType.Bit);
            param[22].Value = DBNull.Value;
            param[23] = new SqlParameter("@pakhordegi", SqlDbType.Bit);
            param[23].Value = DBNull.Value;
            param[24] = new SqlParameter("@tear", SqlDbType.Bit);
            param[24].Value = DBNull.Value;
            param[25] = new SqlParameter("@code_igd", SqlDbType.VarChar,30);
            param[25].Value = txt_code.Text;
            param[26] = new SqlParameter("@plan_desc", SqlDbType.VarChar,100);
            param[26].Value = DBNull.Value;
            param[27] = new SqlParameter("@provider_code", SqlDbType.VarChar, 30);
            param[27].Value = txt_pcode.Text;
            param[28] = new SqlParameter("@good_value", SqlDbType.TinyInt);
            param[28].Value = lst_value.SelectedValue;
            param[29] = new SqlParameter("@color_srl2", SqlDbType.TinyInt);
            if (lst_color2.SelectedIndex == 0)
                param[29].Value = DBNull.Value;
            else
                param[29].Value = Convert.ToInt32(lst_color2.SelectedValue);
            param[30] = new SqlParameter("@raj_srl", SqlDbType.Int);
            if (lst_raj.SelectedIndex == 0)
                param[30].Value = DBNull.Value;
            else
                param[30].Value = Convert.ToInt32(lst_raj.SelectedValue);
            new ManageCommands(param, "insert_goods");
            empty_boxes();
            preparecode();
            lblError.Text = "فرش جدید ایجاد شد";
            
        }
        private void update_goods()
        {
            if (duplicate_goods_code_update())
            {
                lblError.Text = "کد فرش بوم تکراری است";
                return;
            }
            int remain = (Convert.ToInt32(ViewState["srl_details"]) / 100) + 1;
            string path = Server.MapPath("/product_images");
            string save_image = "../product_images/" + remain;
            if (!Directory.Exists(path + "/" + remain))
                Directory.CreateDirectory(path + "/" + remain);
            path = Server.MapPath("/product_images/" + remain);
            if (upload.HasFile)
            {
                string srl = ViewState["srl_details"].ToString();
                string file_type = Path.GetExtension(upload.FileName);
                path = path + "\\" + srl + file_type;
                save_image = save_image + "\\" + srl + file_type;
                upload.SaveAs(path);
                image1.ImageUrl = save_image;
            }
            else
            {
                if (ViewState["title_igd"] != null)
                    save_image = ViewState["title_igd"].ToString();
                else
                    save_image = "..\\img\\person.png";
            }
            Common per = new Common();
            SqlParameter[] param = new SqlParameter[31];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ViewState["srl_details"]);
            param[1] = new SqlParameter("@u_date_time", SqlDbType.Char, 10);
            param[1].Value = per.persian_date();
            param[2] = new SqlParameter("@title_igd", SqlDbType.VarChar, 100);
            param[2].Value = save_image;
            param[3] = new SqlParameter("@provider_srl", SqlDbType.Int);
            param[3].Value = Convert.ToInt32(ViewState["srl"]);
            param[4] = new SqlParameter("@porz_type", SqlDbType.TinyInt);
            if (lst_porz.SelectedIndex == 0)
                param[4].Value = DBNull.Value;
            else
                param[4].Value = Convert.ToInt32(lst_porz.SelectedValue);
            param[5] = new SqlParameter("@chele_type", SqlDbType.TinyInt);
            if (lst_chele.SelectedIndex == 0)
                param[5].Value = DBNull.Value;
            else
                param[5].Value = Convert.ToInt32(lst_chele.SelectedValue);
            param[6] = new SqlParameter("@carpet_type", SqlDbType.TinyInt);
            if (lst_carpet.SelectedIndex == 0)
                param[6].Value = DBNull.Value;
            else
                param[6].Value = Convert.ToInt32(lst_carpet.SelectedValue);
            param[7] = new SqlParameter("@build_state", SqlDbType.TinyInt);
            param[7].Value = lst_status.SelectedValue;
            param[8] = new SqlParameter("@ibt_srl", SqlDbType.TinyInt);
            if (lst_brand.SelectedIndex == 0)
                param[8].Value = DBNull.Value;
            else
                param[8].Value = Convert.ToInt32(lst_brand.SelectedValue);
            param[9] = new SqlParameter("@city_srl", SqlDbType.TinyInt);
            if (lst_plan.SelectedIndex == 0)
                param[9].Value = DBNull.Value;
            else
                param[9].Value = Convert.ToInt32(lst_plan.SelectedValue);
            param[10] = new SqlParameter("@size_srl", SqlDbType.TinyInt);
            if (lst_size.SelectedIndex == 0)
                param[10].Value = DBNull.Value;
            else
                param[10].Value = Convert.ToInt32(lst_size.SelectedValue);
            param[11] = new SqlParameter("@color_srl", SqlDbType.TinyInt);
            if (lst_color.SelectedIndex == 0)
                param[11].Value = DBNull.Value;
            else
                param[11].Value = Convert.ToInt32(lst_color.SelectedValue);
            param[12] = new SqlParameter("@barcode", SqlDbType.Char, 30);
            param[12].Value = DBNull.Value;
            param[13] = new SqlParameter("@sold", SqlDbType.Bit);
            param[13].Value = DBNull.Value;
            param[14] = new SqlParameter("@describtion", SqlDbType.VarChar);
            if (!string.IsNullOrEmpty(txt_describtion.Text))
                param[14].Value = txt_describtion.Text;
            else
                param[14].Value = DBNull.Value;
            param[15] = new SqlParameter("@lenght", SqlDbType.Char, 20);
            if (!string.IsNullOrEmpty(txt_lenght.Text))
                param[15].Value = Convert.ToDecimal(txt_lenght.Text);
            else
                param[15].Value = DBNull.Value;
            param[16] = new SqlParameter("@widht", SqlDbType.Char, 20);
            if (!string.IsNullOrEmpty(txt_weight.Text))
                param[16].Value = Convert.ToDecimal(txt_weight.Text);
            else
                param[16].Value = DBNull.Value;
            param[17] = new SqlParameter("@kind", SqlDbType.VarChar, 30);
            param[17].Value = DBNull.Value;
            param[18] = new SqlParameter("@margin_color", SqlDbType.VarChar, 30);
            param[18].Value = DBNull.Value;
            param[19] = new SqlParameter("@dorangi", SqlDbType.Bit);
            param[19].Value = DBNull.Value;
            param[20] = new SqlParameter("@rofo", SqlDbType.Bit);
            param[20].Value = DBNull.Value;
            param[21] = new SqlParameter("@kaji", SqlDbType.Bit);
            param[21].Value = DBNull.Value;
            param[22] = new SqlParameter("@badbaf", SqlDbType.Bit);
            param[22].Value = DBNull.Value;
            param[23] = new SqlParameter("@pakhordegi", SqlDbType.Bit);
            param[23].Value = DBNull.Value;
            param[24] = new SqlParameter("@tear", SqlDbType.Bit);
            param[24].Value = DBNull.Value;
            param[25] = new SqlParameter("@code_igd", SqlDbType.VarChar, 30);
            param[25].Value = txt_code.Text;
            param[26] = new SqlParameter("@plan_desc", SqlDbType.VarChar, 100);
            param[26].Value = DBNull.Value;
            param[27] = new SqlParameter("@provider_code", SqlDbType.VarChar, 30);
            param[27].Value = txt_pcode.Text;
            param[28] = new SqlParameter("@good_value", SqlDbType.TinyInt);
            param[28].Value = lst_value.SelectedValue;
            param[29] = new SqlParameter("@color_srl2", SqlDbType.TinyInt);
            if (lst_color2.SelectedIndex == 0)
                param[29].Value = DBNull.Value;
            else
                param[29].Value = Convert.ToInt32(lst_color2.SelectedValue);
            param[30] = new SqlParameter("@raj_srl", SqlDbType.Int);
            if (lst_raj.SelectedIndex == 0)
                param[30].Value = DBNull.Value;
            else
                param[30].Value = Convert.ToInt32(lst_raj.SelectedValue);
            new ManageCommands(param, "update_goods");
            lblError.Text = "فرش جاری ویرایش شد";
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
                new ManageCommands(param, "del_goods");
                lblError.Text = "لینک جاری حذف شد";
                fill_grid(ViewState["srl"].ToString());
            }
            catch { lblError.Text = "عملیات ناموفق"; }
        }
        private int max_srl_goods()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.inv_goods";
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
            empty_boxes();
            preparecode();
        }
        private void empty_boxes()
        {
            ViewState["update"] = "0";
            txt_describtion.Text = string.Empty;
            lst_brand.SelectedIndex = 0;
            lst_status.SelectedIndex = 0;
            lst_carpet.SelectedIndex = 0;
            lst_chele.SelectedIndex = 0;
            lst_porz.SelectedIndex = 0;
            lst_color.SelectedIndex = 0;
            lst_color2.SelectedIndex = 0;
            lst_porz.SelectedIndex = 0;
            lst_size.SelectedIndex = 0;
            lst_type.SelectedIndex = 0;
            lst_value.SelectedIndex = 0;
            lst_plan.SelectedIndex = 0;
            txt_lenght.Text = string.Empty;
            txt_weight.Text = string.Empty;
            txt_code.Text = string.Empty;
            txt_pcode.Text = string.Empty;
            image1.ImageUrl = "..\\img\\person.png";
        }
        protected void lnkPrice_Click(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            Session["purchase"] = ViewState["srl"].ToString();
            Response.Redirect("Purchase_Price.aspx?snd=" + lnkRemove.CommandArgument);
        }
        private bool duplicate_provider_name()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_name('" + txt_provider_name.Text + "')";
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
        private bool duplicate_provider_name_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_name('" + txt_provider_name.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_provider_name.Text != ViewState["name"].ToString())
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
        private bool duplicate_provider_tel1()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_tel1('" + txt_tel1.Text + "')";
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
        private bool duplicate_provider_tel1_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_tel1('" + txt_tel1.Text + "')";
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
        private bool duplicate_provider_cell_phone()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_cellphone('" + txt_cell_phone.Text + "')";
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
        private bool duplicate_provider_cell_phone_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_provider_cellphone('" + txt_cell_phone.Text + "')";
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
        private bool duplicate_goods_code()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_goods_code('" + txt_code.Text + "')";
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
        private bool duplicate_goods_code_update()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select dbo.Duplicate_goods_code('" + txt_code.Text + "')";
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0 && txt_code.Text != ViewState["code_igd"].ToString())
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
        protected void ImageButton_print_Click(object sender, ImageClickEventArgs e)
        {
            if(ViewState["srl_details"] == null)
            {
                lblError.Text = "فرش را انتخاب نمائید";
                return;
            }
            else
            {
                Response.Redirect("print_goods_a6.aspx?snd=" + ViewState["srl_details"]);
            }
        }
        private void preparecode()
        {
            if(ViewState["srl"] != null && !string.IsNullOrEmpty(txt_provider_code.Text))
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT Convert(int,code_igd) As code FROM inv_goods Where provider_srl = " + ViewState["srl"]);
                if(dt.Rows.Count > 0)
                {
                    object max_code;
                    max_code = dt.Compute("MAX(code)", "");
                    txt_code.Text = (Convert.ToInt64(max_code) + 1).ToString();
                }
                else
                {
                    txt_code.Text = txt_provider_code.Text.Substring(0, 2) + "1000";
                }
            }
        }
        protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview.PageIndex = e.NewPageIndex;
            fill_grid(ViewState["srl"].ToString());            
        }
    }
}