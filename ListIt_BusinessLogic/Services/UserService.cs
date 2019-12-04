using System;
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
            UserRepository repo = new UserRepository();
            var user = repo.GetUserByEmailAndPasswordHash(email, HashPassword(pw));

            return ConvertDBToDto(user);
        }

        public string HashPassword(string pw)
        {
            //Password hashing:
            HashAlgorithm hash = new SHA256CryptoServiceProvider();
            byte[] buff = System.Text.Encoding.UTF8.GetBytes(pw);
            byte[] hashed = hash.ComputeHash(buff);
            string hashedPW = Convert.ToBase64String(hashed);

            return hashedPW;

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

        public int GetIdByEmail(string email)
        {
            return _userRepository.GetIdByEmail(email);
        }

        protected override UserDto ConvertDBToDto(User entity)
        {
            return StaticDBToDto(entity);
        }

        protected override User ConvertDtoToDB(UserDto dto)
        {
            return StaticDtoToDB(dto);
        }

        public static User StaticDtoToDB(UserDto userDto)
        {
            /* USER HAS TO HAVE COUNTRY AND LANGUAGE, SO COUNTRY_ID / LANGUAGE_ID IS NOT NULLABLE,
            SO I OMIT CHECKS*/
            
            return new User 
                {
                Language = LanguageService.StaticDtoToDB(userDto.Language),
                Language_Id = userDto.Language.Id,
                Country = CountryService.StaticDtoToDB(userDto.Country),
                Country_Id = userDto.Country.Id,
                Email = userDto.Email,
                Id = userDto.Id,
                PasswordHash = userDto.PasswordHash,
                Nickname = userDto.Nickname,
                Timestamp = userDto.Timestamp
            };
        }

        public static UserDto StaticDBToDto(User user)
        {
            return new UserDto
            {
                Language = LanguageService.StaticDBToDto(user.Language),
                Country = CountryService.StaticDBToDto(user.Country),
                Email = user.Email,
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                Nickname = user.Nickname,
                Timestamp = user.Timestamp
            };
        }
    }
}
