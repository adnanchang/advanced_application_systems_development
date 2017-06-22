using Newtonsoft.Json.Linq;
using SentimentAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace SentimentAnalysis.LogicServices
{
    public class BingSearchAPI
    {
        public void SearchKeywordInBing(Search Search, List<SearchResult> SearchResult)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "fd88ac234d0e4e56bc3b1ef8c984b71d");

            // Request parameters
            queryString["q"] = Search.keyword;
            queryString["count"] = "100";
            queryString["offset"] = "0";
            queryString["mkt"] = "en-us";
            queryString["safesearch"] = "Off";
            var uri = "https://api.cognitive.microsoft.com/bing/v5.0/search?" + queryString;

            var response = client.GetStringAsync(uri).Result;
            JObject parser = JObject.Parse(response);
            List<string> snippets = new List<string>();
            List<string> newsDescriptions = new List<string>();
            List<string> videoDescriptions = new List<string>();

            if (parser["webPages"] != null)
            {
                foreach (JToken item in parser["webPages"]["value"].Children())
                {
                    string snippet = item["snippet"].ToString();
                    snippets.Add(snippet);

                }
            }

            if (parser["news"] != null)
            {
                foreach (JToken item in parser["news"]["value"].Children())
                {
                    string newsDescription = item["description"].ToString();
                    newsDescriptions.Add(newsDescription);
                }
            }

            if (parser["videos"] != null)
            {
                foreach (JToken item in parser["videos"]["value"].Children())
                {
                    string videoDescription = item["description"].ToString();
                    videoDescriptions.Add(videoDescription);
                }
            }


            foreach (var item in snippets)
            {
                // snippetsCleaned.Add(RemoveSpecialCharacters(item));
                SearchResult sResult = new SearchResult();
                sResult.comment = RemoveSpecialCharacters(item);
                sResult.Search = Search;
                SearchResult.Add(sResult);
            }
            foreach (var item in newsDescriptions)
            {
                SearchResult sResult = new SearchResult();
                sResult.comment = RemoveSpecialCharacters(item);
                sResult.Search = Search;
                SearchResult.Add(sResult);
            }
            foreach (var item in videoDescriptions)
            {
                SearchResult sResult = new SearchResult();
                sResult.comment = RemoveSpecialCharacters(item);
                sResult.Search = Search;
                SearchResult.Add(sResult);
            }
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}