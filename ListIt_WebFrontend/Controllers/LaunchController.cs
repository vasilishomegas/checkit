using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListIt_WebFrontend.Controllers
{
    public class LaunchController : Controller
    {
        // GET: Launch/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Launch/Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Launch/Register
        public ActionResult Register()
        {
            return View();
        }

        // GET: Launch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Launch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Launch/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Launch/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Launch/Edit/5
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

        // GET: Launch/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Launch/Delete/5
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
