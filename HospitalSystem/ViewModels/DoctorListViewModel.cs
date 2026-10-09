using HospitalSystem.Models;

namespace HospitalSystem.ViewModels;

public class DoctorListViewModel
{
    public List<Doctor> Doctors { get; set; } = new();
    public List<string> Specializations { get; set; } = new();

    public string? Name { get; set; }
    public string? Specialization { get; set; }

    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
}
