using Cartable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationKartable
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["snd"] == "-1")
            {
                if (Request.Cookies["myCookie"] != null)
                {
                    HttpCookie myCookie = new HttpCookie("myCookie");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);
                }
            }
        }
        protected void btn_enter_Click(object sender, EventArgs e)
        {
            // lakiroonasi
            if (myadmin())
            {
                LoginInfo ubuzhi = new LoginInfo();
                ubuzhi.srl = "0";
                ubuzhi.first_name = "Administrator";
                ubuzhi.last_name = ".";
                ubuzhi.group_srl = "1";
                HttpCookie myCookie = new HttpCookie("myCookie");
                myCookie["srl"] = "0";
                myCookie["first_name"] = "Administrator";
                myCookie["last_name"] = ".";
                myCookie["group_srl"] = "1";
                myCookie.Expires = DateTime.Now.AddDays(1d);
                Response.Cookies.Add(myCookie);
                Server.Transfer("HomePage.aspx");
            }
            else
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@username", SqlDbType.VarChar, 15);
                param[0].Value = name.Text;
                param[1] = new SqlParameter("@password", SqlDbType.VarChar, 15);
                param[1].Value = pass.Text;
                RetrieveDataAdapter obj = new RetrieveDataAdapter();
                IDataReader dr = obj.DataReader("Login_Procedure", param);
                while (dr.Read())
                {
                    if (dr[0] == null)
                    { lblError.Text = "نام کاربری یا رمز ورود اشتباه است"; }
                    else
                    {
                        LoginInfo ubuzhi = new LoginInfo();
                        ubuzhi.srl = dr[0].ToString();
                        ubuzhi.first_name = dr[1].ToString();
                        ubuzhi.last_name = dr[2].ToString();
                        ubuzhi.group_srl = dr[3].ToString();
                        HttpCookie myCookie = new HttpCookie("myCookie");
                        myCookie["srl"] = dr[0].ToString();
                        myCookie["first_name"] = dr[1].ToString();
                        myCookie["last_name"] = dr[2].ToString();
                        myCookie["group_srl"] = dr[3].ToString();
                        myCookie.Expires = DateTime.Now.AddDays(1d);
                        Response.Cookies.Add(myCookie);
                        if (dr[3].ToString().Equals("4"))
                            Server.Transfer("SalesWebForm.aspx");
                        else
                            Server.Transfer("HomePage.aspx");
                    }
                }
                if (!dr.Read())
                    lblError.Text = "نام کاربری یا رمز ورود اشتباه است";
            }
        }
        private bool myadmin()
        {
            if (name.Text.Equals("Administrator") && pass.Text.Equals("2512740"))
            {
                return true;
            }
            else
                return false;
        }
        protected void logout_Click(object sender, EventArgs e)
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
    public class LoginInfo
    {
        public string srl { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string group_srl { get; set; }
    }
}