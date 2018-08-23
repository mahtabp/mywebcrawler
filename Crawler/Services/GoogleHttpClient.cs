using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crawler.Services
{
    public interface IGoogleHttpClient
    {
        Task<string> GetSearchResultFromGoogle(string searchKeyword);
    }

    public class GoogleHttpClient: IGoogleHttpClient
    {
        public async Task<string> GetSearchResultFromGoogle(string searchKeyword) 
        {
            var normalizedKeyword = $"q={searchKeyword.ToLower().Replace(" ", "+")}";
            var maxResultNum = $"num=100";

            string searchUrl = $"http://www.google.com/search?{normalizedKeyword}&{maxResultNum}";
            using(HttpClient client = new HttpClient()){
                var response = await client.GetAsync(searchUrl);
                if(response.IsSuccessStatusCode) 
                {
                    return await response.Content.ReadAsStringAsync();
                }
                // TODO: 
                throw new Exception("ERROR");
            }
        }
    }
}
