using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendEmail.Core.EmailModel;
namespace SendEmail.Core.Interface
{
    public interface IMailSender
    {
        Task SendEmail(string address, string subject, string text);
        Task SendEmail(EmailModel email);
    }
}
