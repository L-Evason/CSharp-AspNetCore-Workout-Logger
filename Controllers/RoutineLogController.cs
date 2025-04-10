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
    public class RoutineLogController : Controller
    {
        private readonly MvcWorkoutContext _context;

        public RoutineLogController(MvcWorkoutContext context)
        {
            _context = context;
        }

        // GET: RoutineLog
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoutineLog.ToListAsync());
        }

        // GET: RoutineLog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routineLog = await _context.RoutineLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (routineLog == null)
            {
                return NotFound();
            }

            return View(routineLog);
        }

        // GET: RoutineLog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoutineLog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoutineId,DateLogged")] RoutineLog routineLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routineLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(routineLog);
        }

        // GET: RoutineLog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routineLog = await _context.RoutineLog.FindAsync(id);
            if (routineLog == null)
            {
                return NotFound();
            }
            return View(routineLog);
        }

        // POST: RoutineLog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoutineId,DateLogged")] RoutineLog routineLog)
        {
            if (id != routineLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routineLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutineLogExists(routineLog.Id))
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
            return View(routineLog);
        }

        // GET: RoutineLog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routineLog = await _context.RoutineLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (routineLog == null)
            {
                return NotFound();
            }

            return View(routineLog);
        }

        // POST: RoutineLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routineLog = await _context.RoutineLog.FindAsync(id);
            if (routineLog != null)
            {
                _context.RoutineLog.Remove(routineLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoutineLogExists(int id)
        {
            return _context.RoutineLog.Any(e => e.Id == id);
        }
    }
}
