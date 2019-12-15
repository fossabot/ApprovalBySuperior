using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApprovalBySuperior.Models;
using ApprovalBySuperior.Services;
using System.Data;

namespace DotNetCode.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentrepository;

        public DepartmentsController(IDepartmentRepository departmentrepository)
        {
            _departmentrepository = departmentrepository;
        }

        // GET: Departments
        public IActionResult Index()
        {
            // UserRepository参照
            var _dept = _departmentrepository.GetAll();

            return View(_dept);
        }

        // GET: Departments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Departments departments = _departmentrepository.GetDepartmentId(id);

            return View(departments);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Depid,Depname,Groupname")] Departments departments)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentrepository.Add(departments);
                    _departmentrepository.Save();
                    return RedirectToAction("Index");
                }

            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty,
                                         "Unable to save changes. Try again, " +
                                         "and if the problem persists contact your system administrator.");
            }

            return View(departments);
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Departments departments = _departmentrepository.GetDepartmentId(id);
            return View(departments);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Depid,Depname,Groupname")] Departments departments)
        {
            if (id != departments.Depid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _departmentrepository.Update(departments);
                    _departmentrepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty,
                                                     "Unable to save changes. Try again, " +
                                                     "and if the problem persists contact your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departments);
        }

        // GET: Departments/Delete/5
        public IActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            Departments departments = _departmentrepository.GetDepartmentId(id);

            return View(departments);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                Departments departments = _departmentrepository.GetDepartmentId(id);
                _departmentrepository.Delete(id);
                _departmentrepository.Save();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("index");
        }
    }
}
