﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    //SHOULD COMBINE ShoppingList and LinkUserToList DB-Entities

    public class ShoppingListRepository : Repository<ShoppingList>
    {
        public override IEnumerable<ShoppingList> GetAll()
        {
            using (var context = new ListItContext())
            {
                return context.ShoppingLists
                    .Include(x => x.LinkUserToLists)
                    .ToList();
            }
        }

        public IEnumerable<LinkUserToList> GetLinkByUserId(int userId)
        {
            using (var context = new ListItContext())
            {
                return context.LinkUserToLists
                    .Where(x => x.UserId == userId)
                    .ToList();
            }
            
        }

        //getting single link entry
        public LinkUserToList GetLink(int id, int userid)
        {
            using (var context = new ListItContext())
            {
                return context.LinkUserToLists
                    .Where(x => x.ShoppingListId == id)
                    .SingleOrDefault(x => x.UserId == userid);
            }
        }

        /* UPDATING Create() to save ShoppingList AND LinkUserToList */

        public void Create(ShoppingList entity, LinkUserToList link)
        {
            using (var context = new ListItContext())
            {
                var result = context.ShoppingLists.Add(entity);
                var linkresult = context.LinkUserToLists.Add(link);
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        builder.Append("Entity of type " + eve.Entry.Entity.GetType().Name
                                                         + " in state " + eve.Entry.State + " has the following" +
                                                         " validation errors:");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            builder.Append("Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage);
                        }
                    }
                    throw new Exception(builder.ToString());
                }
            }
        }
        public void Create(LinkUserToList link)
        {
            using (var context = new ListItContext())
            {
                var linkresult = context.LinkUserToLists.Add(link);
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        builder.Append("Entity of type " + eve.Entry.Entity.GetType().Name
                                                         + " in state " + eve.Entry.State + " has the following" +
                                                         " validation errors:");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            builder.Append("Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage);
                        }
                    }
                    throw new Exception(builder.ToString());
                }
            }
        }

        public override void Delete(int id)
        {
            using (var context = new ListItContext())
            {
                //Delete ALL LinkUserToList Entries for this list AND list itself
                var list = context.Set<ShoppingList>().Find(id);
                var links = context.LinkUserToLists.Where(x => x.ShoppingListId == id).ToList();

                try
                {
                    foreach(LinkUserToList link in links)
                    {
                        context.Set<LinkUserToList>().Remove(link);
                    }

                    context.Set<ShoppingList>().Remove(list);
                }
                catch (System.ArgumentNullException e)
                {
                    throw new KeyNotFoundException("No entries were affected; the row does not exist." + e.Message);
                }
                context.SaveChanges();
            }
        }
    }
}