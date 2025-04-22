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
    public class SetLogController : Controller
    {
        private readonly MvcWorkoutContext _context;

        public SetLogController(MvcWorkoutContext context)
        {
            _context = context;
        }

        // GET: SetLog
        public async Task<IActionResult> Index()
        {
            return View(await _context.SetLog.ToListAsync());
        }

        // GET: SetLog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setLog = await _context.SetLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setLog == null)
            {
                return NotFound();
            }

            return View(setLog);
        }

        // GET: SetLog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SetLog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoutineId,ExerciseId,SetTimestamp,Reps,Weight")] SetLog setLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(setLog);
        }

        // GET: SetLog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setLog = await _context.SetLog.FindAsync(id);
            if (setLog == null)
            {
                return NotFound();
            }
            return View(setLog);
        }

        // POST: SetLog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoutineId,ExerciseId,SetTimestamp,Reps,Weight")] SetLog setLog)
        {
            if (id != setLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetLogExists(setLog.Id))
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
            return View(setLog);
        }

        // GET: SetLog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setLog = await _context.SetLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setLog == null)
            {
                return NotFound();
            }
            return View(setLog);
        }

        // POST: SetLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setLog = await _context.SetLog.FindAsync(id);
            if (setLog != null)
            {
                _context.SetLog.Remove(setLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetLogExists(int id)
        {
            return _context.SetLog.Any(e => e.Id == id);
        }
    }
}
