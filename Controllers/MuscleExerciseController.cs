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
    public class MuscleExerciseController : Controller
    {
        private readonly MvcWorkoutContext _context;

        public MuscleExerciseController(MvcWorkoutContext context)
        {
            _context = context;
        }

        // GET: MuscleExercise
        public async Task<IActionResult> Index()
        {
            return View(await _context.MuscleExercise.ToListAsync());
        }

        // GET: MuscleExercise/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muscleExercise = await _context.MuscleExercise
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muscleExercise == null)
            {
                return NotFound();
            }

            return View(muscleExercise);
        }

        // GET: MuscleExercise/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MuscleExercise/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MuscleId,ExerciseId")] MuscleExercise muscleExercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(muscleExercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(muscleExercise);
        }

        // GET: MuscleExercise/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muscleExercise = await _context.MuscleExercise.FindAsync(id);
            if (muscleExercise == null)
            {
                return NotFound();
            }
            return View(muscleExercise);
        }

        // POST: MuscleExercise/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MuscleId,ExerciseId")] MuscleExercise muscleExercise)
        {
            if (id != muscleExercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(muscleExercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuscleExerciseExists(muscleExercise.Id))
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
            return View(muscleExercise);
        }

        // GET: MuscleExercise/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muscleExercise = await _context.MuscleExercise
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muscleExercise == null)
            {
                return NotFound();
            }

            return View(muscleExercise);
        }

        // POST: MuscleExercise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var muscleExercise = await _context.MuscleExercise.FindAsync(id);
            if (muscleExercise != null)
            {
                _context.MuscleExercise.Remove(muscleExercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuscleExerciseExists(int id)
        {
            return _context.MuscleExercise.Any(e => e.Id == id);
        }
    }
}
