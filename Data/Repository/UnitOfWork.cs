using Medical.Data.Interface;
using Medical.Data.Repository;
using Medical.Models;

namespace Medical.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private MedicalContext db;

    public UnitOfWork(MedicalContext db)
    {
        this.db = db;
    }

    IGenericRepository<Provider>? providerRepository;
    IGenericRepository<Doctor>? doctorRepository;
    IGenericRepository<Appointment>? appointmentRepository;
    INotificationRepository? notificationRepository;
    IGenericRepository<Patient>? patientRepository;
    IGenericRepository<Record>? recordRepository;

    public IGenericRepository<Provider> ProviderRepository
    {
        get
        {
            if (this.providerRepository == null)
            {
                this.providerRepository = new GenericRepository<Provider>(db);
            }
            return providerRepository;
        }
    }

    public IGenericRepository<Doctor> DoctorRepository
    {
        get
        {
            if (this.doctorRepository == null)
            {
                this.doctorRepository = new GenericRepository<Doctor>(db);
            }
            return doctorRepository;
        }
    }

    public IGenericRepository<Appointment> AppointmentRepository
    {
        get
        {
            if (this.appointmentRepository == null)
            {
                this.appointmentRepository = new GenericRepository<Appointment>(db);
            }
            return appointmentRepository;
        }
    }

    public INotificationRepository NotificationRepository
    {
        get
        {
            if (this.notificationRepository == null)
            {
                this.notificationRepository = new NotificationRepository(db);
            }
            return notificationRepository;
        }
    }
            
    public IGenericRepository<Patient> PatientRepository
    {
        get
        {
            if (this.patientRepository == null)
            {
                this.patientRepository = new GenericRepository<Patient>(db);
            }
            return patientRepository;
        }
    }

    public IGenericRepository<Record> RecordRepository
    {
        get
        {
            if (this.recordRepository == null)
            {
                this.recordRepository = new GenericRepository<Record>(db);
            }
            return recordRepository;
        }
    }

    // save
    public async Task Save()
    {
        await db.SaveChangesAsync();
    }
}
