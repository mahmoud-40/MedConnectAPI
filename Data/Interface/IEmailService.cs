namespace Medical.Data.Interface;

public interface IEmailService
{

    public Task SendEmailAsync(string sender, string subject, string body);
}
