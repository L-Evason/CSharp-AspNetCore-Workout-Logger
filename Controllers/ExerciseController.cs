using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSharpAspNetCoreExample.Data;
using ExerciseRoutine.Models;
using ViewModels.ExerciseViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CSharpAspNetCoreExample.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly MvcWorkoutContext _context;

        public ExerciseController(MvcWorkoutContext context)
        {
            _context = context;
        }

        // GET: Exercise
        public async Task<IActionResult> Index()
        {
            return View(await _context.Exercise.ToListAsync());
        }

        // GET: Exercise/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise
                .Include(e => e.Muscles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercise/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new ExerciseViewModel
            {
                AvailableMuscles = await _context.Muscle
                    .Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.Name
                    })
                    .ToListAsync(),
                AvailableRoutines = await _context.Routine
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    })
                    .ToListAsync()
            };
            return View(viewModel);
        }

        // POST: Exercise/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseViewModel model)
        {
            // If not a valid model return the view
            if (!ModelState.IsValid)
            {
                model.AvailableMuscles = await _context.Muscle
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name })
                    .ToListAsync();
                model.AvailableRoutines = await _context.Routine
                    .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
                    .ToListAsync();

                return View(model);
            }

            var muscles = await _context.Muscle
                .Where(m => model.SelectedMuscleIds.Contains(m.Id))
                .ToListAsync();

            var routines = await _context.Routine
                .Where(r => model.SelectedRoutineIds.Contains(r.Id))
                .ToListAsync();

            var exercise = new Exercise
            {
                Name = model.Name,
                Muscles = muscles,
                Routines = routines
            };

            _context.Add(exercise);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Exercise/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercise/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
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
            return View(exercise);
        }

        // GET: Exercise/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercise.FindAsync(id);
            if (exercise != null)
            {
                _context.Exercise.Remove(exercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercise.Any(e => e.Id == id);
        }
    }
}
