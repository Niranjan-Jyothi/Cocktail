using Cocktails.Data.Data;
using System.Text.Json;

namespace Cocktails.Data.Implementation
{
    internal sealed class CocktailDataProvider : ICocktailDataProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string CocktailDbUrl = "https://www.thecocktaildb.com/api/json/v1/1/search.php?s={0}";

        public CocktailDataProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public IEnumerable<CocktailData> GetCockTailsBySearchKeyWord(string cocktailSearchKeyword)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = httpClient.GetAsync(string.Format(CocktailDbUrl, cocktailSearchKeyword)).Result;
            response.EnsureSuccessStatusCode();
            var responseString = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrWhiteSpace(responseString))
            {
                return Array.Empty<CocktailData>();
            }

            var cocktails = DeserializeCocktails(responseString);
            return cocktails;
        }

        private List<CocktailData> DeserializeCocktails(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            var cocktailResponse = JsonSerializer.Deserialize<CocktailResponse>(json, options);
            return cocktailResponse?.drinks ?? new List<CocktailData>();
        }

        private class CocktailResponse
        {
            public List<CocktailData> drinks { get; init; }
        }
    }
}
