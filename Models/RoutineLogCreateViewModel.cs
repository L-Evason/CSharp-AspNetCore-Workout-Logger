namespace ViewModels.RoutineLogCreateViewModel;

public class RoutineLogCreateViewModel
{
    public int RoutineId { get; set; }
    public string RoutineName { get; set; } = "";
    public DateTime DateLogged { get; set; } = DateTime.Now;
}