using Medical.Data.Interface;
using Medical.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical.Data.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly MedicalContext db;

        public AppointmentRepository(MedicalContext _db) : base(_db)
        {
            db = _db;
        }
        public async Task<List<Appointment>> GetByDoctorId(int doctorId)
        {
            return await db.Appointments.Where(e => e.DoctorId == doctorId).ToListAsync();
        }

        public async Task<List<Appointment>> GetByPatientId(string patientId)
        {
            return await db.Appointments.Where(e => e.PatientId == patientId).ToListAsync();
        }

        public async Task<List<Appointment>> GetByProviderId(string providerId)
        {
            return await db.Doctors.Where(e => e.ProviderId == providerId).SelectMany(e => e.Appointments).ToListAsync();
        }

        public async Task<bool> IsTaken(int doctorId, DateOnly date, int time)
        {
            return await db.Appointments.AnyAsync(e => e.DoctorId == doctorId && e.Date == date && e.Time == time);
        }

        public async Task<bool> IsTaken(string patientId, DateOnly date, int time)
        {
            return await db.Appointments.AnyAsync(e => e.PatientId == patientId && e.Date == date && e.Time == time);
        }
    }
}
