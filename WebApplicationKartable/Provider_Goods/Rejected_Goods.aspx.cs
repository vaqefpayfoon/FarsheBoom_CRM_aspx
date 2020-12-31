using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplicationKartable
{
    public partial class Rejected_Goods : System.Web.UI.Page
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButton_print_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT code_igd, provider_name, provider_code, size_title FROM dbo.Project_Goods_View Where selection='True' And header_srl=" + lst_project.SelectedValue;
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            DataSet1.RejectedCarpetDataTable table = new DataSet1.RejectedCarpetDataTable();
            foreach(DataRow Woak in dt.Rows)
                table.Rows.Add(Woak.ItemArray);
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RejectedCarpet.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)table);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.Refresh();
            cnn.Close();
        }
    }
}