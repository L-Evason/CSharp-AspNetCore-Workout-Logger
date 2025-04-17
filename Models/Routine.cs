namespace ExerciseRoutine.Models;

public class Routine
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    public ICollection<RoutineLog> RoutineLogs { get; set; } = new List<RoutineLog>();
}
