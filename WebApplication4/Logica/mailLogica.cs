﻿using System.Net;
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
                    var credentials = new NetworkCredential("servicedesk_notif@outlook.com", "MMOrpg77");
                    // Mail message
                    var mail = new MailMessage()
                    {
                        From = new MailAddress("servicedesk_notif@outlook.com", "ServiceDesk"),
                        Subject = "NUEVO TICKET",
                        Body = "Hay un nuevo ticket a la espera de ser atendido",
                        IsBodyHtml = true
                    };

                    mail.To.Add(new MailAddress(email[i]));

                    // Smtp client
                    var client = new SmtpClient()
                    {
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
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
     
            }

            return Task.CompletedTask;
        }


        public Task emailpruebas()
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
                        UseDefaultCredentials = false,
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
        }
    }
}