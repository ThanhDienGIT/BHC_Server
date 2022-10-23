using EmailSend.Core.Common.Email;
using EmailSend.Core.Common.EmailProvider;
using EmailSend.Core.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks.Dataflow;

namespace EmailSend.Core.HostServices
{
    public class EmailHostedService : IHostedService, IDisposable
    {

        private Task? _sendTask;
        private CancellationTokenSource? _cancellationToken;
        private readonly BufferBlock<EmailModel> _mailQueue;
        private readonly IEmailSender _mailSender; 

        public EmailHostedService()
        {
            _mailSender = new MailjetProvider();
            _mailQueue = new BufferBlock<EmailModel>();
            _cancellationToken = new CancellationTokenSource();
        }

        public async Task SendEmailAsync(EmailModel emaiModel) => await _mailQueue.SendAsync(emaiModel);

        public void Dispose()
        {
            DestroyTask();
        }

        private void DestroyTask()
        {
            try
            {
                if(_cancellationToken != null)
                {
                    _cancellationToken.Cancel();
                    _cancellationToken = null;
                }
                Console.WriteLine("DESTROY SERVICE");
            }
            catch { }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("START SERVICE");
            _sendTask = BackgroundSendEmailAsync(_cancellationToken!.Token);
            return Task.CompletedTask;
        }

        private async Task? BackgroundSendEmailAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var email = await _mailQueue.ReceiveAsync();
                    await _mailSender.SendEmail(email);
                }catch(OperationCanceledException) {
                    break;
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            DestroyTask();
            await Task.WhenAny(_sendTask!, Task.Delay(Timeout.Infinite,cancellationToken));
        }
    }
}
