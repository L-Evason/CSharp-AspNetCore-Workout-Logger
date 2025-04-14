namespace ExerciseRoutine.Models;

public class Muscle
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int GroupId { get; set; }
    public required MuscleGroup Group { get; set; }

    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}