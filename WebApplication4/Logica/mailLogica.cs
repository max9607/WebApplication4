using System.Net;
using System.Net.Mail;

namespace WebApplication4.Logica
{
    public class mailLogica
    {
        public Task SendEmailAsync(List<string?> email)
        {
            for(int i = 0; i < email.Count; i++)
            {
                try
                {
                    // Credentials
                    var credentials = new NetworkCredential("servicesdesk39@gmail.com", "Papirico99");
                    // Mail message
                    var mail = new MailMessage()
                    {
                        From = new MailAddress("servicesdesk39@gmail.com", "ServiceDesk"),
                        Subject = "TITULO DEL CORREO",
                        Body = "MENSAJE",
                        IsBodyHtml = true
                    };

                    mail.To.Add(new MailAddress("carlosgtmam@gmail.com"));

                    // Smtp client
                    var client = new SmtpClient()
                    {
                        Port = 465,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Host = "smtp-relay.gmail.com",
                        EnableSsl = true,
                        Credentials = credentials
                    };

                    // Send it...         
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    // TODO: handle exception
                    throw new InvalidOperationException(ex.Message);
                }
     
            }

            return Task.CompletedTask;
        }

    }
}
