namespace ExerciseRoutine.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Muscle
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    public required int GroupId { get; set; }
    public MuscleGroup? Group { get; set; }

    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}