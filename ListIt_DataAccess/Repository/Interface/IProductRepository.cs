using System.Collections.Generic;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        void Create(Product product, UserProduct subProduct);
        void Create(Product product, ApiProduct subProduct);
        void Create(Product product, DefaultProduct subProduct);
        IEnumerable<UserProduct> GetAllUserProducts();
        IEnumerable<ApiProduct> GetAllApiProducts();
        IEnumerable<DefaultProduct> GetAllDefaultProducts();
        UserProduct GetUserProduct(int id);
        ApiProduct GetApiProduct(int id);
        DefaultProduct GetDefaultProduct(int id);
        void Update(UserProduct product);
        void Update(ApiProduct apiProduct);
        void Update(DefaultProduct defaultProduct);
        void Delete(UserProduct userProduct);
        void Delete(ApiProduct apiProduct);
        void Delete(DefaultProduct defaultProduct);

    }
}
