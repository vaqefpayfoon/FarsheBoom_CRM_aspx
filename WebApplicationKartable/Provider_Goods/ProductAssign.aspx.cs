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
    public partial class ProductAssign : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                ViewState["update"] = "0"; image1.ImageUrl = "..\\img\\person.png";
                if(!string.IsNullOrEmpty(Request.QueryString["srl"]))
                {
                    set_query_string(Request.QueryString["srl"]);
                }
            }
        }
        protected void btn_filter_Click(object sender, EventArgs e)
        {
            ViewState["srl"] = lst_provider.SelectedValue;
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT provider_code FROM dbo.bas_provider WHERE srl=" + lst_provider.SelectedValue);
            fill_grid(lst_provider.SelectedValue);
            if(dt.Rows.Count > 0)            
                ViewState["provider_code"] = dt.Rows[0][0].ToString();            
        }
        private void set_query_string(string srl)
        {
            Search obj = new Search(strConnString);
            DataTable dt = new DataTable();
            {
                dt = obj.Get_Data("SELECT srl, u_date_time, title_igd, provider_srl, porz_type, chele_type, carpet_type, build_state, ibt_srl, city_srl, size_srl, color_srl, describtion, lenght, widht, margin_color, dorangi, rofo, kaji, badbaf, pakhordegi, tear, code_igd, provider_code, good_value, color_srl2,raj_srl FROM dbo.inv_goods Where srl = " + srl);
                if (dt.Rows.Count > 0)
                {                    
                    DataRow row = dt.Rows[0];
                    ViewState["update"] = "1";
                    ViewState["srl_details"] = row["srl"].ToString();
                    //
                    lst_provider.SelectedValue = row["provider_srl"].ToString();
                    ViewState["srl"] = row["provider_srl"];
                    DataTable dt2 = new DataTable();
                    dt2 = obj.Get_Data("SELECT provider_code FROM dbo.bas_provider WHERE srl=" + row["provider_srl"]);
                    fill_grid(row["provider_srl"].ToString());
                    if (dt2.Rows.Count > 0)
                        ViewState["provider_code"] = dt2.Rows[0][0].ToString();
                    //
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
                    ViewState["describtion"] = row["describtion"].ToString();
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
            if(ViewState["provider_code"] == null)
            {
                lblError.Text = "لطفا نمایش فرش را اننخاب نمائید";
            }
            if (txt_code.Text.StartsWith(ViewState["provider_code"].ToString().Substring(0, 2)))
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
                dt = obj.Get_Data("SELECT srl, u_date_time, title_igd, provider_srl, porz_type, chele_type, carpet_type, build_state, ibt_srl, city_srl, size_srl, color_srl, describtion, lenght, widht, margin_color, dorangi, rofo, kaji, badbaf, pakhordegi, tear, code_igd, provider_code, good_value, color_srl2, plan_desc, price_home FROM dbo.inv_goods Where code_igd = " + gridview.SelectedRow.Cells[0].Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    ViewState["update"] = "1";
                    ViewState["srl_details"] = row["srl"].ToString();
                    ViewState["code_igd"] = row["code_igd"].ToString();
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
                    ViewState["describtion"] = row["describtion"].ToString();
                    txt_lenght.Text = row["lenght"].ToString();
                    txt_weight.Text = row["widht"].ToString();
                    //txt_color.Text = row["margin_color"].ToString();
                    if (!Convert.IsDBNull(row["dorangi"]))
                        chk_dorangi.Checked = Convert.ToBoolean(row["dorangi"]);
                    else
                        chk_dorangi.Checked = false;
                    if (!Convert.IsDBNull(row["rofo"]))
                        chk_rofo.Checked = Convert.ToBoolean(row["rofo"]);
                    else
                        chk_rofo.Checked = false;
                    if (!Convert.IsDBNull(row["kaji"]))
                        chk_kaji.Checked = Convert.ToBoolean(row["kaji"]);
                    else
                        chk_kaji.Checked = false;
                    if (!Convert.IsDBNull(row["badbaf"]))
                        chk_badbaf.Checked = Convert.ToBoolean(row["badbaf"]);
                    else
                        chk_badbaf.Checked = false;
                    if (!Convert.IsDBNull(row["pakhordegi"]))
                        chk_pakhordegi.Checked = Convert.ToBoolean(row["pakhordegi"]);
                    else
                        chk_pakhordegi.Checked = false;
                    if (!Convert.IsDBNull(row["tear"]))
                        chk_tear.Checked = Convert.ToBoolean(row["tear"]);
                    else
                        chk_tear.Checked = false;
                    txt_code.Text = row["code_igd"].ToString();
                    ViewState["code_igd"] = row["code_igd"].ToString();
                    txt_pcode.Text = row["provider_code"].ToString();
                    txt_plan_desc.Text = row["plan_desc"].ToString();
                    if (!Convert.IsDBNull(row["good_value"]))
                        lst_value.SelectedValue = row["good_value"].ToString();
                    else
                        lst_value.SelectedIndex = 0;
                    Common common = new Common();
                    if (!Convert.IsDBNull(row["price_home"]))
                    {
                        common.str = row["price_home"].ToString();
                        txt_price_home.Text = common.str;
                    }
                    else
                    {
                        txt_price_home.Text = string.Empty;
                    }
                }
            }
        }
        private void fill_grid(string srl)
        {
            Search obj = new Search(strConnString); DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT srl, code_igd, brand_name, size_title, color_name, carpet_title, plan_title, porz_title, chele_title FROM Provider_Goods Where provider_srl = " + srl + " order by code_igd desc");
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
            int remainBarcode = (Convert.ToInt32(ViewState["srl_details"]) / 100) + 1;
            string pathBarcode = Server.MapPath("/Barcode_images");
            string save_imageBarcode = "../Barcode_images/" + remainBarcode;
            if (!Directory.Exists(pathBarcode + "/" + remainBarcode))
                Directory.CreateDirectory(pathBarcode + "/" + remainBarcode);
            pathBarcode = Server.MapPath("/Barcode_images/" + remainBarcode);
            if (FileUploadBarcode.HasFile)
            {
                string srlBarcode = txt_code.Text;
                string file_type = Path.GetExtension(FileUploadBarcode.FileName);
                pathBarcode = pathBarcode + "\\" + srlBarcode + file_type;
                save_imageBarcode = save_imageBarcode + "\\" + srlBarcode + file_type;
                FileUploadBarcode.SaveAs(pathBarcode);
                //image2.ImageUrl = save_imageBarcode;
            }
            else
            {
                if (ViewState["describtion"] != null)
                    save_imageBarcode = ViewState["describtion"].ToString();
                else
                    save_imageBarcode = "..\\img\\person.png";
            }
            double dbl_price_home = 0;
            if (!string.IsNullOrEmpty(txt_price_home.Text))
                dbl_price_home = Convert.ToDouble(obj.remove_cama(txt_price_home.Text));
            SqlParameter[] param = new SqlParameter[32];
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
            if (!string.IsNullOrEmpty(txt_barcode.Text))
                param[12].Value = txt_barcode.Text;
            else
                param[12].Value = DBNull.Value;
            param[13] = new SqlParameter("@sold", SqlDbType.Bit);
            param[13].Value = DBNull.Value;
            param[14] = new SqlParameter("@describtion", SqlDbType.VarChar);
            param[14].Value = save_imageBarcode;
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
            param[19].Value = chk_dorangi.Checked;
            param[20] = new SqlParameter("@rofo", SqlDbType.Bit);
            param[20].Value = chk_rofo.Checked;
            param[21] = new SqlParameter("@kaji", SqlDbType.Bit);
            param[21].Value = chk_kaji.Checked;
            param[22] = new SqlParameter("@badbaf", SqlDbType.Bit);
            param[22].Value = chk_badbaf.Checked;
            param[23] = new SqlParameter("@pakhordegi", SqlDbType.Bit);
            param[23].Value = chk_pakhordegi.Checked;
            param[24] = new SqlParameter("@tear", SqlDbType.Bit);
            param[24].Value = chk_tear.Checked;
            param[25] = new SqlParameter("@code_igd", SqlDbType.VarChar, 30);
            param[25].Value = txt_code.Text;
            param[26] = new SqlParameter("@plan_desc", SqlDbType.VarChar, 100);
            param[26].Value = txt_plan_desc.Text;
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
            param[31] = new SqlParameter("@price_home", SqlDbType.Int);
            if (string.IsNullOrEmpty(txt_price_home.Text))
                param[31].Value = DBNull.Value;
            else
                param[31].Value = dbl_price_home;
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
            int remainBarcode = (Convert.ToInt32(ViewState["srl_details"]) / 100) + 1;
            string pathBarcode = Server.MapPath("/Barcode_images");
            string save_imageBarcode = "../Barcode_images/" + remainBarcode;
            if (!Directory.Exists(pathBarcode + "/" + remainBarcode))
                Directory.CreateDirectory(pathBarcode + "/" + remainBarcode);
            pathBarcode = Server.MapPath("/Barcode_images/" + remainBarcode);
            if (FileUploadBarcode.HasFile)
            {
                string srlBarcode = txt_code.Text;
                string file_type = Path.GetExtension(FileUploadBarcode.FileName);
                pathBarcode = pathBarcode + "\\" + srlBarcode + file_type;
                save_imageBarcode = save_imageBarcode + "\\" + srlBarcode + file_type;
                FileUploadBarcode.SaveAs(pathBarcode);
                //image2.ImageUrl = save_imageBarcode;
            }
            else
            {
                if (ViewState["describtion"] != null)
                    save_imageBarcode = ViewState["describtion"].ToString();
                else
                    save_imageBarcode = "..\\img\\person.png";
            }
            Common obj = new Common();
            double dbl_price_home = 0;
            if (!string.IsNullOrEmpty(txt_price_home.Text))
                dbl_price_home = Convert.ToDouble(obj.remove_cama(txt_price_home.Text));
            Common per = new Common();
            SqlParameter[] param = new SqlParameter[32];
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
            if (!string.IsNullOrEmpty(txt_barcode.Text))
                param[12].Value = txt_barcode.Text;
            else
                param[12].Value = DBNull.Value;
            param[13] = new SqlParameter("@sold", SqlDbType.Bit);
            param[13].Value = DBNull.Value;
            param[14] = new SqlParameter("@describtion", SqlDbType.VarChar);
            param[14].Value = save_imageBarcode;
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
            param[19].Value = chk_dorangi.Checked;
            param[20] = new SqlParameter("@rofo", SqlDbType.Bit);
            param[20].Value = chk_rofo.Checked;
            param[21] = new SqlParameter("@kaji", SqlDbType.Bit);
            param[21].Value = chk_kaji.Checked;
            param[22] = new SqlParameter("@badbaf", SqlDbType.Bit);
            param[22].Value = chk_badbaf.Checked;
            param[23] = new SqlParameter("@pakhordegi", SqlDbType.Bit);
            param[23].Value = chk_pakhordegi.Checked;
            param[24] = new SqlParameter("@tear", SqlDbType.Bit);
            param[24].Value = chk_tear.Checked;
            param[25] = new SqlParameter("@code_igd", SqlDbType.VarChar, 30);
            param[25].Value = txt_code.Text;
            param[26] = new SqlParameter("@plan_desc", SqlDbType.VarChar, 100);
            param[26].Value = txt_plan_desc.Text;
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
            param[31] = new SqlParameter("@price_home", SqlDbType.Int);
            if (string.IsNullOrEmpty(txt_price_home.Text))
                param[31].Value = DBNull.Value;
            else
                param[31].Value = dbl_price_home;
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
            txt_barcode.Text = string.Empty;
            lst_brand.SelectedIndex = 0;
            lst_status.SelectedIndex = 0;
            lst_carpet.SelectedIndex = 0;
            lst_chele.SelectedIndex = 0;
            lst_porz.SelectedIndex = 0;
            lst_color.SelectedIndex = 0;
            lst_color2.SelectedIndex = 0;
            lst_porz.SelectedIndex = 0;
            lst_size.SelectedIndex = 0;
            lst_value.SelectedIndex = 0;
            lst_plan.SelectedIndex = 0;
            txt_lenght.Text = string.Empty;
            txt_weight.Text = string.Empty;
            txt_code.Text = string.Empty;
            txt_pcode.Text = string.Empty;
            txt_plan_desc.Text = string.Empty;
            image1.ImageUrl = "..\\img\\person.png";
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
            if (ViewState["srl_details"] == null)
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
            if (ViewState["srl"] != null && !string.IsNullOrEmpty(ViewState["provider_code"].ToString()))
            {
                DataTable dt = new DataTable(); Search obj = new Search(strConnString);
                dt = obj.Get_Data("SELECT Convert(int,code_igd) As code FROM inv_goods Where provider_srl = " + ViewState["srl"]);
                if (dt.Rows.Count > 0)
                {
                    object max_code;
                    max_code = dt.Compute("MAX(code)", "");
                    txt_code.Text = (Convert.ToInt64(max_code) + 1).ToString();
                }
                else
                {
                    txt_code.Text = ViewState["provider_code"].ToString().Substring(0, 2) + "1000";
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