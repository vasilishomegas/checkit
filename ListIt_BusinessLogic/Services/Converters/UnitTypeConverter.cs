using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class UnitTypeConverter : IDtoDbConverter<UnitType, UnitTypeDto>, IUnitTypeConverter
    {
        public UnitTypeDto ConvertDBToDto(UnitType unit)
        {
            if (unit == null) return null;
            return new UnitTypeDto
            {
                Id = unit.Id
            };
        }

        public UnitType ConvertDtoToDB(UnitTypeDto dto)
        {
            if (dto == null) return null;
            return new UnitType
            {
                Id = dto.Id
            };
        }
    }
}
