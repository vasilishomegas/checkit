using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ListIt_WebFrontend.Models
{
    public class UserVM : UserDto
    {

        
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Passwords do not match!")]
        public string PasswordRepeat { get; set; }


        //Settings Attributes:
        public IList<SelectListItem> CountryList { get; set; }
        public int CountryId { get; set; }
        public IList<SelectListItem> CurrencyList { get; set; }
        public int CurrencyId { get; set; }
        public IList<SelectListItem> SortingsList { get; set; }
        public int SortingId { get; set; }
    }
}