namespace ExerciseRoutine.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SetLog
{
    public int Id { get; set; }
    public required int RoutineLogId { get; set; }
    public RoutineLog? RoutineLog { get; set; }
    public required int ExerciseId { get; set; }
    public Exercise? Exercise { get; set; }
    [DataType(DataType.Date)]
    public DateTime SetTimestamp { get; set; }
    public int Reps { get; set; }
    [Column(TypeName = "decimal(5, 2)")]
    public decimal Weight { get; set; }
    
}