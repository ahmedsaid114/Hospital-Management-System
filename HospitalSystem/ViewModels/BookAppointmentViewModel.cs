using System.ComponentModel.DataAnnotations;
using HospitalSystem.Models;

namespace HospitalSystem.ViewModels;

public class BookAppointmentViewModel
{
    [Required]
    public int DoctorId { get; set; }

    public Doctor? Doctor { get; set; }

    [Required(ErrorMessage = "Please enter the patient name.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
    [Display(Name = "Patient Name")]
    public string PatientName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please choose a date.")]
    [DataType(DataType.Date)]
    [Display(Name = "Date")]
    public DateTime? AppointmentDate { get; set; }

    [Required(ErrorMessage = "Please choose a time slot.")]
    [Display(Name = "Time")]
    public TimeSpan? AppointmentTime { get; set; }

    public IReadOnlyList<TimeSpan> TimeSlots { get; set; } = Array.Empty<TimeSpan>();
}
