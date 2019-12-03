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
                return RedirectToAction("Login", "User");
            }
        }

        // GET: User/SingleList
        public ActionResult SingleList(int? id)
        {
            if (Session["UserId"] != null && id != null)
            {
                ViewBag.Message = TempData["SuccessMessage"];
                ViewBag.Error = TempData["ErrorMessage"];

                ShoppingListService listService = new ShoppingListService();
                var userLists = listService.GetListsByUserId(Int32.Parse(Session["UserId"].ToString()));
                var requestedList = listService.Get((int)id);

                //checking whether the list the user tries to access is his or if he has access rights
                foreach(ShoppingListDto x in userLists)
                {
                    if(x.Id == requestedList.Id)
                    {
                        SingleListVM list = new SingleListVM();
                        list.ListId = (int)id;


                        list.ListName = listService.Get(list.ListId).Name;
                        list.ListAccessTypeId = listService.Get(list.ListId).ListAccessTypeId;

                        //TODO: if listaccesstype == readonly then disable edit/delete buttons

                        ShoppingListEntryService entryService = new ShoppingListEntryService();

                        //TODO: get all entries

                        return View(list);
                    }
                }

                TempData["ErrorMessage"] = "The list you tried to access isn't yours!!";
                return RedirectToAction("Lists", "List");

            }
            else
            {
                return RedirectToAction("Login", "User");
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

        //Post: User/List/Rename
        [HttpPost]
        public ActionResult RenameList(FormCollection collection)
        {
            try
            {
                var newName = collection["ListName"];
                int listId = Int32.Parse(collection["ListId"].ToString());

                ShoppingListService listService = new ShoppingListService();
                var dbList = listService.Get(listId);

                if(newName == dbList.Name)
                {
                    throw new Exception("The name of the list is the same as before.");
                }

                ShoppingListDto list = new ShoppingListDto();
                list.Id = listId;
                list.Name = newName;

                listService.Update(list);

                TempData["SuccessMessage"] = "The listname was successfully updated";
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                TempData["ErrorMessage"] = "Updating the listname wasn't successful";
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        //Post: User/List/Rename
        [HttpPost]
        public ActionResult DeleteList(FormCollection collection)
        {
            try
            {
                int listId = Int32.Parse(collection["listId"].ToString());

                ShoppingListService listService = new ShoppingListService();
                //var dbList = listService.Get(listId);

                //TO DO: delete all related entries (ShoppingListEntry)
                listService.Delete(listId);

                TempData["SuccessMessage"] = "The list was successfully deleted";
                return RedirectToAction("Lists");
            }
            catch
            {
                TempData["ErrorMessage"] = "Deleting the list wasn't successful";
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
        [HttpPost]
        [ValidateAntiForgeryToken]
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
