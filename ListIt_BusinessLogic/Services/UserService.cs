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
using ListIt_BusinessLogic.Services.Converters;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_BusinessLogic.Services.Interface;
using ListIt_DataAccess.Repository.Interface;

namespace ListIt_BusinessLogic.Services
{
    public class UserService : Service<User, UserDto>, IUserService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserConverter _userConverter;

        public UserService(): this(new UserRepository(), new UserConverter(), new CountryRepository(), new LanguageRepository())
        {

        }

        public UserService(IUserRepository userRepository, IUserConverter userConverter, ICountryRepository countryRepository, ILanguageRepository languageRepository) 
            : base(userRepository, userConverter)
        {
            _countryRepository = countryRepository;
            _languageRepository = languageRepository;
            _userRepository = userRepository;
            _userConverter = userConverter;
        }

        public override void Create(UserDto userDto)
        {

            if (userDto.Country == null
                || userDto.Language == null
                || _languageRepository.Get(userDto.Language.Id) == null 
                || _countryRepository.Get(userDto.Country.Id) == null)
                throw new Exception("Country and/or Language cannot be null value");

            _userRepository.Create(new User
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

            return _userConverter.ConvertDBToDto(user);
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

            var inDbUser = _userRepository.Get(userDto.Id);
            if (inDbUser == null) throw new KeyNotFoundException("No user with such ID");

            if (userDto.Nickname == null) userDto.Nickname = inDbUser.Nickname;
            if (userDto.Email == null) userDto.Email = inDbUser.Email;
            if (userDto.PasswordHash == null) userDto.PasswordHash = inDbUser.PasswordHash;
            userDto.Timestamp = inDbUser.Timestamp;
            var newCountryId = userDto.Country?.Id ?? inDbUser.Country_Id;
            var newLanguageId = userDto.Language?.Id ?? inDbUser.Language_Id;
            
            _userRepository.Update(new User
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
    }
}
