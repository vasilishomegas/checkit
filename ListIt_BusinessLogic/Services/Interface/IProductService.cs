namespace ListIt_BusinessLogic.Services.Interface
{
    // public interface IProductService : IService<UserProduct, UserProductDto>, IService<ApiProduct, ApiProductDto>, IService<DefaultProduct, DefaultProductDto>
    public interface IProductService<T, DTO> : IService<T, DTO>
    where T : class
    where DTO : class
    {
    }
}
