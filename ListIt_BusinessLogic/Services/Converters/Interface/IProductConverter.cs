using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters.Interface
{
    public interface IProductConverter : IDtoDbConverter<Product, ProductDto>
    {
        UserProduct ConvertUserProductDtoToDb(UserProductDto dto);
        ApiProduct ConvertApiProductDtoToDb(ApiProductDto dto);
        DefaultProduct ConvertDefaultProductDtoToDb(DefaultProductDto dto);
        UserProductDto ConvertUserProductToUserProductDto(UserProduct product);
        ApiProductDto ConvertApiProductDbToDto(ApiProduct dto);
        DefaultProductDto ConvertDefaultProductDbToDto(DefaultProduct dto);
    }
}
