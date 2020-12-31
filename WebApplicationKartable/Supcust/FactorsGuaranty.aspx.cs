using Cartable;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;

namespace WebApplicationKartable
{
    public partial class FactorsGuaranty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/FactorsGuaranty.rdlc");
            ReportViewer1.LocalReport.EnableExternalImages = true;
            
            string imagePath = new Uri(Server.MapPath("~/images/logo.png")).AbsoluteUri;

            ReportViewer1.LocalReport.Refresh();
        }
    }
}