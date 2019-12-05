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
                int userId = int.Parse(Session["UserId"].ToString());

                ShoppingListService listService = new ShoppingListService();
                var listOfLists = listService.GetListsByUserId(userId).OrderByDescending(x => x.TimeStamp).ToList();

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
                    if (x.Id == requestedList.Id)
                    {
                        SingleListVM list = new SingleListVM();
                        list.ShoppingList_Id = requestedList.Id;

                        var listObj = listService.Get(list.ShoppingList_Id, int.Parse(Session["UserId"].ToString()));

                        list.ListName = listObj.Name;
                        list.ListAccessTypeId = listObj.ListAccessTypeId;

                        LanguageService languageService = new LanguageService();
                        var langId = languageService.GetByCode(Session["LanguageCode"].ToString()).Id;

                        ProductService productService = new ProductService();
                        list.ListEntries = productService.GetEntriesAsProducts(list.ShoppingList_Id, langId);

                        if (list.ListEntries.Count() == 0)
                        {
                            ViewBag.Message = "You don't have any entries yet. Start creating your entries now!";
                        }

                        UnitTypeService unitTypeService = new UnitTypeService();
                        list.UnitTypesListId = 1; //default value
                        list.UnitTypesList = (from item in unitTypeService.GetUnitTypesByLanguage(langId)
                                              select new SelectListItem()
                                              {
                                                  Text = item.Name,
                                                  Value = item.Id.ToString()
                                              }).ToList();

                        CurrencyService currencyService = new CurrencyService();
                        list.CurrencyListId = 5; //default DKK
                        list.CurrencyList = (from item in currencyService.GetAll()
                                             select new SelectListItem()
                                             {
                                                 Text = item.Code,
                                                 Value = item.Id.ToString()
                                             }).ToList();

                        CategoryService categoryService = new CategoryService();
                        list.CategoryListId = 20; //default category: others
                        list.CategoryList = (from item in categoryService.GetCategories(langId, int.Parse(Session["UserId"].ToString()))
                                             select new SelectListItem()
                                             {
                                                 Text = item.Name,
                                                 Value = item.Id.ToString()
                                             }).ToList();

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

        //GET List/Item -> EditItemView
        public ActionResult Item(int? id, int? listId)
        {
            if (Session["UserId"] != null)
            {
                ViewBag.Message = TempData["SuccessMessage"];
                ViewBag.Error = TempData["ErrorMessage"];

                ItemsVM item = new ItemsVM();
                item.ListId = (int)listId;
                item.ProductId = (int)id;

                LanguageService languageService = new LanguageService();
                var langId = languageService.GetByCode(Session["LanguageCode"].ToString()).Id;

                ProductService productService = new ProductService();
                var entry = productService.Get(langId, (int)id); //Gets productDto by langId(for translation if default or api product) and ProductId
                
                if(entry == null)
                {
                    TempData["ErrorMessage"] = "You can't edit default or api products.";
                    return Redirect(Request.UrlReferrer.ToString());
                }

                item.Id = entry.Id;
                item.Name = entry.Name;
                item.Price = entry.Price;
                item.Quantity = entry.Quantity;
                item.Unit_Id = entry.Unit_Id;
                item.Currency_Id = entry.Currency_Id;
                item.Category_Id = entry.Category_Id;
                item.ProductTypeId = entry.ProductTypeId;                

                UnitTypeService unitTypeService = new UnitTypeService();
                item.UnitTypesListId = item.Unit_Id; //saved value from db entry
                item.UnitTypesList = (from x in unitTypeService.GetUnitTypesByLanguage(langId)
                                      select new SelectListItem()
                                      {
                                          Text = x.Name,
                                          Value = x.Id.ToString()
                                      }).ToList();

                CurrencyService currencyService = new CurrencyService();
                item.CurrencyListId = item.Currency_Id; //saved value from db entry
                item.CurrencyList = (from x in currencyService.GetAll()
                                     select new SelectListItem()
                                     {
                                         Text = x.Code,
                                         Value = x.Id.ToString()
                                     }).ToList();

                CategoryService categoryService = new CategoryService();
                item.CategoryListId = (int)item.Category_Id; //saved value from db entry
                item.CategoryList = (from x in categoryService.GetCategories(langId, int.Parse(Session["UserId"].ToString()))
                                     select new SelectListItem()
                                     {
                                         Text = x.Name,
                                         Value = x.Id.ToString()
                                     }).ToList();


                return View(item);
            }
            else if(id == 0 || listId == 0)
            {
                TempData["ErrorMessage"] = "There is no item of a specific list selected to edit.";
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: User/CreateList
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateList(FormCollection collection)
        {
            try
            {
                var name = collection["listname"];

                if (name == null || name == "")
                {
                    throw new Exception("Name cannot be null.");
                }

                var sessionUserId = Session["UserId"];
                int userid = int.Parse(sessionUserId.ToString());

                ShoppingListDto list = new ShoppingListDto();
                list.Name = name;
                list.Path = "somerandomPath";
                list.ListAccessTypeId = 1; //Default owner when creating
                list.UserId = userid;
                list.ChosenSortingId = null;

                ShoppingListService listService = new ShoppingListService();
                listService.Create(list);

                TempData["SuccessMessage"] = "You successfully created a new shopping list!";
                return RedirectToAction("Lists");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Something went wrong. Make sure to have entered a valid list name. Try again!";
                return RedirectToAction("Lists");
            }

        }

        //Post: User/List/Rename
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RenameList(FormCollection collection)
        {
            try
            {
                var newName = collection["ListName"];
                int listId = int.Parse(collection["ShoppingList_Id"]);

                ShoppingListService listService = new ShoppingListService();
                var dbList = listService.Get(listId);

                if(newName == dbList.Name)
                {
                    throw new Exception("The name of the list is unchanged.");
                }

                ShoppingListDto list = new ShoppingListDto();
                list.Id = listId;
                list.Name = newName;

                listService.Update(list);

                TempData["SuccessMessage"] = "The list's name was successfully updated!";
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                TempData["ErrorMessage"] = "Updating the list's name wasn't successful.";
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        //Post: User/List/Rename
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteList(FormCollection collection)
        {
            try
            {
                int listId = int.Parse(collection["listId"].ToString());

                ShoppingListService listService = new ShoppingListService();
                //var dbList = listService.Get(listId);

                //TO DO: delete all related entries (ShoppingListEntry)
                listService.Delete(listId);

                TempData["SuccessMessage"] = "The list was successfully deleted.";
                return RedirectToAction("Lists");
            }
            catch
            {
                TempData["ErrorMessage"] = "Deleting the list wasn't successful. Try again.";
                return RedirectToAction("Lists");
            }
        }


        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(FormCollection collection)
        {
            //try
            //{
                var name = collection["Name"];
                var reusable = collection["UserProduct"];
                var price = decimal.Parse(collection["Price"]);
                var listId = int.Parse(collection["ListId"]);
                var qty = int.Parse(collection["Quantity"]);
                var unitTypeId = int.Parse(collection["UnitTypesListId"]); 
                var catId = int.Parse(collection["CategoryListId"]);
                var userCat = collection["UserCategory"];
                var currencyId = int.Parse(collection["CurrencyListId"]);
                var prodType = int.Parse(collection["ProductTypeId"]);
                var prodId = int.Parse(collection["ProductId"]);

                if (reusable == "false")
                {
                    prodType = 4;   //4 = UserListProduct = non reusable
                }
                else
                {
                    prodType = 3;
                }

                /* UPDATING ShoppingListEntry */

                ShoppingListEntryService entryService = new ShoppingListEntryService();
                ShoppingListEntryDto entry = new ShoppingListEntryDto();
                entry.Id = entryService.GetEntryId(prodId);
                entry.Quantity = qty;
                entry.ProductTypeId = prodType;
                entry.ShoppingList_Id = listId;
                entry.Product_Id = prodId;
                entry.State_Id = 2; //Default is unchecked

                
                entryService.Update(entry); //updates ShoppingListEntry

                /* UPDATING UserProduct */

                ProductService productService = new ProductService();
                UserProductDto userProduct = new UserProductDto();
                userProduct.Id = productService.GetUserProductId(prodId);
                userProduct.ProductId = prodId;
                userProduct.Name = name;
                userProduct.Category_Id = catId;
                userProduct.User_Id = int.Parse(Session["UserId"].ToString());
                userProduct.Unit_Id = unitTypeId;
                userProduct.Price = price;
                userProduct.Currency_Id = currencyId;

                productService.Update(userProduct);

                /* UPDATING Product -> for new Timestamp*/

                ProductDto productDto = new ProductDto();
                productDto.Id = prodId;
                productDto.ProductTypeId = prodType;
                productService.Update(productDto);

                /* UPDATING ShoppingList -> for new Timestamp: */

                ShoppingListDto shoppingList = new ShoppingListDto();
                shoppingList.Id = listId;
                ShoppingListService listService = new ShoppingListService();
                listService.Update(shoppingList);

                TempData["SuccessMessage"] = "Successfully created a new item";
                return RedirectToAction("SingleList", new { @id = listId });
            //}
            //catch
            //{
            //    TempData["ErrorMessage"] = "There was an error while editing the item";
            //    return Redirect(Request.UrlReferrer.ToString());
            //}
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
            try
            {
                var name = collection["Name"];
                var reusable = collection["UserProduct"];
                var price = decimal.Parse(collection["Price"]);
                var listId = int.Parse(collection["ShoppingList_Id"]);
                var qty = int.Parse(collection["quantity"]);
                var unitTypeId = int.Parse(collection["UnitTypesListId"]);  //TODO: create lists in view and get id (like for countries/languages)
                var catId = int.Parse(collection["CategoryListId"]);
                var userCat = collection["UserCategory"];
                var currencyId = int.Parse(collection["CurrencyListId"]);
                var prodType = 4;   //Default non reusable UserProduct

                if (reusable == "false")
                {
                    prodType = 4;   //4 = UserListProduct = non reusable
                }
                else
                {
                    prodType = 3;   
                }
                                

                ShoppingListEntryDto entry = new ShoppingListEntryDto();
                entry.Quantity = qty;
                entry.ProductTypeId = prodType;
                entry.ShoppingList_Id = listId;
                entry.State_Id = 2; //Default is unchecked

                ShoppingListEntryService entryService = new ShoppingListEntryService();
                var prodId = entryService.Create(entry); //Creates Product and ShoppingListEntry, returns created ProductId

                

                if(prodType == 3    //reusable UserProduct
                    || prodType == 4)   //one-time/one-list UserListProduct
                {
                    UserProductDto userProduct = new UserProductDto();
                    userProduct.ProductId = prodId;
                    userProduct.Name = name;
                    userProduct.Category_Id = catId;
                    userProduct.User_Id = int.Parse(Session["UserId"].ToString());
                    userProduct.Unit_Id = unitTypeId;
                    userProduct.Price = price;
                    userProduct.Currency_Id = currencyId;

                    entryService.Create(userProduct);
                    //ERROR while saving UserProduct
                }

                //TODO: add logic to add default products to list


                //Update ShoppingList to update Timestamp:
                ShoppingListDto shoppingList = new ShoppingListDto();
                shoppingList.Id = listId;
                ShoppingListService listService = new ShoppingListService();
                listService.Update(shoppingList);

                TempData["SuccessMessage"] = "Successfully created a new item";
                return RedirectToAction("SingleList", new { @id = listId});
            }
            catch
            {
                TempData["ErrorMessage"] = "There was an error while creating a new item";
                return Redirect(Request.UrlReferrer.ToString());
        }

}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShareList(FormCollection collection)
        {
            try
            {
                var emailAddress = collection["EmailAddress"];
                var type = int.Parse(collection["Type"]);
                var listId = int.Parse(collection["ShoppingList_Id"]);
                UserService userService = new UserService();
                var userId = userService.GetIdByEmail(emailAddress);
                ShoppingListDto shoppingListDto = new ShoppingListDto();
                shoppingListDto.UserId = userId;
                shoppingListDto.Id = listId;
                shoppingListDto.ListAccessTypeId = type;
                ShoppingListService listService = new ShoppingListService();
                listService.CreateLink(shoppingListDto);
                TempData["SuccessMessage"] = "You successfully shared your list!";
                return RedirectToAction("SingleList", new { @id = listId });
            }
            catch
            {
                var listId = int.Parse(collection["ShoppingList_Id"]);
                TempData["ErrorMessage"] = "An error occured and your list hasn't been shared.";
                return RedirectToAction("SingleList", new { @id = listId });
            }
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
