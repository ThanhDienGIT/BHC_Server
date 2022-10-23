using EmailSend.Core.Common.Email;
using EmailSend.Core.Common.Email.EmailSender;
using EmailSend.Core.Interfaces;
using Mailjet.Client;
using Newtonsoft.Json.Linq;

namespace EmailSend.Core.Common.EmailProvider
{
    public class MailjetProvider : EmailSender, IEmailSender
    {
        protected override async Task Send(EmailModel email)
        {
            try
            {
                JArray jArray = new JArray();
                JArray attachments = new JArray();
                if (email.Attachments != null && email.Attachments.Count() > 0)
                {
                    email.Attachments.ToList().ForEach(attachment => attachments.Add(
                        new JObject
                        {
                            new JProperty("Content-Type",attachment.ContentType),
                            new JProperty("Filename",attachment.Name),
                            new JProperty("Content",Convert.ToBase64String(attachment.Data))
                        }));
                }
                jArray.Add(new JObject
                {
                    new JProperty("FromEmail","thanhdiensett@gmail.com"),
                    new JProperty("FromName","BookingHealthCare"),
                    new JProperty("Recipients",new JArray
                    {
                        new JObject
                        {
                            new JProperty("Email",email.EmailAddress),
                            new JProperty("Name",email.EmailAddress)

                        }
                    }),
                    new JProperty("Subject",email.Subject),
                    new JProperty("Text-part",email.Body),
                    new JProperty("Html-part",email.Body),
                    new JProperty("Attachments",attachments)
                });
                var client = EmailSender.CreateMailJetClient();
                var request = new MailjetRequest
                {
                    Resource = Mailjet.Client.Resources.Send.Resource
                }
                .Property(Mailjet.Client.Resources.Send.Messages, jArray);

                var reponse = await client.PostAsync(request);
                Console.WriteLine($"Send Result {reponse.StatusCode} with Message :{reponse.Content}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
            }
        }
    }
}
