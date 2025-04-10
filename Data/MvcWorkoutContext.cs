using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExerciseRoutine.Models;

namespace CSharpAspNetCoreExample.Data
{
    public class MvcWorkoutContext : DbContext
    {
        public MvcWorkoutContext (DbContextOptions<MvcWorkoutContext> options)
            : base(options)
        {
        }

        public DbSet<ExerciseRoutine.Models.MuscleGroup> MuscleGroup { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.Muscle> Muscle { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.Exercise> Exercise { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.MuscleExercise> MuscleExercise { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.Routine> Routine { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.RoutineExercises> RoutineExercises { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.RoutineLog> RoutineLog { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.SetLog> SetLog { get; set; } = default!;
    }
}
