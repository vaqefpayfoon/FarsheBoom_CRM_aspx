using Cartable;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationKartable
{
    public partial class SalesWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private Common obb = new Common();
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private void getData(string code)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, title_igd, brand_name, size_title, provider_srl, code_igd, provider_name, color_name, porz_title, chele_title, plan_title, carpet_title, sale_price, widht, lenght, buy_price, discount,color_srl2, provider_code, rofo, kaji, badbaf, pakhordegi, tear FROM dbo.Provider_Goods where code_igd='" + code + "' ");
            if (dt.Rows.Count > 0)
            {
                DataRow Find = dt.Rows[0];
                txt_carpet_type.Text = Find["carpet_title"].ToString().Trim();
                txt_brand_name.Text = Find["brand_name"].ToString().Trim();
                txt_pcode.Text = Find["provider_code"].ToString().Trim();
                txt_color.Text = Find["color_name"].ToString().Trim();
                if (!Convert.IsDBNull(Find["color_srl2"]))
                    txt_margin.Text = color(Find["color_srl2"].ToString());
                txt_porz.Text = Find["porz_title"].ToString().Trim();
                txt_chele.Text = Find["chele_title"].ToString().Trim();
                txt_size.Text = Find["lenght"].ToString().Trim() + " * " + Find["widht"].ToString().Trim() + ":" + Find["size_title"].ToString().Trim();
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
                if (!Convert.IsDBNull(Find["kaji"]))
                    chk_kaji.Checked = Convert.ToBoolean(Find["kaji"]);
                else
                    chk_kaji.Checked = false;
                if (!Convert.IsDBNull(Find["badbaf"]))
                    chk_badbaf.Checked = Convert.ToBoolean(Find["badbaf"]);
                else
                    chk_badbaf.Checked = false;
                if (!Convert.IsDBNull(Find["pakhordegi"]))
                    chk_pakhordegi.Checked = Convert.ToBoolean(Find["pakhordegi"]);
                else
                    chk_pakhordegi.Checked = false;
                ViewState["title_igd"] = Find["title_igd"].ToString();
                long sale = 0;
                long buy = 0;
                long dis = 0;
                long final = 0;
                if (!Convert.IsDBNull(Find["sale_price"]))
                    sale = Convert.ToInt64(Find["sale_price"]);
                if (!Convert.IsDBNull(Find["buy_price"]))
                    buy = Convert.ToInt64(Find["buy_price"]);
                if (!Convert.IsDBNull(Find["discount"]))
                    dis = Convert.ToInt64(Find["discount"]);
                //txt_disc_per.Text = dis.ToString();
                
                if (dis > 0 && sale > 0)
                {
                    obb.str = (dis * sale / 100).ToString();
                    txt_discount.Text = obb.str;
                }
                obb.str = sale.ToString();
                txt_sale.Text = obb.str;
                //
                if (dis > 0 && sale > 0)
                {
                    dis = sale - (dis * sale / 100);
                }
                obb.str = (sale - dis).ToString();
                txt_final_payment.Text = obb.str;
                txt_plan.Text = Find["plan_title"].ToString().Trim();
            }
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
        protected void btn_list_Click(object sender, EventArgs e)
        {
            getData(txt_product.Text);
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["myCookie"] != null)
            {
                HttpCookie myCookie = new HttpCookie("myCookie");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            Server.Transfer("login.aspx");
        }
    }
}