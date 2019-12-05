using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ListIt_BusinessLogic.Services;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainInterface.Interfaces.Repository;
using ListIt_DomainModel;
using ListIt_DomainModel.DTO;
using ListIt_WebAPI.Controllers.Generics;

namespace ListIt_WebAPI.Controllers
{
    public class UsersController : GenericController<User, UserDto>
    {
        public UsersController(IUserRepository userRepository, IUserConverter userConverter, ICountryRepository countryRepository, ILanguageRepository languageRepository) 
            : base(new UserService(userRepository, userConverter, countryRepository, languageRepository))
        {

        }
    }
}