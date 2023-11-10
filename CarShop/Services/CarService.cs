using Azure;
using CarShop.Interfaces;
using CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace CarShop.Services
{
    public class CarService : ICarService
    {
        HttpClient httpClient = new HttpClient();

        public async Task<BaseResponse<IEnumerable<Car>>> GetCarsAsync()
        {
            var response = await httpClient.GetAsync($"{Api.apiUri}cars");

            var baseResponse = new BaseResponse<IEnumerable<Car>>() { StatusCode = response.StatusCode };

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    baseResponse.Data = await response.Content.ReadFromJsonAsync<IEnumerable<Car>>();
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

        public async Task<BaseResponse<IEnumerable<Car>>> GetFavouriteCarsAsync()
        {
            var response = await httpClient.GetAsync($"{Api.apiUri}cars/favourite");
            var baseResponse = new BaseResponse<IEnumerable<Car>>() { StatusCode = response.StatusCode };

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    baseResponse.Data = await response.Content.ReadFromJsonAsync<IEnumerable<Car>>();
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

        public async Task<BaseResponse<List<Car>>> PostFilterredCarsAsync(CarsFilter filter)
        {
            var response = await httpClient.PostAsJsonAsync($"{Api.apiUri}cars/filtered", filter);
            var baseResponse = new BaseResponse<List<Car>>() { StatusCode = response.StatusCode };

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    baseResponse.Data = await response.Content.ReadFromJsonAsync<List<Car>>();
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

        public async Task<BaseResponse<Car>> GetCarAsync(int id)
        {
            var response = await httpClient.GetAsync($"{Api.apiUri}cars/{id}");

            var baseResponse = new BaseResponse<Car>() { StatusCode = response.StatusCode };

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    baseResponse.Data = await response.Content.ReadFromJsonAsync<Car>();
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

        public async Task<BaseResponse<bool>> CreateCarAsync(Car car)
        {
            var response = await httpClient.PostAsJsonAsync($"{Api.apiUri}cars", car);

            var baseResponse = new BaseResponse<bool>()
            {
                Data = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode
            };

            return baseResponse;
        }

        public async Task<BaseResponse<bool>> UpdateCarAsync(Car car)
        {
            var response = await httpClient.PutAsJsonAsync($"{Api.apiUri}cars/{car.Id}", car);

            var baseResponse = new BaseResponse<bool>()
            {
                Data = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode
            };

            return baseResponse;
        }

        public async Task<BaseResponse<bool>> DeleteCarAsync(int carId)
        {
            var response = await httpClient.DeleteAsync($"{Api.apiUri}cars/{carId}");

            var baseResponse = new BaseResponse<bool>()
            {
                Data = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode
            };

            return baseResponse;
        }
    }
}
