﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class ProductRepository : Repository<Product>
    {
        public UserProduct GetUserProduct(int id)
        {
            using (var context = new ListItContext())
            {
                return context.UserProducts
                    .SingleOrDefault(x => x.Product_Id == id);
            }
        }

        public DefaultProduct GetDefaultProduct(int id)
        {
            using (var context = new ListItContext())
            {
                return context.DefaultProducts
                    .SingleOrDefault(x => x.Product_Id == id);
            }
        }

        public ApiProduct GetApiProduct(int id)
        {
            using (var context = new ListItContext())
            {
                return context.ApiProducts
                    .SingleOrDefault(x => x.Product_Id == id);
            }
        }

        public TranslationOfProduct GetProductTranslation(int langId, int productId)
        {
            using (var context = new ListItContext())
            {
                return context.TranslationOfProducts
                    .Where(x => x.Language_Id == langId)
                    .SingleOrDefault(x => x.Product_Id == productId);
            }
        }

        public void SaveDefaultProductName(TranslationOfProduct translation)
        {
            using (var context = new ListItContext())
            {
                var response = context.Set<TranslationOfProduct>().Add(translation);
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

        public int CreateProduct(Product product)
        {
            using (var context = new ListItContext())
            {
                var prod = context.Set<Product>().Add(product);

                try
                {
                    context.SaveChanges();
                    return prod.Id;
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


        public void Create(UserProduct entity)
        {
            using (var context = new ListItContext())
            {
                var result = context.Set<UserProduct>().Add(entity);

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

        public void Create(ApiProduct entity)
        {
            using (var context = new ListItContext())
            {
                var result = context.Set<ApiProduct>().Add(entity);

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

        public void Create(DefaultProduct entity)
        {
            using (var context = new ListItContext())
            {
                var result = context.Set<DefaultProduct>().Add(entity);

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

        public void Update(UserProduct entity)
        {
            using (var context = new ListItContext())
            {
                context.Set<UserProduct>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;

                bool saveFailed;

                do
                {
                    saveFailed = false;

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
                    {
                        saveFailed = true;
                        e.Entries.Single().Reload();
                        //throw new KeyNotFoundException(e.Message, e.InnerException);
                    }
                } while (saveFailed);
                
            }
        }
    }
}