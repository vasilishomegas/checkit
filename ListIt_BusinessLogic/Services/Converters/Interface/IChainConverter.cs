using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters.Interface
{
    public interface IChainConverter : IDtoDbConverter<Chain, ChainDto>
    {
        ChainDto ConvertDBToDto(Chain chain);
        Chain ConvertDtoToDB(ChainDto chainDto);
    }
}