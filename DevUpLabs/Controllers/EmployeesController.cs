using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevUpLabs.Models;

namespace DevUpLabs.Controllers
{
    public class EmployeesController : Controller
    {
        #region Variables
        DataAccessLayer _objEmployee = new DataAccessLayer();
        public static List<SelectListItem> Employees = null;
        #endregion

        // GET: Employee
        [HttpGet]
        public ActionResult Index()
        {
            var listEmployees = new List<Employees>();
            listEmployees = _objEmployee.ListAllEmployees().ToList();

            Employees = new List<SelectListItem>();
            foreach (var item in listEmployees)
            {
                Employees.Add(new SelectListItem { Text = item.JobTitle, Value = item.JobTitle });
            }

            return View(listEmployees);
        }
        
        public ActionResult Create(Employees employees)
        {
            if (ModelState.IsValid && employees != null)
            {
                _objEmployee.CreateEmployee(employees);
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Redirect("Error");
            }

            var employees = _objEmployee.GetEmployeeData(id);

            if (employees == null)
            {
                return Redirect("Error");
            }

            return View(employees);
        }

        public ActionResult DeleteConfirmed(int? id)
        {
            _objEmployee.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

    }

}