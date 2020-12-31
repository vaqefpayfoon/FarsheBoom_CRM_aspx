using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cartable;
using System.Data.SqlClient;
using System.IO;

namespace WebApplicationKartable
{
    public partial class Edit_Profile : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if (!IsPostBack)
            {
                set_boxes();
            }
        }
        private void set_boxes()
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, first_name, last_name, no_hpl, image_hpl, mobile_no, tel_no, address_hpl, user_name, pass_code, meli_code, mail_address FROM dbo.hpl_personal WHERE srl=" + ubuzhi.srl);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ViewState["srl"] = row["srl"].ToString();
                txt_first_name.Text = row["first_name"].ToString();
                txt_last_name.Text = row["last_name"].ToString();
                txt_hpl_no.Text = row["no_hpl"].ToString();
                if (row["image_hpl"] != DBNull.Value)
                {
                    image1.ImageUrl = row["image_hpl"].ToString();
                    ViewState["image_hpl"] = row["image_hpl"].ToString();
                }
                txt_mobile.Text = row["mobile_no"].ToString();
                txt_tel.Text = row["tel_no"].ToString();
                txt_address.Text = row["address_hpl"].ToString();
                txt_username.Text = row["user_name"].ToString();
                txt_password.Text = row["pass_code"].ToString();
                txt_meli_code.Text = row["meli_code"].ToString();
                txt_mail.Text = row["mail_address"].ToString();
            }

        }
        protected void btn_save_Click(object sender, ImageClickEventArgs e)
        {
            update();
        }
        private void update()
        {
            Common get = new Common();
            string path = Server.MapPath("/personel_image/");
            string save_image = "../personel_image/";
            if (upload.HasFile)
            {
                string srl = ViewState["srl"].ToString();
                string file_type = Path.GetExtension(upload.FileName);
                path = path + "\\" + srl + file_type;
                save_image = save_image + "\\" + srl + file_type;
                upload.SaveAs(path);
            }
            else
            {
                if (ViewState["image_hpl"] != null)
                    save_image = ViewState["image_hpl"].ToString();
                else
                    save_image = "..\\img\\person.png";
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update dbo.hpl_personal set first_name=@first_name,last_name=@last_name,no_hpl=@no_hpl,image_hpl=@image_hpl,mobile_no=@mobile_no,tel_no=@tel_no,address_hpl=@address_hpl,user_name=@user_name,pass_code=@pass_code,meli_code=@meli_code,mail_address=@mail_address where srl=@srl";
            if (!string.IsNullOrEmpty(txt_first_name.Text))
                cmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = txt_first_name.Text;
            else
                cmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = DBNull.Value;
            if (!string.IsNullOrEmpty(txt_last_name.Text))
                cmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value = txt_last_name.Text;
            else
                cmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@no_hpl", SqlDbType.VarChar).Value = txt_hpl_no.Text;
            cmd.Parameters.Add("@image_hpl", SqlDbType.VarChar).Value = save_image;
            if (!string.IsNullOrEmpty(txt_mobile.Text))
                cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar).Value = txt_mobile.Text;
            else
                cmd.Parameters.Add("@mobile_no", SqlDbType.VarChar).Value = DBNull.Value;
            if (!string.IsNullOrEmpty(txt_tel.Text))
                cmd.Parameters.Add("@tel_no", SqlDbType.VarChar).Value = txt_tel.Text;
            else
                cmd.Parameters.Add("@tel_no", SqlDbType.VarChar).Value = DBNull.Value;
            if (!string.IsNullOrEmpty(txt_address.Text))
                cmd.Parameters.Add("@address_hpl", SqlDbType.VarChar).Value = txt_address.Text;
            else
                cmd.Parameters.Add("@address_hpl", SqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = txt_username.Text;
            cmd.Parameters.Add("@pass_code", SqlDbType.VarChar).Value = txt_password.Text;
            if (!string.IsNullOrEmpty(txt_meli_code.Text))
                cmd.Parameters.Add("@meli_code", SqlDbType.VarChar).Value = txt_meli_code.Text;
            else
                cmd.Parameters.Add("@meli_code", SqlDbType.VarChar).Value = DBNull.Value;
            if (!string.IsNullOrEmpty(txt_mail.Text))
                cmd.Parameters.Add("@mail_address", SqlDbType.VarChar).Value = txt_mail.Text;
            else
                cmd.Parameters.Add("@mail_address", SqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@srl", SqlDbType.Int).Value = Convert.ToInt32(ViewState["srl"]);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblError.Text = "اطلاعات شما ویرایش شد";
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
    }
}
