using Medical.Data.Repository;
using Medical.Models;

namespace Medical.Data.Interface
{
    public interface IUnitOfWork
    {
        public Task Save();

        public GenericRepository<Provider> ProviderRepository { get; }
        public GenericRepository<Doctor> DoctorRepository { get; }
        public GenericRepository<Appointment> AppointmentRepository { get; }
        public GenericRepository<Patient> PatientRepository { get; }
        public GenericRepository<Record> RecordRepository { get; }
    }
}
