using CarShop.Interfaces;
using CarShop.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CarShop.Services
{
    public class ShopCartService : IShopCartService
    {
        private readonly HttpClient httpClient;

        public ShopCartService()
        {
            httpClient = new HttpClient();
        }

        public async Task<BaseResponse<bool>> CreateShopCart(int userId)
        {
            if(userId <= 0)
                return new BaseResponse<bool> { StatusCode = HttpStatusCode.BadRequest, Data = false, Message = "Incorrect user Id" };

            //added user checking

            var shopCart = new ShopCart{ UserId = userId };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(shopCart), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{Api.apiUri}shopcart", content);

            return new BaseResponse<bool>()
            {
                StatusCode = response.StatusCode,
                Data = response.IsSuccessStatusCode
            };
        }

        public async Task<BaseResponse<ShopCart>> GetShopCart(int id)
        {
            var response = await httpClient.GetAsync($"{Api.apiUri}shopcart/{id}");

            var baseResponse = new BaseResponse<ShopCart>() { StatusCode = response.StatusCode };

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    baseResponse.Data = await response.Content.ReadFromJsonAsync<ShopCart>();
                }
            }
            catch (JsonException ex)
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
            }

            return baseResponse;
        }

        public async Task<BaseResponse<ShopCart>> GetShopCartByUserId(int userId)
        {
            var response = await httpClient.GetAsync($"{Api.apiUri}shopcart/user/{userId}");

            var baseResponse = new BaseResponse<ShopCart>() { StatusCode = response.StatusCode };

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    baseResponse.Data = await response.Content.ReadFromJsonAsync<ShopCart>();
                }
            }
            catch (JsonException ex)
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
            }

            return baseResponse;
        }

        public async Task<BaseResponse<bool>> AddItem(ShopCartItem item)
        {
            if(item == null)
                return new BaseResponse<bool> { StatusCode = HttpStatusCode.BadRequest, Data = false, Message = "Request not have a data" };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{Api.apiUri}shopcartItems", content);

            var baseResponse = new BaseResponse<bool>() 
            { 
                StatusCode = response.StatusCode,
                Data = response.IsSuccessStatusCode? true : false
            };

            return baseResponse;
        }

        public async Task<BaseResponse<bool>> RemoveItem(int itemId)
        {
            if (itemId <= 0)
                return new BaseResponse<bool> { StatusCode = HttpStatusCode.BadRequest, Data = false, Message = "Incorrect id" };

            var response = await httpClient.DeleteAsync($"{Api.apiUri}shopcartItems/{itemId}");

            var baseResponse = new BaseResponse<bool>()
            {
                StatusCode = response.StatusCode,
                Data = response.IsSuccessStatusCode
            };

            return baseResponse;
        }

        public async Task<BaseResponse<bool>> UpdateItem(int id, ShopCartItem item)
        {
            if(item == null || id != item.Id)
                return new BaseResponse<bool> { StatusCode = HttpStatusCode.BadRequest, Data = false, Message = "Request not have a data" };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{Api.apiUri}shopcart/{id}", content);

            var baseResponse = new BaseResponse<bool>()
            {
                StatusCode = response.StatusCode,
                Data = response.IsSuccessStatusCode
            };

            return baseResponse;
        }

        public async Task<BaseResponse<bool>> CreateOrder(int shopCartId)
        {
            if(shopCartId <= 0)
                return new BaseResponse<bool> { StatusCode = HttpStatusCode.BadRequest, Data = false, Message = "Incorrect id" };

            var shopCartResponse = await GetShopCart(shopCartId);
            var shopCart = shopCartResponse.Data;

            if(shopCart == null)
                return new BaseResponse<bool> { StatusCode = HttpStatusCode.BadRequest, Data = false, Message = "Incorrect id" };
            if(shopCart.Items?.Count == 0)
                return new BaseResponse<bool> { StatusCode = HttpStatusCode.BadRequest, Data = false, Message = "Cart is empty" };

            var order = new Order()
            {
                UserId = shopCart.UserId,
                DateTime = DateTime.Now,
                shopCartItems = shopCart.Items,
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{Api.apiUri}", content);
            if(!response.IsSuccessStatusCode)
                return new BaseResponse<bool> { StatusCode = response.StatusCode, Data = false };

            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = response.StatusCode,
                Message = "Order created"
            };
        }

        public async Task<BaseResponse<List<Order>>> GetUserOrders(int userId)
        {
            if(userId <= 0) 
                return new BaseResponse<List<Order>> { StatusCode = HttpStatusCode.BadRequest, Data = null , Message = "Incorrect userId" };

            var response = await httpClient.GetAsync($"{Api.apiUri}orders");
            if (!response.IsSuccessStatusCode)
                return new BaseResponse<List<Order>> { StatusCode = response.StatusCode, Data = null };

            var baseResponse = new BaseResponse<List<Order>>() { StatusCode = response.StatusCode };

            try
            {
                baseResponse.Data = await response.Content.ReadFromJsonAsync<List<Order>>();
            }
            catch (JsonException ex)
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
                baseResponse.Data = null;
            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
                baseResponse.Data = null;
            }

            return baseResponse;
        }
    }
}
