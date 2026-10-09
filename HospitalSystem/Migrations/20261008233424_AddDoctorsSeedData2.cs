using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorsSeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments");

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "ImageUrl", "Name", "Specialization" },
                values: new object[,]
                {
                    { 1, "/images/doctors/doctor1.png", "Dr. Ahmed Hassan", "Cardiology" },
                    { 2, "/images/doctors/doctor2.png", "Dr. Sara Mahmoud", "Dermatology" },
                    { 3, "/images/doctors/doctor3.png", "Dr. Omar Khaled", "Orthopedics" },
                    { 4, "/images/doctors/doctor4.png", "Dr. Mona Adel", "Pediatrics" },
                    { 5, "/images/doctors/doctor5.png", "Dr. Youssef Ali", "Neurology" },
                    { 6, "/images/doctors/doctor6.png", "Dr. Nour Ibrahim", "Ophthalmology" },
                    { 7, "/images/doctors/doctor7.png", "Dr. Karim Mostafa", "Cardiology" },
                    { 8, "/images/doctors/doctor8.png", "Dr. Laila Samir", "Pediatrics" },
                    { 9, "/images/doctors/doctor3.png", "Dr. Hany Fathy", "Dentistry" },
                    { 10, "/images/doctors/doctor6.png", "Dr. Rana Tarek", "Dermatology" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId_AppointmentDate_AppointmentTime",
                table: "Appointments",
                columns: new[] { "DoctorId", "AppointmentDate", "AppointmentTime" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId_AppointmentDate_AppointmentTime",
                table: "Appointments");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");
        }
    }
}
