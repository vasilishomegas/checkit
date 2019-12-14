﻿using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters.Interface
{
    public interface ICategoryConverter : IDtoDbConverter<Category, CategoryDto>
    {
    }
}
