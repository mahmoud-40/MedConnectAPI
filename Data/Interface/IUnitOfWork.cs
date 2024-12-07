using Medical.Models;

namespace Medical.Data.Interface;

public interface IUnitOfWork
{
    public Task Save();

    public IGenericRepository<Provider> ProviderRepository { get; }
    public IGenericRepository<Doctor> DoctorRepository { get; }
    public IAppointmentRepository AppointmentRepository { get; }
    public INotificationRepository NotificationRepository { get; }
    public IGenericRepository<Patient> PatientRepository { get; }
    public IRecordRepository RecordRepository { get; }
}
