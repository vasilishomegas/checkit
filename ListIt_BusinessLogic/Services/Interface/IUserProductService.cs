﻿using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Interface
{
    public interface IUserProductService : IProductService<UserProduct, UserProductDto>
    {
    }
}
