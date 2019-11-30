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
                ViewBag.Error = TempData["ErrorMessage"];
                ViewBag.Message = TempData["SuccessMessage"];

                UserService service = new UserService();
                var user = service.Get(Int32.Parse(Session["UserId"].ToString()));
                UserVM userVM = new UserVM();
                userVM.Nickname = user.Nickname;
                userVM.Email = user.Email;
                userVM.PasswordHash = user.PasswordHash;

                return View(userVM);
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
            RegisterUserVM user = new RegisterUserVM();
            LanguageService langService = new LanguageService();
            CountryService countryService = new CountryService();
            
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

                UserService userService = new UserService();
                //UserVM userVM = new UserVM();                

                var user = userService.Login(email, pw);

                Session["UserId"] = user.Id;
                Session["LanguageCode"] = user.Language.Code;               

                return RedirectToAction("Lists", "List");
            }
            catch
            {
                TempData["ErrorMessage"] = "Login is not valid! Either Email or Password are wrong.";

                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult EditProfile(FormCollection collection)
        {
            try
            {
                int id = Int32.Parse(Session["UserId"].ToString());
                var newName = collection["Nickname"];
                var newMail = collection["Email"];

                UserService service = new UserService();
                UserDto user = new UserDto();
                user.Id = id;
                user.Nickname = newName;
                user.Email = newMail;

                service.Update(user);

                TempData["SuccessMessage"] = "Your profile has been updated successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "There was an error while updating the profile. Please try again.";
                return RedirectToAction("Index");
            }
        }

        
    }
}
