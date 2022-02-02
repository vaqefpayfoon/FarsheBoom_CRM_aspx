using Cartable;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace WebApplicationKartable
{
    [Serializable]
    public class PricingClass
    {
        public string srl { get; set; }
        public string code_igd { get; set; }
        public string brand_name { get; set; }
        public string size_title { get; set; }
        public string carpet_title { get; set; }
        public string area { get; set; }
        public int discount { get; set; }
        public string buy_price { get; set; }
        public string sale_price { get; set; }
        public string u_buy { get; set; }
        public string u_sale { get; set; }
        public string discount_amount { get; set; }
        public string final_sale { get; set; }
        public string provider_name { get; set; }
        public string u_date_time { get; set; }
        public string provider_code { get; set; }
        public string margin_profit { get; set; }
        public string percent_profit { get; set; }
        public string price_home { get; set; }
        public string title_igd { get; set; }
        public List<PricingClass> GetPricingClass(int state , string provider)
        {
            List<PricingClass> lstPricingClass = new List<PricingClass>();
            DataTable dt = new DataTable(); Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            Common cur = new Common();
            if (state == 1)
            {
                dt = obj.Get_Data("SELECT srl,code_igd, brand_name, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale,discount_amount, final_sale,provider_name,u_date_time, provider_code, u_date_time,margin_profit,price_home, title_igd FROM dbo.Sale_Pricing Where buy_price is null or sale_price is null order by buy_price,sale_price");
            }
            else if (state == 2)
            {
                dt = obj.Get_Data("SELECT srl,code_igd, brand_name, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale,discount_amount, final_sale,provider_name,u_date_time, provider_code, u_date_time,margin_profit,price_home,title_igd FROM dbo.Sale_Pricing order by buy_price,sale_price");
            }
            else if (state == 3)
            {
                dt = obj.Get_Data(string.Format("SELECT srl,code_igd, brand_name, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale,discount_amount, final_sale,provider_name,u_date_time, provider_code, u_date_time,margin_profit,price_home,title_igd FROM dbo.Sale_Pricing Where (buy_price is null or sale_price is null) AND (provider_srl={0}) order by buy_price,sale_price", provider));
            }
            else if (state == 4)
            {
                dt = obj.Get_Data(string.Format("SELECT srl,code_igd, brand_name, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale,discount_amount, final_sale,provider_name,u_date_time, provider_code, u_date_time,margin_profit,price_home,title_igd FROM dbo.Sale_Pricing Where (provider_srl={0}) order by buy_price,sale_price", provider));
            }
            else if (state == 5)
            {
                dt = obj.Get_Data(string.Format("SELECT srl,code_igd, brand_name, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale,discount_amount, final_sale,provider_name,u_date_time, provider_code, u_date_time,margin_profit,price_home,title_igd FROM dbo.Sale_Pricing Where (ibt_srl={0}) order by buy_price,sale_price", provider));
            }
            else if (state == 6)
            {
                dt = obj.Get_Data(string.Format("SELECT srl,code_igd, brand_name, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale,discount_amount, final_sale,provider_name,u_date_time, provider_code, u_date_time,margin_profit,price_home,title_igd FROM dbo.Sale_Pricing Where (size_srl={0}) order by buy_price,sale_price", provider));
            }
            else if (state == 7)
            {
                dt = obj.Get_Data(string.Format(@"SELECT        dbo.inv_goods.srl, dbo.inv_goods.code_igd, dbo.inv_goods.title_igd, dbo.inv_goods.provider_srl, dbo.inv_goods.size_srl, dbo.inv_goods.sale_price, dbo.inv_brand.brand_name, dbo.inv_goods.ibt_srl, 
                         ROUND(dbo.inv_goods.lenght * dbo.inv_goods.widht / 10000, 2) AS area, dbo.inv_goods.sold, dbo.inv_goods.selection, dbo.inv_goods.buy_price, dbo.inv_size.size_title, dbo.inv_goods.discount, dbo.inv_carpet.carpet_title, 
                         ROUND(dbo.inv_goods.buy_price / NULLIF (ROUND(dbo.inv_goods.lenght * dbo.inv_goods.widht / 10000, 2), 0), 0) AS u_buy, 
                         ROUND(dbo.inv_goods.sale_price / NULLIF (ROUND(dbo.inv_goods.lenght * dbo.inv_goods.widht / 10000, 2), 0), 0) AS u_sale, ISNULL(dbo.inv_goods.discount, 0) * ISNULL(dbo.inv_goods.sale_price, 0) 
                         / 100 AS discount_amount, ABS(ISNULL(dbo.inv_goods.discount, 0) * ISNULL(dbo.inv_goods.sale_price, 0) / 100 - dbo.inv_goods.sale_price) AS final_sale, dbo.bas_provider.provider_name, dbo.inv_goods.provider_code, 
                         ABS(ISNULL(dbo.inv_goods.discount, 0) * ISNULL(dbo.inv_goods.sale_price, 0) / 100 - dbo.inv_goods.sale_price) - dbo.inv_goods.buy_price AS margin_profit, dbo.inv_goods.u_date_time, dbo.inv_goods.build_state, 
                         dbo.inv_goods.price_home, dbo.bas_project_goods.header_srl
FROM            dbo.inv_goods INNER JOIN
                         dbo.bas_provider ON dbo.inv_goods.provider_srl = dbo.bas_provider.srl LEFT OUTER JOIN
                         dbo.bas_project_goods ON dbo.inv_goods.srl = dbo.bas_project_goods.igd_srl LEFT OUTER JOIN
                         dbo.inv_brand ON dbo.inv_goods.ibt_srl = dbo.inv_brand.srl LEFT OUTER JOIN
                         dbo.inv_carpet ON dbo.inv_goods.carpet_type = dbo.inv_carpet.srl LEFT OUTER JOIN
                         dbo.inv_size ON dbo.inv_goods.size_srl = dbo.inv_size.srl
WHERE        (dbo.inv_goods.sold = 'False' OR
                         dbo.inv_goods.sold IS NULL) AND (dbo.inv_goods.build_state = '0') And (dbo.bas_project_goods.header_srl = {0})", provider));
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow Woak in dt.Rows)
                {
                    PricingClass pc = new PricingClass();
                    pc.srl = Woak["srl"].ToString();
                    pc.provider_name = Woak["provider_name"].ToString();
                    pc.code_igd = Woak["code_igd"].ToString();
                    pc.brand_name = Woak["brand_name"].ToString();
                    pc.area = Woak["area"].ToString();
                    pc.size_title = Woak["size_title"].ToString();
                    pc.carpet_title = Woak["carpet_title"].ToString();
                    cur.str = Woak["buy_price"].ToString();
                    pc.buy_price = cur.str;
                    cur.str = Woak["u_buy"].ToString();
                    pc.u_buy = cur.str;
                    cur.str = Woak["u_sale"].ToString();
                    pc.u_sale = cur.str;
                    cur.str = Woak["price_home"].ToString();
                    pc.price_home = cur.str;
                    cur.str = Woak["discount_amount"].ToString();
                    pc.discount_amount = cur.str;
                    pc.discount = Convert.IsDBNull(Woak["discount"]) ? 0 : Convert.ToInt32(Woak["discount"]);
                    cur.str = Woak["sale_price"].ToString();
                    pc.sale_price = cur.str;
                    cur.str = Woak["final_sale"].ToString();
                    pc.final_sale = cur.str;
                    pc.u_date_time = Woak["u_date_time"].ToString();
                    pc.provider_code = Woak["provider_code"].ToString();
                    pc.title_igd = Woak["title_igd"].ToString();
                    try
                    {
                        if (Woak["margin_profit"] != null)
                            pc.margin_profit = Convert.ToInt64(Woak["margin_profit"]).ToString();
                        else
                            pc.margin_profit = "0";
                    }
                    catch { pc.margin_profit = "0"; }
                    if (!Convert.IsDBNull(Woak["buy_price"]) && !Convert.IsDBNull(Woak["final_sale"]))
                    {
                        cur.str = Convert.ToInt64(Math.Round((((Convert.ToDouble(Woak["final_sale"]) - Convert.ToDouble(Woak["buy_price"])) / Convert.ToDouble(Woak["final_sale"])) * 100), 0)).ToString();
                        pc.percent_profit = cur.str;
                    }
                    lstPricingClass.Add(pc);
                }
            }
            return lstPricingClass;
        }
        public List<PricingClass> CallPerformed(string lst_project, string lst_provider)
        {
            List<PricingClass> lstPricingClass = new List<PricingClass>();
            DataTable dt = new DataTable(); Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            Common cur = new Common();
            dt = obj.Get_Data(string.Format("SELECT srl,code_igd, brand_name, area, size_title, carpet_title, buy_price, discount, sale_price, u_buy, u_sale,discount_amount, final_sale,provider_name,u_date_time, provider_code,margin_profit,title_igd FROM dbo.Sale_Pricing Where (provider_srl = {1}) AND (build_state = 0) AND (code_igd NOT IN (SELECT inv_goods_1.code_igd FROM dbo.bas_project LEFT OUTER JOIN dbo.bas_project_goods ON dbo.bas_project.srl = dbo.bas_project_goods.header_srl LEFT OUTER JOIN dbo.inv_goods AS inv_goods_1 ON dbo.bas_project_goods.igd_srl = inv_goods_1.srl WHERE (dbo.bas_project.srl = {0}) AND(inv_goods_1.provider_srl = {1}))) order by code_igd", lst_project, lst_provider));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow Woak in dt.Rows)
                {
                    PricingClass pc = new PricingClass();
                    pc.srl = Woak["srl"].ToString();
                    pc.provider_name = Woak["provider_name"].ToString();
                    pc.code_igd = Woak["code_igd"].ToString();
                    pc.brand_name = Woak["brand_name"].ToString();
                    pc.area = Woak["area"].ToString();
                    pc.size_title = Woak["size_title"].ToString();
                    pc.carpet_title = Woak["carpet_title"].ToString();
                    cur.str = Woak["buy_price"].ToString();
                    pc.buy_price = cur.str;
                    cur.str = Woak["u_buy"].ToString();
                    pc.u_buy = cur.str;
                    cur.str = Woak["u_sale"].ToString();
                    pc.u_sale = cur.str;
                    cur.str = Woak["discount_amount"].ToString();
                    pc.discount_amount = cur.str;
                    pc.discount = Convert.IsDBNull(Woak["discount"]) ? 0 : Convert.ToInt32(Woak["discount"]);
                    cur.str = Woak["sale_price"].ToString();
                    pc.sale_price = cur.str;
                    cur.str = Woak["final_sale"].ToString();
                    pc.final_sale = cur.str;
                    pc.u_date_time = Woak["u_date_time"].ToString();
                    pc.provider_code = Woak["provider_code"].ToString();
                    pc.title_igd = Woak["title_igd"].ToString();
                    try
                    {
                        if (Woak["margin_profit"] != null)
                            pc.margin_profit = Convert.ToInt64(Woak["margin_profit"]).ToString();
                        else
                            pc.margin_profit = "0";
                    }
                    catch { pc.margin_profit = "0"; }
                    if (!Convert.IsDBNull(Woak["buy_price"]) && !Convert.IsDBNull(Woak["final_sale"]))
                    {
                        cur.str = Convert.ToInt64(Math.Round((((Convert.ToDouble(Woak["final_sale"]) - Convert.ToDouble(Woak["buy_price"])) / Convert.ToDouble(Woak["final_sale"])) * 100), 0)).ToString();
                        pc.percent_profit = cur.str;
                    }
                    lstPricingClass.Add(pc);
                }
            }
            return lstPricingClass;
        }
    }
    [Serializable]
    public class GoodsClass
    {
        public string srl { get; set; }
        public string code_igd { get; set; }
        public string brand_name { get; set; }
        public string size_title { get; set; }
        public string u_buy { get; set; }
        public string color_name { get; set; }
        public string porz_title { get; set; }
        public string chele_title { get; set; }
        public string plan_title { get; set; }
        public string widht { get; set; }
        public string lenght { get; set; }
        public string provider_code { get; set; }
        public string provider_name { get; set; }
        public string selection { get; set; }
        public string sold { get; set; }
        public string buy_price { get; set; }
        public string area { get; set; }
        public string margin_color { get; set; }
        public string title_igd { get; set; }
        public string has_pic { get; set; }

        public List<GoodsClass> GetGoodsClass()
        {
            List<GoodsClass> lstGoodsClass = new List<GoodsClass>();
            DataTable dt = new DataTable(); Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            Common cur = new Common();
            dt = obj.Get_Data("SELECT srl, code_igd, brand_name, size_title, u_buy, color_name, porz_title, chele_title, plan_title, widht, lenght, provider_code, provider_name, selection, sold, buy_price, area, color_srl2,title_igd FROM dbo.Provider_Goods order by code_igd");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow Woak in dt.Rows)
                {
                    GoodsClass pc = new GoodsClass();
                    pc.srl = Woak["srl"].ToString();
                    pc.code_igd = Woak["code_igd"].ToString();
                    pc.brand_name = Woak["brand_name"].ToString();
                    pc.size_title = Woak["size_title"].ToString();
                    pc.u_buy = Woak["u_buy"].ToString();
                    pc.color_name = Woak["color_name"].ToString();
                    pc.porz_title = Woak["porz_title"].ToString();
                    pc.chele_title = Woak["chele_title"].ToString();
                    pc.plan_title = Woak["plan_title"].ToString();
                    pc.widht = Woak["widht"].ToString();
                    pc.lenght = Woak["lenght"].ToString();
                    pc.provider_code = Woak["provider_code"].ToString();
                    pc.provider_name = Woak["provider_name"].ToString();
                    pc.selection = Woak["selection"].ToString();
                    pc.sold = Woak["sold"].ToString();
                    pc.buy_price = Woak["buy_price"].ToString();
                    pc.margin_color = color(Woak["color_srl2"].ToString());
                    pc.area = Woak["area"].ToString();
                    pc.title_igd = Woak["title_igd"].ToString();
                    if (Woak["title_igd"].ToString().Length > 1)
                        pc.has_pic = "دارد";
                    else
                        pc.has_pic = "ندارد";
                    lstGoodsClass.Add(pc);
                }
            }
            return lstGoodsClass;
        }
        public List<GoodsClass> GetGoodAssign(string srl)
        {
            List<GoodsClass> lstGoodsClass = new List<GoodsClass>();
            DataTable dt = new DataTable(); Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            Common cur = new Common();
            dt = obj.Get_Data(string.Format("SELECT srl, code_igd, brand_name, size_title, u_buy, color_name, porz_title, chele_title, plan_title, widht, lenght, provider_code, provider_name, selection, sold, buy_price, area, color_srl2,title_igd FROM dbo.Provider_Goods Where provider_srl = {0} order by code_igd", srl));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow Woak in dt.Rows)
                {
                    GoodsClass pc = new GoodsClass();
                    pc.srl = Woak["srl"].ToString();
                    pc.code_igd = Woak["code_igd"].ToString();
                    pc.brand_name = Woak["brand_name"].ToString();
                    pc.size_title = Woak["size_title"].ToString();
                    pc.u_buy = Woak["u_buy"].ToString();
                    pc.color_name = Woak["color_name"].ToString();
                    pc.porz_title = Woak["porz_title"].ToString();
                    pc.chele_title = Woak["chele_title"].ToString();
                    pc.plan_title = Woak["plan_title"].ToString();
                    pc.widht = Woak["widht"].ToString();
                    pc.lenght = Woak["lenght"].ToString();
                    pc.provider_code = Woak["provider_code"].ToString();
                    pc.provider_name = Woak["provider_name"].ToString();
                    pc.selection = Woak["selection"].ToString();
                    pc.sold = Woak["sold"].ToString();
                    pc.buy_price = Woak["buy_price"].ToString();
                    pc.margin_color = color(Woak["color_srl2"].ToString());
                    pc.area = Woak["area"].ToString();
                    pc.title_igd = Woak["title_igd"].ToString();
                    if (Woak["title_igd"].ToString().Length > 1)
                        pc.has_pic = "دارد";
                    else
                        pc.has_pic = "ندارد";
                    lstGoodsClass.Add(pc);
                }
            }
            return lstGoodsClass;
        }
        private string color(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            DataTable dt = new DataTable(); Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            dt = obj.Get_Data(string.Format("SELECT color_name FROM inv_color where srl={0}", str));
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return string.Empty;
        }
        public List<GoodsClass> GetNullGoodsClass(string provider_srl)
        {
            List<GoodsClass> lstGoodsClass = new List<GoodsClass>();
            DataTable dt = new DataTable(); Search obj = new Search(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            Common cur = new Common();
            dt = obj.Get_Data(string.Format("SELECT srl, code_igd, brand_name, size_title, carpet_title, color_name, porz_title, chele_title, plan_title, widht, lenght, provider_code, provider_name, selection, sold, buy_price, area, color_srl2,title_igd FROM dbo.Provider_Goods WHERE (provider_srl={0}) and (ibt_srl is null) OR (size_srl is null) OR (city_srl is null) OR (color_srl is null) OR (color_srl2 is null) OR (widht is null) OR (lenght is null) OR (buy_price is null) order by code_igd", provider_srl));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow Woak in dt.Rows)
                {
                    GoodsClass pc = new GoodsClass();
                    pc.srl = Woak["srl"].ToString();
                    pc.code_igd = Woak["code_igd"].ToString();
                    pc.brand_name = Woak["brand_name"].ToString();
                    pc.size_title = Woak["size_title"].ToString();
                    pc.color_name = Woak["color_name"].ToString();
                    pc.porz_title = Woak["porz_title"].ToString();
                    pc.chele_title = Woak["chele_title"].ToString();
                    pc.plan_title = Woak["plan_title"].ToString();
                    pc.widht = Woak["widht"].ToString();
                    pc.lenght = Woak["lenght"].ToString();
                    pc.provider_code = Woak["provider_code"].ToString();
                    pc.provider_name = Woak["provider_name"].ToString();
                    pc.selection = Woak["selection"].ToString();
                    pc.sold = Woak["sold"].ToString();
                    pc.buy_price = Woak["buy_price"].ToString();
                    pc.margin_color = color(Woak["color_srl2"].ToString());
                    pc.area = Woak["area"].ToString();
                    pc.title_igd = Woak["title_igd"].ToString();
                    if (Woak["title_igd"].ToString().Length > 1)
                        pc.has_pic = "دارد";
                    else
                        pc.has_pic = "ندارد";
                    lstGoodsClass.Add(pc);
                }
            }
            return lstGoodsClass;
        }
    }
}