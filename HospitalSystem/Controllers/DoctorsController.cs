using HospitalSystem.Data;
using HospitalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Controllers;

public class DoctorsController : Controller
{
    private const int PageSize = 4;
    private readonly AppDbContext _db;

    public DoctorsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? name, string? specialization, int page = 1)
    {
        var query = _db.Doctors.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            var term = name.Trim().ToLower();
            query = query.Where(d => d.Name.ToLower().Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(specialization))
            query = query.Where(d => d.Specialization == specialization);

        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var doctors = await query
            .OrderBy(d => d.Name)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        var specializations = await _db.Doctors
            .Select(d => d.Specialization)
            .Distinct()
            .OrderBy(s => s)
            .ToListAsync();

        return View(new DoctorListViewModel
        {
            Doctors = doctors,
            Specializations = specializations,
            Name = name,
            Specialization = specialization,
            Page = page,
            TotalPages = totalPages,
            TotalCount = totalCount
        });
    }
}
