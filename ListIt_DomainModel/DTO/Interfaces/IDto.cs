using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListIt_DomainModel.DTO.Interfaces
{
    // I use this interface to make generic controllers, which I have to inform somehow,
    // that a DTO class has a property named Id.
    public interface IDto
    {
        int Id { get; set; }
    }
}
