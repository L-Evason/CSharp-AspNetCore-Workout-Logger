namespace ViewModels.RoutineSelectViewModel;

// For SelectListItem
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


public class RoutineSelectViewModel
{
    [Required]
    public int RoutineId { get; set; }
    public List<SelectListItem> AvailableRoutines { get; set; } = new();
}