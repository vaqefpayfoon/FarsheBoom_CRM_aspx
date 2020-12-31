using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationKartable
{
    public partial class CompleteWithPic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bring2();
            }
        }
        protected void bring2()
        {
            Common obo = new Common();
            List<GoodsClass> lstPricingClass = new List<GoodsClass>();
            lstPricingClass = Session["ThrowList"] as List<GoodsClass>;
            Session.Remove("ThrowList");
            if (lstPricingClass.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (GoodsClass Woak in lstPricingClass)
                {                    
                    sb.Append("<table><tr><td>");
                    sb.Append("<table class='table table-bordered'>");
                    sb.Append("<tr><td><span>کد فرش</span>");
                    sb.Append(string.Format("<p>{0}</p>", Woak.code_igd));
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td><span>تامین کننده</span>");
                    sb.Append(string.Format("<p>{0}</p>", Woak.provider_name));
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td><span>کد تامین کننده</span>");
                    sb.Append(string.Format("<p>{0}</p>", Woak.provider_code));
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td><span>اندازه</span>");
                    sb.Append(string.Format("<p>{0}</p>", Woak.size_title));
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td><span>گونه</span>");
                    sb.Append(string.Format("<p>{0}</p>", Woak.brand_name));
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td><span>طول</span>");
                    sb.Append(string.Format("<p>{0}</p>", Woak.lenght));
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td><span>عرض</span>");
                    sb.Append(string.Format("<p>{0}</p>", Woak.widht));
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td><span>مساحت</span>");
                    sb.Append(string.Format("<p>{0}</p>", Woak.area));
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td><span>قیمت تخته ای به ریال</span>");
                    sb.Append(string.Format("<p>{0}</p>", Woak.buy_price));
                    sb.Append("</td></tr>");
                    sb.Append("</table></td><td>");
                    sb.Append(string.Format("<img src='{0}' alt='فرشی یافت نشد'/>", Woak.title_igd));
                    sb.Append("</td></table>");
                    sb.Append("<br/><hr/><br/>");
                }
                literal.Text = sb.ToString();
            }
        }
    }
}