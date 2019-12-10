using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using System.Text;

namespace ListIt_DataAccess.Repository
{
    public class TemplateSortingRepository : Repository<TemplateSortedProduct>
    {
        public TemplateListOrdering GetListOrdering(int id)
        {
            using (var context = new ListItContext())
            {
                return context.TemplateListOrderings
                    .SingleOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<TemplateSortedProduct> GetTemplates(int id)
        {
            using (var context = new ListItContext())
            {
                return context.TemplateSortedProducts
                    .Where(x => x.TemplateListOrderingId == id)
                    .ToList();
            }
        }

        public new IEnumerable<TemplateListOrdering> GetAll()
        {
            using (var context = new ListItContext())
            {
                return context.TemplateListOrderings
                    .ToList();
            }
        }
    }
}