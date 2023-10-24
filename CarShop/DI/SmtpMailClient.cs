using MailKit.Net.Smtp;
using MimeKit;

namespace CarShop.DI
{
    public class SmtpMailClient
    {
        private readonly SmtpSettings _settings;
        
        public SmtpMailClient(SmtpSettings settings)
        {
            _settings = settings;
        }

        internal bool Send(MimeMessage message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(_settings.Host, _settings.Port, false);
                    client.Authenticate(_settings.Login, _settings.Password);

                    client.Send(message);
                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
