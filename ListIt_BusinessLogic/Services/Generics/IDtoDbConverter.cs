namespace ListIt_BusinessLogic.Services.Generics
{
    public interface IDtoDbConverter<T, DTO>
    where T : class
    where DTO : class
    {
        T ConvertDtoToDB(DTO dto);
        DTO ConvertDBToDto(T entity);
    }
}