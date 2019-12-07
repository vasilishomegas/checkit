using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Repository;
using ListIt_DomainModel.DTO;

namespace ListIt_DomainInterface.Interfaces.Converter
{
    public interface ICategoryConverter : IDtoDbConverter<Category, CategoryDto>
    {
    }
}
