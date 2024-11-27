namespace Medical.Utils;

public interface IValidator
{
    bool IsBirthdayValid(DateOnly date, out Exception exception);
}
public class Validator : IValidator
{
    private readonly IConverter converter;

    public Validator(IConverter converter)
    {
        this.converter = converter;
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
}
