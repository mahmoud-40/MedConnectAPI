using Castle.Core.Smtp;
using Medical.Data.Interface;
using System.Net;
using System.Net.Mail;

namespace Medical.Data.Repository
{
    public class EmailService : IEmailService
    {
        //public void Send(string from, string to, string subject, string messageText)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Send(MailMessage message)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Send(IEnumerable<MailMessage> messages)
        //{
        //    throw new NotImplementedException();
        //}

        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> logger;

        public EmailService(IConfiguration configuration,ILogger<EmailService> logger)
        {
            _configuration = configuration;
            this.logger = logger;
        }

        //public async void Mail(string sender, string sub, string body)
        //{
        //    var smtpSettings = _configuration.GetSection("SmtpSettings");

        //    SmtpClient smtp = new SmtpClient(smtpSettings["Server"])
        //    {
        //        EnableSsl = bool.Parse(smtpSettings["EnableSsl"]),
        //        Port = int.Parse(smtpSettings["Port"]),
        //        Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"])
        //    };

        //    smtp.Send(smtpSettings["Username"], sender, sub, body);
           
        //}

       

        public async Task SendEmailAsync(string sender, string subject, string body)
        {

            try
            {
                IConfigurationSection? smtpSettings = _configuration.GetSection("SmtpSettings");

                MailMessage? mailMessage = new MailMessage(smtpSettings["Username"]!, sender, subject, body)
                {
                    IsBodyHtml = true
                };

                SmtpClient smtp = new SmtpClient(smtpSettings["Server"])
                {
                    EnableSsl = bool.Parse(smtpSettings["EnableSsl"]!),
                    Port = int.Parse(smtpSettings["Port"]!),
                    Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"])
                };

                await smtp.SendMailAsync(mailMessage);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }



    }
}
