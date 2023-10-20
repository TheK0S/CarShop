namespace CarShop.Models
{
    public class CategoryService
    {
        public async Task<List<Category>> GetCategoriesAsync()
        {
            HttpClient httpClient = new HttpClient();
            var categories = await httpClient.GetFromJsonAsync<IEnumerable<Category>>($"{Api.apiUri}category");

            return categories?.ToList() ?? new List<Category>();
        }
    }
}
