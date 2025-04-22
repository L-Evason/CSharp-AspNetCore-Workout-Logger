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
    public class MuscleController : Controller
    {
        private readonly MvcWorkoutContext _context;

        public MuscleController(MvcWorkoutContext context)
        {
            _context = context;
        }

        // GET: Muscle
        public async Task<IActionResult> Index()
        {
            ViewData["GroupId"] = new SelectList(_context.MuscleGroup, "Id", "Name");
            return View(await _context.Muscle.ToListAsync());
        }

        // GET: Muscle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muscle = await _context.Muscle
                .Include(m => m.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muscle == null)
            {
                return NotFound();
            }

            return View(muscle);
        }

        // GET: Muscle/Create
        public IActionResult Create()
        {
            // Load MuscleGroups into view for Create.cshtml ViewBag.GroupId
            ViewData["GroupId"] = new SelectList(_context.MuscleGroup, "Id", "Name");
            return View();
        }

        // POST: Muscle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GroupId")] Muscle muscle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(muscle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // If validation fails return MuscleGroup list
            ViewData["GroupId"] = new SelectList(_context.MuscleGroup, "Id", "Name", muscle.GroupId);
            return View(muscle);
        }

        // GET: Muscle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muscle = await _context.Muscle.FindAsync(id);
            if (muscle == null)
            {
                return NotFound();
            }
            return View(muscle);
        }

        // POST: Muscle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GroupId")] Muscle muscle)
        {
            if (id != muscle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(muscle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuscleExists(muscle.Id))
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
            return View(muscle);
        }

        // GET: Muscle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muscle = await _context.Muscle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muscle == null)
            {
                return NotFound();
            }

            return View(muscle);
        }

        // POST: Muscle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var muscle = await _context.Muscle.FindAsync(id);
            if (muscle != null)
            {
                _context.Muscle.Remove(muscle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuscleExists(int id)
        {
            return _context.Muscle.Any(e => e.Id == id);
        }
    }
}
