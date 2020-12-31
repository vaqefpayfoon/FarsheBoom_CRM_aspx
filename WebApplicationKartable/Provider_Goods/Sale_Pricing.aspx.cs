using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using Cartable;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebApplicationKartable
{
    public partial class Sale_Pricing : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        private Common obobo = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if(!IsPostBack)
            {
                image1.ImageUrl = "..\\img\\person.png";
                fill_grid();
            }
        }
        private void fill_grid()
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, code_igd, brand_name, provider_srl, size_srl, ibt_srl, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy,  u_sale,discount_amount, final_sale FROM dbo.Sale_Pricing order by buy_price,sale_price");
            grid.DataSource = dt; grid.DataBind(); calc(dt);
        }
        private void set_grid(DataTable dt)
        {
            Search obj = new Search(strConnString);
            DataTable dt2 = new DataTable();
            dt2 = obj.Get_Data("SELECT srl, igd_srl, u_date_time, price, active FROM  dbo.inv_purchase_price");
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataRow Woak in dt2.Rows)
                {
                    if (Convert.ToInt32(row["srl"]) == Convert.ToInt32(Woak["igd_srl"]))
                    {
                        if (!Convert.IsDBNull(Woak["active"]))
                        {
                            if (Convert.ToBoolean(Woak["active"]))
                            {
                                row["purchase_price"] = Woak["price"];
                                row["u_buy"] = Math.Round(Convert.ToDouble(Woak["price"]) / Convert.ToInt32((row["area"])),0);                                
                                row["u_date_time"] = Woak["u_date_time"];
                                if(!Convert.IsDBNull(row["sale_price"]) || !string.IsNullOrEmpty(row["sale_price"].ToString()))
                                {
                                    row["u_sale"] = Math.Round(Convert.ToDouble(row["sale_price"]) / Convert.ToInt32((row["area"])),0);
                                }
                                break;
                            }
                        }
                    }
                }
            }
            grid.DataSource = dt; grid.DataBind();
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
            return obj.FilterSearch("SELECT code_igd FROM dbo.inv_goods where code_igd like '%'+ @SearchText + '%'", prefixText, count);
        }
        protected void btn_filter_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            if (!string.IsNullOrEmpty(txtContactsSearch.Text))
            {
                int provider = provider_srl(txtContactsSearch.Text);
                dt = obj.Get_Data("SELECT srl, code_igd, brand_name, provider_srl, size_srl, ibt_srl, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale, discount_amount, final_sale FROM dbo.Sale_Pricing where  provider_srl=" + provider + " order by buy_price,sale_price");
                grid.DataSource = dt; grid.DataBind(); calc(dt);
            }
            else if (!string.IsNullOrEmpty(txt_product.Text))
            {
                int good = goods_srl(txt_product.Text);
                dt = obj.Get_Data("SELECT srl, code_igd, brand_name, provider_srl, size_srl, ibt_srl, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale,  discount_amount, final_sale FROM dbo.Sale_Pricing where srl=" + good + " order by buy_price,sale_price");
                grid.DataSource = dt; grid.DataBind(); calc(dt);
            }
            else if (lst_city2.SelectedIndex > 0)
            {
                dt = obj.Get_Data("SELECT srl, code_igd, brand_name, provider_srl, size_srl, ibt_srl, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale,  discount_amount, final_sale FROM dbo.Sale_Pricing where ibt_srl=" + lst_city2.SelectedValue + " order by buy_price,sale_price");
                grid.DataSource = dt; grid.DataBind(); calc(dt);
            }
            else if (lst_size.SelectedIndex > 0)
            {
                dt = obj.Get_Data("SELECT srl, code_igd, brand_name, provider_srl, size_srl, ibt_srl, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale,  discount_amount, final_sale FROM dbo.Sale_Pricing where size_srl=" + lst_size.SelectedValue + " order by buy_price,sale_price");
                grid.DataSource = dt; grid.DataBind(); calc(dt);
            }
            else
            {
                dt = obj.Get_Data("SELECT srl, code_igd, brand_name, provider_srl, size_srl, ibt_srl, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale, discount_amount, final_sale FROM dbo.Sale_Pricing order by buy_price,sale_price");
                grid.DataSource = dt; grid.DataBind(); calc(dt);
            }
        }
        private int provider_srl(string name)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.bas_provider where provider_name='" + name + "' order by srl desc");
            int srl = 0;
            if (dt.Rows.Count > 0)
                srl = Convert.ToInt32(dt.Rows[0][0]);
            return srl;
        }
        private int goods_srl(string name)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl FROM dbo.inv_goods where code_igd='" + name + "' order by srl desc");
            int srl = 0;
            if (dt.Rows.Count > 0)
                srl = Convert.ToInt32(dt.Rows[0][0]);
            return srl;
        }
        protected void grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["code_igd"] = grid.SelectedRow.Cells[1].Text.Trim();
            Search obj = new Search(strConnString);
            DataTable dt = new DataTable();
                dt = obj.Get_Data("SELECT title_igd FROM dbo.inv_goods Where code_igd = " + grid.SelectedRow.Cells[1].Text.Trim());
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
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
            }
            txt_sell.Text = string.Empty;
            txt_buy.Text = string.Empty;
            long buy = 0, sell = 0, dis = 0;
            try
            {
                buy = Convert.ToInt64(obobo.remove_cama(grid.SelectedRow.Cells[7].Text.Trim().Substring(1, grid.SelectedRow.Cells[7].Text.Length - 1)));
            }catch { }
            try
            {
                sell = Convert.ToInt64(obobo.remove_cama(grid.SelectedRow.Cells[8].Text.Trim().Substring(1, grid.SelectedRow.Cells[8].Text.Length - 1)));
            }
            catch { }
            try
            {
                dis = Convert.ToInt64(obobo.remove_cama(grid.SelectedRow.Cells[6].Text.Trim().Substring(1, grid.SelectedRow.Cells[6].Text.Length - 1)));
            }
            catch { }
            obobo.str = buy.ToString();
            txt_buy.Text = obobo.str;
            obobo.str = sell.ToString();
            txt_sell.Text = obobo.str;
            if(sell != 0 || dis != 0)
                txt_discount.Text = ((dis * 100) / sell).ToString();
            obobo.str = (sell - buy - dis).ToString();
            lbl_profit.Text = obobo.str + " حاشیه سود ";
            txt_product.Text = grid.SelectedRow.Cells[1].Text.Trim();
            DataTable fill = obj.Get_Data("SELECT provider_name,ibt_srl,size_srl FROM dbo.Provider_Goods where code_igd=" + grid.SelectedRow.Cells[1].Text.Trim());
            if(fill.Rows.Count > 0)
            {
                txtContactsSearch.Text = fill.Rows[0][0].ToString();
                if (!Convert.IsDBNull(fill.Rows[0][1]))
                    lst_city2.SelectedValue = fill.Rows[0][1].ToString();
                if (!Convert.IsDBNull(fill.Rows[0][2]))
                    lst_size.SelectedValue = fill.Rows[0][2].ToString();
            }

        }
        protected void ImageButton2_save_Click(object sender, EventArgs e)
        {
            if (ViewState["code_igd"] == null)
            {
                lblError.Text = "لطفا یک فرش را انتخاب نمائید";
                return;
            }
            Common obj = new Common();
            double dbl_sale_price = Convert.ToDouble(obj.remove_cama(txt_sell.Text));
            double dbl_buy_price = Convert.ToDouble(obj.remove_cama(txt_buy.Text));
            double dbl_discount = Convert.ToDouble(obj.remove_cama(txt_discount.Text));
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update inv_goods set sale_price=@sale_price,buy_price=@buy_price, discount=@discount where code_igd=@code_igd";
            cmd.Parameters.Add("@code_igd", SqlDbType.Int).Value = Convert.ToInt32(ViewState["code_igd"]);
            cmd.Parameters.Add("@sale_price", SqlDbType.BigInt).Value = dbl_sale_price;
            cmd.Parameters.Add("@buy_price", SqlDbType.BigInt).Value = dbl_buy_price;
            cmd.Parameters.Add("@discount", SqlDbType.BigInt).Value = dbl_discount;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lbl_profit.Text = string.Empty;
            fill_grid();
        }
        private void calc(DataTable dt)
        {
            Common objcom = new Common();
            Int64 dbl_purchase = 0, dbl_sale = 0;
            object purchase;
            purchase = dt.Compute("SUM(buy_price)", "");
            if (!Convert.IsDBNull(purchase))
            {
                objcom.str = purchase.ToString();
                dbl_purchase = Convert.ToInt64(objcom.remove_cama(objcom.str));
            }
            purchase = dt.Compute("SUM(final_sale)", "");
            if (!Convert.IsDBNull(purchase))
            {
                objcom.str = purchase.ToString();
                dbl_sale = Convert.ToInt64(objcom.remove_cama(objcom.str));
            }
            objcom.str = (dbl_sale - dbl_purchase).ToString();
            lbl_profit.Text = objcom.str;
        }
    }
}