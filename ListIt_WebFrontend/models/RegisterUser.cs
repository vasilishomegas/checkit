using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO;
using System.ComponentModel.DataAnnotations;


namespace ListIt_WebFrontend.Models
{
    public class RegisterUser : UserDto
    {
        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Nickname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "Passwords do not match!")]
        public string PasswordRepeat { get; set; }

        //public virtual CountryDto Country { get; set; }
        //public virtual LanguageDto Language { get; set; }

        public IEnumerable<LanguageDto> LangList { get; set; }
        public IEnumerable<CountryDto> CountryList { get; set; }

    }
}