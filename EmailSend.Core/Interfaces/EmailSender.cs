using EmailSend.Core.Common.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSend.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmail(string address,string subject,string body,List<EmailAttachment>? emailAttachments = null);

        Task SendEmail(EmailModel emailModel);

    }
}
