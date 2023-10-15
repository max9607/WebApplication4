using System.Net;
using System.Net.Mail;

namespace WebApplication4.Logica
{
    public class mailLogica
    {
        public Task SendEmailAsync(List<string?> email,string asunto, string correoUsuario)
        {
            for(int i = 0; i < email.Count; i++)
            {
                try
                {
                    // Credentials
                    var credentials = new NetworkCredential("servicedesk@siibolivia.com", "Bolivia.2022");
                    //var credentials = new NetworkCredential("aldunatejustinianoanthonygabri@gmail.com", "Ranger1998.");
                    // Mail message
                    var mail = new MailMessage()
                    {
                        From = new MailAddress("servicedesk@siibolivia.com", "ServiceDesk"),
                        Subject = "NUEVO TICKET " + correoUsuario,
                        Body = "Hay un nuevo ticket a la espera de ser atendido, " +"\n asunto:"+ asunto,
                        IsBodyHtml = true
                    };

                    mail.To.Add(new MailAddress(email[i]));

                    // Smtp client
                    var client = new SmtpClient()
                    {
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Host = "smtp.gmail.com",
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


       /* public Task emailpruebas()
        {

                try
                {
                    // Credentials
                    var credentials = new NetworkCredential("servicedesk_notif@outlook.com", "MMOrpg77");
                    // Mail message
                    var mail = new MailMessage()
                    {
                        From = new MailAddress("servicedesk_notif@outlook.com", "ServiceDesk"),
                        Subject = "TITULO DEL CORREO",
                        Body = "MENSAJE",
                        IsBodyHtml = true
                    };

                    mail.To.Add(new MailAddress("carlosgtmam@gmail.com"));

                    // Smtp client
                    var client = new SmtpClient()
                    {
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = true,
                        Host = "smtp-mail.outlook.com",
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

            return Task.CompletedTask;
        }*/
    }
}
