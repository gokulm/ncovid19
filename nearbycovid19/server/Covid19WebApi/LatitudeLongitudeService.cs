using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Covid19WebApi.DTOs;
using Microsoft.AspNetCore.WebUtilities;

namespace Covid19WebApi
{
    public class LatitudeLongitudeService
    {
        public async Task<LatitudeLongitudeResponse> Get(string address)
        {
            var queryParams = new Dictionary<string, string>()
            {
                {"format", "json" },
                {"q", address },
                {"accept-language","en" }
            };
            var finalUrl = QueryHelpers.AddQueryString("https://locationiq.com/v1/search_sandbox.php", queryParams);
            var httpClient = new HttpClient();
            var task = httpClient.GetStreamAsync(finalUrl);
            var locations = await JsonSerializer.DeserializeAsync<List<LatitudeLongitudeResponse>>(await task);
            var result = locations.FirstOrDefault(l => l.lat != null && l.lon != null);
            return result;
        }
    }
}
