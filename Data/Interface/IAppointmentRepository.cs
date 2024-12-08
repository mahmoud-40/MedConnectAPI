using Medical.Models;

namespace Medical.Data.Interface;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<List<Appointment>> GetByDoctorId(int doctorId);
    Task<List<Appointment>> GetByPatientId(string patientId);
    Task<List<Appointment>> GetByProviderId(string providerId);
    Task<bool> IsTaken(int doctorId, DateOnly date, int time);
    Task<bool> IsTaken(string patientId, DateOnly date, int time);
}
