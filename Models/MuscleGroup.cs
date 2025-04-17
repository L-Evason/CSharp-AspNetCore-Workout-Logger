namespace ExerciseRoutine.Models;


public class MuscleGroup
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Muscle> Muscles { get; set; } = new List<Muscle>();
}