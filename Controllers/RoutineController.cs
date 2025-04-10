using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSharpAspNetCoreExample.Data;
using ExerciseRoutine.Models;

namespace CSharpAspNetCoreExample.Controllers
{
    public class RoutineController : Controller
    {
        private readonly MvcWorkoutContext _context;

        public RoutineController(MvcWorkoutContext context)
        {
            _context = context;
        }

        // GET: Routine
        public async Task<IActionResult> Index()
        {
            return View(await _context.Routine.ToListAsync());
        }

        // GET: Routine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routine = await _context.Routine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (routine == null)
            {
                return NotFound();
            }

            return View(routine);
        }

        // GET: Routine/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Routine/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Routine routine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(routine);
        }

        // GET: Routine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routine = await _context.Routine.FindAsync(id);
            if (routine == null)
            {
                return NotFound();
            }
            return View(routine);
        }

        // POST: Routine/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Routine routine)
        {
            if (id != routine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutineExists(routine.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(routine);
        }

        // GET: Routine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routine = await _context.Routine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (routine == null)
            {
                return NotFound();
            }

            return View(routine);
        }

        // POST: Routine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routine = await _context.Routine.FindAsync(id);
            if (routine != null)
            {
                _context.Routine.Remove(routine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoutineExists(int id)
        {
            return _context.Routine.Any(e => e.Id == id);
        }
    }
}
