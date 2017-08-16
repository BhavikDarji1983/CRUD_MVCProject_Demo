using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CRUD_Application_Demo.Models;
using System.Collections;
using FluentValidation;
using MVC_CRUD_Application_Demo.Validation;
using FluentValidation.Results;
using System.Data.Entity;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace MVC_CRUD_Application_Demo.Controllers
{
    public class EmployeeController : Controller
    {
        private const int pageSize = 5;
        private MVCCRUDEntities db = new MVCCRUDEntities();

        public ActionResult Index(int? page, string sortFieldName, string sortType, string searchByFirstName)
        {
            List<Employee> lstEmployee = GetSortedEmployee(sortFieldName, sortType, searchByFirstName);
            if (searchByFirstName != null)
                page = 1;
            ViewBag.CurrentSearch = searchByFirstName;

            int pageNumber = (page ?? 1);
            if (page == null)
                ViewBag.SortType = "desc";
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            ViewBag.PageNumber = pageNumber;
            return View(lstEmployee.ToPagedList(pageNumber, pageSize));
        } 
        
        public ActionResult AddEmployee()
        {
            return View(new Employee());
        }

        [HttpPost] 
        public ActionResult AddEmployee(Employee objEmp)
        {
            EmployeeValidator validator = new EmployeeValidator();
            ValidationResult result = validator.Validate(objEmp);
            if (!result.IsValid)
            {
                foreach (ValidationFailure item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            else
            {
                db.Employees.Add(objEmp);
                db.SaveChanges();
                TempData["Message"] = "Added record sucessfully.";
                return RedirectToAction("Index");
            }

            return View(objEmp);
        }

        public ActionResult UpdateEmployee(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        [HttpPost] 
        public ActionResult UpdateEmployee(Employee objEmp)
        {
            EmployeeValidator validator = new EmployeeValidator();
            ValidationResult result = validator.Validate(objEmp);
            if (!result.IsValid)
            {
                foreach (ValidationFailure item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            else
            {
                db.Entry(objEmp).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Updated record sucessfully.";
                return RedirectToAction("Index");
            }
            return View(objEmp);
        }

        public virtual ActionResult DeleteEmployee(int id, int? page)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            TempData["Message"] = "Deleted record sucessfully.";
            return RedirectToAction("Index", new { page = page });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult DownloadPDF()
        {
            FileInfo info = new FileInfo(Server.MapPath(String.Format("/PDF/{0}", "EmployeeList.pdf")));

            string certificateFilePath = Server.MapPath(String.Format("/PDF/{0}", "EmployeeList.pdf"));
            List<Employee> lstEmployee = (from emp in db.Employees
                                          select emp).ToList();


            LocalReport report = new LocalReport();
            report.ReportPath = "Report/EmployeeList.rdlc";
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dsEmployeeList";
            rds.Value = lstEmployee;
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF");
            using (FileStream fs = System.IO.File.Create(certificateFilePath))
            {
                fs.Write(mybytes, 0, mybytes.Length);
            }
            return File(info.FullName, "application/pdf", info.Name);
        }

        private List<Employee> GetSortedEmployee(string sortFieldName, string sortType, string searchString)
        {
            var vEmployee = from emp in db.Employees select emp;

            if (!String.IsNullOrEmpty(searchString))
            {
                vEmployee = vEmployee.Where(s => s.FirstName.Contains(searchString));
            }

            if (sortFieldName == "id")
            {
                if (sortType == "desc")
                {
                    vEmployee = vEmployee.OrderByDescending(emp => emp.Id);
                    ViewBag.SortType = "asc";
                }
                else
                {
                    vEmployee = vEmployee.OrderBy(emp => emp.Id);
                    ViewBag.SortType = "desc";
                }
            }
            else if (sortFieldName == "firstname")
            {
                if (sortType == "desc")
                {
                    vEmployee = vEmployee.OrderByDescending(emp => emp.FirstName);
                    ViewBag.SortType = "asc";
                }
                else
                {
                    vEmployee = vEmployee.OrderBy(emp => emp.FirstName);
                    ViewBag.SortType = "desc";
                }
            }
            else if (sortFieldName == "lastname")
            {

                if (sortType == "desc")
                {
                    vEmployee = vEmployee.OrderByDescending(emp => emp.LastName);
                    ViewBag.SortType = "asc";
                }
                else
                {
                    vEmployee = vEmployee.OrderBy(emp => emp.LastName);
                    ViewBag.SortType = "desc";
                }
            }
            else if (sortFieldName == "middlename")
            {
                if (sortType == "desc")
                {

                    vEmployee = vEmployee.OrderByDescending(emp => emp.MiddleName);
                    ViewBag.SortType = "asc";
                }
                else
                {
                    vEmployee = vEmployee.OrderBy(emp => emp.MiddleName);
                    ViewBag.SortType = "desc";
                }
            }
            else if (sortFieldName == "designation")
            {
                if (sortType == "desc")
                {

                    vEmployee = vEmployee.OrderByDescending(emp => emp.Designation);
                    ViewBag.SortType = "asc";
                }
                else
                {
                    vEmployee = vEmployee.OrderBy(emp => emp.Designation);
                    ViewBag.SortType = "desc";
                }
            }
            else
            {
                vEmployee = vEmployee.OrderBy(emp => emp.Id);
            }

            return vEmployee.ToList();
        }
    }
}
