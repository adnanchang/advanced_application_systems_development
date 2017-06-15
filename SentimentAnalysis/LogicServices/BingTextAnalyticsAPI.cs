using Newtonsoft.Json.Linq;
using SentimentAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace SentimentAnalysis.LogicServices
{
    public class BingTextAnalyticsAPI
    {
        public void Sentiment(List<SearchResult> SearchResult, List<SearchScores> SearchScores)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "6b35e11b79b84404b6f3508910c11434");

            var uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment?" + queryString;

            HttpResponseMessage response;
            string theContent;

            //string body = "{\"documents\": [ { \"language\": \"en\", \"id\": \"Lala\", \"text\": \""+ snippets[0].ToString() +"\" } ]}";
            string body = "{\"documents\": [";
            SearchResult resultLastItem = SearchResult[SearchResult.Count - 1];
            int count = 0;

            foreach (var item in SearchResult)
            {
                body += "{ \"language\": \"en\", \"id\": \"" + count + "\", \"text\": \"" + item.comment + "\" }";
                count++;
                if (item != resultLastItem)
                {
                    body += ",";
                }
            }


            body += "]}";
            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes(body);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = client.PostAsync(uri, content).Result;
                theContent = response.Content.ReadAsStringAsync().Result;
            }

            JObject parser = JObject.Parse(theContent);
            count = 0; // restarting count to access the search result
            foreach (JToken item in parser["documents"].Children())
            {
                double dScore = Convert.ToDouble(item["score"].ToString());
                dScore = dScore * 100;
                SearchScores sScore = new Models.SearchScores();
                sScore.score = dScore;
                sScore.SearchResult = SearchResult[count];
                count++;
                SearchScores.Add(sScore);
            }
        }
    }
}