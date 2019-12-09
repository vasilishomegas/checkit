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

        #region Lists
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

                        list.ChosenProductId = 0;  //By default no defaultProduct should be selected
                        list.ChooseProductsList = (from item in productService.GetDefaultAndReusableProductsByLanguage(langId)
                                                   select new SelectListItem()
                                                   {
                                                       Text = item.Name,
                                                       Value = item.ProductId.ToString()
                                                   }).ToList();

                        UnitTypeService unitTypeService = new UnitTypeService();
                        list.UnitTypesListId = 8; //default value: pc.
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
                        list.CategoryList = (from item in categoryService.GetAllCategories(langId, int.Parse(Session["UserId"].ToString()))
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
                int userid = Int32.Parse(sessionUserId.ToString());

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

                if (newName == dbList.Name)
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

        //Post: User/List/Rename
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteList(FormCollection collection)
        {
            try
            {
                int listId = Int32.Parse(collection["ShoppingList_Id"].ToString());

                ShoppingListService listService = new ShoppingListService();
                LanguageService languageService = new LanguageService();
                var langId = languageService.GetByCode(Session["LanguageCode"].ToString()).Id;

                //TO DO: delete all related entries (ShoppingListEntry)
                ProductService productService = new ProductService();
                var listEntries = productService.GetEntriesAsProducts(listId, langId);

                foreach(ProductDto product in listEntries)
                {
                    DeleteItem(product.Id, listId);
                }
                
                //delete list itself & all links in LinkUserToList
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


        #endregion Lists

        #region Items

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
                var entry = productService.Get(langId, (int)id, (int)listId); //Gets productDto by langId(for translation if default or api product) and ProductId

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
                if (item.Category_Id == null) item.CategoryListId = 0;
                else item.CategoryListId = (int)item.Category_Id; //saved value from db entry
                if (item.ProductTypeId == 1)    //if deafult product -> only show UserCategories
                {
                    item.CategoryList = (from x in categoryService.GetUserCategories(langId, int.Parse(Session["UserId"].ToString()))
                                         select new SelectListItem()
                                         {
                                             Text = x.Name,
                                             Value = x.Id.ToString()
                                         }).ToList();
                }
                else
                {
                    item.CategoryList = (from x in categoryService.GetAllCategories(langId, int.Parse(Session["UserId"].ToString()))
                                         select new SelectListItem()
                                         {
                                             Text = x.Name,
                                             Value = x.Id.ToString()
                                         }).ToList();
                }              
                


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
                int userCatId = 0;
                var currencyId = int.Parse(collection["CurrencyListId"]);
                int prodType = 0;
                var prodId = 0;
                var chosenProductId = collection["ChosenProductId"]; //gets defaultProductId from dropdown

                if (name != "" && chosenProductId != "") //Name filled in and defaultProduct chosen
                {
                    throw new Exception("You can either create a new item or choose from the default products, but not both!");
                }

                if (userCat != "")
                {
                    LanguageService languageService = new LanguageService();

                    CategoryDto category = new CategoryDto();
                    category.Name = userCat;
                    category.LanguageId = languageService.GetByCode(Session["LanguageCode"].ToString()).Id;
                    category.UserId = int.Parse(Session["UserId"].ToString());

                    CategoryService categoryService = new CategoryService();
                    userCatId = categoryService.Create(category);
                }

                ShoppingListEntryService entryService = new ShoppingListEntryService();
                ProductService productService = new ProductService();

                if (name != "") //IF UserProduct -> create Product - ShoppingListEntry - UserProduct
                {
                    if (reusable == "false")
                    {
                        prodType = 4;   //4 = UserListProduct = non reusable
                    }
                    else
                    {
                        prodType = 3;   //reusable UserProduct
                    }

                    //create a new Product
                    ProductDto productDto = new ProductDto();
                    productDto.ProductTypeId = prodType;
                    prodId = productService.Create(productDto);

                    //create new UserProduct
                    UserProductDto userProduct = new UserProductDto();
                    userProduct.ProductId = prodId;
                    userProduct.Name = name;
                    if (userCat != "") userProduct.Category_Id = userCatId;
                    else userProduct.Category_Id = catId;
                    userProduct.User_Id = int.Parse(Session["UserId"].ToString());
                    userProduct.Unit_Id = unitTypeId;
                    userProduct.Price = price;
                    userProduct.Currency_Id = currencyId;

                    entryService.Create(userProduct);
                }
                else if (chosenProductId != "")   //IF DefaultProduct -> create ShoppingListEntry & LinkDefaultProductToUser
                {
                    //check if chosen defaultProduct or reusable UserProduct
                    prodId = int.Parse(chosenProductId);
                    prodType = productService.GetProductTypeId(prodId);

                    if (prodType == 1)    //if DefaultProduct: create Link entry
                    {
                        DefaultProductDto defaultProductDto = new DefaultProductDto();
                        defaultProductDto.Id = productService.GetDefaultProductId(prodId);
                        productService.CreateLink(defaultProductDto, int.Parse(Session["UserId"].ToString()));
                    }

                    //if reusable UserProduct: only create ShoppingListEntry
                }

                //create Entry if not existent right now!
                var existentEntries = entryService.GetEntriesByListId(listId);
                foreach(ShoppingListEntryDto shoppingListEntry in existentEntries)
                {
                    if (shoppingListEntry.Product_Id == prodId) throw new Exception("You can't add the same product to your list twice.");
                }

                ShoppingListEntryDto entry = new ShoppingListEntryDto();
                entry.Quantity = qty;
                entry.Product_Id = prodId;
                entry.ShoppingList_Id = listId;
                entry.State_Id = 2; //Default is unchecked
                entryService.Create(entry);

                //Update ShoppingList to update Timestamp:                
                ShoppingListDto shoppingList = new ShoppingListDto();
                shoppingList.Id = listId;
                ShoppingListService listService = new ShoppingListService();
                listService.Update(shoppingList);

                TempData["SuccessMessage"] = "Successfully created a new item";
                return RedirectToAction("SingleList", new { @id = listId });
            }
            catch
            {
                TempData["ErrorMessage"] = "There was an error while creating a new item. Be aware that you can only create either a new item or choose from the dropdown list of default products and your own reusable items, but you can't do both.";
                return Redirect(Request.UrlReferrer.ToString());
            }

        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(FormCollection collection)
        {
            try
            {
                var name = collection["Name"];
                var reusable = collection["UserProduct"];
                var price = decimal.Parse(collection["Price"]);
                var listId = int.Parse(collection["ListId"]);
                var qty = int.Parse(collection["Quantity"]);
                var unitTypeId = int.Parse(collection["UnitTypesListId"]);
                var catId = 0;
                if(collection["CategoryListId"] != "") catId = int.Parse(collection["CategoryListId"]);
                var userCat = collection["UserCategory"];
                var currencyId = int.Parse(collection["CurrencyListId"]);
                var prodTypeId = int.Parse(collection["ProductTypeId"]);
                var prodId = int.Parse(collection["ProductId"]);

                /* UPDATING ShoppingListEntry */

                ShoppingListEntryService entryService = new ShoppingListEntryService();
                ShoppingListEntryDto entry = new ShoppingListEntryDto();
                entry.Id = entryService.GetEntryId(prodId, listId);
                entry.Quantity = qty;
                entry.ProductTypeId = prodTypeId;
                entry.ShoppingList_Id = listId;
                entry.Product_Id = prodId;
                entry.State_Id = 2; //Default is unchecked

                entryService.Update(entry); //updates ShoppingListEntry

                ProductService productService = new ProductService();
                if (prodTypeId == 4 || prodTypeId == 3)
                {
                    if (reusable == "false")
                    {
                        prodTypeId = 4;   //4 = UserListProduct = non reusable
                    }
                    else
                    {
                        prodTypeId = 3;
                    }

                    /* UPDATING UserProduct */

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
                }
                else if(prodTypeId == 1) //if default Product
                {
                    //TODO: if catId != 0 -> create LinkDefaultProductToCategory entry
                }                

                /* UPDATING Product -> for new Timestamp*/
                ProductDto productDto = new ProductDto();
                productDto.Id = prodId;
                productDto.ProductTypeId = prodTypeId;
                productService.Update(productDto);

                /* UPDATING ShoppingList -> for new Timestamp: */
                ShoppingListDto shoppingList = new ShoppingListDto();
                shoppingList.Id = listId;
                ShoppingListService listService = new ShoppingListService();
                listService.Update(shoppingList);

                TempData["SuccessMessage"] = "Successfully edited this item";
                return RedirectToAction("SingleList", new { @id = listId });
            }
            catch
            {
                TempData["ErrorMessage"] = "There was an error while editing the item";
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        // GET: List/Item/Delete/5
            public ActionResult DeleteItem(int id, int listId)  //id = productId
        {
            //try
            //{
                //1. If UserProduct -> Delete
                ProductService productService = new ProductService();
                var prodType = productService.GetProductTypeId(id);

                if (prodType == 4) //one-time UserProduct
                {
                    var userProdId = productService.GetUserProductId(id);
                    productService.DeleteUserProduct(userProdId);
                }

                //2. Delete ShoppingListEntry (if default or reusable only this)
                ShoppingListEntryService shoppingListEntryService = new ShoppingListEntryService();
                var entryId = shoppingListEntryService.GetEntryId(id, listId);
                shoppingListEntryService.Delete(entryId);

                TempData["SuccessMessage"] = "Successfully deleted the entry.";
                return Redirect(Request.UrlReferrer.ToString());
            //}
            //catch
            //{
            //    TempData["ErrorMessage"] = "There was an error while trying to delete this entry.";
            //    return Redirect(Request.UrlReferrer.ToString());
            //}
        }

        #endregion Items





        // GET: List
        public ActionResult Index()
        {
            return View();
        }





    }
}
