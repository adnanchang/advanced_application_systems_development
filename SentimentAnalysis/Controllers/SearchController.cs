using Newtonsoft.Json.Linq;
using SentimentAnalysis.Context;
using SentimentAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SentimentAnalysis.Controllers
{
    public class SearchController : Controller
    {
        private SearchContext db = new SearchContext();

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        // GET: Search/Details/5
        
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Search/Create
        [Route("/create")]
        [Route("")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(Search Search)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    List<SearchResult> SearchResult = new List<Models.SearchResult>();
                    SearchKeywordInBing(Search, SearchResult);
                    Search.searchDate = DateTime.Now;
                    //Search.searchResults = SearchResult;
                    db.Search.Add(Search);
                    db.SaveChanges();
                    TempData["SearchResult"] = SearchResult;
                    return RedirectToAction("Create", "SearchResult", new { area = "" });
                }

                return View(Search);
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Sentiment Analysis";

            return View();
        }

        public void SearchKeywordInBing(Search Search, List<SearchResult> SearchResult)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "022013306e6b411fa0c5f3a755d0bca6");

            // Request parameters
            queryString["q"] = Search.keyword;
            queryString["count"] = "100";
            queryString["offset"] = "0";
            queryString["mkt"] = "en-us";
            queryString["safesearch"] = "Moderate";
            var uri = "https://api.cognitive.microsoft.com/bing/v5.0/search?" + queryString;

            var response = client.GetStringAsync(uri).Result;
            JObject parser = JObject.Parse(response);
            List<string> snippets = new List<string>();
            List<string> newsDescriptions = new List<string>();
            List<string> videoDescriptions = new List<string>();


            foreach (JToken item in parser["webPages"]["value"].Children())
            {
                string snippet = item["snippet"].ToString();
                snippets.Add(snippet);

            }

            foreach (JToken item in parser["news"]["value"].Children())
            {
                string newsDescription = item["description"].ToString();
                newsDescriptions.Add(newsDescription);
            }

            foreach (JToken item in parser["videos"]["value"].Children())
            {
                string videoDescription = item["description"].ToString();
                videoDescriptions.Add(videoDescription);
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
