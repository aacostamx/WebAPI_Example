using mxm.biz.Entities;
using System;
using System.Net;
using System.Net.Mail;

namespace mxm.biz.Servicies
{
    public class EmailService : IEmailService
    {
        public void SendEmail(Email email)
        {
            try
            {
                new SmtpClient
                {
                    Host = "Smtp.Gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("comunidad.mxm@gmail.com", "Admin00$")
                }.Send(new MailMessage
                {
                    From = new MailAddress("no-reply@techo.org", "Mas por Mexico"),
                    To = { email.To },
                    Subject = email.Subject,
                    IsBodyHtml = email.IsBodyHtml,
                    Body = email.Body
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
