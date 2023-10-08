using CarShopAPI.Models;

namespace CarShopAPI.Interfaces
{
    public interface IGetCategory
    {
        IEnumerable<Category> GetCategories { get; }
    }
}
