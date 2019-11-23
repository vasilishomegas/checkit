using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListIt_BusinessLogic;

namespace ListIt_WebFrontend.Controllers
{
    public class UserController : Controller
    {
        // GET: User/Profile
        public ActionResult Index()
        {
            //leads to profile view
            return View();
        }

        // GET: Launch/Login
        public ActionResult Login()
        {
            //leads to login view
            return View();
        }

        // GET: Launch/Register
        public ActionResult Register()
        {
            //leads to register view
            return View();
        }

        // GET: Launch/Logout
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "User");
        }

        // GET: User/Lists
        public ActionResult Lists()
        {
            return View();
        }

        // GET: User/SingleList
        public ActionResult SingleList()
        {
            return View();
        }

        // GET: User/Trashbin
        public ActionResult Trashbin()
        {
            return View();
        }

        // GET: User/Settings
        public ActionResult Settings()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/CreateUser
        [HttpPost]
        public ActionResult CreateUser(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var name = collection["Nickname"];
                var email = collection["Email"];
                var pw = collection["PasswordHash"].ToHashSet().ToString();

                ListIt_DomainModel.DTO.UserDto user = new ListIt_DomainModel.DTO.UserDto();
                user.Nickname = name;
                user.Email = email;
                user.PasswordHash = pw;

                //user.Language = 1;
                //user.Country = 1;



                //ListIt_BusinessLogic.Services.UserService.Create(user);

                return RedirectToAction("Login");
            }
            catch
            {
                ViewBag.Error = "Something went wrong. Please try again";

                return View("Register");
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
