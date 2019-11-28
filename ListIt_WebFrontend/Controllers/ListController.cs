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
    public class ListController : Controller
    {
        // GET: User/Lists
        public ActionResult Lists()
        {
            if (Session["UserId"] != null)
            {
                ViewBag.Message = TempData["SuccessMessage"];
                ViewBag.Error = TempData["ErrorMessage"];

                //TODO: Logic to show all lists of a user
                int userId = Int32.Parse(Session["UserId"].ToString());

                ShoppingListService listService = new ShoppingListService();
                var listOfLists = listService.GetListsByUserId(userId);

                if (listOfLists.Count() == 0)
                {
                    ViewBag.Message = "You don't have any lists yet. Start creating your lists now!";
                }

                ListsVM lists = new ListsVM();
                lists.AllUserLists = listOfLists;

                return View(lists);
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

        // GET: User/CreateList
        [HttpPost]
        public ActionResult CreateList(FormCollection collection)
        {
            try
            {
                var name = collection["listname"];

                if (name == null || name == "")
                {
                    throw new Exception("Name cannot be null");
                }

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

                TempData["SuccessMessage"] = "You successfully created a new shopping list";
                return RedirectToAction("Lists");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "There went something wrong. Make sure to have a valid Listname entered. Try it again!";
                return RedirectToAction("Lists");
            }

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

        // GET: List
        public ActionResult Index()
        {
            return View();
        }

        // GET: List/Details/5
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateItem(FormCollection collection)
        {
            var name = collection["Name"];
            var reusable = collection["UserProduct"];
            var price = collection["Price"];
            var qty = Int32.Parse(collection["Quantity"]);
            var unitType = collection["unit"];
            var cat = collection["category"];
            var userCat = collection["UserCategory"];
            var prodType = 4;   //Default non reusable UserProduct
            var listId = Int32.Parse(collection["ShoppingList_Id"]);

            //if(reusable == checked)
            //{
            //    prodType = 3;
            //}


            ShoppingListEntryDto entry = new ShoppingListEntryDto();
            entry.Quantity = qty;
            entry.ProductTypeId = prodType;
            entry.ShoppingList_Id = listId;
            entry.State_Id = 1; //Default is unchecked


            ShoppingListEntryService entryService = new ShoppingListEntryService();
            entryService.Create(entry);

            return View();
        }

        // GET: List/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: List/Create
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

        // GET: List/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: List/Edit/5
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

        // GET: List/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: List/Delete/5
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
