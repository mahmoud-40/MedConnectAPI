using Medical.Data.Interface;
using Medical.Models;

namespace Medical.Utils;

public interface IValidator
{
    bool IsBirthdayValid(DateOnly date, out Exception exception);
    bool IsAppointmentValid(Appointment appointment, out Exception exception);
}
public class Validator : IValidator
{
    private readonly IConverter converter;
    private readonly IUnitOfWork unit;
    private readonly IConfiguration config;

    public Validator(IConverter converter, IUnitOfWork unit, IConfiguration config)
    {
        this.converter = converter;
        this.unit = unit;
        this.config = config;
    }

    public bool IsBirthdayValid(DateOnly date, out Exception exception)
    {
        exception = new Exception();
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        if (date > today)
        {
            exception = new ArgumentException("Date is in the Future");
            return false;
        }

        int age = converter.GetAge(date);

        if (age < 18)
        {
            exception = new ArgumentException("You must be at least 18 years old");
            return false;
        }

        return true;
    }

    //TODO: Add Doctor Working Days and Holidays and split the SlotDuration to every Provider Instead of Global
    public bool IsAppointmentValid(Appointment appointment, out Exception exception)
    {
        exception = new Exception();
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        if (appointment.Date < today)
        {
            exception = new ArgumentException("Date is in the Past");
            return false;
        }
        if (appointment.Date > today.AddDays(30))
        {
            exception = new ArgumentException("Can't book an appointment more than 30 days in advance");
            return false;
        }
        if (appointment.Time < 0 )
        {
            exception = new ArgumentException("Time can't be negative");
            return false;
        }
        (TimeOnly startTime, TimeOnly endTime) = converter.GetShiftTime(appointment.Doctor!.Provider!.Shift);
        int slotDuration = config.GetValue<int>("SlotDurationInMinutes");
        int slots = (int)(endTime - startTime).TotalMinutes / slotDuration;
        if (appointment.Time > slots)
        {
            exception = new ArgumentException($"Time is out of working hours ({appointment.Doctor!.Provider!.Shift}) From {startTime} to {endTime}");
            return false;
        }
        
        bool IsTaken = unit.AppointmentRepository.IsTaken(appointment.DoctorId, appointment.Date, appointment.Time).Result;
        if (IsTaken)
        {
            exception = new ArgumentException("This appointment is already taken");
            return false;
        }

        IsTaken = unit.AppointmentRepository.IsTaken(appointment.PatientId, appointment.Date, appointment.Time).Result;
        if (IsTaken)
        {
            exception = new ArgumentException("You already have an appointment at this time");
            return false;
        }

        return true;
    }
}
