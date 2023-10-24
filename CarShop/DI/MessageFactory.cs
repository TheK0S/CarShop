using MimeKit;

namespace CarShop.DI
{
    public class MessageFactory
    {
        public MimeMessage Create(string stringMessage, string name, string email, string subject = "")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Konstantin", "turchakkonstantin@gmail.com"));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = stringMessage };
            return message;
        }
    }
}
