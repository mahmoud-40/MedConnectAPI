using Medical.Models;

namespace Medical.Data.Interface;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<List<Appointment>> GetAppointmentsByDoctorId(int doctorId);
    Task<List<Appointment>> GetAppointmentsByPatientId(string patientId);
    Task<List<Appointment>> GetAppointmentsByProviderId(string providerId);
    Task<bool> IsAppointmentTaken(int doctorId, DateOnly date, int time);
}
