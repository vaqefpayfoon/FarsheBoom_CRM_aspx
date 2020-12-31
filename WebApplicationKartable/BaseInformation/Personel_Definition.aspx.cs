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
    public partial class Personel_Definition : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
            if(!IsPostBack)
            {
                if(Request.QueryString["srl"] != "-1")
                    set_boxes(Request.QueryString["srl"].ToString());
            }
        }
        private void set_boxes(string srl)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, group_srl, first_name, last_name, no_hpl, mobile_no, tel_no, address_hpl, user_name, pass_code, meli_code, mail_address, active, image_hpl FROM dbo.hpl_personal WHERE srl=" + srl);
            if(dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ViewState["srl"] = row["srl"].ToString();
                lst_group.SelectedValue = row["group_srl"].ToString();
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
                if (Convert.IsDBNull(row["active"]))
                    chk_active.Checked = false;
                else
                    chk_active.Checked = Convert.ToBoolean(row["active"]);
            }

        }
        protected void btn_save_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.QueryString["srl"] == "-1")
                insert();
            else
                update();
        }
        private void insert()
        {
            int srl = max_srl();
            if (lblError.Text == "پرسنل جدید ایجاد شد")
                return;
            Common get = new Common();
            string path = Server.MapPath("/personel_image");
            string save_image = "../personel_image";
            if (upload.HasFile)
            {
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
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = srl;
            param[1] = new SqlParameter("@group_srl", SqlDbType.TinyInt);
            if (lst_group.Items.Count > 0)
                param[1].Value = Convert.ToInt32(lst_group.SelectedValue);
            else
                param[1].Value = DBNull.Value;
            param[2] = new SqlParameter("@first_name", SqlDbType.VarChar, 40);
            if (!string.IsNullOrEmpty(txt_first_name.Text))
                param[2].Value = txt_first_name.Text;
            else
                param[2].Value = DBNull.Value;
            param[3] = new SqlParameter("@last_name", SqlDbType.VarChar, 40);
            if (!string.IsNullOrEmpty(txt_last_name.Text))
                param[3].Value = txt_last_name.Text;
            else
                param[3].Value = DBNull.Value;
            param[4] = new SqlParameter("@no_hpl", SqlDbType.VarChar, 10);
            if (!string.IsNullOrEmpty(txt_hpl_no.Text))
                param[4].Value = txt_hpl_no.Text;
            else
                param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@mobile_no", SqlDbType.VarChar, 11);
            if (!string.IsNullOrEmpty(txt_mobile.Text))
                param[5].Value = txt_mobile.Text;
            else
                param[5].Value = DBNull.Value;
            param[6] = new SqlParameter("@tel_no", SqlDbType.VarChar, 12);
            if (!string.IsNullOrEmpty(txt_tel.Text))
                param[6].Value = txt_tel.Text;
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@address_hpl", SqlDbType.VarChar, 70);
            if (!string.IsNullOrEmpty(txt_address.Text))
                param[7].Value = txt_address.Text;
            else
                param[7].Value = DBNull.Value;
            param[8] = new SqlParameter("@user_name", SqlDbType.VarChar, 15);
            param[8].Value = txt_username.Text;
            param[9] = new SqlParameter("@pass_code", SqlDbType.VarChar, 15);
            param[9].Value = txt_password.Text;
            param[10] = new SqlParameter("@meli_code", SqlDbType.VarChar, 10);
            if (!string.IsNullOrEmpty(txt_meli_code.Text))
                param[10].Value = txt_meli_code.Text;
            else
                param[10].Value = DBNull.Value;
            param[11] = new SqlParameter("@mail_address", SqlDbType.VarChar, 50);
            if (!string.IsNullOrEmpty(txt_mail.Text))
                param[11].Value = txt_mail.Text;
            else
                param[11].Value = DBNull.Value;
            param[12] = new SqlParameter("@active", SqlDbType.Bit);
            param[12].Value = chk_active.Checked;
            param[13] = new SqlParameter("@image_hpl", SqlDbType.VarChar);
            param[13].Value = save_image;
            new ManageCommands(param, "insert_personal");
            lblError.Text = "پرسنل جدید ایجاد شد";
        }
        private void update()
        {
            Common get = new Common();
            string path = Server.MapPath("/personel_image");
            string save_image = "../personel_image";
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
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@srl", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(ViewState["srl"]);
            param[1] = new SqlParameter("@group_srl", SqlDbType.TinyInt);
            if (lst_group.Items.Count > 0)
                param[1].Value = Convert.ToInt32(lst_group.SelectedValue);
            else
                param[1].Value = DBNull.Value;
            param[2] = new SqlParameter("@first_name", SqlDbType.VarChar, 40);
            if (!string.IsNullOrEmpty(txt_first_name.Text))
                param[2].Value = txt_first_name.Text;
            else
                param[2].Value = DBNull.Value;
            param[3] = new SqlParameter("@last_name", SqlDbType.VarChar, 40);
            if (!string.IsNullOrEmpty(txt_last_name.Text))
                param[3].Value = txt_last_name.Text;
            else
                param[3].Value = DBNull.Value;
            param[4] = new SqlParameter("@no_hpl", SqlDbType.VarChar, 10);
            if (!string.IsNullOrEmpty(txt_hpl_no.Text))
                param[4].Value = txt_hpl_no.Text;
            else
                param[4].Value = DBNull.Value;
            param[5] = new SqlParameter("@mobile_no", SqlDbType.VarChar, 11);
            if (!string.IsNullOrEmpty(txt_mobile.Text))
                param[5].Value = txt_mobile.Text;
            else
                param[5].Value = DBNull.Value;
            param[6] = new SqlParameter("@tel_no", SqlDbType.VarChar, 12);
            if (!string.IsNullOrEmpty(txt_tel.Text))
                param[6].Value = txt_tel.Text;
            else
                param[6].Value = DBNull.Value;
            param[7] = new SqlParameter("@address_hpl", SqlDbType.VarChar, 70);
            if (!string.IsNullOrEmpty(txt_address.Text))
                param[7].Value = txt_address.Text;
            else
                param[7].Value = DBNull.Value;
            param[8] = new SqlParameter("@user_name", SqlDbType.VarChar, 15);
            param[8].Value = txt_username.Text;
            param[9] = new SqlParameter("@pass_code", SqlDbType.VarChar, 15);
            param[9].Value = txt_password.Text;
            param[10] = new SqlParameter("@meli_code", SqlDbType.VarChar, 10);
            if (!string.IsNullOrEmpty(txt_meli_code.Text))
                param[10].Value = txt_meli_code.Text;
            else
                param[10].Value = DBNull.Value;
            param[11] = new SqlParameter("@mail_address", SqlDbType.VarChar, 50);
            if (!string.IsNullOrEmpty(txt_mail.Text))
                param[11].Value = txt_mail.Text;
            else
                param[11].Value = DBNull.Value;
            param[12] = new SqlParameter("@active", SqlDbType.Bit);
            param[12].Value = chk_active.Checked;
            param[13] = new SqlParameter("@image_hpl", SqlDbType.VarChar);
            param[13].Value = save_image;
            new ManageCommands(param, "update_personal");
            lblError.Text = "پرسنل جاری ویرایش شد";
        }
        private int max_srl()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = strConnString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ISNULL((MAX(srl) + 1),1) FROM dbo.hpl_personal";
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
                if (Request.Cookies["myCookie"]["group_srl"].ToString() != "3") Response.Redirect("~/login.aspx");
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