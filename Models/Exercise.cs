namespace ExerciseRoutine.Models;

public class Exercise
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<Muscle> Muscles { get; set; } = new List<Muscle>();

    public ICollection<Routine> Routines { get; set; } = new List<Routine>();
}