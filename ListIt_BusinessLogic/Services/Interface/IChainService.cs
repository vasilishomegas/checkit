using System.Collections.Generic;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Interface
{
    public interface IChainService : IService<Chain, ChainDto>
    {
        void Create(ChainDto chainDto);
        void Update(ChainDto chainDto);
        IEnumerable<ChainDto> GetAll();
        ChainDto Get(int id);
        void Delete(int id);
    }
}
