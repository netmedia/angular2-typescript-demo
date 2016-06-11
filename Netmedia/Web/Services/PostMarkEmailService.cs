using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Netmedia.Common.Extensions;
using PostmarkDotNet;
using PostmarkDotNet.Legacy;
using Netmedia.Infrastructure.Services;

namespace Netmedia.Web.Services
{
    public abstract class PostMarkEmailService : IEmailService
    {
        public string PostmarkApiKey { set { _postmarkApiKey = value; } }
        string _postmarkApiKey;

        public PostMarkEmailService() 
        { 
            _postmarkApiKey = string.Empty;
        }

        public PostMarkEmailService(string postmarkApiKey)
        {
            _postmarkApiKey = postmarkApiKey;
        }
        
        public string SendEmail(string from, string to, string cc, string bcc, string replyTo, string subject, string htmlBody)
        {
            try
            {
                if (_postmarkApiKey.IsNullOrEmpty())
                    throw new ArgumentException("Postmark API key should be provided!");

                var postmarkMessage = new PostmarkMessage
                {
                    From = from,
                    To = to,
                    Cc = cc,
                    Bcc = bcc,
                    ReplyTo = replyTo,
                    Subject = subject,
                    HtmlBody = htmlBody
                };

                var client = new PostmarkClient(_postmarkApiKey);
                var response = client.SendMessage(postmarkMessage);
            
                if(response.Status == PostmarkStatus.Success) return string.Empty;
            
                return response.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
