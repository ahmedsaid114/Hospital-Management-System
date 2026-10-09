
using Microsoft.EntityFrameworkCore;
using HospitalSystem.Models;

namespace HospitalSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent double booking for the same doctor, date, and time.
            modelBuilder.Entity<Appointment>()
                .HasIndex(a => new
                {
                    a.DoctorId,
                    a.AppointmentDate,
                    a.AppointmentTime
                })
                .IsUnique();

            // Configure the relationship between appointments and doctors.
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed the initial doctors.
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    Name = "Dr. Ahmed Hassan",
                    Specialization = "Cardiology",
                    ImageUrl = "/images/doctors/doctor1.png"
                },
                new Doctor
                {
                    Id = 2,
                    Name = "Dr. Sara Mahmoud",
                    Specialization = "Dermatology",
                    ImageUrl = "/images/doctors/doctor2.png"
                },
                new Doctor
                {
                    Id = 3,
                    Name = "Dr. Omar Khaled",
                    Specialization = "Orthopedics",
                    ImageUrl = "/images/doctors/doctor3.png"
                },
                new Doctor
                {
                    Id = 4,
                    Name = "Dr. Mona Adel",
                    Specialization = "Pediatrics",
                    ImageUrl = "/images/doctors/doctor4.png"
                },
                new Doctor
                {
                    Id = 5,
                    Name = "Dr. Youssef Ali",
                    Specialization = "Neurology",
                    ImageUrl = "/images/doctors/doctor5.png"
                },
                new Doctor
                {
                    Id = 6,
                    Name = "Dr. Nour Ibrahim",
                    Specialization = "Ophthalmology",
                    ImageUrl = "/images/doctors/doctor6.png"
                },
                new Doctor
                {
                    Id = 7,
                    Name = "Dr. Karim Mostafa",
                    Specialization = "Cardiology",
                    ImageUrl = "/images/doctors/doctor7.png"
                },
                new Doctor
                {
                    Id = 8,
                    Name = "Dr. Laila Samir",
                    Specialization = "Pediatrics",
                    ImageUrl = "/images/doctors/doctor8.png"
                },
                new Doctor
                {
                    Id = 9,
                    Name = "Dr. Hany Fathy",
                    Specialization = "Dentistry",
                    ImageUrl = "/images/doctors/doctor3.png"
                },
                new Doctor
                {
                    Id = 10,
                    Name = "Dr. Rana Tarek",
                    Specialization = "Dermatology",
                    ImageUrl = "/images/doctors/doctor6.png"
                }
            );
        }
    }
}