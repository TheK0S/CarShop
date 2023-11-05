using CarShop.Models;

namespace CarShop.Interfaces
{
    public interface IShopCartService
    {
        Task<BaseResponse<bool>> CreateShopCart(int userId);
        Task<BaseResponse<ShopCart>> GetShopCart(int id);
        Task<BaseResponse<ShopCart>> GetShopCartByUserId(int userId);
        Task<BaseResponse<bool>> AddItem(ShopCartItem item);
        Task<BaseResponse<bool>> RemoveItem(int itemId);
        Task<BaseResponse<bool>> UpdateItem(int id, ShopCartItem item);
        Task<BaseResponse<bool>> CreateOrder(int shopCartId);
        Task<BaseResponse<List<Order>>> GetUserOrders(int userId);
    }
}
