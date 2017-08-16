using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MVC_CRUD_Application_Demo.Models;
using Microsoft.Reporting.WebForms;

namespace MVC_CRUD_Application_Demo.Report
{
    public partial class EmployeeList : System.Web.UI.Page
    {
        private MVCCRUDEntities db = new MVCCRUDEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Employee> lstEmployee = (from emp in db.Employees
                                              select emp).ToList();
                ucrvBondList.rdlcFile = "MVC_CRUD_Application_Demo.Report.EmployeeList.rdlc";
                ReportDataSource[] rd = new ReportDataSource[1];
                rd[0] = new ReportDataSource("dsEmployeeList", lstEmployee);
                ucrvBondList.DataSource = rd;
            }
        }
    }
}