using GravelFamily.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravelFamilyWeb.Controllers
{
    public class UnitsController : Controller
    {
        private GravelFamilyContext _context;

        public UnitsController(GravelFamilyContext context)
        {
            _context = context;
        }

        //GET Units
        public IActionResult Index()
        {
            return View(_context.Units.ToList());
        }

        //GET: Units/Details/id
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var unit = _context.Units
                .FirstOrDefault(u => u.UnitId == id);

            if(unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        //Get: Units/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Units/Create
        [HttpPost]
        public IActionResult Create(Unit unit)
        {
            _context.Add(unit);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //GET Units/Edit/id
        public IActionResult Edit (int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var unit = _context.Units.Find(id);
            if(unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        //POST: Unit/Edit/id
        [HttpPost]
        public IActionResult Edit(int id, Unit unit)
        {
            if(id != unit.UnitId)
            {
                return NotFound();
            }

            _context.Update(unit);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //GET: Employees/Delete/id
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var unit = _context.Units
                .FirstOrDefault(m => m.UnitId == id);
            if(unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        //POST: Unit/Delete/id
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var unit = _context.Units.Find(id);
            _context.Units.Remove(unit);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
