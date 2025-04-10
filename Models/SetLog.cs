namespace ExerciseRoutine.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class SetLog
{
    public int Id { get; set;}
    public int RoutineId { get; set;}
    public int ExerciseId { get; set;}
    [DataType(DataType.Date)]
    public DateTime SetTimestamp { get; set; }
    public int Reps { get; set; }
    public float Weight { get; set; }
}