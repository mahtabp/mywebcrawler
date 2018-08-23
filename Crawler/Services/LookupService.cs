using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

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
            var searchResult = await googleHttpClient.GetSearchResultFromGoogle(searchKeyword);
            var parsedSearchResult = ParseResult(searchResult);
            var keywordLocation = GetKeyWordLocation(target, parsedSearchResult);
            if(keywordLocation.Count == 0) {
                return "0";
            }

            string result = "";
            foreach(var item in keywordLocation)
            {
                result += item.ToString() + " ";
            }

            return result.Trim();
        }

        private List<int> GetKeyWordLocation(string target, List<string> lookupList)
        {
            var locationList = new List<int>();
            for (int i = 0; i < lookupList.Count; i++) {
                var normalizedItem = lookupList[i].ToLower().Replace("<b>", "").Replace("</b>", "");
                if (normalizedItem.Contains(target.ToLower()))
                {
                    locationList.Add(i);
                }
            }
            return locationList;
        }

        private List<string> ParseResult(string value)
        {
            string pattern = "<cite.*?>(.*?)<\\/cite>";
            var resultList = new List<string>();

            Regex regex = new Regex(pattern, RegexOptions.Multiline);
            MatchCollection collection = regex.Matches(value);

            foreach (Match item in collection)
            {
                resultList.Add(item.Groups[1].Value);
            }

            return resultList;
        }
    }
}
