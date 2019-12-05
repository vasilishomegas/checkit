using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListIt_BusinessLogic.Services.Generics
{
    public interface IService<DTO>
        where DTO : class
    {
        IEnumerable<DTO> GetAll();
        DTO Get(int id);
        void Create(DTO dto);
        void Update(DTO dto);
        void Delete(int id);
    }
}
