using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListIt_DomainInterface.Interfaces.Converter
{
    public interface IShoppingListEntryConverter : IDtoDbConverter<ShoppingListEntry, ShoppingListEntryDto>
    {
    }
}
