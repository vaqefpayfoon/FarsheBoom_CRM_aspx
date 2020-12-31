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
    public partial class CallPerform : System.Web.UI.Page
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
            if (!IsPostBack)
            {

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
                    if (txtBox.ID == "txtcode_igd")
                    {
                        allPricingClass = allPricingClass.Where(x => x.code_igd.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ocode_igd"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtsize_title")
                    {
                        allPricingClass = allPricingClass.Where(x => x.size_title.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Osize_title"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtbrand_name")
                    {
                        allPricingClass = allPricingClass.Where(x => x.brand_name.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Obrand_name"] = txtBox.Text.Trim().ToUpper();
                    }
                    else if (txtBox.ID == "txtcarpet_title")
                    {
                        allPricingClass = allPricingClass.Where(x => x.carpet_title.Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Ocarpet_title"] = txtBox.Text.Trim().ToUpper();
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
                    else if (txtBox.ID == "txtpercent_profit")
                    {
                        allPricingClass = allPricingClass.Where(x => x.percent_profit.ToString().Contains(txtBox.Text.Trim().ToUpper())).ToList();
                        ViewState["Opercent_profit"] = txtBox.Text.Trim().ToUpper();
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
            if (ViewState["Osize_title"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtsize_title")).Text = ViewState["Osize_title"].ToString().ToUpper();
            if (ViewState["Obrand_name"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtbrand_name")).Text = ViewState["Obrand_name"].ToString().ToUpper();
            if (ViewState["Ocarpet_title"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtcarpet_title")).Text = ViewState["Ocarpet_title"].ToString().ToUpper();
            if (ViewState["Obuy_price"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtbuy_price")).Text = ViewState["Obuy_price"].ToString().ToUpper();
            if (ViewState["Osale_price"] != null)
                ((TextBox)grdViewOutstanding.HeaderRow.FindControl("txtsale_price")).Text = ViewState["Osale_price"].ToString().ToUpper();           
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
            if (ViewState["Osize_title"] != null) ViewState["Osize_title"] = null;
            if (ViewState["Obrand_name"] != null) ViewState["Obrand_name"] = null;
            if (ViewState["Ocode_igd"] != null) ViewState["Ocode_igd"] = null;
            if (ViewState["Obuy_price"] != null) ViewState["Obuy_price"] = null;
            if (ViewState["Osale_price"] != null) ViewState["Osale_price"] = null;
            if (ViewState["Ou_date_time"] != null) ViewState["Ou_date_time"] = null;
            if (ViewState["Omargin_profit"] != null) ViewState["Omargin_profit"] = null;
            if (ViewState["Opercent_profit"] != null) ViewState["Opercent_profit"] = null;

            PricingClass objPricingClass = new PricingClass();
            List<PricingClass> lstPricingClass = new List<PricingClass>();
            lstPricingClass = objPricingClass.CallPerformed(lst_project.SelectedValue, lst_provider.SelectedValue);
            grdViewOutstanding.DataSource = lstPricingClass;
            grdViewOutstanding.DataBind();

            ViewState["lstOutstandingOrders"] = lstPricingClass;
        }
        protected void btn_report_Click(object sender, ImageClickEventArgs e)
        {
            PricingClass objPricingClass = new PricingClass();
            List<PricingClass> lstPricingClass = new List<PricingClass>();
            lstPricingClass = objPricingClass.CallPerformed(lst_project.SelectedValue, lst_provider.SelectedValue);
            grdViewOutstanding.DataSource = lstPricingClass;
            grdViewOutstanding.DataBind();
            ViewState["lstOutstandingOrders"] = lstPricingClass;
            upnlOutstanding.Update();
            ViewState["getall"] = 1;
            txt_count.Text = lstPricingClass.Count.ToString();
        }
        protected void btn_assign_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in grdViewOutstanding.Rows)
            {
                string srl = grdViewOutstanding.Rows[count].Cells[0].Text;
                CheckBox check = (CheckBox)row.FindControl("confirm");
                if(check.Checked)
                {
                    int maxsrl = max_srl_details();
                    if (!Duplicate_GoodsInproject(srl, lst_project.SelectedValue))
                    {
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@srl", SqlDbType.Int);
                        param[0].Value = maxsrl;
                        param[1] = new SqlParameter("@header_srl", SqlDbType.Int);
                        param[1].Value = Convert.ToInt32(lst_project.SelectedValue);
                        param[2] = new SqlParameter("@igd_srl", SqlDbType.Int);
                        param[2].Value = Convert.ToInt32(srl);
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
                    }
                    else
                    {
                        lblError.Text = "فرش انتخاب شده در همین نمایشگاه موجود می باشد";
                        return;
                    }
                }
                count++;
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
        private string get_goods_srl(string code)
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data(string.Format("SELECT srl FROM inv_goods where code_igd={0}", code));
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return string.Empty;
        }
    }
}