using System;
using System.Data;
using System.Configuration;
using Cartable;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Linq.Expressions;
using System.Web.UI;

namespace WebApplicationKartable
{
    public partial class Pricing : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        private Common obobo = new Common();
        private string columnName = "";
        private int doneColouring = 0;
        private int doneColouring2 = 0;
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin(); lblError.Text = string.Empty;
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Confirm","<script type='text/javascript'>Confirm();</script>", true);
            //ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "Confirm();", true);
            if (!IsPostBack)
            {
                image1.ImageUrl = "..\\img\\person.png";
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
        protected void ImageButton2_save_Click(object sender, EventArgs e)
        {
            if (ViewState["srl_good"] == null)
            {
                lblError.Text = "لطفا یک فرش را انتخاب نمائید";
                return;
            }
            Common obj = new Common();
            double dbl_sale_price = 0;
            double dbl_buy_price = 0;
            double dbl_discount = 0;
            if (!string.IsNullOrEmpty(txt_sell.Text))
                dbl_sale_price = Convert.ToDouble(obj.remove_cama(txt_sell.Text));
            if (!string.IsNullOrEmpty(txt_buy.Text))
                dbl_buy_price = Convert.ToDouble(obj.remove_cama(txt_buy.Text));
            if (!string.IsNullOrEmpty(txt_discount.Text))
                dbl_discount = Convert.ToDouble(obj.remove_cama(txt_discount.Text));
            double dbl_price_home = 0;
            if (!string.IsNullOrEmpty(txt_price_home.Text))
                dbl_price_home = Convert.ToDouble(obj.remove_cama(txt_price_home.Text));
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update inv_goods set sale_price=@sale_price,buy_price=@buy_price, discount=@discount, price_home=@price_home where srl=@srl";
            cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(ViewState["srl_good"]);
            if(dbl_sale_price == 0)
                cmd.Parameters.Add("@sale_price", SqlDbType.BigInt).Value = DBNull.Value;
            else
                cmd.Parameters.Add("@sale_price", SqlDbType.BigInt).Value = dbl_sale_price;
            if(dbl_buy_price == 0)
                cmd.Parameters.Add("@buy_price", SqlDbType.BigInt).Value = DBNull.Value;
            else
                cmd.Parameters.Add("@buy_price", SqlDbType.BigInt).Value = dbl_buy_price;
            cmd.Parameters.Add("@discount", SqlDbType.BigInt).Value = dbl_discount;
            if (dbl_price_home == 0)
                cmd.Parameters.Add("@price_home", SqlDbType.BigInt).Value = DBNull.Value;
            else
                cmd.Parameters.Add("@price_home", SqlDbType.BigInt).Value = dbl_price_home;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //List<PricingClass> lstPricingClass = ViewState["lstOutstandingOrders"] as List<PricingClass>;
            //PricingClass Found = lstPricingClass.Find(Woak => Woak.srl == ViewState["srl_good"].ToString());

            DataTable dt = new DataTable(); Search objdb = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            dt = objdb.Get_Data(string.Format("SELECT srl,code_igd, brand_name, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale,discount_amount, final_sale,provider_name,price_home FROM dbo.Sale_Pricing where srl={0}", ViewState["srl_good"]));
            if (dt.Rows.Count > 0)
            {
                ViewState["buy_price"] = dt.Rows[0]["buy_price"];
                ViewState["sale_price"] = dt.Rows[0]["sale_price"];
                ViewState["u_buy"] = dt.Rows[0]["u_buy"];
                ViewState["u_sale"] = dt.Rows[0]["u_sale"];
                ViewState["discount_amount"] = dt.Rows[0]["discount_amount"];
                ViewState["final_sale"] = dt.Rows[0]["final_sale"];
                if(!Convert.IsDBNull(dt.Rows[0]["price_home"]))
                    ViewState["price_home"] = dt.Rows[0]["price_home"];
                else
                    ViewState["price_home"] = string.Empty;
                load_panel();
            }
            lblError.Text = "قیمت گذاری انجام شد";
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
            param[3].Value = Convert.ToInt32(ViewState["provider_srl"]);
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
            param[19].Value = chk_choose.Checked;
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
        private void set_boxes(string srl)
        {
            Search obj = new Search(strConnString);
            DataTable dt = new DataTable();
            {
                dt = obj.Get_Data("SELECT srl, u_date_time, title_igd, provider_srl, porz_type, chele_type, carpet_type, build_state, ibt_srl, city_srl, size_srl, color_srl, lenght, widht, margin_color, dorangi, rofo, kaji, badbaf, pakhordegi, tear, code_igd, provider_code, good_value, color_srl2,raj_srl, buy_price, sale_price, discount,price_home FROM dbo.inv_goods Where srl = " + srl);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    ViewState["provider_srl"] = row["provider_srl"].ToString();
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
                    if (!Convert.IsDBNull(row["raj_srl"]))
                        lst_raj.SelectedValue = row["raj_srl"].ToString();
                    else
                        lst_raj.SelectedIndex = 0;
                    txt_u_date_time.Text = row["u_date_time"].ToString();
                    txt_lenght.Text = row["lenght"].ToString();
                    txt_weight.Text = row["widht"].ToString();
                    if(row["lenght"] != null && row["widht"]!= null)
                    {
                        List<PricingClass> lstPricingClass = new List<PricingClass>();
                        lstPricingClass = (List<PricingClass>) ViewState["lstOutstandingOrders"];
                        txt_area.Text = lstPricingClass.Find(woak => woak.srl == srl).area;
                    }
                    txt_code.Text = row["code_igd"].ToString();
                    ViewState["code_igd"] = row["code_igd"].ToString();
                    txt_pcode.Text = row["provider_code"].ToString();
                    if (!Convert.IsDBNull(row["good_value"]))
                        lst_value.SelectedValue = row["good_value"].ToString();
                    else
                        lst_value.SelectedIndex = 0;
                    obobo.str = row["buy_price"].ToString();
                    txt_buy.Text = obobo.str;
                    obobo.str = row["sale_price"].ToString();
                    txt_sell.Text = obobo.str;
                    obobo.str = row["price_home"].ToString();
                    txt_price_home.Text = obobo.str;
                    txt_discount.Text = row["discount"].ToString();
                    if (!Convert.IsDBNull(row["dorangi"]))
                        chk_choose.Checked = Convert.ToBoolean(row["dorangi"]);
                    else
                        chk_choose.Checked = false;
                }
            }
        }
        private void SortGridViewOutstanding(string sortExpression, string direction)
        {
            if (ViewState["lstOutstandingOrders"] != null)
            {

                List<PricingClass> lstPricingClass = (List<PricingClass>)ViewState["lstOutstandingOrders"];

                var param = Expression.Parameter(typeof(PricingClass), sortExpression);
                var sortby = Expression.Lambda<Func<PricingClass, object>>(Expression.Convert(Expression.Property(param, sortExpression), typeof(object)), param);
                if (direction == "ASC")
                {
                    lstPricingClass = lstPricingClass.AsQueryable<PricingClass>().OrderBy(sortby).ToList();
                }
                else
                {
                    lstPricingClass = lstPricingClass.AsQueryable<PricingClass>().OrderByDescending(sortby).ToList();
                }
                ViewState["lstOutstandingOrders"] = lstPricingClass;
                grdViewOutstanding.DataSource = lstPricingClass;
                grdViewOutstanding.DataBind();

                upnlOutstanding.Update();
                ResetFilterAndValueOutstanding();
            }
        }
        protected void grdViewOutstanding_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && doneColouring2 == 0)
            {
                GridViewRow headerRow = grdViewOutstanding.HeaderRow;
                if (ViewState["columnNameO"] != null) columnName = ViewState["columnNameO"].ToString();
                for (int i = 0; i < headerRow.Cells.Count; i++)
                {
                    if (headerRow.Cells[i].Controls.Count != 0)
                    {
                        //if (!(headerRow.Cells[i].Controls[0] is System.Web.UI.LiteralControl))
                        //{
                        if (((LinkButton)headerRow.Cells[i].Controls[1]).Text == columnName)
                        {
                            headerRow.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#2F8CDE");
                            Image img = new Image();
                            img.CssClass = "imgClass";
                            if (GridViewSortDirection == SortDirection.Ascending)
                            {
                                img.ImageUrl = "./images/up.png";
                            }
                            if (GridViewSortDirection == SortDirection.Descending)
                            {
                                img.ImageUrl = "./images/down.png";
                            }
                            headerRow.Cells[i].Controls.Add(img);
                            doneColouring2 = 1;
                        }
                        //}
                    }
                }
            }
        }
        protected void grdViewOutstanding_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            columnName = e.SortExpression;
            ViewState["columnNameO"] = columnName;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridViewOutstanding(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridViewOutstanding(sortExpression, "ASC");
            }

        }
        protected void grdViewOutstanding_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["lstOutstandingOrders"] != null)
            {
                List<PricingClass> lstOutstandingOrders = (List<PricingClass>)ViewState["lstOutstandingOrders"];
                grdViewOutstanding.PageIndex = e.NewPageIndex;
                ViewState["lstOutstandingOrders"] = lstOutstandingOrders;
                grdViewOutstanding.DataSource = lstOutstandingOrders;
                grdViewOutstanding.DataBind();
                ResetFilterAndValueOutstanding();
            }
        }
        protected void txt_TextChanged(object sender, EventArgs e)
        {

            if (sender is TextBox)
            {
                if (ViewState["lstOutstandingOrders"] != null)
                {
                    List<PricingClass> allPricingClass = (List<PricingClass>)ViewState["lstOutstandingOrders"];
                    TextBox txtBox = (TextBox)sender;
                    if (txtBox.ID == "txtprovider_name")
                    {
                        allPricingClass = allPricingClass.Where(x => x.provider_name.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Oprovider_name"] = txtBox.Text.Trim().ToUpper();
                    }
                    if (txtBox.ID == "txtcode_igd")
                    {
                        allPricingClass = allPricingClass.Where(x => x.code_igd.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ocode_igd"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtbrand_name")
                    {
                        allPricingClass = allPricingClass.Where(x => x.brand_name.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Obrand_name"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtsize_title")
                    {
                        allPricingClass = allPricingClass.Where(x => x.size_title.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Osize_title"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtcarpet_title")
                    {
                        allPricingClass = allPricingClass.Where(x => x.carpet_title.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ocarpet_title"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtarea")
                    {
                        allPricingClass = allPricingClass.Where(x => x.area.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Oarea"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtdiscount")
                    {
                        allPricingClass = allPricingClass.Where(x => x.discount.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Odiscount"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtprovider_code")
                    {
                        allPricingClass = allPricingClass.Where(x => x.provider_code.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Oprovider_code"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtbuy_price")
                    {
                        allPricingClass = allPricingClass.Where(x => x.buy_price.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Obuy_price"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtsale_price")
                    {
                        allPricingClass = allPricingClass.Where(x => x.sale_price.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Osale_price"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtu_buy")
                    {
                        allPricingClass = allPricingClass.Where(x => x.u_buy.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ou_buy"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtu_sale")
                    {
                        allPricingClass = allPricingClass.Where(x => x.u_sale.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ou_sale"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtdiscount_amount")
                    {
                        allPricingClass = allPricingClass.Where(x => x.discount_amount.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Odiscount_amount"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtfinal_sale")
                    {
                        allPricingClass = allPricingClass.Where(x => x.final_sale.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ofinal_sale"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtu_date_time")
                    {
                        allPricingClass = allPricingClass.Where(x => x.u_date_time.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ou_date_time"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtmargin_profit")
                    {
                        allPricingClass = allPricingClass.Where(x => x.margin_profit.ToString().Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Omargin_profit"] = txtBox.Text.Trim().ToUpper();
                    }
                    ViewState["lstOutstandingOrders"] = allPricingClass;
                    grdViewOutstanding.DataSource = allPricingClass;
                    grdViewOutstanding.DataBind();
                    ResetFilterAndValueOutstanding();
                }
            }
        }
        protected void ResetFilterAndValueOutstanding()
        {
            if (ViewState["Ocode_igd"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtcode_igd")).Text = ViewState["Ocode_igd"].ToString().ToUpper();
            if (ViewState["Obrand_name"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtbrand_name")).Text = ViewState["Obrand_name"].ToString().ToUpper();
            if (ViewState["Osize_title"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtsize_title")).Text = ViewState["Osize_title"].ToString().ToUpper();
            //if (ViewState["Oarea"] != null)
            //    ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtarea")).Text = ViewState["Oarea"].ToString().ToUpper();
            if (ViewState["Ocarpet_title"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtcarpet_title")).Text = ViewState["Ocarpet_title"].ToString().ToUpper();
            if (ViewState["Oprovider_name"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtprovider_name")).Text = ViewState["Oprovider_name"].ToString().ToUpper();
            if (ViewState["Odiscount"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtdiscount")).Text = ViewState["Odiscount"].ToString().ToUpper();
            if (ViewState["Oprovider_code"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtprovider_code")).Text = ViewState["Oprovider_code"].ToString().ToUpper();
            if (ViewState["Obuy_price"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtbuy_price")).Text = ViewState["Obuy_price"].ToString().ToUpper();
            if (ViewState["Osale_price"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtsale_price")).Text = ViewState["Osale_price"].ToString().ToUpper();
            if (ViewState["Ou_buy"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtu_buy")).Text = ViewState["Ou_buy"].ToString().ToUpper();
            if (ViewState["Ou_sale"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtu_sale")).Text = ViewState["Ou_sale"].ToString().ToUpper();
            if (ViewState["Odiscount_amount"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtdiscount_amount")).Text = ViewState["Odiscount_amount"].ToString().ToUpper();
            if (ViewState["Ofinal_sale"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtfinal_sale")).Text = ViewState["Ofinal_sale"].ToString().ToUpper();
            if (ViewState["Ou_date_time"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtu_date_time")).Text = ViewState["Ou_date_time"].ToString().ToUpper();
            if (ViewState["Omargin_profit"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtmargin_profit")).Text = ViewState["Omargin_profit"].ToString().ToUpper();
            if (ViewState["Opercent_profit"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtpercent_profit")).Text = ViewState["Opercent_profit"].ToString().ToUpper();
        }
        protected void lbRemoveFilterOutstanding_Click(object sender, EventArgs e)
        {
            if (ViewState["Ocode_igd"] != null) ViewState["Ocode_igd"] = null;
            if (ViewState["Obrand_name"] != null) ViewState["Obrand_name"] = null;
            if (ViewState["Osize_title"] != null) ViewState["Osize_title"] = null;
            if (ViewState["Oarea"] != null) ViewState["Oarea"] = null;
            if (ViewState["Ocode_igd"] != null) ViewState["Ocode_igd"] = null;
            if (ViewState["Oprovider_name"] != null) ViewState["Oprovider_name"] = null;
            if (ViewState["Odiscount"] != null) ViewState["Odiscount"] = null;
            if (ViewState["Oprovider_code"] != null) ViewState["Oprovider_code"] = null;
            if (ViewState["Obuy_price"] != null) ViewState["Obuy_price"] = null;
            if (ViewState["Osale_price"] != null) ViewState["Osale_price"] = null;
            if (ViewState["Ou_buy"] != null) ViewState["Ou_buy"] = null;
            if (ViewState["Ou_sale"] != null) ViewState["Ou_sale"] = null;
            if (ViewState["Odiscount_amount"] != null) ViewState["Odiscount_amount"] = null;
            if (ViewState["Ofinal_sale"] != null) ViewState["Ofinal_sale"] = null;
            if (ViewState["Ou_date_time"] != null) ViewState["Ou_date_time"] = null;
            if (ViewState["Omargin_profit"] != null) ViewState["Omargin_profit"] = null;
            if (ViewState["Opercent_profit"] != null) ViewState["Opercent_profit"] = null;

            PricingClass objOutstanding = new PricingClass();
            List<PricingClass> lstPricingClass = new List<PricingClass>();
            if(ViewState["getall"].ToString().Equals("0"))
                lstPricingClass = objOutstanding.GetPricingClass(1,"-1");
            else
                lstPricingClass = objOutstanding.GetPricingClass(2, "-1");

            grdViewOutstanding.DataSource = lstPricingClass;
            grdViewOutstanding.DataBind();

            ViewState["lstOutstandingOrders"] = lstPricingClass;
        }        
        protected void ImageButton_save_Click(object sender, EventArgs e)
        {
            if (ViewState["code_igd"] != null)
                update_goods();
        }
        protected void grdViewOutstanding_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["srl_good"] = grdViewOutstanding.SelectedRow.Cells[0].Text.Trim();
            List<PricingClass> lstPricingClass = ViewState["lstOutstandingOrders"] as List<PricingClass>;
            PricingClass Found = lstPricingClass.Find(Woak => Woak.srl == ViewState["srl_good"].ToString());
            ViewState["srl_good"] = Found.srl;
            ViewState["buy_price"] = Found.buy_price;
            ViewState["sale_price"] = Found.sale_price;
            ViewState["u_buy"] = Found.u_buy;
            ViewState["u_sale"] = Found.u_sale;
            ViewState["discount_amount"] = Found.discount_amount;
            ViewState["final_sale"] = Found.final_sale;
            //load_panel();
        }
        protected void btn_load_Click(object sender, EventArgs e)
        {
            load_panel();
        }
        private void load_panel()
        {
            if (ViewState["srl_good"] != null)
                set_boxes(ViewState["srl_good"].ToString());
            double buy_price = 0, sale_price = 0, u_buy = 0, u_sale = 0, discount_amount = 0, final_sale = 0, area = 0, price_home = 0;
            if (ViewState["area"] != null)
                txt_area.Text = ViewState["area"].ToString();
            try
            {
                if (ViewState["buy_price"] != null)
                    buy_price = Convert.ToDouble(ViewState["buy_price"]);
            }
            catch {}
            try
            {
                if (ViewState["sale_price"] != null)
                    sale_price = Convert.ToDouble(ViewState["sale_price"]);
            }
            catch {}
            try
            {
                if (ViewState["u_buy"] != null)
                    u_buy = Convert.ToDouble(ViewState["u_buy"]);
            }
            catch { }
            try
            {
                if (ViewState["u_sale"] != null)
                    u_sale = Convert.ToDouble(ViewState["u_sale"]);
            }
            catch { }
            try
            {
                if (ViewState["discount_amount"] != null)
                    discount_amount = Convert.ToDouble(ViewState["discount_amount"]);
            }
            catch { }
            try
            {
                if (ViewState["final_sale"] != null)
                    final_sale = Convert.ToDouble(ViewState["final_sale"]);
            }
            catch { }
            try
            {
                if (ViewState["price_home"] != null)
                    price_home = Convert.ToDouble(ViewState["price_home"]);
            }
            catch { }
            obobo.str = (final_sale - buy_price).ToString();
                List<PricingClass> lst = ViewState["lstOutstandingOrders"] as List<PricingClass>;
                foreach (PricingClass Woak in lst)
                {
                    if (Woak.srl.Equals(ViewState["srl_good"].ToString()))
                        if (!string.IsNullOrEmpty(Woak.area))
                            area = Convert.ToDouble(Woak.area);
                }
                if ((final_sale - buy_price) > 0)
                    lbl_Margin.Text = " حاشیه سود : " + obobo.str;
                else
                    lbl_Margin.Text = string.Empty;
            obobo.str = (price_home - sale_price).ToString();
            lblCompaire.Text = "مقایسه قیمت = " + obobo.str;
                //if (area != 0)
                //{
                //    obobo.str = Math.Round(((final_sale / area) - u_buy), 0).ToString();
                //    lbl_UnitMargin.Text = " حاشیه سود متراژ : " + obobo.str;
                //}
                //else
                //    lbl_UnitMargin.Text = string.Empty;
                lbl_MarginRatio.Text = " درصد حاشیه سود : " + Math.Round((((final_sale - buy_price) / final_sale) * 100), 0).ToString();
            Search obj = new Search(strConnString);
            DataTable dt = new DataTable();
            dt = obj.Get_Data("SELECT dbo.bas_project.project_code, dbo.bas_project_goods.igd_srl FROM dbo.bas_project INNER JOIN dbo.bas_project_goods ON dbo.bas_project.srl = dbo.bas_project_goods.header_srl Where igd_srl = " + ViewState["srl_good"].ToString());
            if (dt.Rows.Count > 0)
            {
                string project = " تخصیص در نمایشگاه/های : ";
                foreach (DataRow Row in dt.Rows)
                {
                    project += Row["project_code"].ToString() + "  ,  ";
                }
                lbl_project_goods.Text = project;
            }
            else
                lbl_project_goods.Text = string.Empty;
        }
        protected void btn_assign_Click(object sender, EventArgs e)
        {
            if(ViewState["srl_good"] == null)
            {
                lblError.Text = "فرشی بارگذاری نشده است";
                return;
            }
            int srl = max_srl_details();
            if (!Duplicate_GoodsInproject(ViewState["srl_good"].ToString(), lst_project.SelectedValue))
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@srl", SqlDbType.Int);
                param[0].Value = srl;
                param[1] = new SqlParameter("@header_srl", SqlDbType.Int);
                param[1].Value = Convert.ToInt32(lst_project.SelectedValue);
                param[2] = new SqlParameter("@igd_srl", SqlDbType.Int);
                param[2].Value = Convert.ToInt32(ViewState["srl_good"]);
                new ManageCommands(param, "insert_project_goods");
                SqlConnection con = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update inv_goods set selection=@selection where srl=@srl;";
                cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(ViewState["srl_good"]);
                cmd.Parameters.Add("@selection", SqlDbType.Bit).Value = true;
                cmd.ExecuteNonQuery();
                con.Close();
                lblError.Text = "تخصیص انجام شد";
                load_panel();
            }
            else
            {
                lblError.Text = "فرش انتخاب شده در همین نمایشگاه موجود می باشد";
                return;
            }            
        }
        private bool Duplicate_GoodsInproject(string igd, string header_srl)
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
        protected void btn_cache_all_data_Click(object sender, EventArgs e)
        {
            PricingClass objPricingClass = new PricingClass();
            List<PricingClass> lstPricingClass = new List<PricingClass>();
            lstPricingClass = objPricingClass.GetPricingClass(2, "-1");
            grdViewOutstanding.DataSource = lstPricingClass;
            grdViewOutstanding.DataBind();
            ViewState["lstOutstandingOrders"] = lstPricingClass;
            upnlOutstanding.Update();
            ViewState["getall"] = 1;
        }
        protected void btn_load_without_price_Click(object sender, EventArgs e)
        {
            PricingClass objPricingClass = new PricingClass();
            List<PricingClass> lstPricingClass = new List<PricingClass>();
            ViewState["getall"] = 0;
            lstPricingClass = objPricingClass.GetPricingClass(1, "-1");
            grdViewOutstanding.DataSource = lstPricingClass;
            grdViewOutstanding.DataBind();
            ViewState["lstOutstandingOrders"] = lstPricingClass;
            upnlOutstanding.Update();
        }
        protected void btn_previous_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["srl_good"] != null)
                {
                    List<PricingClass> lstPricingClass = ViewState["lstOutstandingOrders"] as List<PricingClass>;
                    //PricingClass Find = lstPricingClass.Find(Woak => Woak.srl == ViewState["srl_good"].ToString());
                    int cnt = 0;
                    foreach (PricingClass Woak in lstPricingClass)
                    {
                        if (Woak.srl.Equals(ViewState["srl_good"].ToString()))
                            break;
                        else
                            cnt++;
                    }
                    PricingClass Found = lstPricingClass[cnt - 1];
                    ViewState["srl_good"] = Found.srl;
                    ViewState["buy_price"] = Found.buy_price;
                    ViewState["sale_price"] = Found.sale_price;
                    ViewState["u_buy"] = Found.u_buy;
                    ViewState["u_sale"] = Found.u_sale;
                    ViewState["discount_amount"] = Found.discount_amount;
                    ViewState["final_sale"] = Found.final_sale;
                    load_panel();
                }
            }
            catch { }
        }
        protected void btn_next_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["srl_good"] != null)
                {
                    List<PricingClass> lstPricingClass = ViewState["lstOutstandingOrders"] as List<PricingClass>;
                    //PricingClass Find = lstPricingClass.Find(Woak => Woak.srl == ViewState["srl_good"].ToString());
                    int cnt = 0;
                    foreach (PricingClass Woak in lstPricingClass)
                    {
                        if (Woak.srl.Equals(ViewState["srl_good"].ToString()))
                            break;
                        else
                            cnt++;
                    }
                    PricingClass Found = lstPricingClass[cnt + 1];
                    ViewState["srl_good"] = Found.srl;
                    ViewState["buy_price"] = Found.buy_price;
                    ViewState["sale_price"] = Found.sale_price;
                    ViewState["u_buy"] = Found.u_buy;
                    ViewState["u_sale"] = Found.u_sale;
                    ViewState["discount_amount"] = Found.discount_amount;
                    ViewState["final_sale"] = Found.final_sale;
                    load_panel();
                }
            }
            catch { }
        }
        protected void btn_delete_assign_Click(object sender, EventArgs e)
        {
            if (ViewState["srl_good"] == null)
            {
                lblError.Text = "فرشی بارگذاری نشده است";
                return;
            }
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@header_srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(lst_project.SelectedValue);
            param[1] = new SqlParameter("@igd_srl", SqlDbType.Int);
            param[1].Value = Convert.ToInt32(ViewState["srl_good"]);
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
            load_panel();
        }
        protected void btn_with_price_Click(object sender, EventArgs e)
        {
            PricingClass objPricingClass = new PricingClass();
            List<PricingClass> lstPricingClass = new List<PricingClass>();
            ViewState["getall"] = 0;
            lstPricingClass = objPricingClass.GetPricingClass(4, lst_provider.SelectedValue);
            grdViewOutstanding.DataSource = lstPricingClass;
            grdViewOutstanding.DataBind();
            ViewState["lstOutstandingOrders"] = lstPricingClass;
            upnlOutstanding.Update();
        }
        protected void btn_without_price_Click(object sender, EventArgs e)
        {
            PricingClass objPricingClass = new PricingClass();
            List<PricingClass> lstPricingClass = new List<PricingClass>();
            ViewState["getall"] = 0;
            lstPricingClass = objPricingClass.GetPricingClass(3, lst_provider.SelectedValue);
            grdViewOutstanding.DataSource = lstPricingClass;
            grdViewOutstanding.DataBind();
            ViewState["lstOutstandingOrders"] = lstPricingClass;
            upnlOutstanding.Update();
        }
    }
}