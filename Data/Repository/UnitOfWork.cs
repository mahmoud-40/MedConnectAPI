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

    GenericRepository<Provider>? providerRepository;
    GenericRepository<Doctor>? doctorRepository;
    GenericRepository<Appointment>? appointmentRepository;
    GenericRepository<Patient>? patientRepository;
    GenericRepository<Record>? recordRepository;

    public GenericRepository<Provider> ProviderRepository
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

    public GenericRepository<Doctor> DoctorRepository
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

    public GenericRepository<Appointment> AppointmentRepository
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

    public GenericRepository<Patient> PatientRepository
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

    public GenericRepository<Record> RecordRepository
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