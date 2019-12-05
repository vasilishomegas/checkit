using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class UserConverter : IDtoDbConverter<User, UserDto>
    {
        private readonly LanguageConverter _languageConverter = new LanguageConverter();
        private readonly CountryConverter _countryConverter = new CountryConverter();

        public UserDto ConvertDBToDto(User user)
        {
            return new UserDto
            {
                Language = _languageConverter.ConvertDBToDto(user.Language),
                Country = _countryConverter.ConvertDBToDto(user.Country),
                Email = user.Email,
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                Nickname = user.Nickname,
                Timestamp = user.Timestamp
            };
        }

        public User ConvertDtoToDB(UserDto userDto)
        {
            return new User
            {
                Language = _languageConverter.ConvertDtoToDB(userDto.Language),
                Language_Id = userDto.Language.Id,
                Country = _countryConverter.ConvertDtoToDB(userDto.Country),
                Country_Id = userDto.Country.Id,
                Email = userDto.Email,
                Id = userDto.Id,
                PasswordHash = userDto.PasswordHash,
                Nickname = userDto.Nickname,
                Timestamp = userDto.Timestamp
            };
        }
    }
}
