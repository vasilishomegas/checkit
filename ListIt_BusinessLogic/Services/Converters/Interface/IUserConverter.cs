using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters.Interface
{
    public interface IUserConverter : IDtoDbConverter<User, UserDto>
    {
        UserDto ConvertDBToDto(User user);
        User ConvertDtoToDB(UserDto userDto);
    }
}