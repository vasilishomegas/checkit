using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DomainModel.DTO;

namespace ListIt_DomainInterface.Interfaces.Service
{
    public interface IChainService : IService<ChainDto>
    {
        void Create(ChainDto chainDto);
        void Update(ChainDto chainDto);
        IEnumerable<ChainDto> GetAll();
        ChainDto Get(int id);
        void Delete(int id);
    }
}
