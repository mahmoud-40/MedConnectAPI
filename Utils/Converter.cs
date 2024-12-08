using Medical.DTOs.Doctors;
using Medical.DTOs.Patients;
using Medical.Models;

namespace Medical.Utils;
public interface IConverter
{
    int GetAge(DateOnly birthday);
    TimeSpan CalcDuration(DateTime Start, DateTime? End = null);
    TimeSpan CalcDuration(DateOnly Start, DateOnly? End = null);
    TimeSpan CalcDuration(TimeOnly Start, TimeOnly End);
    Tuple<TimeOnly, TimeOnly> GetShiftTime(Shift shift);
    TimeOnly CalcTime(int slot, Shift shift);
}
public class Converter : IConverter
{
    private readonly IConfiguration config;

    public Converter(IConfiguration config)
    {
        this.config = config;
    }

    public int GetAge(DateOnly birthday)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        int age = today.Year - birthday.Year;
        if (today.Month < birthday.Month || (today.Month == birthday.Month && today.Day < birthday.Day))
        {
            age--;
        }
        return age;
    }

    public TimeSpan CalcDuration(DateTime Start, DateTime? End = null)
    {
        DateTime effectiveEnd = End ?? DateTime.UtcNow;

        if (Start > effectiveEnd)
            throw new ArgumentException("Start date can't be earlier than End date");

        return effectiveEnd - Start;
    }

    public TimeSpan CalcDuration(DateOnly Start, DateOnly? End = null)
    {
        DateTime effectiveEnd = End?.ToDateTime(TimeOnly.MinValue) ?? DateTime.UtcNow;

        DateTime effectiveStart = Start.ToDateTime(TimeOnly.MinValue);

        if (Start > End)
            throw new ArgumentException("Start date can't be earlier than End date");

        return effectiveEnd - effectiveStart;
    }

    public TimeSpan CalcDuration(TimeOnly Start, TimeOnly End)
    {
        return End - Start;
    }

    public Tuple<TimeOnly, TimeOnly> GetShiftTime(Shift shift)
    {
        string timeJson = config.GetSection("WorkingHours").GetSection(shift.ToString()).Value ?? throw new ArgumentException("Shift not found in config");
        string[] times = timeJson.Split(" - ");
        TimeOnly startTime = ParseTime(times[0]);
        TimeOnly endTime = ParseTime(times[1]);
        return new Tuple<TimeOnly, TimeOnly>(startTime, endTime);
    }
    public TimeOnly CalcTime(int slot, Shift shift)
    {
        (TimeOnly startTime, TimeOnly endTime) = GetShiftTime(shift);
        int slotDuration = config.GetValue<int>("SlotDurationInMinutes");

        TimeOnly re = startTime.AddMinutes(slot * slotDuration);
        if (re > endTime)
            throw new ArgumentException("Slot is out of shift time");
        return re;
    }
    private TimeOnly ParseTime(string time)
    {
        return TimeOnly.ParseExact(time, "h:mm tt", null);
    }
}
