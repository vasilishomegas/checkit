using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ListIt_WebFrontend.Models
{
    public class RegisterUserVM : UserDto
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
        [System.ComponentModel.DataAnnotations
            .Compare("PasswordHash", 
            ErrorMessage = "Passwords do not match!")]
        public string PasswordRepeat { get; set; }

        public IList<SelectListItem> LangList { get; set; }
        public IList<SelectListItem> CountryList { get; set; }
        public int CountryId { get; set; }
        public int LanguageId { get; set; }
    }
}