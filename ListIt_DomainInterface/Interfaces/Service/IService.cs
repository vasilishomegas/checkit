﻿using System.Collections.Generic;

namespace ListIt_DomainInterface.Interfaces.Service
{
    public interface IService<T, DTO>
    where T : class
    where DTO : class
    {
        IEnumerable<DTO> GetAll();
        DTO Get(int id);
        void Create(DTO dto);
        void Update(DTO dto);
        void Delete(int id);
    }
}
