using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using Serilog;

namespace Crawler.Services
{
    public interface ILookupService
    {
        Task<string> GetOccurrence(string keyword, string target);
    }

    public class LookupService: ILookupService
    {
        IGoogleHttpClient googleHttpClient;

        public LookupService(IGoogleHttpClient googleHttpClient)
        {
            this.googleHttpClient = googleHttpClient;
        }

        public async Task<string> GetOccurrence(string searchKeyword, string target)
        {
            var searchResponseBody = await googleHttpClient.GetSearchResultFromGoogle(searchKeyword);
            var parsedSearchResponse = ParseResponseBody(searchResponseBody);
            var keywordLocation = GetKeyWordLocations(target, parsedSearchResponse);
            if(keywordLocation.Count == 0) {
                return "0";
            }

            string result = string.Join(", ", keywordLocation);
            return result.Trim();
        }

        private List<int> GetKeyWordLocations(string target, List<string> lookupList)
        {
            var locationList = new List<int>();
            for (int i = 0; i < lookupList.Count; i++) {
                if (lookupList[i].Contains(target.ToLower()))
                {
                    locationList.Add(i);
                }
            }
            return locationList;
        }

        private List<string> ParseResponseBody(string value)
        {
            string pattern = "<cite.*?>(.*?)<\\/cite>";
            var resultList = new List<string>();
            Regex regex = new Regex(pattern, RegexOptions.Multiline);

            MatchCollection collection = regex.Matches(value);

            foreach (Match item in collection)
            {
                string matchedItem;
                if(item.Groups.Count > 1) {
                    matchedItem = item.Groups[1].Value.NormalizeString();
                } else {
                    matchedItem = item.Groups[0].Value.NormalizeString();
                }
                
                resultList.Add(matchedItem);
            }

            return resultList;
        }
    }

}
