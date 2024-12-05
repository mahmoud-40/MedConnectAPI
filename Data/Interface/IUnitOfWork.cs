using Medical.Data.Repository;
using Medical.Models;

namespace Medical.Data.Interface;

public interface IUnitOfWork
{
    public Task Save();

    public IGenericRepository<Provider> ProviderRepository { get; }
    public IGenericRepository<Doctor> DoctorRepository { get; }
    public IGenericRepository<Appointment> AppointmentRepository { get; }
    public INotificationRepository NotificationRepository { get; }
    public IGenericRepository<Patient> PatientRepository { get; }
    public IGenericRepository<Record> RecordRepository { get; }
}
