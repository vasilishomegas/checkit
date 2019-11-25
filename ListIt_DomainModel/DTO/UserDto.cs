using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class UserDto : IDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public System.DateTime Timestamp { get; set; }
        public virtual CountryDto Country { get; set; }
        public virtual LanguageDto Language { get; set; }

        
        
    }
}