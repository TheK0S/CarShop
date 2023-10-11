namespace CarShop
{
    public static class Api
    {
        public static async Task<HttpResponseMessage> GetApiResponse(string path)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync("https://localhost:7294/api/" + path);
            }
            return response;
        }
    }
}
