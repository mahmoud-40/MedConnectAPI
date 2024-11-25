using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Medical.Models;

public class MedicalContext : IdentityDbContext
{
    public virtual DbSet<AppUser> AppUsers { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<Provider> Providers { get; set; }
    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<Doctor> Doctors { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<Record> Records { get; set; }

    public MedicalContext(DbContextOptions<MedicalContext> options) : base(options)
    {
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

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "Patient", NormalizedName = "PATIENT" });
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "Provider", NormalizedName = "PROVIDER" });
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" });
    }

}
