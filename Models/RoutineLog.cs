namespace ExerciseRoutine.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class RoutineLog
{
    public int Id { get; set; }
    public int RoutineId { get; set; }
    public required Routine Routine { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateLogged { get; set; }
    public ICollection<SetLog> SetLogs { get; set; } = new List<SetLog>();
}