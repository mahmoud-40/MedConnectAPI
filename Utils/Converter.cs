using Medical.DTOs.Doctors;
using Medical.DTOs.Patients;
using Medical.Models;

namespace Medical.Utils;
public interface IConverter
{
    int GetAge(DateOnly birthday);
    TimeSpan CalcDuration(DateTime Start, DateTime? End = null);
    TimeSpan CalcDuration(DateOnly Start, DateOnly? End = null);
}
public class Converter : IConverter
{
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

}
