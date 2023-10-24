using CarShop.Interfaces;
using CarShop.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace CarShop.DI
{
    public class Messanger : IMessanger
    {
        private readonly SmtpClient _client;
        private readonly MessageFactory _factory;

        public Messanger(SmtpClient client, MessageFactory factory)
        {
            _client = client;
            _factory = factory;
        }

        public void SendMessage(string message, User user, string title = "")
        {
             MimeMessage mimeMessage = _factory.Create(message, user.UserName, user.Email, title);
            _client.Send(mimeMessage);
        }
    }
}
