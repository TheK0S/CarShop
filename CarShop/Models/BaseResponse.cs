using System.Net;

namespace CarShop.Models
{
    public class BaseResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
