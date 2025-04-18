namespace ViewModels.ExerciseLogViewModel;

public class ExerciseLogViewModel
{
    public int RoutineLogId { get; set; }
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; } = "";
    public List<SetLogInputModel> SetLogs { get; set; } = new();
}

public class SetLogInputModel
{
    public int Reps { get; set; }
    public decimal Weight { get; set; }
}