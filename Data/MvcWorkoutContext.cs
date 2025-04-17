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
        public MvcWorkoutContext(DbContextOptions<MvcWorkoutContext> options)
            : base(options)
        {
        }

        public DbSet<ExerciseRoutine.Models.MuscleGroup> MuscleGroup { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.Muscle> Muscle { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.Exercise> Exercise { get; set; } = default!;
        //public DbSet<ExerciseRoutine.Models.MuscleExercise> MuscleExercise { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.Routine> Routine { get; set; } = default!;
        //public DbSet<ExerciseRoutine.Models.RoutineExercises> RoutineExercises { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.RoutineLog> RoutineLog { get; set; } = default!;
        public DbSet<ExerciseRoutine.Models.SetLog> SetLog { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Muscle>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Muscles)
                .HasForeignKey(m => m.GroupId);

            modelBuilder.Entity<Muscle>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.Muscles)
                .WithMany(m => m.Exercises)
                .UsingEntity(j => j.ToTable("MuscleExercises"));

            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.Routines)
                .WithMany(r => r.Exercises)
                .UsingEntity(j => j.ToTable("RoutineExercises"));

            modelBuilder.Entity<Exercise>()
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder.Entity<Routine>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<RoutineLog>()
                .HasOne(rl => rl.Routine)
                .WithMany(r => r.RoutineLogs)
                .HasForeignKey(rl => rl.RoutineId);

            modelBuilder.Entity<SetLog>()
                .HasOne(sl => sl.RoutineLog)
                .WithMany(rl => rl.SetLogs)
                .HasForeignKey(sl => sl.RoutineLogId);

            modelBuilder.Entity<SetLog>()
                .HasOne(sl => sl.Exercise)
                .WithMany()
                .HasForeignKey(sl => sl.ExerciseId);
        }
    }
}
