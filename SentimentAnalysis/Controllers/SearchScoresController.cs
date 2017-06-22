using SentimentAnalysis.Context;
using SentimentAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SentimentAnalysis.Controllers
{
    public class SearchScoresController : Controller
    {

        SearchContext db = new SearchContext();

        // GET: SearchScores
        public ActionResult Index()
        {
            return View();
        }

        // GET: SearchScores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchScores/Create
        public ActionResult Create()
        {
            List<SearchScores> SearchScores = (List<SearchScores>)TempData["SearchScores"];
            return Create(SearchScores);
        }

        // POST: SearchScores/Create
        [HttpPost]
        public ActionResult Create(List<SearchScores> SearchScores)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    foreach (var item in SearchScores)
                    {
                        item.searchResultId = item.SearchResult.id;
                        db.SearchScores.Add(item);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index", "SearchScores");
                }

                return View(SearchScores);
                
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchScores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchScores/Edit/5
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

        // GET: SearchScores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchScores/Delete/5
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

       public ActionResult CreateChart()
        {
            string keyword = TempData["Keyword"].ToString();
            string myQueryNegative = String.Format("SELECT COUNT(score) " +
            "FROM SearchScores, SearchResults, Searches " +
            "WHERE Searches.id = SearchResults.searchId " +
            "AND SearchResults.id = SearchScores.searchResultId " +
            "AND Searches.keyword like \'{0}\' " +
            "AND Score >= {1} AND Score <= {2}", keyword, 0, 35);

            string myQueryNeutral = String.Format("SELECT COUNT(score) " +
            "FROM SearchScores, SearchResults, Searches " +
            "WHERE Searches.id = SearchResults.searchId " +
            "AND SearchResults.id = SearchScores.searchResultId " +
            "AND Searches.keyword like \'{0}\' " +
            "AND Score >= {1} AND Score <= {2}", keyword, 36, 65);

            string myQueryPositive = String.Format("SELECT COUNT(score) " +
            "FROM SearchScores, SearchResults, Searches " +
            "WHERE Searches.id = SearchResults.searchId " +
            "AND SearchResults.id = SearchScores.searchResultId " +
            "AND Searches.keyword like \'{0}\' " +
            "AND Score >= {1} AND Score <= {2}", keyword, 66, 100);


            //SentimentAnalysis.Context.SearchContext db = new SentimentAnalysis.Context.SearchContext();
            var negative = db.Database.SqlQuery<int>(myQueryNegative).ToList();
            var neutral = db.Database.SqlQuery<int>(myQueryNeutral).ToList();
            var positive = db.Database.SqlQuery<int>(myQueryPositive).ToList();
            var myChart = new Chart(width: 600, height: 500)
                .AddTitle("SENTIMENTS")
                .AddSeries(
                chartType: "column",
                xValue: new[] { "Negative", "Neutral", "Positive" },
                yValues: new[] { negative[0].ToString(), neutral[0].ToString(), positive[0].ToString() }).Write("png");
            ViewData["myChart"] = myChart;
            return null;
        }
    }
}
