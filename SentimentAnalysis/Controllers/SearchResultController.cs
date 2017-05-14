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
    public class SearchResultController : Controller
    {
        private SearchContext db = new SearchContext();

        // GET: SearchResult
        public ActionResult Index()
        {
            return View();
        }

        // GET: SearchResult/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchResult/Create
        public ActionResult Create()
        {
            List<SearchResult> sResult = (List<SearchResult>)TempData["SearchResult"];
            return Create(sResult);
        }

        // POST: SearchResult/Create
        [HttpPost]
        public ActionResult Create(List<SearchResult> SearchResult)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var item in SearchResult)
                    {
                        item.searchId = item.Search.id;
                        db.SearchResult.Add(item);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Create", "Search", new { area = "" });
                }
                return View(SearchResult);
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchResult/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchResult/Edit/5
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

        // GET: SearchResult/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchResult/Delete/5
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
    }
}
