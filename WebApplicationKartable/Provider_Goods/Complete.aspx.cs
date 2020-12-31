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
using System.Web;
using System.Text;

namespace WebApplicationKartable
{
    public partial class Complete : System.Web.UI.Page
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
            CheckLogin();
            if (!IsPostBack)
            {

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
        private void SortGridViewOutstanding(string sortExpression, string direction)
        {
            if (ViewState["lstOutstandingOrders"] != null)
            {

                List<GoodsClass> lstPricingClass = (List<GoodsClass>)ViewState["lstOutstandingOrders"];

                var param = Expression.Parameter(typeof(GoodsClass), sortExpression);
                var sortby = Expression.Lambda<Func<GoodsClass, object>>(Expression.Convert(Expression.Property(param, sortExpression), typeof(object)), param);
                if (direction == "ASC")
                {
                    lstPricingClass = lstPricingClass.AsQueryable<GoodsClass>().OrderBy(sortby).ToList();
                }
                else
                {
                    lstPricingClass = lstPricingClass.AsQueryable<GoodsClass>().OrderByDescending(sortby).ToList();
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
                                img.ImageUrl = "./Images/up.png";
                            }
                            if (GridViewSortDirection == SortDirection.Descending)
                            {
                                img.ImageUrl = "./Images/down.png";
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
                List<GoodsClass> lstOutstandingOrders = (List<GoodsClass>)ViewState["lstOutstandingOrders"];
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
                    List<GoodsClass> allGoodsClass = (List<GoodsClass>)ViewState["lstOutstandingOrders"];
                    TextBox txtBox = (TextBox)sender;
                    if (txtBox.ID == "txtprovider_name")
                    {
                        allGoodsClass = allGoodsClass.Where(x => x.provider_name.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Oprovider_name"] = txtBox.Text.Trim().ToUpper();
                    }
                    if (txtBox.ID == "txtcode_igd")
                    {
                        allGoodsClass = allGoodsClass.Where(x => x.code_igd.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ocode_igd"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtbrand_name")
                    {
                        allGoodsClass = allGoodsClass.Where(x => x.brand_name.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Obrand_name"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtsize_title")
                    {
                        allGoodsClass = allGoodsClass.Where(x => x.size_title.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Osize_title"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtcarpet_title")
                    {
                        allGoodsClass = allGoodsClass.Where(x => x.carpet_title.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ocarpet_title"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtcolor_name")
                    {
                        allGoodsClass = allGoodsClass.Where(x => x.color_name.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ocolor_name"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtporz_title")
                    {
                        allGoodsClass = allGoodsClass.Where(x => x.porz_title.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Oporz_title"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtchele_title")
                    {
                        allGoodsClass = allGoodsClass.Where(x => x.chele_title.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ochele_title"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtplan_title")
                    {
                        allGoodsClass = allGoodsClass.Where(x => x.plan_title.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Oplan_title"] = txtBox.Text.Trim().ToUpper();
                    }
                    ViewState["lstOutstandingOrders"] = allGoodsClass;
                    grdViewOutstanding.DataSource = allGoodsClass;
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
            if (ViewState["Ocolor_name"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtcolor_name")).Text = ViewState["Ocolor_name"].ToString().ToUpper();
            if (ViewState["Ocarpet_title"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtcarpet_title")).Text = ViewState["Ocarpet_title"].ToString().ToUpper();
            if (ViewState["Oprovider_name"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtprovider_name")).Text = ViewState["Oprovider_name"].ToString().ToUpper();
            if (ViewState["Oporz_title"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtporz_title")).Text = ViewState["Oporz_title"].ToString().ToUpper();
            if (ViewState["Ochele_title"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtchele_title")).Text = ViewState["Ochele_title"].ToString().ToUpper();
            if (ViewState["Oplan_title"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtplan_title")).Text = ViewState["Oplan_title"].ToString().ToUpper();
        }
        protected void lbRemoveFilterOutstanding_Click(object sender, EventArgs e)
        {
            if (ViewState["Ocode_igd"] != null) ViewState["Ocode_igd"] = null;
            if (ViewState["Obrand_name"] != null) ViewState["Obrand_name"] = null;
            if (ViewState["Osize_title"] != null) ViewState["Osize_title"] = null;
            if (ViewState["Ocolor_name"] != null) ViewState["Ocolor_name"] = null;
            if (ViewState["Ocode_igd"] != null) ViewState["Ocode_igd"] = null;
            if (ViewState["Oprovider_name"] != null) ViewState["Oprovider_name"] = null;
            if (ViewState["Oporz_title"] != null) ViewState["Oporz_title"] = null;
            if (ViewState["Ochele_title"] != null) ViewState["Ochele_title"] = null;
            if (ViewState["Oplan_title"] != null) ViewState["Oplan_title"] = null;
            
            GoodsClass objOutstanding = new GoodsClass();
            List<GoodsClass> lstPricingClass = new List<GoodsClass>();
            if(ViewState["Gstate"].ToString().Equals("1"))
                lstPricingClass = objOutstanding.GetGoodsClass();
            else
                lstPricingClass = objOutstanding.GetNullGoodsClass();
            grdViewOutstanding.DataSource = lstPricingClass;
            grdViewOutstanding.DataBind();
            ViewState["lstOutstandingOrders"] = lstPricingClass;
        }
        protected void btn_load_Click(object sender, EventArgs e)
        {
            ViewState["Gstate"] = "1";
            GoodsClass objPricingClass = new GoodsClass();
            List<GoodsClass> lstGoodsClass = new List<GoodsClass>();
            lstGoodsClass = objPricingClass.GetGoodsClass();
            grdViewOutstanding.DataSource = lstGoodsClass;
            grdViewOutstanding.DataBind();
            ViewState["lstOutstandingOrders"] = lstGoodsClass;
            upnlOutstanding.Update();
        }
        protected void btn_null_Click(object sender, EventArgs e)
        {
            ViewState["Gstate"] = "2";
            GoodsClass objPricingClass = new GoodsClass();
            List<GoodsClass> lstGoodsClass = new List<GoodsClass>();
            lstGoodsClass = objPricingClass.GetNullGoodsClass();
            grdViewOutstanding.DataSource = lstGoodsClass;
            grdViewOutstanding.DataBind();
            ViewState["lstOutstandingOrders"] = lstGoodsClass;
            upnlOutstanding.Update();
        }
        protected void grdViewOutstanding_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void SendingInvoice()
        {
            Common ubozhic = new Common();
            StringBuilder stringattach = new StringBuilder();
            stringattach.Append("<style type='text / css'>td{border-style:solid;border-width:1px;border-color:maroon}</style>");
            stringattach.Append("<div style='font-family:B Nazanin;font-size:13.0pt;direction:rtl;float:right;'>");
            stringattach.Append("<p style='font-family:B Nazanin;font-size:15.0pt; font-weight:bold;text-align:center'>” بسمه تعالي ”</p>");
            stringattach.Append("<table style='font-family:B Nazanin;font-size:13.0pt;width:100%'><thead><tr style='font-weight:bold;background-color:whitesmoke;'><td style='width:7%'>ردیف</td><td style='width:80%'>شرح</td><td style='width:13%'>مبلغ به ریال</td></tr></thead><tbody>");
            stringattach.Append("<tbody></table>");
            stringattach.Append("</div>");
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-word";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToShortDateString() + ".doc");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.UnicodeEncoding.UTF8;
            StringBuilder htmlCode = new StringBuilder();
            htmlCode.Append("<html>");
            htmlCode.Append("<body style='top:0px;bottom:0px;right:0px;left:0px;margin:0px 0px 0px 0px;'>");
            htmlCode.Append(stringattach.ToString());
            htmlCode.Append("</body></html>");
            HttpContext.Current.Response.Write(htmlCode.ToString());
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
        }
        protected void btn_print_pic_Click(object sender, EventArgs e)
        {
            Session["ThrowList"] = ViewState["lstOutstandingOrders"];
            Response.Redirect("CompleteWithPic.aspx");
        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            Session["ThrowList"] = ViewState["lstOutstandingOrders"];
            Response.Redirect("CompletePrint.aspx");
        }
    }
}