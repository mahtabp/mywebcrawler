using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog;
using System.Text;

namespace Crawler.Services
{
    public interface IGoogleHttpClient
    {
        Task<string> GetSearchResultFromGoogle(string searchKeyword);
    }

    public class GoogleHttpClient: IGoogleHttpClient
    {
        const string SEARCH_ENGINE = "http://www.google.com.au/search";
        const int MAX_RESULT_NUM = 100;

        public async Task<string> GetSearchResultFromGoogle(string searchKeyword) 
        {
            var normalizedKeyword = $"q={searchKeyword.ToLower().Replace(" ", "+")}";

            try {
                string searchUrl = $"{SEARCH_ENGINE}?num={MAX_RESULT_NUM}&{normalizedKeyword}";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(searchUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    Log.Error($"Http client failed to Get reponse: {response.StatusCode} - {response.Content}");
                    return string.Empty;
                }
            }
            catch (Exception ex) {
                throw new Exception($"Failed to query google: {ex.Message}");
            }
        }

    }
}
