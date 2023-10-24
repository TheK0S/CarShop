using MailKit.Net.Smtp;

namespace CarShop.DI
{
    public class SmtpSettings
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
