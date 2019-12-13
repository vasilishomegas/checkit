using System;
using System.Linq;
using System.Web.Mvc;
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
                var user = service.Get(int.Parse(Session["UserId"].ToString()));
                UserVM userVM = new UserVM
                {
                    Nickname = user.Nickname,
                    Email = user.Email
                };

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
                ViewBag.Error = TempData["ErrorMessage"];
                ViewBag.Message = TempData["SuccessMessage"];

                UserVM user = new UserVM();
                UserService service = new UserService();
                CountryService countryService = new CountryService();

                var userDto = service.Get(Int32.Parse(Session["UserId"].ToString()));                
                user.CountryId = userDto.Country.Id;

                user.CountryList = (from item in countryService.GetAll().OrderBy(x => x.Name)
                                    select new SelectListItem()
                                    {
                                        Text = item.Name,
                                        Value = item.Id.ToString()
                                    }).ToList();

                //TODO: Currency list and sorting list

                return View(user);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
      

        // POST: User/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(FormCollection collection)
        {
            try
            {
                UserDto user = new UserDto();

                CountryDto country = new CountryDto
                {
                    Id = int.Parse(collection["CountryId"])    //Denmark is default
                };


                LanguageDto lang = new LanguageDto
                {
                    Id = int.Parse(collection["LanguageId"])      //English is default
                };

                user.Nickname = collection["Nickname"]; 
                user.Email = collection["Email"]; 
                user.PasswordHash = collection["PasswordHash"]; 
                user.Language = lang;
                user.Country = country;

                var userService = new UserService();
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
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(FormCollection collection)
        {
            try
            {
                int id = Int32.Parse(Session["UserId"].ToString());
                var newName = collection["Nickname"];
                var newMail = collection["Email"];

                UserService service = new UserService();
                UserDto user = new UserDto
                {
                    Id = id,
                    Nickname = newName,
                    Email = newMail
                };

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPw(FormCollection collection)
        {
            try
            {
                int id = Int32.Parse(Session["UserId"].ToString());
                var oldPw = collection["PasswordHash"];
                var newPW = collection["NewPassword"];

                UserService service = new UserService();

                //Verify old password
                if(service.HashPassword(oldPw) != service.Get(id).PasswordHash)
                {
                    TempData["ErrorMessage"] = "The old Password doesn't match!";
                    throw new Exception("Old PW doesn't match");
                }

                UserDto user = new UserDto
                {
                    Id = id,
                    PasswordHash = service.HashPassword(newPW)
                };

                service.Update(user);

                TempData["SuccessMessage"] = "Your password has been updated successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                if(TempData["ErrorMessage"] == null)
                {
                    TempData["ErrorMessage"] = "There was an error while updating the password. Please try again.";
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSettings(FormCollection collection)
        {
            try
            {
                UserService service = new UserService();
                UserDto user = new UserDto
                {
                    Id = int.Parse(Session["UserId"].ToString())
                };

                CountryDto country = new CountryDto
                {
                    Id = Int32.Parse(collection["CountryId"])
                };
                //user.Country.Id = Int32.Parse(collection["CountryId"].ToString());
                user.Country = country;

                //TODO: add attributes in user for default currency and sorting

                service.Update(user);

                TempData["SuccessMessage"] = "Your settings has been updated successfully";
                return RedirectToAction("Settings");
            }
            catch
            {
                TempData["ErrorMessage"] = "An error has occured. Please try again.";
                return RedirectToAction("Settings");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveLang(FormCollection collection)
        {
            try
            {
                var langCode = collection["lang"].ToString();
                int position = langCode.IndexOf("-");
                string shortCode = langCode.Substring(0, position);
                Session["LanguageCode"] = shortCode;

                UserDto user = new UserDto();
                UserService service = new UserService();
                //LanguageDto lang = new LanguageDto();
                LanguageService languageService = new LanguageService();
                user.Language = languageService.GetByCode(shortCode);
                user.Id = Int32.Parse(Session["UserId"].ToString());
                //lang.Code = shortCode;
                //user.Language = lang;
                service.Update(user);

                ViewBag.Message = "Language saved successfully";
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                ViewBag.Error = "An error has occured. Please try again.";
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
        
    }
}
