using CarShopAPI.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Mocks
{
    public class MockCategory : IGetCategory
    {
        public IEnumerable<Category> GetCategories { get => new List<Category>(); }
        
    }
}
