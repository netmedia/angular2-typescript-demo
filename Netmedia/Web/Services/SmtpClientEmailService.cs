using System;
using System.Net;
using System.Net.Mail;
using Netmedia.Common.Extensions;
using Netmedia.Infrastructure.Services;

namespace Netmedia.Web.Services
{
    public abstract class SmtpClientEmailService : IEmailService
    {
        public string SmtpServer { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }

        public string SendEmail(string from, string to, string cc, string bcc, string replyTo, string subject, string htmlBody)
        {
            try
            {
                var mailMessage = new MailMessage();
                mailMessage.Subject = subject;
                mailMessage.Body = htmlBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress(from);
                mailMessage.To.Add(to);
        
                if (cc.IsNotNullOrEmpty()) 
                    mailMessage.CC.Add(cc);
                
                if (bcc.IsNotNullOrEmpty()) 
                    mailMessage.Bcc.Add(bcc);
                
                if (replyTo.IsNotNullOrEmpty()) 
                    mailMessage.ReplyToList.Add(replyTo);


                var smtp = new SmtpClient();
                if (SmtpServer.IsNotNullOrEmpty()) smtp.Host = SmtpServer;
                if (SmtpPort != default(int)) smtp.Port = SmtpPort;
                if (SmtpUser.IsNotNullOrEmpty() && SmtpPassword.IsNotNullOrEmpty())
                    smtp.Credentials = new NetworkCredential(SmtpUser, SmtpPassword);


                smtp.EnableSsl = EnableSsl;

                smtp.Send(mailMessage);


                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}