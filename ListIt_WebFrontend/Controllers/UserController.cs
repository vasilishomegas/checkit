using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListIt_BusinessLogic;
using ListIt_WebFrontend.Models;

namespace ListIt_WebFrontend.Controllers
{
    public class UserController : Controller
    {    

        // GET: User/Profile
        public ActionResult Index()
        {
            //leads to profile view
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
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
            RegisterUser user = new RegisterUser();
            ListIt_BusinessLogic.Services.LanguageService langService = new ListIt_BusinessLogic.Services.LanguageService();
            ListIt_BusinessLogic.Services.CountryService countryService = new ListIt_BusinessLogic.Services.CountryService();
            user.CountryList = countryService.GetAll().OrderBy(x => x.Name);
            user.LangList = langService.GetAll().OrderBy(x => x.Name);

            //leads to register view
            return View(user);
        }

        // GET: Launch/Logout
        public ActionResult Logout()
        {
            //removing Session:
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "User");
        }

        // GET: User/Lists
        public ActionResult Lists()
        {
            if (Session["UserId"] != null)
            {

                //TODO: Logic to show all lists of a user

                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: User/SingleList
        public ActionResult SingleList()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: User/Trashbin
        //public ActionResult Trashbin()
        //{
        //    return View();
        //}

        // GET: User/Settings
        public ActionResult Settings()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/CreateList
        [HttpPost]
        public ActionResult CreateList(FormCollection collection)
        {
            var name = collection["listname"];
            var userid = Session["UserId"];



            return View();
        }

        // POST: User/CreateUser
        [HttpPost]
        public ActionResult CreateUser(FormCollection collection)
        {
            try
            {
                ListIt_DomainModel.DTO.UserDto user = new ListIt_DomainModel.DTO.UserDto();                

                ListIt_DomainModel.DTO.CountryDto country = new ListIt_DomainModel.DTO.CountryDto();              
                country.Id = 73;    //Denmark is default

                ListIt_DomainModel.DTO.LanguageDto lang = new ListIt_DomainModel.DTO.LanguageDto();
                lang.Id = 2;      //English is default

                user.Nickname = collection["Nickname"]; 
                user.Email = collection["Email"]; 
                user.PasswordHash = collection["PasswordHash"]; 
                user.Language = lang;
                user.Country = country;

                var userService = new ListIt_BusinessLogic.Services.UserService();
                userService.Create(user);

                ViewBag.Message = "You have registered successfully. Now you can login here!";

                return RedirectToAction("Login");
            }
            catch
            {
                ViewBag.Error = "Something went wrong. Please try again";

                return View("Register");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // User/Login/
        public ActionResult LoginUser(FormCollection collection)
        {
            try
            {
                var email = collection["Email"];
                var pw = collection["PasswordHash"];

                ListIt_BusinessLogic.Services.UserService userService = new ListIt_BusinessLogic.Services.UserService();
                //UserVM userVM = new UserVM();                

                var user = userService.Login(email, pw);

                Session["UserId"] = user.Id;
                Session["LanguageId"] = user.Language.Id;               

                return RedirectToAction("Lists");
            }
            catch
            {
                ViewBag.Error = "Login is not valid! Either Email or Password are wrong.";

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
