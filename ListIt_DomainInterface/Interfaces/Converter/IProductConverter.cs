using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListIt_DomainInterface.Interfaces.Converter
{
    public interface IProductConverter : IDtoDbConverter<Product, ProductDto>
    {
        UserProduct ConvertUserProductDtoToDb(UserProductDto dto);
        UserProduct ConvertApiProductDtoToDb(ApiProductDto dto);
        UserProduct ConvertDefaultProductDtoToDb(DefaultProductDto dto);
        UserProductDto ConvertUserProductToUserProductDto(UserProduct product);
    }
}
