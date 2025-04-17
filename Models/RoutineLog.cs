namespace ExerciseRoutine.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class RoutineLog
{
    public int Id { get; set; }
    public required int RoutineId { get; set; }
    public Routine? Routine { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateLogged { get; set; }
    public ICollection<SetLog> SetLogs { get; set; } = new List<SetLog>();
}