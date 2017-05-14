using SentimentAnalysis.Context;
using SentimentAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Search.searchDate = DateTime.Now;
                    db.Searches.Add(Search);
                    db.SaveChanges();
                    return RedirectToAction("Create");
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
    }
}
