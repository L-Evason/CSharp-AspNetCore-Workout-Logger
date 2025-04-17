using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSharpAspNetCoreExample.Data;
using ExerciseRoutine.Models;
using ViewModels.RoutineViewModel;

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
                .Include(r => r.Exercises)
                .Include(r => r.RoutineLogs)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (routine == null)
            {
                return NotFound();
            }

            return View(routine);
        }

        // GET: Routine/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new RoutineViewModel
            {
                AvailableExercises = await _context.Exercise
                    .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Name
                    })
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Routine/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoutineViewModel model)
        {               
            //  If not a valid model return the view
            if (!ModelState.IsValid)
            {
                model.AvailableExercises = await _context.Muscle
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name })
                    .ToListAsync();

                return View(model);
            }

            var exercise = await _context.Exercise
                .Where(e => model.SelectedExerciseIds.Contains(e.Id))
                .ToListAsync();

            var routine = new Routine
            {
                Name = model.Name
            };
            _context.Add(routine);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Routine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routine = await _context.Routine
                .Include(r => r.Exercises)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (routine == null)
            {
                return NotFound();
            }

            var allExercises = await _context.Exercise.ToListAsync();

            var viewModel = new RoutineViewModel
            {
                Id = routine.Id,
                Name = routine.Name,
                SelectedExerciseIds = routine.Exercises.Select(e => e.Id).ToList(),
                AvailableExercises = allExercises.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Routine/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoutineViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // Re-populate AvailableExercises if returning to the view
                model.AvailableExercises = await _context.Exercise
                    .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Name
                    }).ToListAsync();

                return View(model);
            }

            var routine = await _context.Routine
                .Include(r => r.Exercises)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (routine == null)
                return NotFound();

            // Update name
            routine.Name = model.Name;

            // Clear old exercises and add selected ones
            routine.Exercises.Clear();

            var selectedExercises = await _context.Exercise
                .Where(e => model.SelectedExerciseIds.Contains(e.Id))
                .ToListAsync();

            foreach (var exercise in selectedExercises)
            {
                routine.Exercises.Add(exercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
