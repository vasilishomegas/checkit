using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListIt_BusinessLogic;
using ListIt_WebFrontend.Models;
using ListIt_BusinessLogic.Services;
using ListIt_DomainModel.DTO;

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
            ViewBag.Error = TempData["ErrorMessage"];
            ViewBag.Message = TempData["SuccessMessage"];
            //leads to login view
            return View();
        }

        // GET: Launch/Register
        public ActionResult Register()
        {
            RegisterUser user = new RegisterUser();
            ListIt_BusinessLogic.Services.LanguageService langService = new ListIt_BusinessLogic.Services.LanguageService();
            ListIt_BusinessLogic.Services.CountryService countryService = new ListIt_BusinessLogic.Services.CountryService();
            
            user.CountryId = 73;
            user.LanguageId = 2;

            user.CountryList = (from item in countryService.GetAll().OrderBy(x => x.Name)
                        select new SelectListItem()
                        {
                            Text = item.Name,
                            Value = item.Id.ToString()
                        }).ToList();

            user.LangList = (from item in langService.GetAll().OrderBy(x => x.Name)
                                select new SelectListItem()
                                {
                                    Text = item.Name,
                                    Value = item.Id.ToString()
                                }).ToList();

            ViewBag.Error = TempData["ErrorMessage"];
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
                var userId = Session["UserId"];
                ShoppingListService listService = new ShoppingListService();


                
                //TODO: Implement Service and Repository
                //listService.GetListsByUserId(userId);

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
            var sessionUserId = Session["UserId"];
            int userid = Int32.Parse(sessionUserId.ToString());

            ShoppingListDto list = new ShoppingListDto();
            list.Name = name;
            list.Path = "somerandomPath";
            list.ListAccessTypeId = 1; //Default owner when creating
            list.UserId = userid;
            list.ChosenSortingId = null;

            ShoppingListService listService = new ShoppingListService();
            listService.Create(list);

            ViewBag.Message = "You successfully created a new shopping list";
            return View("Lists");
        }

        // POST: User/CreateUser
        [HttpPost]
        public ActionResult CreateUser(FormCollection collection)
        {
            try
            {
                ListIt_DomainModel.DTO.UserDto user = new ListIt_DomainModel.DTO.UserDto();                

                ListIt_DomainModel.DTO.CountryDto country = new ListIt_DomainModel.DTO.CountryDto();              
                country.Id = int.Parse(collection["CountryId"]);    //Denmark is default


                ListIt_DomainModel.DTO.LanguageDto lang = new ListIt_DomainModel.DTO.LanguageDto();
                lang.Id = int.Parse(collection["LanguageId"]);      //English is default

                user.Nickname = collection["Nickname"]; 
                user.Email = collection["Email"]; 
                user.PasswordHash = collection["PasswordHash"]; 
                user.Language = lang;
                user.Country = country;

                var userService = new ListIt_BusinessLogic.Services.UserService();
                userService.Create(user);

                TempData["SuccessMessage"] = "You have registered successfully. Now you can login here!";

                return RedirectToAction("Login");
            }
            catch
            {
                TempData["ErrorMessage"] = "Something went wrong. Please try again";

                return RedirectToAction("Register");
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
                TempData["ErrorMessage"] = "Login is not valid! Either Email or Password are wrong.";

                return RedirectToAction("Login");
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult EditItem(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                // Create a new item if no ID retrieved (edit & create in same form)

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
