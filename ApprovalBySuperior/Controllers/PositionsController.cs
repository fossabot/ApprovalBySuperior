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
    public class PositionsController : Controller
    {
        private readonly IPositionRepository _positionRepository;

        public PositionsController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        // GET: Positions
        public IActionResult Index()
        {
            // UserRepository参照
            var _posit = _positionRepository.GetAll();

            return View(_posit);
        }

        // GET: Positions/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Positions positions = _positionRepository.GetPositionId(id);

            return View(positions);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Positionid,Positionname")] Positions positions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _positionRepository.Add(positions);
                    _positionRepository.Save();
                    return RedirectToAction("Index");
                }

            }
            catch(DataException)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, 
                                         "Unable to save changes. Try again, " +
                                         "and if the problem persists contact your system administrator.");
            }

            return View(positions);
        }

        // GET: Positions/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Positions positions = _positionRepository.GetPositionId(id);
            return View(positions);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Positionid,Positionname")] Positions positions)
        {
            if (id != positions.Positionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _positionRepository.Update(positions);
                    _positionRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty,
                                                     "Unable to save changes. Try again, " +
                                                     "and if the problem persists contact your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(positions);
        }

        // GET: Positions/Delete/5
        public IActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            Positions positions = _positionRepository.GetPositionId(id);

            return View(positions);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                Positions positions = _positionRepository.GetPositionId(id);
                _positionRepository.Delete(id);
                _positionRepository.Save();
            }
            catch(DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("index");
        }
    }
}
