namespace ExerciseRoutine.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class RoutineLog
{
    public int Id { get; set;}
    public int RoutineId { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateLogged { get; set; }
}