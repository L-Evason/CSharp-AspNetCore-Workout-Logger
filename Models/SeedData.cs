using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CSharpAspNetCoreExample.Data;
using System;
using System.Linq;
using ExerciseRoutine.Models;

namespace CSharpAspNetCoreExample.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcWorkoutContext(
            serviceProvider.GetRequiredService<DbContextOptions<MvcWorkoutContext>>()))
        {
            // Look for any data
            if (IsSeeded(context))
            {
                return; // Db has been seeded skip seeding
            }
            // Seed Muscle Group
            var group = new MuscleGroup
            {
                Id = 1,
                Name = "Anterior upper arm"
            };

            var biceps = new Muscle
            {
                Id = 1,
                Name = "Biceps brachii",
                GroupId = 1,
                Group = group
            };

            var brachialis = new Muscle
            {
                Id = 2,
                Name = "Brachialis",
                GroupId = 1,
                Group = group
            };

            var coracobrachialis = new Muscle
            {
                Id = 3,
                Name = "Coracobrachialis",
                GroupId = 1,
                Group = group
            };

            var exercise = new Exercise
            {
                Id = 1,
                Name = "Barbell Curl",
                Muscles = new List<Muscle> { biceps, brachialis }
            };

            // Add to context
            context.MuscleGroup.Add(group);
            context.Muscle.AddRange(biceps, brachialis, coracobrachialis);
            context.Exercise.Add(exercise);

            context.SaveChanges();
        }
    }

    private static bool IsSeeded(MvcWorkoutContext context)
    {
        return context.MuscleGroup.Any() ||
            context.Muscle.Any() ||
            context.Exercise.Any() ||
            context.Routine.Any() ||
            context.RoutineLog.Any() ||
            context.SetLog.Any();
    }
}