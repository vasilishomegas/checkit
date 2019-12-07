using ListIt_DataAccessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListIt_DomainInterface.Interfaces.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Create(Product product, UserProduct subProduct);
        void Create(Product product, ApiProduct subProduct);
        void Create(Product product, DefaultProduct subProduct);
        IEnumerable<UserProduct> GetAllUserProducts();
        UserProduct GetUserProduct(int id);
        void Update(UserProduct userProduct);
    }
}
