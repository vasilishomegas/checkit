﻿using System.Web.Mvc;
using ListIt_WebFrontend.Models;

namespace ListIt_WebFrontend.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store/FindItem
        public ActionResult Index()
        {
            return View();
        }

        #region FindStore
        protected string mapsKey = "&key=AIzaSyAk3NRXmyq5nFc3iXwZMcNNp-jDNJzup1c";
        protected string defaultUrl = "https://www.google.com/maps/embed/v1/search?q=";
        protected string parameter = "+grocery+stores";
        // GET: Store/FindStore
        public ActionResult FindStore()
        {
            if(Session["UserId"] != null)
            {
                ViewBag.Message = TempData["SuccessMessage"];
                ViewBag.Error = TempData["ErrorMessage"];

                // https://developers.google.com/maps/documentation/urls/guide
                /* ENCODING URLs */
                // pipe character (|) as a separator, which you must encode as %7C in the final URL
                // encode the comma as %2C
                // Encode spaces with %20, or replace them with a plus sign (+).
                //https://developers.google.com/maps/documentation/embed/start

                FindStoreVM findStoreVM = new FindStoreVM();
                findStoreVM.Place = "Aalborg, Denmark"; //initialized default place Aalborg
                findStoreVM.Url = defaultUrl + "Aalborg+Denmark" + parameter + mapsKey;

                return View(findStoreVM);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMap(FormCollection collection)
        {
            try
            {
                var searchPlace = collection["Place"];
                FindStoreVM findStoreVM = new FindStoreVM();
                findStoreVM.Place = searchPlace;

                string search = searchPlace.Replace(" ", "%20");
                search = search.Replace(",", "%2C");

                findStoreVM.Url = defaultUrl + search + parameter + mapsKey;

                return View("FindStore", findStoreVM);
            }
            catch
            {
                TempData["ErrorMessage"] = "You search request could not be found.";
                return RedirectToAction("FindStore");
            }
            
        }

        #endregion FindStore

        // GET: Store/BrowseStore
        public ActionResult BrowseStore()
        {
            return View();
        }

        
    }
}
