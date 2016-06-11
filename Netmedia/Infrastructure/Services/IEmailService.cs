using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Netmedia.Infrastructure.Services
{
    public interface IEmailService
    {
        string SendEmail(string from, string to, string cc, string bcc, string replyTo, string subject, string htmlBody);
    }
}
