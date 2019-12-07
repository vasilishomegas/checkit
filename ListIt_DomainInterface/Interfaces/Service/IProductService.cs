using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_DomainInterface.Interfaces.Service
{
    // public interface IProductService : IService<UserProduct, UserProductDto>, IService<ApiProduct, ApiProductDto>, IService<DefaultProduct, DefaultProductDto>
    public interface IProductService<T, DTO> : IService<T, DTO>
    where T : class
    where DTO : class
    {

    }
}
