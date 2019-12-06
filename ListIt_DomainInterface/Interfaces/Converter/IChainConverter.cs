using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_DomainInterface.Interfaces.Converter
{
    public interface IChainConverter : IDtoDbConverter<Chain, ChainDto>
    {
        ChainDto ConvertDBToDto(Chain chain);
        Chain ConvertDtoToDB(ChainDto chainDto);
    }
}