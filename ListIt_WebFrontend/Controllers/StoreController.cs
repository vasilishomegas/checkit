using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListIt_WebFrontend.Models;
using ListIt_BusinessLogic.Services;
using ListIt_DomainModel.DTO;

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
        protected string defaultUrl = "https://www.google.com/maps/embed/v1/search?q=grocery+stores+near";

        // GET: Store/FindStore
        public ActionResult FindStore()
        {
            ViewBag.Message = TempData["SuccessMessage"];
            ViewBag.Error = TempData["ErrorMessage"];

            // https://developers.google.com/maps/documentation/urls/guide
            /* ENCODING URLs */
            // pipe character (|) as a separator, which you must encode as %7C in the final URL
            // encode the comma as %2C
            // Encode spaces with %20, or replace them with a plus sign (+).
            //https://developers.google.com/maps/documentation/embed/usage-and-billing

            //var mapsKey = "&key=AIzaSyAk3NRXmyq5nFc3iXwZMcNNp-jDNJzup1c";

            FindStoreVM findStoreVM = new FindStoreVM();
            findStoreVM.Place = "Aalborg, Denmark"; //initialized default place Aalborg
            findStoreVM.Url = defaultUrl + "+Aalborg+Denmark" + mapsKey;

            return View(findStoreVM);
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

                char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
                string[] keywords = searchPlace.Split(delimiterChars);
                string search = "";                

                foreach (String word in keywords)
                {
                    search = search + "+" + word;
                }

                findStoreVM.Url = defaultUrl + search + mapsKey;

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
