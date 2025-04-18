namespace ViewModels.LogInputsViewModel;

using ViewModels.ExerciseLogViewModel;


public class RoutineLogInputModel
{
    public int RoutineId { get; set; }
    public DateTime DateLogged { get; set; } = DateTime.Now;
    public List<SetLogInputModel> ExerciseLogs { get; set; } = new();
}


