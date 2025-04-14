namespace ExerciseRoutine.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class SetLog
{
    public int Id { get; set; }
    public int RoutineLogId { get; set; }
    public required RoutineLog RoutineLog { get; set; }
    public int ExerciseId { get; set; }
    public required Exercise Exercise { get; set; }
    [DataType(DataType.Date)]
    public DateTime SetTimestamp { get; set; }
    public int Reps { get; set; }
    public float Weight { get; set; }
    
}