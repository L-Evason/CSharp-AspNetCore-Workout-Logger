using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSharpAspNetCoreExample.Data;
using ExerciseRoutine.Models;
using ViewModels.ExerciseLogViewModel;
using ViewModels.LogInputsViewModel;
using ViewModels.RoutineLogCreateViewModel;
using ViewModels.RoutineSelectViewModel;

namespace CSharpAspNetCoreExample.Controllers
{
    public class RoutineLogController : Controller
    {
        private readonly MvcWorkoutContext _context;

        public RoutineLogController(MvcWorkoutContext context)
        {
            _context = context;
        }

        //Select Routine
        public IActionResult SelectRoutine()
        {
            var model = new RoutineSelectViewModel
            {
                AvailableRoutines = _context.Routine
                .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
                .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SelectRoutine(RoutineSelectViewModel model)
        {
             return RedirectToAction("CreateRoutineLog", new { routineId = model.RoutineId });
        }

        public IActionResult CreateRoutineLog(int routineId)
        {
            var routine = _context.Routine.Find(routineId);
            if (routine == null) return NotFound();

            var viewModel = new RoutineLogCreateViewModel
            {
                RoutineId = routine.Id,
                RoutineName = routine.Name
            }; 
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateRoutineLog(RoutineLogCreateViewModel model)
        {   
            var routineLog = new RoutineLog
            {
                RoutineId = model.RoutineId,
                DateLogged = model.DateLogged
            };

            _context.RoutineLog.Add(routineLog);
            _context.SaveChanges();

            // Redirect to the first exercise in the routine
            var firstExercise = _context.Routine
                .Include(r => r.Exercises)
                .Where(r => r.Id == model.RoutineId)
                .SelectMany(r => r.Exercises)
                .FirstOrDefault();

            if (firstExercise == null)
                return RedirectToAction("Details", "RoutineLogs", new { id = routineLog.Id }); // No exercises

            return RedirectToAction("LogExercise", new { routineLogId = routineLog.Id, exerciseId = firstExercise.Id });
        }

        public IActionResult LogExercise(int routineLogId, int exerciseId)
        {
            var exercise = _context.Exercise.Find(exerciseId);
        if (exercise == null) return NotFound();

        var viewModel = new ExerciseLogViewModel
        {
            RoutineLogId = routineLogId,
            ExerciseId = exerciseId,
            ExerciseName = exercise.Name,
            SetLogs = new List<SetLogInputModel> { new() } // Default 1 set
        };

        return View(viewModel);
        }

        [HttpPost]
        public IActionResult LogExercise(ExerciseLogViewModel model)
        {
            foreach (var set in model.SetLogs)
            {
                var setLog = new SetLog
                {
                    RoutineLogId = model.RoutineLogId,
                    ExerciseId = model.ExerciseId,
                    Reps = set.Reps,
                    Weight = set.Weight,
                    SetTimestamp = DateTime.Now
                };

                _context.SetLog.Add(setLog);
            }

            _context.SaveChanges();

            // Move to next exercise in the routine
            var routineLog = _context.RoutineLog
                .Include(rl => rl.Routine)
                    .ThenInclude(r => r.Exercises)
                .FirstOrDefault(rl => rl.Id == model.RoutineLogId);

            if (routineLog == null) return NotFound();

            var exercises = routineLog.Routine?.Exercises.OrderBy(e => e.Id).ToList();
            var currentIndex = exercises?.FindIndex(e => e.Id == model.ExerciseId) ?? -1;

            if (exercises != null && currentIndex >= 0 && currentIndex + 1 < exercises.Count)
            {
                var nextExerciseId = exercises[currentIndex + 1].Id;
                return RedirectToAction("LogExercise", new { routineLogId = model.RoutineLogId, exerciseId = nextExerciseId });
            }

            return RedirectToAction("Details", new { id = model.RoutineLogId });
        }

        // GET: RoutineLog
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoutineLog
                .Include(l => l.Routine)
                .ToListAsync());
        }

        // GET: RoutineLog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routineLog = await _context.RoutineLog
                .Include(l => l.Routine)
                .Include(l => l.SetLogs)
                    .ThenInclude(sl => sl.Exercise)
                .FirstOrDefaultAsync(l => l.Id == id);
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

        /*
        //Controller Get
        public IActionResult CreateLog()
        {
            var viewModel = new RoutineLogViewModel
            {
                DateLogged = DateTime.Now,
                RoutineLogExercises = _context.Routine
                    .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
                    .ToList()
            };
            return View(viewModel);
        }

        // Controller POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoutineLog(RoutineLogViewModel viewModel, List<SetLogViewModel> setLogs)
        {
            var routineLog = new RoutineLog
            {
                RoutineId = viewModel.RoutineId,
                DateLogged = viewModel.DateLogged,
                SetLogs = setLogs.Select(s => new SetLog
                {
                    ExerciseId = s.ExerciseId,
                    RoutineLogId = s.RoutineLogId,
                    Reps = s.Reps,
                    Weight = s.Weight,
                    SetTimestamp = DateTime.Now // Or let user pick
                }).ToList()
            };

            _context.RoutineLog.Add(routineLog);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        } */

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
