using System;
using System.Data;
using System.Text;

namespace Cartable
{
    public class Report
    {
        private string _str;
        private string str
        {
            get
            {
                return _str;
            }
            set
            {
                int b = (value.Length) % 3;
                for (int i = value.Length; i >= b; i -= 3)
                {
                    try
                    {
                        if (i <= 3)
                            break;
                        value = value.Insert(i - 3, ",");
                    }
                    catch { }
                }
                _str = value;
            }
        }
        public string personels_list(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='example' class='display' cellspacing='0' width='100%'>");
            sb.Append("<thead><tr><th>ردیف</th><th>نام و نام خانوادگی</th><th>شماره پرسنلی</th><th>موبایل</th><th>تلفن</th><th>آدرس</th></tr></thead><tfoot><tr><th>ردیف</th><th>نام و نام خانوادگی</th><th>شماره پرسنلی</th><th>موبایل</th><th>تلفن</th><th>آدرس</th></tr></tfoot>");
            sb.Append("<tbody>");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sb.Append("<tr><td>");
                sb.Append(i + 1);
                sb.Append("</td><td>");    
                sb.Append(dt.Rows[i]["full_name"]);
                sb.Append("</td><td>");                
                sb.Append(dt.Rows[i]["no_hpl"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["mobile_no"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["tel_no"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["address_hpl"]);
                sb.Append("</td></tr>");
            }
            sb.Append("</tbody></table>");
            return sb.ToString();
        }
        public string supcust_list(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='example' class='display' cellspacing='0' width='100%'>");
            sb.Append("<thead><tr><th>ردیف</th><th>نام طرف حساب</th><th>تاریخ</th><th>موبایل</th><th>سطح ارتباط</th><th>توضیحات</th></tr></thead><tfoot><tr><th>ردیف</th><th>نام طرف حساب</th><th>تاریخ</th><th>موبایل</th><th>سطح ارتباط</th><th>توضیحات</th></tr></tfoot>");
            sb.Append("<tbody>");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sb.Append("<tr><td>");
                sb.Append(i + 1);
                sb.Append("</td><td><a href='../Supcust/Supcust.aspx?srl=" + dt.Rows[i]["srl"] + "'>");
                sb.Append(dt.Rows[i]["full_name"] + "</a>");
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["u_date_time"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["cell_phone"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["meet_title"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["describtion"]);
                sb.Append("</td></tr>");
            }
            sb.Append("</tbody></table>");
            return sb.ToString();
        }
        public string provider_list(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='example' class='display' cellspacing='0' width='100%'>");
            sb.Append("<thead><tr><th>ردیف</th><th>سریال</th><th>نام تامین کننده</th><th>طرف تماس</th><th>تلفن</th><th>موبایل</th></tr></thead><tfoot><tr><th>ردیف</th><th>سریال</th><th>نام تامین کننده</th><th>طرف تماس</th><th>تلفن</th><th>موبایل</th></tr></tfoot>");
            sb.Append("<tbody>");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sb.Append("<tr><td>");
                sb.Append(i + 1);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["srl"]);
                sb.Append("</td><td><a href='../Provider_Goods/Provider.aspx?srl=" + dt.Rows[i]["srl"] + "'>");
                sb.Append(dt.Rows[i]["provider_name"] + "</a>");
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["related_person"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["tel1"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["cell_phone"]);
                sb.Append("</td></tr>");
            }
            sb.Append("</tbody></table>");
            return sb.ToString();
        }
        public string project_list(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='example' class='display' cellspacing='0' width='100%'>");
            sb.Append("<thead><tr><th>ردیف</th><th>کد محصول</th><th>کد فرش</th><th>اندازه</th><th>نام شهر</th><th>نوع</th><th>رنگ</th><th>قیمت فروش</th></tr></thead><tfoot><tr><th>ردیف</th><th>کد محصول</th><th>کد فرش</th><th>اندازه</th><th>نام شهر</th><th>نوع</th><th>رنگ</th><th>قیمت فروش</th></tr></tfoot>");
            sb.Append("<tbody>");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sb.Append("<tr><td>");
                sb.Append(i + 1);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["code_igd"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["code_igd"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["size_title"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["city_name"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["carpet_title"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["color_name"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["sale_price"]);
                sb.Append("</td></tr>");
            }
            sb.Append("</tbody></table>");
            return sb.ToString();
        }
        public string AllProviderGoods(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='example' class='display' cellspacing='0' width='100%'>");
            sb.Append("<thead><tr><th>ردیف</th><th>کد فرش</th><th>تامین کننده</th><th>گونه</th><th>اندازه</th><th>کد تامین کننده</th><th>پرز</th><th>چله</th><th>رنگ متن</th><th>نقشه فرش</th><th>وضعیت</th></tr></thead><tfoot><tr><th>ردیف</th><th>کد فرش</th><th>تامین کننده</th><th>گونه</th><th>اندازه</th><th>کد تامین کننده</th><th>پرز</th><th>چله</th><th>رنگ متن</th><th>نقشه فرش</th><th>وضعیت</th></tr></tfoot>");
            sb.Append("<tbody>");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sb.Append("<tr><td>");
                sb.Append(i + 1);
                sb.Append("</td><td><a href='../Provider_Goods/ProductAssign.aspx?srl=" + dt.Rows[i]["srl"] + "'>");
                sb.Append(dt.Rows[i]["code_igd"] + "</a>");
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["provider_name"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["brand_name"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["size_title"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["provider_code"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["porz_title"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["chele_title"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["color_name"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["plan_title"]);
                sb.Append("</td><td>");
                sb.Append(dt.Rows[i]["build_state"]);
                sb.Append("</td></tr>");
            }
            sb.Append("</tbody></table>");
            return sb.ToString();
        }
    }
}
