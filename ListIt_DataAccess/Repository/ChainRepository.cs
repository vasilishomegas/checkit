using ListIt_DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_DataAccess.Repository
{
    // TO BE DELETED AFTER REPOSITORY CLASS PASSES ITS TESTS
    public class ChainRepository
    {
        public IQueryable<Chain> GetAll()
        {
            using (var context = new ListItContext())
            {
                return context.Chains.AsQueryable<Chain>();
            }
        }

        public Chain Get(int id)
        {
            using (var context = new ListItContext())
            {
                return context.Chains.Find(id);
            }
        }

        public void Create(Chain chain)
        {
            using (var context = new ListItContext())
            {
                context.Chains.Add(chain);
            }
        }

        public void Update(Chain chain)
        {
            using (var context = new ListItContext())
            {
                Chain _chain = context.Chains.SingleOrDefault((x) => x.Id == chain.Id);
                context.Entry(_chain).CurrentValues.SetValues(chain);
                context.SaveChanges();
            }
        }

        public void Delete(Chain chain)
        {
            using (var context = new ListItContext())
            {
                context.Chains.Remove(chain);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new ListItContext())
            {
                var chain = context.Chains.FirstOrDefault((x) => x.Id == id);

                if (chain == null) return;

                context.Chains.Remove(chain);
                context.SaveChanges();
            }
        }
    }
}