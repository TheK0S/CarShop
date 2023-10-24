using CarShop.Models;

namespace CarShop.Interfaces
{
    public interface IMessanger
    {
        void SendMessage(string message, User user, string title);
    }
}
