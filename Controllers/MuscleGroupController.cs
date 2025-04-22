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
    public class MuscleGroupController : Controller
    {
        private readonly MvcWorkoutContext _context;

        public MuscleGroupController(MvcWorkoutContext context)
        {
            _context = context;
        }

        // GET: MuscleGroup
        public async Task<IActionResult> Index()
        {
            return View(await _context.MuscleGroup.ToListAsync());
        }

        // GET: MuscleGroup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muscleGroup = await _context.MuscleGroup
                .Include(g => g.Muscles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muscleGroup == null)
            {
                return NotFound();
            }

            return View(muscleGroup);
        }

        // GET: MuscleGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MuscleGroup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MuscleGroup muscleGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(muscleGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(muscleGroup);
        }

        // GET: MuscleGroup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muscleGroup = await _context.MuscleGroup.FindAsync(id);
            if (muscleGroup == null)
            {
                return NotFound();
            }
            return View(muscleGroup);
        }

        // POST: MuscleGroup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MuscleGroup muscleGroup)
        {
            if (id != muscleGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(muscleGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuscleGroupExists(muscleGroup.Id))
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
            return View(muscleGroup);
        }

        // GET: MuscleGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muscleGroup = await _context.MuscleGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muscleGroup == null)
            {
                return NotFound();
            }

            return View(muscleGroup);
        }

        // POST: MuscleGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var muscleGroup = await _context.MuscleGroup.FindAsync(id);
            if (muscleGroup != null)
            {
                _context.MuscleGroup.Remove(muscleGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuscleGroupExists(int id)
        {
            return _context.MuscleGroup.Any(e => e.Id == id);
        }
    }
}
