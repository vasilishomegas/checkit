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
using System.Security.Cryptography;

namespace ListIt_BusinessLogic.Services
{
    public class UserService : Service<User, UserDto>
    {
        private readonly UserRepository _userRepository;
        public UserService() : base(new UserRepository())
        {
            // Does not create a new instance of UserRepository, but just casts it to a proper variable type,
            // so we can use methods, which are implemented in UserRepository subclass, without creating another
            // instance.
            _userRepository = (UserRepository)_repository;
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
                PasswordHash = HashPassword(userDto.PasswordHash),
                Nickname = userDto.Nickname,
                Timestamp = DateTime.Now
            }) ;
        }

        public UserDto Login(string email, string pw)
        {
            var user = _userRepository.GetUserByEmailAndPasswordHash(email, HashPassword(pw));
            return ConvertDamToDto(user);
        }

        public string HashPassword(string pw)
        {
            //Password hashing:
            using (HashAlgorithm hash = new SHA256CryptoServiceProvider())
            {
                byte[] buff = System.Text.Encoding.UTF8.GetBytes(pw);
                byte[] hashed = hash.ComputeHash(buff);
                string hashedPW = Convert.ToBase64String(hashed);
                return hashedPW;
            }
        }  

        public override void Update(UserDto userDto)
        {

            var inDbUser = _repository.Get(userDto.Id);
            if (inDbUser == null) throw new KeyNotFoundException("No user with such ID");

            if (userDto.Nickname == null) userDto.Nickname = inDbUser.Nickname;
            if (userDto.Email == null) userDto.Email = inDbUser.Email;
            if (userDto.PasswordHash == null) userDto.PasswordHash = inDbUser.PasswordHash;
            userDto.Timestamp = inDbUser.Timestamp;
            var newCountryId = userDto.Country?.Id ?? inDbUser.Country_Id;
            var newLanguageId = userDto.Language?.Id ?? inDbUser.Language_Id;
            
            _repository.Update(new User
            {
                Language_Id = newLanguageId,
                Country_Id = newCountryId,
                Email = userDto.Email,
                Id = userDto.Id,
                PasswordHash = userDto.PasswordHash,
                Nickname = userDto.Nickname,
                Timestamp = userDto.Timestamp
            });
        }

        protected override UserDto ConvertDamToDto(User entity)
        {
            return StaticDamToDto(entity);
        }

        protected override User ConvertDtoToDam(UserDto dto)
        {
            return StaticDtoToDam(dto);
        }

        public static User StaticDtoToDam(UserDto userDto)
        {
            /* USER HAS TO HAVE COUNTRY AND LANGUAGE, SO COUNTRY_ID / LANGUAGE_ID IS NOT NULLABLE,
            SO I OMIT CHECKS*/
            
            return new User 
                {
                Language = LanguageService.StaticDtoToDam(userDto.Language),
                Language_Id = userDto.Language.Id,
                Country = CountryService.StaticDtoToDam(userDto.Country),
                Country_Id = userDto.Country.Id,
                Email = userDto.Email,
                Id = userDto.Id,
                PasswordHash = userDto.PasswordHash,
                Nickname = userDto.Nickname,
                Timestamp = userDto.Timestamp
            };
        }

        public static UserDto StaticDamToDto(User user)
        {
            return new UserDto
            {
                Language = LanguageService.StaticDamToDto(user.Language),
                Country = CountryService.StaticDamToDto(user.Country),
                Email = user.Email,
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                Nickname = user.Nickname,
                Timestamp = user.Timestamp
            };
        }
    }
}
