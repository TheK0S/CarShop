using Microsoft.AspNetCore.Mvc;
using MimeKit;
using CarShop.Models;
using MailKit.Net.Smtp;

namespace CarShop.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RsvpForm(GuestResponse guest)
        {
            if (ModelState.IsValid)
            {
                string msgText = "Вы еще не определились придете или нет?";

                if (guest.WillAttend != null)
                    msgText = (bool)guest.WillAttend ?
                    "Ждем тебя на вечеринке. Начало в 16:00 30.09.23"
                    : "Жаль что ты не придешь. У нас будет весело!";

                try
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Konstantin", "turchakkonstantin@gmail.com"));
                    message.To.Add(new MailboxAddress(guest.Name, guest.Email));
                    message.Subject = "Вечеринка!!!";
                    message.Body = new TextPart("plain") { Text = msgText };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("turchakkonstantin@gmail.com", "jwjn owcn prki keuj");

                        client.Send(message);
                        client.Disconnect(true);
                    }

                    ViewBag.Msg = "На вашу почту отправлено уведомление";
                }
                catch (Exception ex)
                {
                    ViewBag.Msg = $"К сожалению при отправке письма возникла ошибка.\n{ex.Message}";
                }
                return View("Thanks", guest);
            }
            else
            {
                //Ошибка приоверки достоверности данных
                return View();
            }
        }
    }
}
