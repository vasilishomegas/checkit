﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DomainModel;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class UserService : Service<User, UserDto>
    {
        public UserService() : base(new UserRepository())
        {

        }

        public override void Create(UserDto userDto)
        {
            var countryRepository = new CountryRepository();
            var languageRepository = new LanguageRepository();

            if (userDto.Country == null 
                || userDto.Language == null 
                || languageRepository.Get(userDto.Language.Id) == null 
                || countryRepository.Get(userDto.Country.Id) == null)
                throw new Exception("Country and/or Language cannot be null value");

            _repository.Create(new User
            {
                Language_Id = userDto.Language.Id,
                Country_Id = userDto.Country.Id,
                Email = userDto.Email,
                Id = userDto.Id,
                PasswordHash = userDto.PasswordHash,
                Nickname = userDto.Nickname,
                Timestamp = DateTime.Now
            });
        }

        /*
        public override void Update(UserDto userDto)
        {
            var countryRepository = new CountryRepository();
            var languageRepository = new LanguageRepository();

            if (userDto.Country == null
                || userDto.Language == null
                || languageRepository.Get(userDto.Language.Id) == null
                || countryRepository.Get(userDto.Country.Id) == null)
                throw new Exception("Country and/or Language cannot be null value");

            var dateTime = _repository.Get(userDto.Id).Timestamp;

            _repository.Update(new User
            {
                Language_Id = userDto.Language.Id,
                Country_Id = userDto.Country.Id,
                Email = userDto.Email,
                Id = userDto.Id,
                PasswordHash = userDto.PasswordHash,
                Nickname = userDto.Nickname,
                Timestamp = dateTime
            });
        }
        */

        protected override UserDto ConvertDomainToDto(User entity)
        {
            return StaticDomainToDto(entity);
        }

        protected override User ConvertDtoToDomain(UserDto dto)
        {
            return StaticDtoToDomain(dto);
        }

        public static User StaticDtoToDomain(UserDto userDto)
        {
            /* USER HAS TO HAVE COUNTRY AND LANGUAGE, SO COUNTRY_ID / LANGUAGE_ID IS NOT NULLABLE,
            SO I OMIT CHECKS*/
            
            return new User 
                {
                Language = LanguageService.StaticDtoToDomain(userDto.Language),
                Language_Id = userDto.Language.Id,
                Country = CountryService.StaticDtoToDomain(userDto.Country),
                Country_Id = userDto.Country.Id,
                Email = userDto.Email,
                Id = userDto.Id,
                PasswordHash = userDto.PasswordHash,
                Nickname = userDto.Nickname,
                Timestamp = userDto.Timestamp
            };
        }

        public static UserDto StaticDomainToDto(User user)
        {
            return new UserDto
            {
                Language = LanguageService.StaticDomainToDto(user.Language),
                Country = CountryService.StaticDomainToDto(user.Country),
                Email = user.Email,
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                Nickname = user.Nickname,
                Timestamp = user.Timestamp
            };
        }
    }
}
