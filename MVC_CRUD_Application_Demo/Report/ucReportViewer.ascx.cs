using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace MVC_CRUD_Application_Demo.Report
{
    public partial class ucReportViewer : System.Web.UI.UserControl
    {
        public string rdlcFile { get; set; }

        public bool isSubReport { get; set; }
        //public ReportDataSource DataSource { get; set; }
        public ReportDataSource[] DataSource { get; set; }
        public ReportDataSource SubRptDataSource { get; set; }
        public ReportParameter[] rparams { get; set; }
        public ReportViewer RepView
        {
            get
            {
                return rvReportView;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (DataSource != null && !string.IsNullOrEmpty(rdlcFile))
                {
                    rvReportView.LocalReport.ReportEmbeddedResource = rdlcFile;
                    rvReportView.LocalReport.DataSources.Clear(); 
                    foreach (ReportDataSource item in DataSource)
                    {
                        rvReportView.LocalReport.DataSources.Add(item);
                    } 

                    if (rparams != null)
                        rvReportView.LocalReport.SetParameters(rparams);
                    rvReportView.LocalReport.Refresh();
                }
            }
        }
    }
}