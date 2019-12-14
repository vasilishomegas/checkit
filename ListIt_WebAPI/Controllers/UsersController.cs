﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ListIt_BusinessLogic.Services;
using ListIt_BusinessLogic.Services.Interface;
using ListIt_DataAccessModel;
using ListIt_DomainModel;
using ListIt_DomainModel.DTO;
using ListIt_WebAPI.Controllers.Generics;

namespace ListIt_WebAPI.Controllers
{
    public class UsersController : GenericController<User, UserDto>
    {
        public UsersController() : this(new UserService())
        {

        }

        public UsersController(IUserService userService) 
            : base(userService)
        {

        }
    }
}