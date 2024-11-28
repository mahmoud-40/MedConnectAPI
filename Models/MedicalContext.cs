using Medical.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Medical.Utils;

namespace Medical.Models;

public class MedicalContext : IdentityDbContext
{
    private readonly IConfiguration config;

    public virtual DbSet<AppUser> AppUsers { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<Provider> Providers { get; set; }
    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<Doctor> Doctors { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<Record> Records { get; set; }

    public MedicalContext(DbContextOptions<MedicalContext> options, IConfiguration _config) : base(options)
    {
        config = _config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Provider)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Record)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.RecordId)
            .OnDelete(DeleteBehavior.Restrict);

        #region Seed Roles
        List<IdentityRole> roles = new List<IdentityRole>()
        {
            new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole() { Name = "Provider", NormalizedName = "PROVIDER" },
            new IdentityRole() { Name = "Patient", NormalizedName = "PATIENT" }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);
        #endregion

        #region Seed Admins
        List<AdminConfig>? adminsConfig = config.GetSection("Admins").Get<List<AdminConfig>>();
        PasswordHasher<Admin> hasher = new PasswordHasher<Admin>();
        if (adminsConfig != null && adminsConfig.Any())
        {
            List<Admin> admins = adminsConfig.Select(config => new Admin
            {
                Name = config.Name,
                UserName = config.UserName,
                NormalizedUserName = config.UserName.ToUpper(),
                Email = config.Email,
                NormalizedEmail = config.Email.ToUpper(),
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = hasher.HashPassword(new Admin(), config.Password)
            }).ToList();

            modelBuilder.Entity<Admin>().HasData(admins);

            List<IdentityUserRole<string>> userRoles = admins.Select(admin => new IdentityUserRole<string>
            {
                UserId = admin.Id,
                RoleId = roles[0].Id
            }).ToList();

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
        #endregion

        PasswordHasher<Patient> hasher1 = new PasswordHasher<Patient>();

        #region Test Patient
        //Patient patientTest = new Patient()
        //{
        //    Name = "Patient1",
        //    UserName = "Joo",
        //    NormalizedUserName = "JOO",
        //    Email = "yuossef@gmail.com",
        //    NormalizedEmail = "YUOSSEF@GMAIL.COM",
        //    EmailConfirmed = true,
        //    CreatedAt = DateTime.UtcNow,
        //    BirthDay = new DateOnly(2000, 1, 1),
        //    Address = "Egypt",
        //    Gender = Gender.Male,
        //    PasswordHash = hasher1.HashPassword(new Patient(), "asdASD123!@#")
        //};

        //modelBuilder.Entity<Patient>().HasData(patientTest);
        //modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = patientTest.Id, RoleId = roles[2].Id });
        #endregion
    }
}
