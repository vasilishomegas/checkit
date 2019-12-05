﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ListIt_DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using ListIt_DataAccessModel;
    
    public partial class dmaj0918_1074524Entities : DbContext
    {
        public dmaj0918_1074524Entities()
            : base("name=dmaj0918_1074524Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<ApiProduct> ApiProducts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Chain> Chains { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<DefaultProduct> DefaultProducts { get; set; }
        public virtual DbSet<EntryState> EntryStates { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LinkUserToDefaultProduct> LinkUserToDefaultProducts { get; set; }
        public virtual DbSet<LinkUserToList> LinkUserToLists { get; set; }
        public virtual DbSet<ListAccessType> ListAccessTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<ShopApi> ShopApis { get; set; }
        public virtual DbSet<ShoppingListEntry> ShoppingListEntries { get; set; }
        public virtual DbSet<ShoppingList> ShoppingLists { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<TemplateListOrdering> TemplateListOrderings { get; set; }
        public virtual DbSet<TemplateSortedProduct> TemplateSortedProducts { get; set; }
        public virtual DbSet<TranslationOfCategory> TranslationOfCategories { get; set; }
        public virtual DbSet<TranslationOfProduct> TranslationOfProducts { get; set; }
        public virtual DbSet<TranslationOfUnitType> TranslationOfUnitTypes { get; set; }
        public virtual DbSet<UnitType> UnitTypes { get; set; }
        public virtual DbSet<UserEntrySorting> UserEntrySortings { get; set; }
        public virtual DbSet<UserListSorting> UserListSortings { get; set; }
        public virtual DbSet<UserProduct> UserProducts { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
