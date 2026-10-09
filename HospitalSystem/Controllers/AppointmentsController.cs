using HospitalSystem.Data;
using HospitalSystem.Helpers;
using HospitalSystem.Models;
using HospitalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Controllers;

public class AppointmentsController : Controller
{
    private readonly AppDbContext _db;

    public AppointmentsController(AppDbContext db) => _db = db;

    // GET: /Appointments
    public async Task<IActionResult> Index()
    {
        var appointments = await _db.Appointments
            .AsNoTracking()
            .Include(a => a.Doctor)
            .OrderBy(a => a.AppointmentDate)
            .ThenBy(a => a.AppointmentTime)
            .ToListAsync();

        return View(appointments);
    }

    // GET: /Appointments/Book/5
    public async Task<IActionResult> Book(int id)
    {
        var doctor = await _db.Doctors.FindAsync(id);
        if (doctor is null) return NotFound();

        return View(new BookAppointmentViewModel
        {
            DoctorId = doctor.Id,
            Doctor = doctor,
            TimeSlots = ScheduleRules.TimeSlots
        });
    }

    // POST: /Appointments/Book
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(BookAppointmentViewModel model)
    {
        var doctor = await _db.Doctors.FindAsync(model.DoctorId);
        if (doctor is null) return NotFound();

        if (model.AppointmentDate is { } date)
        {
            if (date.Date < DateTime.Today)
                ModelState.AddModelError(nameof(model.AppointmentDate), "The date cannot be in the past.");
            else if (!ScheduleRules.IsWorkingDay(date))
                ModelState.AddModelError(nameof(model.AppointmentDate), "Appointments are available Sunday to Thursday only.");
        }

        if (model.AppointmentTime is { } time && !ScheduleRules.IsValidSlot(time))
            ModelState.AddModelError(nameof(model.AppointmentTime), "Please choose a valid time slot (9:00 AM – 4:30 PM).");

        if (ModelState.IsValid)
        {
            var slotTaken = await _db.Appointments.AnyAsync(a =>
                a.DoctorId == model.DoctorId &&
                a.AppointmentDate == model.AppointmentDate!.Value.Date &&
                a.AppointmentTime == model.AppointmentTime!.Value);

            if (slotTaken)
                ModelState.AddModelError(nameof(model.AppointmentTime),
                    $"{doctor.Name} is already booked at this time. Please choose another slot.");
        }

        if (!ModelState.IsValid)
        {
            model.Doctor = doctor;
            model.TimeSlots = ScheduleRules.TimeSlots;
            return View(model);
        }

        _db.Appointments.Add(new Appointment
        {
            DoctorId = model.DoctorId,
            PatientName = model.PatientName.Trim(),
            AppointmentDate = model.AppointmentDate!.Value.Date,
            AppointmentTime = model.AppointmentTime!.Value
        });

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            // Unique index caught a race condition between two simultaneous bookings.
            ModelState.AddModelError(nameof(model.AppointmentTime), "This slot was just booked. Please choose another time.");
            model.Doctor = doctor;
            model.TimeSlots = ScheduleRules.TimeSlots;
            return View(model);
        }

        TempData["Success"] = $"Appointment booked with {doctor.Name} on {model.AppointmentDate:dddd, dd/MM/yyyy} at {ScheduleRules.Format(model.AppointmentTime!.Value)}.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Appointments/BookedSlots?doctorId=1&date=2026-03-22
    // Used by the booking form to disable already-taken time slots.
    [HttpGet]
    public async Task<IActionResult> BookedSlots(int doctorId, DateTime date)
    {
        var times = await _db.Appointments
            .Where(a => a.DoctorId == doctorId && a.AppointmentDate == date.Date)
            .Select(a => a.AppointmentTime)
            .ToListAsync();

        return Json(times.Select(t => t.ToString(@"hh\:mm")));
    }
}
