using EmailSend.Core.Interfaces;
using Mailjet.Client;

namespace EmailSend.Core.Common.Email.EmailSender
{
    public abstract class EmailSender : IEmailSender
    {
    

        public static MailjetClient CreateMailJetClient()
        {
            return new MailjetClient("95559afdc585d8c56b04d10cb971b546", "0d750660848fbdb315b2d4826523b4bf");
        }

      
        protected abstract Task Send(EmailModel email);

        public async Task SendEmail(EmailModel emailmodel)
        {
            await Send(emailmodel);
        }

        public async Task SendEmail(string address, string subject, string body, List<EmailAttachment>? emailAttachments = null)
        {
            await Send(new EmailModel 
            {
                Attachments = emailAttachments!,
                Body = body,
                EmailAddress = address,     
                Subject = subject,  
            });
        }
    }
}
