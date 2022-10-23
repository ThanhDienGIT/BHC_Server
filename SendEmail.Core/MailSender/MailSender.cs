using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Client;
using SendEmail.Core.Interface;
namespace SendEmail.Core.MailSender
{
    public abstract class MailSender : IMailSender
    {
        public static MailjetClient CreateMailJetV3Client()
        {
            return new MailjetClient("<API KEY>", "<SECRET KEY>");
        }
        protected abstract Task Send(EmailModel.EmailModel email);
        public Task SendEmail(string address, string subject, string text)
        {
            return SendEmail(new EmailModel.EmailModel { EmailAddress = address, Subject = subject, Body = text });
        }

        public Task SendEmail(EmailModel.EmailModel email)
        {
            return Send(email);
        }
    }
}
