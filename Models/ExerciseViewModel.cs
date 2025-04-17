namespace ViewModels.ExerciseViewModel;

// For SelectListItem
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

// Data transfer object for exercise
public class ExerciseViewModel
{   
    public int? Id { get; set; }
    // Name field is required and cannot be empty
    [Required]
    [MinLength(1, ErrorMessage = "Name cannot be empty.")]
    public string? Name { get; set; }
    // Lists default to empty. MuscleId list required
    public List<int> SelectedMuscleIds { get; set; } = new List<int>();
    public List<int>? SelectedRoutineIds { get; set; } = new List<int>();

    public List<SelectListItem> AvailableMuscles { get; set; } = new();
    public List<SelectListItem> AvailableRoutines { get; set; } = new();
}